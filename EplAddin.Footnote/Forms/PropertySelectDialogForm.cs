using BrightIdeasSoftware;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.DataModel.E3D;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ST.EplAddin.Footnote.ProperyBrowser
{
    public partial class PropertySelectDialogForm : Form
    {
        public string CurrentSelection { get; set; }
        public Placement3D Placement3D { get; }
        public List<PropertiesEplan> propertiesEplans { get; set; }
        public PropertySelectDialogForm(Placement3D placement3D)
        {
            InitializeComponent();
            Placement3D = placement3D;

            //  GetProperties();
        }
        public PropertySelectDialogForm()
        {
            InitializeComponent();

        }
        public void GetProperties()
        {
            var properties = Placement3D.FunctionDefinition.Properties.ExistingValues;



            List<string> s = new List<string>();
            foreach (var property in Eplan.EplApi.DataModel.Properties.AllPlacement3DPropIDs)
            {
                try
                {
                    PropertyDefinition propertyDefinition = new PropertyDefinition(property);
                    bool IsIndexed = propertyDefinition.IsIndexed;
                    var fff = property.Definition.Name;
                    var ss = property.Definition.Id;
                    if (true /*&& !Placement3D.Properties[property].IsEmpty*/)
                    {

                        var value = Placement3D.Properties[property];
                        var name = property.Definition.Name;
                        s.Add(value);

                    }

                }
                catch (System.Exception)
                {


                }
            }
            s.Add("");
        }

        private void cancel_button_Click(object sender, System.EventArgs e)
        {
            Application.Exit();
        }

        private void ok_button_Click(object sender, System.EventArgs e)
        {

        }
        public void AppentText()
        {
            treeView.Nodes.Add("id", "1 часть");
            treeView.Nodes.Add("id1", "2 часть");
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode treeNode = e.Node;
            if (treeNode == null) return;
            CurrentSelection = treeNode.Text;// по name можно достать ID
            if (CurrentSelection == "1 часть")
            {
                SetDataToTreeListView();

            }

        }

        private void PropertySelectDialogForm_Load(object sender, System.EventArgs e)
        {
            AppentText();

        }
        public void SetDataToTreeListView()
        {
            try
            {
                var s = new DataTreeListView();
                s.Items.Add("vdf");
                dataTreeListView1.DataSource = s;


            }
            catch (System.Exception)
            {


            }
        }
    }
}
