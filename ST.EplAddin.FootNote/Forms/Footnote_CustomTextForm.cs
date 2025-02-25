#nullable enable
using Eplan.EplApi.DataModel.E3D;
using Microsoft.Extensions.DependencyInjection;
using ST.EplAddin.FootNote.Forms;
using ST.EplAddin.FootNote.Services;
using System.Windows.Forms;

namespace ST.EplAddin.FootNote
{
    public partial class Footnote_CustomTextForm : Form
    {
        private readonly IWindowsServiceDialog _dialog;
        private readonly Placement3D _placement3D;
        private PropertyEplan propertiesEplanObject;
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
            _dialog = App.Services.GetService<IWindowsServiceDialog>(); ;

        }
        public string GetUserText()
        {
            return CommentText.Text;
        }

        private void propertiesButton_Click(object sender, System.EventArgs e)
        {
            var result = _dialog.ShowFullPropertiesWindow(_placement3D, out PropertyEplan prop);
            if (result)
            {
                //TODO:Удалить 
                propertiesEplanObject = prop;
                AppendText(prop.PropertyIndex.ToString());
            }
            DialogResult = DialogResult.None;
        }

        private void Ok_Button_Click(object sender, System.EventArgs e)
        {
            PropertySelectDialogForm.PropertySelectedEvent -= PropertySelectDialogForm_PropertySelectedEvent;
        }

        private void Footnote_CustomTextForm_Load(object sender, System.EventArgs e)
        {
            PropertySelectDialogForm.PropertySelectedEvent += PropertySelectDialogForm_PropertySelectedEvent;
        }

        public PropertyEplan TryGetPropertiesSelectedObject()
        {
            return propertiesEplanObject;
        }
    }
}
