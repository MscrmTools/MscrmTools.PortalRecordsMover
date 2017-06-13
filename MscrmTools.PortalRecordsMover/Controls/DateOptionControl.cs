using System;
using System.Windows.Forms;

namespace MscrmTools.PortalRecordsMover.Controls
{
    public partial class DateOptionControl : UserControl
    {
        public DateOptionControl()
        {
            InitializeComponent();
        }

        public string Label { get; set; }

        public string Attribute { get; set; }

        public bool IsEnabled
        {
            get { return chkEnabled.Checked; }
            set { chkEnabled.Checked = value; }
        }

        public DateTime SelectedDate
        {
            get { return dateTimePicker1.Value; }
            set { dateTimePicker1.Value = value; }
        } 

        private void DateOptionControl_Load(object sender, EventArgs e)
        {
            chkEnabled.Text = Label;
        }
    }
}
