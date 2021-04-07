using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace MscrmTools.PortalRecordsMover.AppCode
{
    public class ExportSettings
    {
        public ExportSettings()
        {
            SelectedEntities = new List<string>();
        }

        public bool ActiveItemsOnly { get; set; }

        [XmlIgnore]
        public List<EntityMetadata> AllEntities { get; set; }

        public DateTime? CreateFilter { get; set; }

        [XmlIgnore]
        public List<EntityMetadata> Entities
        {
            get { return AllEntities.Where(e => SelectedEntities.Contains(e.LogicalName)).ToList(); }
        }

        public bool ExportInFolderStructure { get; set; }
        public DateTime? ModifyFilter { get; set; }
        public bool RemoveFormattedValues { get; set; }
        public List<string> SelectedEntities { get; set; }
        public bool ShowEntitiesWithNoRecords { get; set; }
        public Guid WebsiteFilter { get; set; }
        public bool ZipFolderStructure { get; set; }
    }
}