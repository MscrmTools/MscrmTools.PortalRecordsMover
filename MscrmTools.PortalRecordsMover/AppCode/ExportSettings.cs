using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Microsoft.Xrm.Sdk.Metadata;

namespace MscrmTools.PortalRecordsMover.AppCode
{
    public class ExportSettings
    {
        public ExportSettings()
        {
            SelectedEntities = new List<string>();
        }

        public bool ActiveItemsOnly { get; set; }
        public DateTime? CreateFilter { get; set; }
        public DateTime? ModifyFilter { get; set; }
        public Guid WebsiteFilter { get; set; }

        public List<string> SelectedEntities { get; set; }

        [XmlIgnore]
        public List<EntityMetadata> Entities {
            get { return AllEntities.Where(e => SelectedEntities.Contains(e.LogicalName)).ToList(); }
        }

        [XmlIgnore]
        public List<EntityMetadata> AllEntities { get; set; }
    }
}
