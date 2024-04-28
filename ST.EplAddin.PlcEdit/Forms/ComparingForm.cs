using ST.EplAddin.PlcEdit.Helpers;
using System;
using System.Windows.Forms;

namespace ST.EplAddin.PlcEdit.Forms
{
    public partial class ComparingForm : Form
    {
        public ComparisonState ComparisonSelection { get; set; }
        public ComparingForm()
        {
            InitializeComponent();
        }

        private void ComparingForm_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = Enum.GetValues(typeof(ComparisonState));
            comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComparisonSelection = (ComparisonState)Enum.Parse(typeof(ComparisonState), comboBox1.Text);
        }
    }
}
