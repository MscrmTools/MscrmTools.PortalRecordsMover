using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using MscrmTools.PortalRecordsMover.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
using XrmToolBox.Extensibility;

namespace MscrmTools.PortalRecordsMover.AppCode
{
    internal class RecordManager
    {
        private const int maxErrorLoopCount = 5;
        private readonly LogManager logger;
        private readonly List<EntityReference> recordsToDeactivate;
        private IOrganizationService service;

        public RecordManager(IOrganizationService service)
        {
            this.service = service;
            logger = new LogManager(GetType());
            recordsToDeactivate = new List<EntityReference>();
        }

        public bool ProcessRecords(EntityCollection ec, List<EntityMetadata> emds, int organizationMajorVersion, BackgroundWorker worker, ImportSettings settings)
        {
            var records = new List<Entity>(ec.Entities);

            // Move annotation at the beginning if the list as the list will be
            // inverted to allow list removal. This way, annotation are
            // processed as the last records
            var annotations = records.Where(e => e.LogicalName == "annotation").Reverse().ToList();
            records = records.Except(annotations).ToList();
            records.InsertRange(0, annotations);

            var progress = new ImportProgress(records.Count);

            var nextCycle = new List<Entity>();
            progress.Entities.ForEach(p => { p.ErrorFirstPhase = 0; });

            for (int i = records.Count - 1; i >= 0; i--)
            {
                if (worker.CancellationPending)
                {
                    return true;
                }

                var record = records[i];

                if (record.LogicalName != "annotation")
                {
                    if (record.Attributes.Values.Any(v =>
                        v is EntityReference reference
                        && records.Select(r => r.Id).Contains(reference.Id)
                        ))
                    {
                        if (nextCycle.Any(r => r.Id == record.Id))
                        {
                            continue;
                        }

                        var newRecord = new Entity(record.LogicalName) { Id = record.Id };
                        var toRemove = new List<string>();
                        foreach (var attr in record.Attributes)
                        {
                            if (attr.Value is EntityReference)
                            {
                                newRecord.Attributes.Add(attr.Key, attr.Value);
                                toRemove.Add(attr.Key);
                                nextCycle.Add(newRecord);
                            }
                        }

                        foreach (var attr in toRemove)
                        {
                            record.Attributes.Remove(attr);
                        }
                    }

                    if (record.Attributes.Values.Any(v =>
                        v is Guid guid
                        && records.Where(r => r.Id != record.Id)
                            .Select(r => r.Id)
                            .Contains(guid)
                        ))
                    {
                        nextCycle.Add(record);
                        records.RemoveAt(i);
                        continue;
                    }
                }

                var entityProgress = progress.Entities.FirstOrDefault(e => e.LogicalName == record.LogicalName);
                if (entityProgress == null)
                {
                    var emd = emds.FirstOrDefault(e => e.LogicalName == record.LogicalName);
                    if (emd == null)
                    {
                        logger.LogError($"Record: Entity Logical Name: {record.LogicalName} for ID: {record.Id} not found in the target instance metadata.");
                        continue;
                    }

                    string displayName = emd.DisplayName?.UserLocalizedLabel?.Label;

                    if (displayName == null && emd.IsIntersect.Value)
                    {
                        var rel = emds.SelectMany(ent => ent.ManyToManyRelationships)
                        .First(r => r.IntersectEntityName == emd.LogicalName);

                        displayName = $"{emds.First(ent => ent.LogicalName == rel.Entity1LogicalName).DisplayName?.UserLocalizedLabel?.Label} / {emds.First(ent => ent.LogicalName == rel.Entity2LogicalName).DisplayName?.UserLocalizedLabel?.Label}";
                    }
                    if (displayName == null)
                    {
                        displayName = emd.SchemaName;
                    }

                    entityProgress = new EntityProgress(emd, displayName)
                    {
                        Count = records.Count(r => r.LogicalName == record.LogicalName)
                    };
                    progress.Entities.Add(entityProgress);
                }

                var name = string.IsNullOrEmpty(entityProgress.Metadata.PrimaryNameAttribute)
                    ? "(N/A)"
                    : record.GetAttributeValue<string>(entityProgress.Metadata.PrimaryNameAttribute);

                try
                {
                    record.Attributes.Remove("ownerid");

                    if (record.Attributes.Contains("statecode") &&
                       record.GetAttributeValue<OptionSetValue>("statecode").Value == 1)
                    {
                        logger.LogInfo($"Record {name} ({record.Id}) is inactive : Added for deactivation step");

                        recordsToDeactivate.Add(record.ToEntityReference());
                        record.Attributes.Remove("statecode");
                        record.Attributes.Remove("statuscode");
                    }

                    if (organizationMajorVersion >= 8)
                    {
                        var result = (UpsertResponse)service.Execute(new UpsertRequest
                        {
                            Target = record
                        });

                        logger.LogInfo(
                            $"Record {name} ({record.Id}) {(result.RecordCreated ? "created" : "updated")} ({entityProgress.Entity}/{record.Id})");
                    }
                    else
                    {
                        bool exists = false;
                        try
                        {
                            service.Retrieve(record.LogicalName, record.Id, new ColumnSet());
                            exists = true;
                        }
                        catch
                        {
                            // Do nothing
                        }

                        if (exists)
                        {
                            service.Update(record);
                            logger.LogInfo(
                                $"Record {name} ({record.Id}) updated ({entityProgress.Entity}/{record.Id})");
                        }
                        else
                        {
                            service.Create(record);
                            logger.LogInfo(
                                $"Record {name} ({record.Id}) created ({entityProgress.Entity}/{record.Id})");
                        }
                    }

                    if (record.LogicalName == "annotation" && settings.CleanWebFiles)
                    {
                        var reference = record.GetAttributeValue<EntityReference>("objectid");
                        if (reference?.LogicalName == "adx_webfile")
                        {
                            logger.LogInfo("Searching for extra annotation in web file {0:B}", reference.Id);

                            var qe = new QueryExpression("annotation")
                            {
                                NoLock = true,
                                Criteria = new FilterExpression
                                {
                                    Conditions =
                                            {
                                                new ConditionExpression("annotationid", ConditionOperator.NotEqual,
                                                    record.Id),
                                                new ConditionExpression("objectid", ConditionOperator.Equal,
                                                    reference.Id),
                                            }
                                }
                            };

                            var extraNotes = service.RetrieveMultiple(qe);
                            foreach (var extraNote in extraNotes.Entities)
                            {
                                logger.LogInfo("Deleting extra note {0:B}", extraNote.Id);
                                service.Delete(extraNote.LogicalName, extraNote.Id);
                            }
                        }
                    }

                    records.RemoveAt(i);
                    entityProgress.SuccessFirstPhase++;
                }
                catch (Exception error)
                {
                    logger.LogError($"{name} ({entityProgress.Entity}/{record.Id}): {error.Message}");
                    entityProgress.ErrorFirstPhase++;
                }
                finally
                {
                    entityProgress.Processed++;
                    worker.ReportProgress(0, progress.Clone());
                }
            }

            var count = nextCycle.DistinctBy(r => r.Id).Count();
            var index = 0;

            if (count > 0)
            {
                worker.ReportProgress(0, @"Updating records to add references and processing many-to-many relationships...");
            }

            foreach (var record in nextCycle.DistinctBy(r => r.Id))
            {
                var entityProgress = progress.Entities.FirstOrDefault(e => e.LogicalName == record.LogicalName);
                if (entityProgress == null)
                {
                    var emd = emds.First(e => e.LogicalName == record.LogicalName);
                    string displayName = emd.DisplayName?.UserLocalizedLabel?.Label;

                    if (displayName == null && emd.IsIntersect.Value)
                    {
                        var rel = emds.SelectMany(ent => ent.ManyToManyRelationships)
                            .First(r => r.IntersectEntityName == emd.LogicalName);

                        displayName = $"{emds.First(ent => ent.LogicalName == rel.Entity1LogicalName).DisplayName?.UserLocalizedLabel?.Label} / {emds.First(ent => ent.LogicalName == rel.Entity2LogicalName).DisplayName?.UserLocalizedLabel?.Label}";
                    }
                    if (displayName == null)
                    {
                        displayName = emd.SchemaName;
                    }

                    entityProgress = new EntityProgress(emd, displayName)
                    {
                        Count = nextCycle.Count(r => r.LogicalName == record.LogicalName)
                    };
                    progress.Entities.Add(entityProgress);
                }

                if (!entityProgress.SuccessSecondPhase.HasValue)
                {
                    entityProgress.SuccessSecondPhase = 0;
                    entityProgress.ErrorSecondPhase = 0;
                }

                try
                {
                    index++;

                    record.Attributes.Remove("ownerid");

                    if (record.Attributes.Count == 3 && record.Attributes.Values.All(v => v is Guid))
                    {
                        logger.LogInfo($"Creating association {entityProgress.Entity} ({record.Id})");

                        try
                        {
                            var rel =
                                emds.SelectMany(e => e.ManyToManyRelationships)
                                    .First(r => r.IntersectEntityName == record.LogicalName);

                            service.Associate(rel.Entity1LogicalName,
                                record.GetAttributeValue<Guid>(rel.Entity1IntersectAttribute),
                                new Relationship(rel.SchemaName),
                                new EntityReferenceCollection(new List<EntityReference>
                                {
                                    new EntityReference(rel.Entity2LogicalName,
                                        record.GetAttributeValue<Guid>(rel.Entity2IntersectAttribute))
                                }));

                            logger.LogInfo($"Association {entityProgress.Entity} ({record.Id}) created");
                        }
                        catch (FaultException<OrganizationServiceFault> error)
                        {
                            if (error.Detail.ErrorCode != -2147220937)
                            {
                                throw;
                            }

                            logger.LogInfo($"Association {entityProgress.Entity} ({record.Id}) already exists");
                        }
                        finally
                        {
                            entityProgress.Processed++;
                        }
                    }
                    else
                    {
                        logger.LogInfo($"Upating record {record.LogicalName} ({record.Id})");
                        service.Update(record);
                    }

                    var percentage = index * 100 / count;

                    entityProgress.SuccessSecondPhase = entityProgress.SuccessSecondPhase.Value + 1;

                    worker.ReportProgress(percentage, progress.Clone());
                }
                catch (Exception error)
                {
                    logger.LogInfo(error.Message);
                    var percentage = index * 100 / count;
                    entityProgress.ErrorSecondPhase = entityProgress.ErrorSecondPhase.Value + 1;

                    worker.ReportProgress(percentage, progress.Clone());
                }
            }

            if (recordsToDeactivate.Any())
            {
                count = recordsToDeactivate.Count;
                index = 0;

                worker.ReportProgress(0, "Deactivating records...");

                foreach (var er in recordsToDeactivate)
                {
                    var entityProgress = progress.Entities.First(e => e.LogicalName == er.LogicalName);
                    if (!entityProgress.SuccessSecondPhase.HasValue)
                    {
                        entityProgress.SuccessSetStatePhase = 0;
                        entityProgress.ErrorSetState = 0;
                    }

                    try
                    {
                        index++;

                        logger.LogInfo($"Deactivating record {er.LogicalName} ({er.Id})");

                        var recordToUpdate = new Entity(er.LogicalName)
                        {
                            Id = er.Id,
                            ["statecode"] = new OptionSetValue(1),
                            ["statuscode"] = new OptionSetValue(-1)
                        };

                        service.Update(recordToUpdate);

                        var percentage = index * 100 / count;
                        entityProgress.SuccessSetStatePhase++;
                        worker.ReportProgress(percentage, progress.Clone());
                    }
                    catch (Exception error)
                    {
                        logger.LogInfo(error.Message);
                        var percentage = index * 100 / count;
                        entityProgress.ErrorSetState++;
                        worker.ReportProgress(percentage, progress.Clone());
                    }
                }
            }

            return false;
        }

        public List<EntityResult> RetrieveNnRecords(ExportSettings exSettings, List<Entity> records)
        {
            var ers = new List<EntityResult>();
            var rels = new List<ManyToManyRelationshipMetadata>();

            foreach (var emd in exSettings.Entities)
            {
                foreach (var mm in emd.ManyToManyRelationships)
                {
                    var e1 = mm.Entity1LogicalName;
                    var e2 = mm.Entity2LogicalName;
                    var isValid = false;

                    if (e1 == emd.LogicalName)
                    {
                        if (exSettings.Entities.Any(e => e.LogicalName == e2))
                        {
                            isValid = true;
                        }
                    }
                    else
                    {
                        if (exSettings.Entities.Any(e => e.LogicalName == e1))
                        {
                            isValid = true;
                        }
                    }

                    if (isValid && rels.All(r => r.IntersectEntityName != mm.IntersectEntityName))
                    {
                        rels.Add(mm);
                    }
                }
            }

            foreach (var mm in rels)
            {
                var ids = records.Where(r => r.LogicalName == mm.Entity1LogicalName).Select(r => r.Id).ToList();
                if (!ids.Any())
                {
                    continue;
                }

                var query = new QueryExpression(mm.IntersectEntityName)
                {
                    ColumnSet = new ColumnSet(true),
                    Criteria = new FilterExpression
                    {
                        Conditions =
                        {
                            new ConditionExpression(mm.Entity1IntersectAttribute, ConditionOperator.In, ids.ToArray())
                        }
                    }
                };

                ers.Add(new EntityResult { Records = service.RetrieveMultiple(query) });
            }

            return ers;
        }

        public EntityCollection RetrieveRecords(EntityMetadata emd, ExportSettings settings)
        {
            var query = new QueryExpression(emd.LogicalName)
            {
                ColumnSet = new ColumnSet(true),
                Criteria = new FilterExpression { Filters = { new FilterExpression(LogicalOperator.Or) } }
            };

            if (settings.CreateFilter.HasValue)
            {
                query.Criteria.Filters[0].AddCondition("createdon", ConditionOperator.OnOrAfter, settings.CreateFilter.Value.ToString("yyyy-MM-dd"));
            }

            if (settings.ModifyFilter.HasValue)
            {
                query.Criteria.Filters[0].AddCondition("modifiedon", ConditionOperator.OnOrAfter, settings.ModifyFilter.Value.ToString("yyyy-MM-dd"));
            }

            if (settings.WebsiteFilter != Guid.Empty)
            {
                var lamd = emd.Attributes.FirstOrDefault(a =>
                    a is LookupAttributeMetadata metadata && metadata.Targets[0] == "adx_website");
                if (lamd != null)
                {
                    query.Criteria.AddCondition(lamd.LogicalName, ConditionOperator.Equal, settings.WebsiteFilter);
                }
                else
                {
                    switch (emd.LogicalName)
                    {
                        case "adx_webfile":
                            var noteLe = new LinkEntity
                            {
                                LinkFromEntityName = "adx_webfile",
                                LinkFromAttributeName = "adx_webfileid",
                                LinkToAttributeName = "objectid",
                                LinkToEntityName = "annotation",
                                LinkCriteria = new FilterExpression(LogicalOperator.Or)
                            };

                            bool addLinkEntity = false;

                            if (settings.CreateFilter.HasValue)
                            {
                                noteLe.LinkCriteria.AddCondition("createdon", ConditionOperator.OnOrAfter, settings.CreateFilter.Value.ToString("yyyy-MM-dd"));
                                addLinkEntity = true;
                            }

                            if (settings.ModifyFilter.HasValue)
                            {
                                noteLe.LinkCriteria.AddCondition("modifiedon", ConditionOperator.OnOrAfter, settings.ModifyFilter.Value.ToString("yyyy-MM-dd"));
                                addLinkEntity = true;
                            }

                            if (addLinkEntity)
                            {
                                query.LinkEntities.Add(noteLe);
                            }
                            break;

                        case "adx_entityformmetadata":
                            query.LinkEntities.Add(
                                CreateParentEntityLinkToWebsite(
                                    emd.LogicalName,
                                    "adx_entityform",
                                    "adx_entityformid",
                                    "adx_entityform",
                                    settings.WebsiteFilter));
                            break;

                        case "adx_webformmetadata":
                            var le = CreateParentEntityLinkToWebsite(
                                emd.LogicalName,
                                "adx_webformstep",
                                "adx_webformstepid",
                                "adx_webformstep",
                                Guid.Empty);

                            le.LinkEntities.Add(CreateParentEntityLinkToWebsite(
                                "adx_webformstep",
                                "adx_webform",
                                "adx_webformid",
                                "adx_webform",
                                settings.WebsiteFilter));

                            query.LinkEntities.Add(le);
                            break;

                        case "adx_weblink":
                            query.LinkEntities.Add(CreateParentEntityLinkToWebsite(
                                emd.LogicalName,
                                "adx_weblinksetid",
                                "adx_weblinksetid",
                                "adx_weblinkset",
                                settings.WebsiteFilter));
                            break;

                        case "adx_blogpost":
                            query.LinkEntities.Add(CreateParentEntityLinkToWebsite(
                                emd.LogicalName,
                                "adx_blogid",
                                "adx_blogid",
                                "adx_blog",
                                settings.WebsiteFilter));
                            break;

                        case "adx_communityforumaccesspermission":
                        case "adx_communityforumannouncement":
                        case "adx_communityforumthread":
                            query.LinkEntities.Add(CreateParentEntityLinkToWebsite(
                                emd.LogicalName,
                                "adx_forumid",
                                "adx_communityforumid",
                                "adx_communityforum",
                                settings.WebsiteFilter));
                            break;

                        case "adx_communityforumpost":
                            var lef = CreateParentEntityLinkToWebsite(
                                emd.LogicalName,
                                "adx_forumthreadid",
                                "adx_communityforumthreadid",
                                "adx_communityforumthread",
                                Guid.Empty);

                            lef.LinkEntities.Add(CreateParentEntityLinkToWebsite(
                                "adx_communityforumthread",
                                "adx_forumid",
                                "adx_communityforumid",
                                "adx_communityforum",
                                settings.WebsiteFilter));

                            query.LinkEntities.Add(lef);

                            break;

                        case "adx_idea":
                            query.LinkEntities.Add(CreateParentEntityLinkToWebsite(
                                emd.LogicalName,
                                "adx_ideaforumid",
                                "adx_ideaforumid",
                                "adx_ideaforum",
                                settings.WebsiteFilter));
                            break;

                        case "adx_pagealert":
                        case "adx_webpagehistory":
                        case "adx_webpagelog":
                            query.LinkEntities.Add(CreateParentEntityLinkToWebsite(
                                emd.LogicalName,
                                "adx_webpageid",
                                "adx_webpageid",
                                "adx_webpage",
                                settings.WebsiteFilter));
                            break;

                        case "adx_pollsubmission":
                            query.LinkEntities.Add(CreateParentEntityLinkToWebsite(
                                emd.LogicalName,
                                "adx_pollid",
                                "adx_pollid",
                                "adx_poll",
                                settings.WebsiteFilter));
                            break;

                        case "adx_webfilelog":
                            query.LinkEntities.Add(CreateParentEntityLinkToWebsite(
                                emd.LogicalName,
                                "adx_webfileid",
                                "adx_webfileid",
                                "adx_webfile",
                                settings.WebsiteFilter));
                            break;

                        case "adx_webformsession":
                        case "adx_webformstep":
                            query.LinkEntities.Add(CreateParentEntityLinkToWebsite(
                                emd.LogicalName,
                                "adx_webform",
                                "adx_webformid",
                                "adx_webform",
                                settings.WebsiteFilter));
                            break;
                    }
                }
            }

            if (settings.ActiveItemsOnly && emd.LogicalName != "annotation")
            {
                query.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);
            }

            return service.RetrieveMultiple(query);
        }

        public List<Entity> RetrieveViews(List<EntityMetadata> entities)
        {
            var query = new QueryExpression("savedquery")
            {
                ColumnSet = new ColumnSet("returnedtypecode", "layoutxml"),
                Criteria = new FilterExpression
                {
                    Conditions =
                    {
                        new ConditionExpression("isquickfindquery", ConditionOperator.Equal, true),
                        new ConditionExpression("returnedtypecode", ConditionOperator.In, entities.Select(e=>e.LogicalName).Cast<object>().ToArray())
                    }
                }
            };

            return service.RetrieveMultiple(query).Entities.ToList();
        }

        public List<Entity> RetrieveWebfileAnnotations(List<Guid> ids)
        {
            return service.RetrieveMultiple(new QueryExpression("annotation")
            {
                ColumnSet = new ColumnSet(true),
                Criteria = new FilterExpression
                {
                    Conditions =
                    {
                        new ConditionExpression("objectid", ConditionOperator.In, ids.ToArray())
                    }
                },
                LinkEntities =
                {
                    new LinkEntity
                    {
                        LinkFromEntityName = "annotation",
                        LinkFromAttributeName = "objectid",
                        LinkToAttributeName = "adx_webfileid",
                        LinkToEntityName = "adx_webfile",
                        EntityAlias = "webfile",
                        Columns = new ColumnSet("adx_name")
                    }
                },
                Orders =
                    {
                        new OrderExpression("modifiedon", OrderType.Descending)
                    }
            }).Entities.ToList();
        }

        public void SetService(IOrganizationService newService)
        {
            service = newService;
        }

        private LinkEntity CreateParentEntityLinkToWebsite(string fromEntity, string fromAttribute, string toAttribute, string toEntity, Guid websiteId)
        {
            var le = new LinkEntity
            {
                LinkFromEntityName = fromEntity,
                LinkFromAttributeName = fromAttribute,
                LinkToAttributeName = toAttribute,
                LinkToEntityName = toEntity,
            };

            if (websiteId != Guid.Empty)
            {
                le.LinkCriteria.AddCondition("adx_websiteid", ConditionOperator.Equal, websiteId);
            }

            return le;
        }
    }
}