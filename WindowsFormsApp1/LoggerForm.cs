using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class LoggerForm : Form
    {
        Point startPoint = Point.Empty;
        bool drag = false;

        public LoggerForm()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 10, 10));
            panel1.BackColor = Color.FromArgb(249, 239, 240);
            gradientPanel1.ColorTop = Color.FromArgb(241, 239, 241);
            gradientPanel1.ColorBottom = Color.FromArgb(234, 232, 234);

        }
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
       (
           int nLeftRect,     // x-coordinate of upper-left corner
           int nTopRect,      // y-coordinate of upper-left corner
           int nRightRect,    // x-coordinate of lower-right corner
           int nBottomRect,   // y-coordinate of lower-right corner
           int nWidthEllipse, // width of ellipse
           int nHeightEllipse // height of ellipse
       );

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void close_button_MouseLeave(object sender, EventArgs e)
        {
            close_button.BackColor = panel1.BackColor;
        }

        private void close_button_MouseMove(object sender, MouseEventArgs e)
        {
            close_button.BackColor = Color.Red;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (drag)
            {
                Point p = PointToScreen(e.Location);
                this.Location = new Point(p.X - startPoint.X, p.Y - startPoint.Y);
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            drag = true;
            startPoint = new Point(e.X, e.Y);
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            drag = false;
        }
    }


}
