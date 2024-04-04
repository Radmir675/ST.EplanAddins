using Eplan.EplApi.ApplicationFramework;
using System;
using System.Windows.Forms;

namespace ST.EplAddin.Footnote
{
    public partial class PropertiesDialog : Form
    {
        public PropertiesDialog()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        public void setItem(FootnoteItem note)
        {
            propertyGrid1.SelectedObject = note;
        }

        private void button_apply_Click(object sender, EventArgs e)
        {
            (propertyGrid1.SelectedObject as FootnoteItem).UpdateSubItems();
            (propertyGrid1.SelectedObject as FootnoteItem).Serialize();
            ActionManager oMng = new ActionManager();
            Eplan.EplApi.ApplicationFramework.Action baseAction = oMng.FindAction("gedRedraw");
            baseAction.Execute(new ActionCallingContext());
        }

        private void button_close_Click(object sender, EventArgs e)
        {
            (propertyGrid1.SelectedObject as FootnoteItem).Deserialize();
            (propertyGrid1.SelectedObject as FootnoteItem).UpdateSubItems();
            this.Close();
        }
    }
}
