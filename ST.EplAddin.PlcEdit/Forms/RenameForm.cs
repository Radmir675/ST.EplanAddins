using System.Windows.Forms;

namespace ST.EplAddin.PlcEdit.Forms
{
    public partial class RenameForm : Form
    {
        private readonly string _templateName;
        public static string Path { get; set; }
        public RenameForm(string templateName)
        {
            _templateName = templateName;
            InitializeComponent();
            Path = string.Empty;
        }
        private void Rename_Click(object sender, System.EventArgs e)
        {
            if (!string.IsNullOrEmpty(pathTextBox.Text) && _templateName != pathTextBox.Text)
            {
                this.DialogResult = DialogResult.OK;
                Path = pathTextBox.Text;
                Close();
            }
        }
    }
}
