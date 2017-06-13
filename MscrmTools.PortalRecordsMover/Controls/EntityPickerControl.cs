using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using MscrmTools.PortalRecordsMover.AppCode;

namespace MscrmTools.PortalRecordsMover.Controls
{
    public partial class EntityPickerControl : UserControl
    {
        #region Variables

       private readonly List<ListViewItem> items = new List<ListViewItem>();

        #endregion

        #region Constructor

        public EntityPickerControl()
        {
            InitializeComponent();
        }

        public EntityPickerControl(IOrganizationService service) : this()
        {
            Service = service;
        }

        #endregion

        #region Properties
        public IOrganizationService Service { get; set; }

        public List<EntityMetadata> Metadata { get; private set; }

        public List<EntityMetadata> SelectedMetadatas
        {
            get { return lvEntities.CheckedItems.Cast<ListViewItem>().Select(i => i.Tag as EntityMetadata).ToList(); }
        }

        #endregion

        #region Events

        private void llClear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (ListViewItem item in lvEntities.Items)
            {
                item.Checked = false;
            }
        }

        private void llSelectAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (ListViewItem item in lvEntities.Items)
            {
                item.Checked = true;
            }
        }

        #endregion

        #region Methods

        public void LoadEntities(ExportSettings settings)
        {
            Metadata = MetadataManager.GetEntitiesList(Service);
            
            foreach (var emd in Metadata.Where(m => m.IsIntersect == null || m.IsIntersect.Value == false))
            {
                items.Add(new ListViewItem(emd.DisplayName?.UserLocalizedLabel?.Label??emd.SchemaName)
                {
                    Tag = emd,
                    Checked = settings.SelectedEntities.Contains(emd.LogicalName)
                });
            }
        }

        public void FillList()
        {
            lvEntities.Items.AddRange(items.ToArray());
        }

        public void SelectItems(List<string> entities)
        {
            foreach (ListViewItem item in lvEntities.Items)
            {
                item.Checked = entities.Contains(((EntityMetadata) item.Tag).LogicalName);
            }
        }

        #endregion
    }
}
