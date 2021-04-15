using System;
using System.Windows.Forms;

namespace MscrmTools.PortalRecordsMover.Forms
{
    public partial class PreImportWarningDialog : Form
    {
        public PreImportWarningDialog(bool showPluginMessage, bool showJavaScriptMessage, bool webFileCleaning, bool siteSettingsCheck)
        {
            InitializeComponent();

            pnlPagePlugin.Visible = showPluginMessage;
            pnlJavaScriptRestriction.Visible = showJavaScriptMessage;
            pnlWebFile.Visible = webFileCleaning;
            pnlSiteSettings.Visible = siteSettingsCheck;
        }

        public bool CleanWebFiles { get; private set; }
        public bool CreateOnlyNewSiteSettings { get; private set; }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            CleanWebFiles = rdbWebFileCleaningYes.Checked;
            CreateOnlyNewSiteSettings = rdbCreateOnlyNewSettingsYes.Checked;
            Close();
        }
    }
}