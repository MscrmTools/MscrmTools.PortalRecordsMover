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
        private readonly IOrganizationService service;

        public RecordManager(IOrganizationService service)
        {
            this.service = service;
            logger = new LogManager(GetType());
            recordsToDeactivate = new List<EntityReference>();
        }

        public bool ProcessRecords(EntityCollection ec, List<EntityMetadata> emds, int organizationMajorVersion, BackgroundWorker worker)
        {
            var records = new List<Entity>(ec.Entities);
            var progress = new ImportProgress(records.Count);

            var nextCycle = new List<Entity>();
            int loopIndex = 0;
            while (records.Any())
            {
                loopIndex++;
                if (loopIndex == maxErrorLoopCount)
                {
                    logger.LogWarning("Max loop count reached! Exit record first cycle processing !");
                    break;
                }

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
                            continue;
                        }
                    }

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

                        entityProgress = new EntityProgress(emd, displayName);
                        progress.Entities.Add(entityProgress);
                    }

                    try
                    {
                        record.Attributes.Remove("ownerid");

                        if (record.Attributes.Count == 3 && record.Attributes.Values.All(v => v is Guid))
                        {
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
                        }
                        else
                        {
                            if (record.Attributes.Contains("statecode") &&
                               record.GetAttributeValue<OptionSetValue>("statecode").Value == 1)
                            {
                                logger.LogInfo($"Record {record.GetAttributeValue<string>(entityProgress.Metadata.PrimaryNameAttribute)} is inactive : Added for deactivation step");

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
                                    $"Record {record.GetAttributeValue<string>(entityProgress.Metadata.PrimaryNameAttribute)} {(result.RecordCreated ? "created" : "updated")} ({entityProgress.Entity}/{record.Id})");
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
                                        $"Record {record.GetAttributeValue<string>(entityProgress.Metadata.PrimaryNameAttribute)} updated ({entityProgress.Entity}/{record.Id})");
                                }
                                else
                                {
                                    service.Create(record);
                                    logger.LogInfo(
                                        $"Record {record.GetAttributeValue<string>(entityProgress.Metadata.PrimaryNameAttribute)} created ({entityProgress.Entity}/{record.Id})");
                                }
                            }
                        }

                        records.RemoveAt(i);
                        entityProgress.Success++;
                        entityProgress.Processed++;
                    }
                    catch (Exception error)
                    {
                        logger.LogError($"{record.GetAttributeValue<string>(entityProgress.Metadata.PrimaryNameAttribute)} ({entityProgress.Entity}/{record.Id}): {error.Message}");
                        entityProgress.Error++;
                    }
                    finally
                    {
                        worker.ReportProgress(0, progress.Clone());
                    }
                }
            }

            worker.ReportProgress(0, "Updating records to add references...");

            var count = nextCycle.DistinctBy(r => r.Id).Count();
            var index = 0;

            foreach (var record in nextCycle.DistinctBy(r => r.Id))
            {
                try
                {
                    index++;

                    logger.LogInfo($"Upating record {record.LogicalName} ({record.Id})");

                    record.Attributes.Remove("ownerid");
                    service.Update(record);
                    var percentage = index * 100 / count;
                    worker.ReportProgress(percentage, true);
                }
                catch (Exception error)
                {
                    logger.LogInfo(error.Message);
                    var percentage = index * 100 / count;
                    worker.ReportProgress(percentage, false);
                }
            }

            if (recordsToDeactivate.Any())
            {
                count = recordsToDeactivate.Count;
                index = 0;

                worker.ReportProgress(0, "Deactivating records...");

                foreach (var er in recordsToDeactivate)
                {
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
                        worker.ReportProgress(percentage, true);
                    }
                    catch (Exception error)
                    {
                        logger.LogInfo(error.Message);
                        var percentage = index * 100 / count;
                        worker.ReportProgress(percentage, false);
                    }
                }
            }

            return false;
        }
    }
}