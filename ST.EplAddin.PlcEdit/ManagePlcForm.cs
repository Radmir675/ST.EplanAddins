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

        public DataGridViewRow[] SelectedRows
        {
            get
            {
                return dataGridView.SelectedCells.Cast<DataGridViewCell>()
                                       .Select(c => c.OwningRow).Distinct().ToArray();
            }
        }

        public ManagePlcForm(List<PlcDataModelView> plcDataModelView)
        {
            InitializeComponent();
            PlcDataModelView = plcDataModelView;
        }

        private void ManagePlcForm_Load(object sender, EventArgs e)
        {
            exchange_button.Enabled = false;
            AddData(PlcDataModelView);
        }


        private void AddData(List<PlcDataModelView> data)
        {
            dataGridView.DataSource = data;
        }

        private void up_button_Click(object sender, EventArgs e)
        {
            if (SelectedRows.Count() != 1)
                return;
            InsertRowInEmptyPosition(Direction.Up);

        }

        private void dowm_button_Click(object sender, EventArgs e)
        {
            var s = dataGridView.SelectedCells.Cast<DataGridViewCell>()
                                       .Select(c => c.OwningRow).Distinct().ToArray();
            if (SelectedRows.Count() != 1)
                return;
            InsertRowInEmptyPosition(Direction.Down);
        }
        private void exchange_button_Click(object sender, EventArgs e)
        {
            if (SelectedRows.Count() != 2)
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
            DataGridViewRow properlyRow = null;
            switch (direction)
            {
                case Direction.Up:

                    properlyRow = dataGridView.Rows
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
                    properlyRow = dataGridView.Rows.Cast<DataGridViewRow>().FirstOrDefault(x =>
                  x.Cells["SymbolicAdress"].Value.ToString() == string.Empty
                 && x.Cells["FunctionText"].Value.ToString() == string.Empty
                 && x.Index > currentPositionIndex
                 && functionDefinition == x.Cells["FunctionDefinition"].Value.ToString());
                    break;
            }

            var properlyRowIndex = properlyRow?.Index;
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
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            exchange_button.Enabled = false;
            if (SelectedRows.Count() == 2)
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

        internal void Exit()
        {
            CancelButton.PerformClick();
        }

        public void UpdateTable(List<PlcDataModelView> plcDataModelView)
        {
            dataGridView.DataSource = plcDataModelView;
            dataGridView.Update();
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            var ss = dataGridView.RowCount;
            if (SelectedRows.Count() > 1 || SelectedRows.Count() == 0)
            {
                up_button.Enabled = false;
                dowm_button.Enabled = false;
                return;
            }
            var currentRow = SelectedRows.FirstOrDefault();//туть null
            if (currentRow == null)
            {
                return;
            }
            var functionDefinition = currentRow.Cells["FunctionDefinition"].Value.ToString();

            var emptyUpRow = TryGetEmptyIndexRow(currentRow.Index, Direction.Up, functionDefinition);
            if (!emptyUpRow.HasValue || SelectedRows.Count() != 1)
            {
                up_button.Enabled = false;
            }
            else
            {
                up_button.Enabled = true;
            }
            var emptyDownRow = TryGetEmptyIndexRow(currentRow.Index, Direction.Down, functionDefinition);
            if (!emptyDownRow.HasValue || SelectedRows.Count() != 1)
            {
                dowm_button.Enabled = false;
            }
            else
            {
                dowm_button.Enabled = true;
            }
        }
    }
}


