using Eplan.EplApi.DataModel.E3D;
using ST.EplAddin.FootNote.Forms;
using System.Windows.Forms;

namespace ST.EplAddin.FootNote
{
    public partial class Footnote_CustomTextForm : Form
    {
        private readonly Placement3D _placement3D;
        public Footnote_CustomTextForm()
        {
            InitializeComponent();
            var subscribersCount = PropertySelectDialogForm.GetSubscriberCount();
        }

        private void PropertySelectDialogForm_PropertySelectedEvent(object sender, string message)
        {
            AppendText(message);
        }

        private void AppendText(string message)
        {
            var oldText = CommentText.Text.ToString();
            CommentText.Text = oldText + " " + "{" + message + "}";
        }

        public Footnote_CustomTextForm(Placement3D placement3D) : this()
        {
            this._placement3D = placement3D;
        }
        //public void ShowForm()
        //{
        //    Process oCurrent = Process.GetCurrentProcess();
        //    var eplanHandle = new WindowWrapper(oCurrent.MainWindowHandle);
        //    Footnote_CustomTextForm dialog = new Footnote_CustomTextForm();
        //    dialog.ShowDialog(eplanHandle);
        //}

        public string GetUserText()
        {
            return CommentText.Text;
        }

        private void propertiesButton_Click(object sender, System.EventArgs e)
        {
            PropertySelectDialogForm form = new PropertySelectDialogForm(_placement3D);
            form.ShowDialog(this);
            Update();
            this.DialogResult = DialogResult.None;
        }

        private void Ok_Button_Click(object sender, System.EventArgs e)
        {
            PropertySelectDialogForm.PropertySelectedEvent -= PropertySelectDialogForm_PropertySelectedEvent;
        }

        private void Footnote_CustomTextForm_Load(object sender, System.EventArgs e)
        {
            PropertySelectDialogForm.PropertySelectedEvent += PropertySelectDialogForm_PropertySelectedEvent;
        }
    }
}
