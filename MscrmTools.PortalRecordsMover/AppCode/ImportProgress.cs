using System;
using System.Collections.Generic;
using Microsoft.Xrm.Sdk.Metadata;

namespace MscrmTools.PortalRecordsMover.AppCode
{
    internal class ImportProgress : ICloneable
    {
        public ImportProgress(int count)
        {
            Entities = new List<EntityProgress>();
            Count = count;
        }

        public List<EntityProgress> Entities { get; }
        public int Count { get; }

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

    internal class EntityProgress : ICloneable
    {
        public EntityProgress(EntityMetadata emd, string displayName)
        {
            LogicalName = emd.LogicalName;
            Entity = displayName;
            Metadata = emd;
        }

        public EntityMetadata Metadata { get; }
        public string LogicalName { get; }
        public string Entity { get; }
        public int Processed { get; set; }
        public int Success { get; set; }
        public int Error { get; set; }

        public object Clone()
        {
            return new EntityProgress(Metadata, Entity)
            {
                Error = Error,
                Success = Success,
                Processed = Processed
            };
        }
    }
}
