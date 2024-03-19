using System.Diagnostics;
using System.Windows.Forms;

namespace ST.EplAddin.Footnote
{
    public partial class Footnote_CustomTextForm : Form
    {
        public Footnote_CustomTextForm()
        {
            InitializeComponent();
        }
        public void ShowForm()
        {
            Process oCurrent = Process.GetCurrentProcess();
            var eplanHandle = new WindowWrapper(oCurrent.MainWindowHandle);
            Footnote_CustomTextForm dialog = new Footnote_CustomTextForm();
            dialog.ShowDialog(eplanHandle);
        }

        public string GetUserText()
        {
            ShowForm();
            if (AcceptButton.DialogResult == DialogResult.OK)
            {
                return CommentText.Text;
            }
            return null;

        }

        private void Ok_Button_Click(object sender, System.EventArgs e)
        {

        }
    }
}
