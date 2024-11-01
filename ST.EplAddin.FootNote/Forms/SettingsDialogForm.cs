using System;
using System.Windows.Forms;

namespace ST.EplAddin.FootNote
{
    public partial class SettingsDialogForm : Form
    {
        public SettingsDialogForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            propertyGrid1.SelectedObject = STSettings.instance;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            STSettings.instance.SaveSettings();
        }

        private void SettingsDialog_Load(object sender, EventArgs e)
        {
            STSettings.instance.LoadSettings();
        }
    }
}
