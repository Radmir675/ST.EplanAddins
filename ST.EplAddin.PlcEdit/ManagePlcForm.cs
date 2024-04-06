using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace ST.EplAddin.PlcEdit
{
    public partial class ManagePlcForm : Form
    {
        private DataTable dataTable;
        public List<DataModelView> Data { get; private set; }
        public ManagePlcForm()
        {
            InitializeComponent();
        }

        private void ManagePlcForm_Load(object sender, EventArgs e)
        {
            CreateDataTable();
        }

        private void CreateDataTable()
        {
            dataTable = new DataTable();
            dataTable.Columns.Add("Number", typeof(int));
            dataTable.Columns.Add("PLC adress", typeof(string));
            dataTable.Columns.Add("Data type", typeof(string));
            dataTable.Columns.Add("Symbolic address", typeof(string));
            dataTable.Columns.Add("Functional text", typeof(string));
            dataTable.Columns.Add("DT(identifying)", typeof(string));
            dataTable.Columns.Add("Connection point designation", typeof(string));
            dataTable.Columns.Add("Function definition", typeof(string));
            dataGridView.DataSource = dataTable;
        }
        private void AddData(List<DataModelView> data)
        {
            data.ForEach(x => dataTable.Rows.Add(x));
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                row.HeaderCell.Value = (row.Index + 1).ToString();
            }
        }
    }
}
