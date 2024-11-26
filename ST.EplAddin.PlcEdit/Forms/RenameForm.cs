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
            if (string.IsNullOrEmpty(errorProvider.GetError(pathTextBox)))
            {
                this.DialogResult = DialogResult.OK;
                Path = pathTextBox.Text;
                Close();
            }
        }

        private void pathTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(pathTextBox.Text))
            {
                //pathTextBox.Focus();
                errorProvider.SetError(pathTextBox, "Value is empty");
                e.Cancel = false;
            }
            else if (_templateName == pathTextBox.Text)
            {
                //pathTextBox.Focus();
                errorProvider.SetError(pathTextBox, "Value the same of primary");
                e.Cancel = false;
            }
            else
            {
                errorProvider.Clear();

            }

        }
    }
}
