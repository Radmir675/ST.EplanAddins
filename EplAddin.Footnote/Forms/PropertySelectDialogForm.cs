using Eplan.EplApi.DataModel;
using Eplan.EplApi.DataModel.E3D;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ST.EplAddin.Footnote.ProperyBrowser
{
    public partial class PropertySelectDialogForm : Form
    {
        public string CurrentSelection { get; set; }
        public Placement3D Placement3D { get; }
        public List<PropertiesEplan> propertiesEplans { get; set; }
        // private fields
        private List<Node> data;

        public PropertySelectDialogForm(Placement3D placement3D)
        {

            InitializeComponent();
            Placement3D = placement3D;

            GetProperties();
        }

        public PropertySelectDialogForm()
        {
            InitializeComponent();

            GetProperties();

        }
        public void GetProperties()
        {
            var properties = Placement3D.FunctionDefinition.Properties.ExistingValues;



            List<Tuple<string, string>> result = new();
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
                        var name = property?.Definition.Name;
                        result.Add(new Tuple<string, string>(name, value));
                    }

                }
                catch (System.Exception)
                {


                }
            }
            // var data = Placement3D.Properties[22074].ToMultiLangString().GetAsString();
            dataGridView1.DataSource = result;
            var countResult = result.Count;
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
                InitializeData();
                // set the tree roots
                this.treeListView.Roots = data;

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


            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }
        }
        // embedded class
        class Node
        {
            public string Name { get; private set; }
            public string Column1 { get; private set; }
            public string Column2 { get; private set; }
            public string Column3 { get; private set; }
            public List<Node> Children { get; private set; }
            public Node(string name, string col1, string col2, string col3)
            {
                this.Name = name;
                this.Column1 = col1;
                this.Column2 = col2;
                this.Column3 = col3;
                this.Children = new List<Node>();
            }
        }






        // private methods
        private void FillTree()
        {
            // set the delegate that the tree uses to know if a node is expandable
            this.treeListView.CanExpandGetter = x => (x as Node).Children.Count > 0;
            // set the delegate that the tree uses to know the children of a node
            this.treeListView.ChildrenGetter = x => (x as Node).Children;

            // create the tree columns and set the delegates to print the desired object proerty
            var nameCol = new BrightIdeasSoftware.OLVColumn("Name", "Name");
            nameCol.AspectGetter = x => (x as Node).Name;

            var col1 = new BrightIdeasSoftware.OLVColumn("Column1", "Column1");
            col1.AspectGetter = x => (x as Node).Column1;

            var col2 = new BrightIdeasSoftware.OLVColumn("Column2", "Column2");
            col2.AspectGetter = x => (x as Node).Column2;

            var col3 = new BrightIdeasSoftware.OLVColumn("Column3", "Column3");
            col3.AspectGetter = x => (x as Node).Column3;

            // add the columns to the tree
            this.treeListView.Columns.Add(nameCol);
            this.treeListView.Columns.Add(col1);
            this.treeListView.Columns.Add(col2);
            this.treeListView.Columns.Add(col3);

            // set the tree roots
            this.treeListView.Roots = data;
        }

        private void InitializeData()
        {
            // create fake nodes
            var parent1 = new Node("PARENT1", "-", "-", "-");
            parent1.Children.Add(new Node("CHILD_1_1", "A", "X", "1"));
            parent1.Children.Add(new Node("CHILD_1_2", "A", "Y", "2"));
            parent1.Children.Add(new Node("CHILD_1_3", "A", "Z", "3"));

            var parent2 = new Node("PARENT2", "-", "-", "-");
            parent2.Children.Add(new Node("CHILD_2_1", "B", "W", "7"));
            parent2.Children.Add(new Node("CHILD_2_2", "B", "Z", "8"));
            parent2.Children.Add(new Node("CHILD_2_3", "B", "J", "9"));

            var parent3 = new Node("PARENT3", "-", "-", "-");
            parent3.Children.Add(new Node("CHILD_3_1", "C", "R", "10"));
            parent3.Children.Add(new Node("CHILD_3_2", "C", "T", "12"));
            parent3.Children.Add(new Node("CHILD_3_3", "C", "H", "14"));

            data = new List<Node> { parent1, parent2, parent3 };
        }



    }

}

