using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    class GradientPanel : Panel
    {
        public Color ColorTop { get; set; }
        public Color ColorBottom { get; set; }
        protected override void OnPaint(PaintEventArgs e)
        {
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(this.ClientRectangle, this.ColorTop, this.ColorBottom, 90F);
            Graphics graphics = e.Graphics;
            graphics.FillRectangle(linearGradientBrush, this.ClientRectangle);
            base.OnPaint(e);
        }
    }
}
