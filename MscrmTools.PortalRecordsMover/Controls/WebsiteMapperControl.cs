using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Xrm.Sdk;
using MscrmTools.PortalRecordsMover.AppCode;

namespace MscrmTools.PortalRecordsMover.Controls
{
    public partial class WebsiteMapperControl : UserControl
    {
        public WebsiteMapperControl(EntityReference sourceSiteRef, List<Website> targetWebsites)
        {
            InitializeComponent();

            lblSourceWebSite.Text = $"{sourceSiteRef.Name} ({sourceSiteRef.Id})";

            cbbTargetWebsites.Items.AddRange(targetWebsites.ToArray());

            var existingWebsite = targetWebsites.FirstOrDefault(w => w.Record.Id == sourceSiteRef.Id);
            if (existingWebsite != null)
            {
                cbbTargetWebsites.SelectedItem = existingWebsite;
                cbbTargetWebsites.Enabled = false;
            }

            InitialSourceId = sourceSiteRef.Id;
        }

        public Guid NewSourceId { get; private set; }

        public Guid InitialSourceId { get; private set; }

        private void cbbTargetWebsites_SelectedIndexChanged(object sender, EventArgs e)
        {
            NewSourceId = ((Website) cbbTargetWebsites.SelectedItem).Record.Id;
        }
    }
}
