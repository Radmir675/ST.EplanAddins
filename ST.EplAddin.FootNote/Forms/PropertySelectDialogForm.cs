using ST.EplAddin.FootNote.Services.Implementations;
using System;
using System.Windows.Forms;
using Placement3D = Eplan.EplApi.DataModel.E3D.Placement3D;

namespace ST.EplAddin.FootNote.Forms
{
    public partial class PropertySelectDialogForm : Form
    {
        public static event EventHandler<string> PropertySelectedEvent;
        private TreeNode CurrentSelection { get; set; }
        private PropertiesStore PropertiesStore { get; set; }

        public PropertySelectDialogForm(Placement3D placement3D)
        {
            InitializeComponent();
            PropertiesStore = new PropertiesStore(placement3D);
        }
        private void cancel_button_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void ok_button_Click(object sender, System.EventArgs e)
        {
            var selectedRow = dataGridView1.SelectedRows[0];
            string message = string.Empty;
            if (selectedRow != null)
            {
                message = selectedRow.Cells[2].Value.ToString();
            }
            PropertySelectedEvent?.Invoke(this, message);
            Close();
        }

        public static int GetSubscriberCount()
        {
            return PropertySelectedEvent?.GetInvocationList().Length ?? 0;
        }

        public void AppendText()
        {
            treeView1.Nodes.Add("id1", "Размещение изделия");
            treeView1.Nodes.Add("id2", "Ссылка изделия");
            treeView1.Nodes.Add("id3", "Изделие");
        }
        private void PropertySelectDialogForm_Load(object sender, System.EventArgs e)
        {
            AppendText();
            treeView1.SelectedNode = treeView1.Nodes[0];
            // dataGridView1.Columns[2].Visible = false;
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            switch (treeView1.SelectedNode.Text)
            {
                case "Размещение изделия":
                    dataGridView1.DataSource = PropertiesStore.Placement3DProperties; break;
                case "Ссылка изделия":
                    dataGridView1.DataSource = PropertiesStore.ArticleReferenceProperties; break;
                case "Изделие":
                    dataGridView1.DataSource = PropertiesStore.ArticleProperties; break;
            }
        }
    }
}

