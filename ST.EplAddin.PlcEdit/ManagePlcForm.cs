using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace ST.EplAddin.PlcEdit
{
    public partial class ManagePlcForm : Form
    {
        private DataTable dataTable;
        public List<PlcDataModelView> PlcDataModelView { get; set; }
        private int selectedRowsCount;

        public int SelectedRowsCount
        {
            get { return dataGridView?.SelectedRows.Count ?? 0; }
            set { selectedRowsCount = value; }
        }


        public ManagePlcForm(List<PlcDataModelView> plcDataModelView)
        {
            InitializeComponent();
            PlcDataModelView = plcDataModelView;
        }

        private void ManagePlcForm_Load(object sender, EventArgs e)
        {
            CreateDataTable();
            AddData(PlcDataModelView);
        }

        private void CreateDataTable()
        {
            dataTable = new DataTable();
            dataTable.Columns.Add("Connection point description", typeof(string));
            dataTable.Columns.Add("PLC adress", typeof(string));
            dataTable.Columns.Add("Data type", typeof(string));
            dataTable.Columns.Add("Symbolic address", typeof(string));
            dataTable.Columns.Add("Functional text", typeof(string));
            dataTable.Columns.Add("DT(identifying)", typeof(string));
            dataTable.Columns.Add("Connection point designation", typeof(string));
            dataTable.Columns.Add("Function definition", typeof(string));
            dataGridView.DataSource = dataTable;
        }
        private void AddData(List<PlcDataModelView> data)
        {
            data.ForEach(x => dataTable.Rows.Add(x));
        }

        private void up_button_Click(object sender, EventArgs e)
        {
            if (selectedRowsCount != 1)
                return;

        }

        private void dowm_button_Click(object sender, EventArgs e)
        {
            if (selectedRowsCount != 1)
                return;
            var s = dataGridView?.SelectedRows[0];
            //          if (row.Index == 0 && offset == -1 || ((row.Index == dgv.NewRowIndex - 1) &&
            //  offset == 1 || row.Index == dgv.NewRowIndex)
            //return; // Ничего делать не надо => выходим
            //        // Получаем текущий индекс строки
            //          int currentIndex = row.Index;
            //          // Удаляем ее из коллекции
            //          dgv.Rows.Remove(row);
            //          // А теперь добавляем со смещением
            //          dgv.Rows.Insert(currentIndex + offset, row);
            //offset=Так же у нас будет переменная offset,
            //которая будет задавать смещение (-1 если двигаем строку вверх, 1 - если вниз)
        }

        private void exchange_button_Click(object sender, EventArgs e)
        {
            if (selectedRowsCount != 2)
                return;


        }
    }
}
//foreach (DataGridViewRow row in dataGridView.Rows)
//{
//    row.HeaderCell.Value = (row.Index + 1).ToString();
//}

//int selectedRows = dataGridView?.SelectedRows.Count??0;
//bool isTableSelected=dataGridView.Sele
//if (selectedRows == null)
//{
//    up_button.Enabled = false;
//    dowm_button.Enabled = false;
//    exchange_button.Enabled = false;
//}
//            Примерный алгоритм.

//Получаем и сохраняем индекс выбранной строки.
//Сохраняем данные строки.
//Получаем данные из индекс -1 и заменяем их в нашей строке.
//Вставляем наши сохранённые данные в индекс -1.