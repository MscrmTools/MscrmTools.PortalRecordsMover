using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MscrmTools.PortalRecordsMover.Controls
{
    public class CustomProgressBar: ProgressBar
    {
            public CustomProgressBar()
            {
                this.SetStyle(ControlStyles.UserPaint, true);
            }

        public bool IsOnError { get; set; }

            protected override void OnPaint(PaintEventArgs e)
            {
                Rectangle rec = e.ClipRectangle;

                rec.Width = (int)(rec.Width * ((double)Value / Maximum)) - 4;
                if (ProgressBarRenderer.IsSupported)
                    ProgressBarRenderer.DrawHorizontalBar(e.Graphics, e.ClipRectangle);
                rec.Height = rec.Height - 4;
                e.Graphics.FillRectangle(IsOnError ? Brushes.Red : Brushes.LimeGreen, 2, 2, rec.Width, rec.Height);
            }
    }
}
