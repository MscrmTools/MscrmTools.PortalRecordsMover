using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;

namespace MscrmTools.PortalRecordsMover.AppCode
{
    internal class EntityProgress : ICloneable
    {
        public EntityProgress(EntityMetadata emd, string displayName)
        {
            LogicalName = emd.LogicalName;
            Entity = displayName;
            Metadata = emd;
        }

        public int Count { get; set; }
        public string Entity { get; }
        public int ErrorFirstPhase { get; set; }
        public int? ErrorSecondPhase { get; set; }
        public int? ErrorSetState { get; set; }
        public string LogicalName { get; }
        public EntityMetadata Metadata { get; }
        public int Processed { get; set; }
        public int SuccessFirstPhase { get; set; }
        public int? SuccessSecondPhase { get; set; }
        public int? SuccessSetStatePhase { get; set; }

        public object Clone()
        {
            return new EntityProgress(Metadata, Entity)
            {
                ErrorFirstPhase = ErrorFirstPhase,
                ErrorSecondPhase = ErrorSecondPhase,
                ErrorSetState = ErrorSetState,
                SuccessFirstPhase = SuccessFirstPhase,
                SuccessSecondPhase = SuccessSecondPhase,
                SuccessSetStatePhase = SuccessSetStatePhase,
                Processed = Processed,
                Count = Count
            };
        }
    }

    internal class ImportProgress : ICloneable
    {
        public ImportProgress(int count)
        {
            Entities = new List<EntityProgress>();
            Count = count;
        }

        public int Count { get; }
        public List<EntityProgress> Entities { get; }

        public object Clone()
        {
            var clone = new ImportProgress(Count);
            foreach (var entity in Entities)
            {
                clone.Entities.Add((EntityProgress)entity.Clone());
            }

            return clone;
        }
    }
}