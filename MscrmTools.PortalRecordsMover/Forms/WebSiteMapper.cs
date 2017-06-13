using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xrm.Sdk;
using MscrmTools.PortalRecordsMover.AppCode;
using MscrmTools.PortalRecordsMover.Controls;

namespace MscrmTools.PortalRecordsMover.Forms
{
    public partial class WebSiteMapper : Form
    {
        private EntityCollection ec;
        private List<Website> targetWebsites;

        public WebSiteMapper(EntityCollection ec, List<Website> targetWebsites)
        {
            InitializeComponent();

            this.ec = ec;
            this.targetWebsites = targetWebsites;
        }

        private void WebSiteMapper_Load(object sender, EventArgs e)
        {
            foreach (var record in
                ec.Entities.SelectMany(ent => ent.Attributes)
                    .Where(a => a.Value is EntityReference && ((EntityReference)a.Value).LogicalName == "adx_website")
                    .Select(a => (EntityReference)a.Value).DistinctBy(r => r.Id)
                )
            {
                if (targetWebsites.FirstOrDefault(w => w.Record.Id == record.Id) == null)
                {
                    var ctrl = new WebsiteMapperControl(record, targetWebsites);
                    ctrl.Dock = DockStyle.Top;

                    pnlMain.Controls.Add(ctrl);
                    pnlMain.Controls.SetChildIndex(ctrl, 0);
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            foreach (Control ctrl in pnlMain.Controls)
            {
                var wmc = ctrl as WebsiteMapperControl;
                if (wmc != null)
                {
                    foreach (var attr in ec.Entities.SelectMany(ent => ent.Attributes)
                        .Where(a => a.Value is EntityReference && ((EntityReference)a.Value).Id == wmc.InitialSourceId))
                    {
                        ((EntityReference)attr.Value).Id = wmc.NewSourceId;
                    }
                }
            }

            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

    public static class Extensions
    {
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>
    (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }
}
