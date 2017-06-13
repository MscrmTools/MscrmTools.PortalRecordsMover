using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Xrm.Sdk;
using MscrmTools.PortalRecordsMover.AppCode;

namespace MscrmTools.PortalRecordsMover.Controls
{
    public partial class WebsitePickerControl : UserControl
    {
        public WebsitePickerControl()
        {
            InitializeComponent();
        }

        public List<Website> Websites
        {
            set
            {
                cbbWebsite.Items.AddRange(value.Select(v => (object)v).ToArray());
            }
        }

        public bool IsEnabled
        {
            get { return chkEnabled.Checked; }
            set { chkEnabled.Checked = value; }
        }

        public Guid SelectedWebSiteId
        {
            get { return ((Website) cbbWebsite.SelectedItem)?.Record.Id ?? Guid.Empty; }
            set
            {
                foreach (Website website in cbbWebsite.Items)
                {
                    if (website.Record.Id == value)
                    {
                        cbbWebsite.SelectedItem = website;
                    }
                }

            }
        }

        public Entity SelectedWebsite => cbbWebsite.SelectedItem != null ? ((Website) cbbWebsite.SelectedItem).Record : null;
    }
}
