using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace ST.EplAddin.PlcEdit
{
    public partial class ManagePlcForm : Form
    {
        public event EventHandler<CustomEventArgs> ApplyEvent;
        private List<PlcDataModelView> PlcDataModelView { get; set; }

        public int SelectedRowsCount
        {
            get
            {
                return dataGridView.SelectedCells.Cast<DataGridViewCell>()
                                       .Select(c => c.RowIndex).Distinct().Count();
            }
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
        }


        private void AddData(List<PlcDataModelView> data)
        {
            dataGridView.DataSource = data;
        }

        private void up_button_Click(object sender, EventArgs e)
        {
            if (SelectedRowsCount != 1)
                return;
            InsertRowInEmptyPosition(Direction.Up);

        }

        private void dowm_button_Click(object sender, EventArgs e)
        {

            if (SelectedRowsCount != 1)
                return;
            InsertRowInEmptyPosition(Direction.Down);
        }
        private void exchange_button_Click(object sender, EventArgs e)
        {
            if (SelectedRowsCount != 2)
                return;
            ExchangePositions();
        }

        private void HighlightRow(int rowIndex)
        {
            dataGridView.ClearSelection();
            dataGridView.Rows[rowIndex].Selected = true;
            dataGridView.Refresh();
        }
        private void HighlightRow(int rowIndex1, int rowIndex2)
        {
            dataGridView.ClearSelection();
            dataGridView.Rows[rowIndex1].Selected = true;
            dataGridView.Rows[rowIndex2].Selected = true;

            dataGridView.Refresh();
        }
        private void InsertRowInEmptyPosition(Direction direction)
        {
            var currentIndexRow = dataGridView.SelectedCells.Cast<DataGridViewCell>().First().RowIndex;
            var currentRow = dataGridView.Rows[currentIndexRow];
            var functionDefinition = currentRow.Cells["FunctionDefinition"].Value.ToString();
            var targetIndexRow = TryGetEmptyIndexRow(currentRow.Index, direction, functionDefinition);// задать смещение 
            if (targetIndexRow == null)
            {
                //  MessageBox.Show("Не удалось");
                return;
            }
            AssignDataToTargetRow(currentIndexRow, targetIndexRow.Value);
            HighlightRow(targetIndexRow.Value);
        }
        private void ExchangePositions()
        {
            var rowsIndex = dataGridView.SelectedCells.Cast<DataGridViewCell>().Select(c => c.RowIndex).Distinct().ToArray();
            var currentIndexRow = rowsIndex[0];
            var targetIndexRow = rowsIndex[1];
            if (currentIndexRow != targetIndexRow)
            {
                AssignDataToTargetRow(currentIndexRow, targetIndexRow);
                HighlightRow(currentIndexRow, targetIndexRow);
            }
        }

        private void AssignDataToTargetRow(int sourceIndexRow, int targetIndexRow)
        {
            var sourceObject = PlcDataModelView[sourceIndexRow];
            var targetObject = PlcDataModelView[targetIndexRow];
            var targetObjectClone = targetObject.Clone() as PlcDataModelView;


            targetObject.SymbolicAdressDefined = string.Copy(sourceObject?.SymbolicAdressDefined ?? String.Empty);
            targetObject.FunctionText = string.Copy(sourceObject?.FunctionText ?? String.Empty);
            targetObject.SymbolicAdress = string.Copy(sourceObject?.SymbolicAdress ?? String.Empty);
            targetObject.Datatype = string.Copy(sourceObject?.Datatype ?? String.Empty);
            targetObject.PLCAdress = string.Copy(sourceObject?.PLCAdress ?? String.Empty);

            sourceObject.SymbolicAdressDefined = string.Copy(targetObjectClone?.SymbolicAdressDefined ?? String.Empty);
            sourceObject.FunctionText = string.Copy(targetObjectClone?.FunctionText ?? String.Empty);
            sourceObject.SymbolicAdress = string.Copy(targetObjectClone?.SymbolicAdress ?? String.Empty);
            sourceObject.Datatype = string.Copy(targetObjectClone?.Datatype ?? String.Empty);
            sourceObject.PLCAdress = string.Copy(targetObjectClone?.PLCAdress ?? String.Empty);
        }

        public bool IsModuleAssigned(string terminalName)
        {
            if (PlcDataModelView.Single(x => x.DT == terminalName).SymbolicAdressDefined == string.Empty)
            {
                return true;
            }
            return false;
        }
        private int? TryGetEmptyIndexRow(int currentPositionIndex, Direction direction, string functionDefinition)
        {
            DataGridViewRow firstProperlyRow = null;
            switch (direction)
            {
                case Direction.Up:

                    firstProperlyRow = dataGridView.Rows
                        .Cast<DataGridViewRow>()
                        .Where(z => z.Index < currentPositionIndex)
                        .Reverse()
                        .FirstOrDefault(x =>
                  x.Cells["SymbolicAdress"].Value.ToString() == string.Empty
                 && x.Cells["FunctionText"].Value.ToString() == string.Empty
                  && x.Index < currentPositionIndex
                 && functionDefinition == x.Cells["FunctionDefinition"].Value.ToString());
                    break;
                case Direction.Down:
                    firstProperlyRow = dataGridView.Rows.Cast<DataGridViewRow>().FirstOrDefault(x =>
                  x.Cells["SymbolicAdress"].Value.ToString() == string.Empty
                 && x.Cells["FunctionText"].Value.ToString() == string.Empty
                 && x.Index > currentPositionIndex
                 && functionDefinition == x.Cells["FunctionDefinition"].Value.ToString());
                    break;
            }

            var properlyRowIndex = firstProperlyRow?.Index;
            return properlyRowIndex;
        }



        public List<PlcDataModelView> ReadDataFromGrid()
        {
            return PlcDataModelView;
        }

        private void Ok_button_Click(object sender, EventArgs e)
        {
            Apply_button.PerformClick();
            Application.Exit();
        }
        private void Cancel_button_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Apply_button_Click(object sender, EventArgs e)
        {
            ApplyEvent?.Invoke(this, new CustomEventArgs(PlcDataModelView));
            //здесь наверное надо считать данные
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            exchange_button.Enabled = false;
            if (SelectedRowsCount == 2)
            {
                exchange_button.Enabled = true;
            }

        }
        private void ManagePlcForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Cancel_button.PerformClick();
            }
            if (e.KeyCode == Keys.Enter)
            {
                Ok_button.PerformClick();
            }
        }
    }
}


