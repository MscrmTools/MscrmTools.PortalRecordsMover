using System.Collections.Generic;
using Microsoft.Xrm.Sdk;

namespace MscrmTools.PortalRecordsMover.AppCode
{
    internal class ExportResults
    {
        public ExportResults()
        {
            Entities = new List<EntityResult>();
        }

        public List<EntityResult> Entities { get; set; }
        public List<EntityResult> NnRecords { get; internal set; }
        public ExportSettings Settings { get; internal set; }
        public List<Entity> Views { get; internal set; }
    }

    internal class EntityResult
    {
        public EntityCollection Records { get; set; }
    }
}
