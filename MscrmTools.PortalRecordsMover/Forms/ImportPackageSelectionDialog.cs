using System;
using System.IO;
using System.Windows.Forms;

namespace MscrmTools.PortalRecordsMover.Forms
{
    public partial class ImportPackageSelectionDialog : Form
    {
        public ImportPackageSelectionDialog()
        {
            InitializeComponent();
        }

        public string Path { get; private set; }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (rdbSelectFile.Checked)
            {
                var ofd = new OpenFileDialog
                {
                    Filter = "XML or Zip File (*.xml,*.zip)|*.xml;*.zip",
                    Title = "Select the file containing portal records to import"
                };

                if (ofd.ShowDialog(this) == DialogResult.OK)
                {
                    txtPath.Text = ofd.FileName;
                }
            }
            else
            {
                var fbd = new FolderBrowserDialog
                {
                    Description = "Folder with portal data to import"
                };

                if (fbd.ShowDialog(this) == DialogResult.OK)
                {
                    txtPath.Text = fbd.SelectedPath;
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (rdbSelectFile.Checked && File.Exists(txtPath.Text)
                || rdbSelectFolder.Checked && Directory.Exists(txtPath.Text))
            {
                Path = txtPath.Text;
            }
            else
            {
                MessageBox.Show(this, @"The path you specified does not exist", @"Invalid path", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}