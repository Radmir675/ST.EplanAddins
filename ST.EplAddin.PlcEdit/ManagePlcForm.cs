using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ST.EplAddin.PlcEdit
{
    public partial class ManagePlcForm : Form
    {

        public event EventHandler<EventArgs> OkEvent;
        public event EventHandler<EventArgs> ApplyEvent;
        public List<PlcDataModelView> PlcDataModelView { get; set; }
        private int selectedRowsCount;
        public int SelectedRowsCount
        {
            get
            {
                return dataGridView.SelectedCells.Cast<DataGridViewCell>()
                                       .Select(c => c.RowIndex).Distinct().Count();
            }
            set { selectedRowsCount = value; }
        }

        public ManagePlcForm(List<PlcDataModelView> plcDataModelView)
        {
            InitializeComponent();
            PlcDataModelView = plcDataModelView;
        }

        private void ManagePlcForm_Load(object sender, EventArgs e)
        {
            AddData(PlcDataModelView);
            exchange_button.Enabled = false;
            // dataGridView.Columns["SymbolicAdressDefined"].Visible = false;
        }


        private void AddData(List<PlcDataModelView> data)
        {
            dataGridView.DataSource = data;
        }

        private void up_button_Click(object sender, EventArgs e)
        {
            if (selectedRowsCount != 1)
                return;
            InsertRowInEmptyPosition(Direction.Up);

        }

        private void dowm_button_Click(object sender, EventArgs e)
        {

            if (selectedRowsCount != 1)
                return;
            InsertRowInEmptyPosition(Direction.Down);
        }

        private void InsertRowInEmptyPosition(Direction direction)
        {
            var row = dataGridView?.SelectedRows[0];//выбранная строка
            var currentIndex = row.Index;
            var functionDefinition = row.Cells["Function definition"].Value.ToString();
            var targerIndex = GetEmptyRow(currentIndex, direction, functionDefinition);// задать смещение 
            dataGridView.Rows.Remove(row);
            dataGridView.Rows.Insert(targerIndex, row);
        }
        public bool IsModuleAssigned(string terminalName)
        {
            if (PlcDataModelView.Single(x => x.DT == terminalName).SymbolicAdressDefined == string.Empty)
            {
                return true;
            }
            return false;
        }
        private int GetEmptyRow(int currentPositionIndex, Direction direction, string functionDefinition)
        {
            DataGridViewRow firstProperlyRow = null;
            switch (direction)
            {
                case Direction.Up:
                    firstProperlyRow = dataGridView.Rows.OfType<DataGridViewRow>().First(x =>
                 x.Cells["SymbolicAdress"].Value == null
                && x.Cells["FunctionText"].Value == null
                 && x.Index < currentPositionIndex
                  && functionDefinition == x.Cells["FunctionDefinition"].Value.ToString());
                    break;
                case Direction.Down:
                    firstProperlyRow = dataGridView.Rows.OfType<DataGridViewRow>().First(x =>
               x.Cells["SymbolicAdress"].Value == null
              && x.Cells["FunctionText"].Value == null
               && x.Index > currentPositionIndex
                && functionDefinition == x.Cells["FunctionDefinition"].Value.ToString());
                    break;
            }

            var properlyRowIndex = firstProperlyRow.Index;
            return properlyRowIndex;
        }

        private void exchange_button_Click(object sender, EventArgs e)
        {
            if (selectedRowsCount != 2)
                return;
        }

        //недописано
        public List<PlcDataModelView> ReadDataFromGrid()
        {
            List<PlcDataModelView> plcDataModelView = new List<PlcDataModelView>();
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    string value = cell.Value.ToString();
                }
            }
            return plcDataModelView;

        }

        private void Ok_button_Click(object sender, EventArgs e)
        {
            OkEvent?.Invoke(this, EventArgs.Empty);
            Application.Exit();
        }
        private void Cancel_button_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Apply_button_Click(object sender, EventArgs e)
        {
            ApplyEvent?.Invoke(this, EventArgs.Empty);
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            exchange_button.Enabled = false;
            if (SelectedRowsCount == 2)
            {
                exchange_button.Enabled = true;
            }

        }
    }
}


