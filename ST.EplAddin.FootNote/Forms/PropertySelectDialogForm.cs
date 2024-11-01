using Eplan.EplApi.DataModel;
using Eplan.EplApi.DataModel.E3D;
using ST.EplAddin.FootNote.ProperyBrowser;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ST.EplAddin.FootNote
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

            //List<Tuple<string, string>> placement3DProperties = Getplacement3DProperties();
            List<Tuple<string, string>> articleProperties = GetArticleProperties();
            //List<Tuple<string, string>> articleReferenceProperties = GetarticleReferenceProperties();


            dataGridView1.DataSource = articleProperties;
            var countResult = articleProperties.Count;
            return;
        }

        private List<Tuple<string, string>> GetArticleProperties()
        {
            List<Tuple<string, string>> result = new();
            foreach (var property in Properties.AllArticlePropIDs)
            {
                try
                {
                    //Article article = new Article(property);
                    //bool IsIndexed = article.IsIndexed;
                    //var fff = property.Definition.Name;
                    //var ss = property.Definition.Id;
                    //if (true /*&& !Placement3D.Properties[property].IsEmpty*/)
                    //{

                    //    var value = Placement3D.Properties[property];
                    //    var name = property?.Definition.Name;
                    //    result.Add(new Tuple<string, string>(name, value));
                    //}

                }
                catch (System.Exception)
                {


                }
            }

            return result;
        }

        private List<Tuple<string, string>> Getplacement3DProperties()
        {
            List<Tuple<string, string>> result = new();
            foreach (var property in Properties.AllPlacement3DPropIDs)
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

            return result;
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

