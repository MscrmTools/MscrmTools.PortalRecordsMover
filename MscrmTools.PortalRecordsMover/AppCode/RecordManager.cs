using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
using System.Windows.Forms;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using MscrmTools.PortalRecordsMover.Forms;
using XrmToolBox.Extensibility;

namespace MscrmTools.PortalRecordsMover.AppCode
{
    internal class RecordManager
    {
        private readonly IOrganizationService service;
        private readonly LogManager logger;

        public RecordManager(IOrganizationService service)
        {
            this.service = service;
            logger = new LogManager(GetType());
        }

        public void ProcessRecords(EntityCollection ec, List<EntityMetadata> emds, BackgroundWorker worker)
        {
            var records = new List<Entity>(ec.Entities);
            var progress = new ImportProgress(records.Count);

            var nextCycle = new List<Entity>();

            while (records.Any())
            {
                for (int i = records.Count - 1; i >= 0; i--)
                {
                    var record = records[i];

                    if (record.LogicalName != "annotation")
                    {
                        if (record.Attributes.Values.Any(v =>
                            v is EntityReference
                            && records.Select(r => r.Id).Contains(((EntityReference) v).Id)
                            ))
                        {
                            if (nextCycle.Any(r => r.Id == record.Id))
                            {
                                continue;
                            }

                            var newRecord = new Entity(record.LogicalName) {Id = record.Id};
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
                            v is Guid
                            && records.Where(r => r.Id != record.Id)
                                .Select(r => r.Id)
                                .Contains((Guid) v)
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
                            var result = (UpsertResponse) service.Execute(new UpsertRequest
                            {
                                Target = record
                            });

                            logger.LogInfo($"Record {record.GetAttributeValue<string>(entityProgress.Metadata.PrimaryNameAttribute)} {(result.RecordCreated ? "created" : "updated")} ({entityProgress.Entity}/{record.Id})");
                        }

                        records.RemoveAt(i);
                        entityProgress.Success++;
                    }
                    catch(Exception error)
                    {
                        logger.LogError($"{record.GetAttributeValue<string>(entityProgress.Metadata.PrimaryNameAttribute)} ({entityProgress.Entity}/{record.Id}): {error.Message}");
                        entityProgress.Error++;
                    }
                    finally
                    {
                        entityProgress.Processed++;
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
                    var percentage = index*100/count;
                    worker.ReportProgress(percentage, true);
                }
                catch (Exception error)
                {
                    logger.LogInfo(error.Message);
                    var percentage = index*100/count;
                    worker.ReportProgress(percentage, false);
                }
            }
        }
    }
}
