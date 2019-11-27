using System.Windows.Forms;

namespace MscrmTools.PortalRecordsMover.Controls
{
    public partial class ProgressTile : UserControl
    {
        public ProgressTile(string text)
        {
            InitializeComponent();

            lblText.Text = text;
        }

        public void Cancel()
        {
            pbIcon.Image = global::MscrmTools.PortalRecordsMover.Properties.Resources.cancel;
        }

        public void Complete()
        {
            pbIcon.Image = global::MscrmTools.PortalRecordsMover.Properties.Resources.tick;
        }
    }
}