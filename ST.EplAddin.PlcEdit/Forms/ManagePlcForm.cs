using ST.EplAddin.PlcEdit.Forms;
using ST.EplAddin.PlcEdit.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Application = System.Windows.Forms.Application;
using MessageBox = System.Windows.Forms.MessageBox;

namespace ST.EplAddin.PlcEdit
{
    public partial class ManagePlcForm : Form
    {
        private readonly string pathToSaveTemplate;

        public static event EventHandler<CustomEventArgs> ApplyEvent;
        private List<PlcDataModelView> PlcDataModelView { get; set; }
        public int InitialFormWidth { get; set; }
        private int LastSelectedRow { get; set; }
        public string TemplateName { get; set; }

        public DataGridViewRow[] SelectedRows
        {
            get
            {
                return dataGridView.SelectedCells.Cast<DataGridViewCell>()
                                       .Select(c => c.OwningRow).Distinct().ToArray();
            }
        }

        public ManagePlcForm(List<PlcDataModelView> plcDataModelView, string pathToSaveTemplate)
        {
            InitializeComponent();
            PlcDataModelView = plcDataModelView;
            this.pathToSaveTemplate = pathToSaveTemplate;
            AddData(PlcDataModelView);
            PropertiesForm.SettingsChanged += PropertiesForm_SettingsChanged;
            ImportCsvForm.ImportCsvData += ImportCsvForm_ImportCsvData;
            LoadTemplateForm.TemplateAction += LoadTemplateForm_TemplateAction;
            ComparingForm.OkEvent += ComparingForm_OkEvent;
            TryDowmLoadTemplates(pathToSaveTemplate);
            this.dataGridView.Columns["FunctionText"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }
        public ManagePlcForm(List<PlcDataModelView> plcDataModelView)
        {
            InitializeComponent();
            PlcDataModelView = plcDataModelView;
            AddData(PlcDataModelView);
        }
        private void ComparingForm_OkEvent(object sender, EventArgs e)
        {
            dataGridView.Refresh();
        }

        private void ImportCsvForm_ImportCsvData(object sender, List<CsvFileDataModelView> e)
        {
            var convertedData = Mapper.ConvertDataFromCsvModel(e);
            UpdateDataTable(convertedData);
        }

        public int GetCurrentColumnsHeaderWidth()
        {
            double sum = 0;
            foreach (DataGridViewColumn item in dataGridView.Columns)
            {
                sum += item.Width;
            }
            return (int)Math.Round(sum);
        }
        private void ManagePlcForm_Load(object sender, EventArgs e)
        {
            exchange_button.Enabled = false;
            if (dataGridView.Rows.Count > 0)
            {
                dataGridView.Rows[0].Selected = true;
            }
            (sender as Form).Width = GetCurrentColumnsHeaderWidth() + 58;
            InitialFormWidth = GetCurrentColumnsHeaderWidth() + 58;
            ChangeColorDisableColumns();
            GetDefaultColumnSetting();
        }

        private void PropertiesForm_SettingsChanged(object sender, List<string> columnsToView)
        {
            SetVisibleColumns(columnsToView);
        }

        private void SetVisibleColumns(List<string> columnsToView)
        {
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                if (columnsToView.Contains(column.Name))
                {
                    dataGridView.Columns[column.Name].Visible = true;
                }
                else
                {
                    dataGridView.Columns[column.Name].Visible = false;
                }
            }
        }

        private void GetDefaultColumnSetting()
        {
            try
            {
                EplanSettings eplanSettings = new EplanSettings();
                var columnsToView = eplanSettings.TryGetSelectedColumns();
                if (columnsToView.Any())
                {
                    SetVisibleColumns(columnsToView);
                }
            }
            catch (Exception)
            {
                //не найдена зависиосить на сборку                
            }
        }

        private void ChangeColorDisableColumns()
        {
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                if (column.ReadOnly == true)
                {
                    dataGridView.Columns[$"{column.Name}"].DefaultCellStyle.BackColor = Color.FromArgb(169, 169, 169);
                }
            }
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
            LastSelectedRow = rowIndex;
            dataGridView.Refresh();
        }
        private void HighlightRow(int rowIndex1, int rowIndex2)
        {
            dataGridView.ClearSelection();
            dataGridView.Rows[rowIndex1].Selected = true;
            dataGridView.Rows[rowIndex2].Selected = true;

            dataGridView.Refresh();
        }
        private void InsertRowInEmptyPosition(Direction direction, bool jumpThroughAll = false)
        {
            var currentIndexRow = dataGridView.SelectedCells.Cast<DataGridViewCell>().First().RowIndex;
            var currentRow = dataGridView.Rows[currentIndexRow];
            var functionDefinition = currentRow.Cells["FunctionDefinition"].Value.ToString();
            var targetIndexRow = TryGetEmptyIndexRow(currentRow.Index, direction, functionDefinition, jumpThroughAll);// задать смещение 
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
            targetObject.TerminalId = string.Copy(sourceObject?.TerminalId ?? String.Empty);

            sourceObject.SymbolicAdressDefined = string.Copy(targetObjectClone?.SymbolicAdressDefined ?? String.Empty);
            sourceObject.FunctionText = string.Copy(targetObjectClone?.FunctionText ?? String.Empty);
            sourceObject.SymbolicAdress = string.Copy(targetObjectClone?.SymbolicAdress ?? String.Empty);
            sourceObject.Datatype = string.Copy(targetObjectClone?.Datatype ?? String.Empty);
            sourceObject.PLCAdress = string.Copy(targetObjectClone?.PLCAdress ?? String.Empty);
            sourceObject.TerminalId = string.Copy(targetObjectClone?.TerminalId ?? String.Empty);
        }

        public bool IsModuleAssigned(string terminalName)
        {
            if (PlcDataModelView.Single(x => x.DT == terminalName).SymbolicAdressDefined == string.Empty)
            {
                return true;
            }
            return false;
        }
        private int? TryGetEmptyIndexRow(int currentPositionIndex, Direction direction, string functionDefinition, bool jumpThroughAll = false)
        {
            DataGridViewRow properlyRow = null;
            if (jumpThroughAll == true)
            {
                switch (direction)
                {
                    case Direction.Up:
                        properlyRow = dataGridView.Rows.Cast<DataGridViewRow>().FirstOrDefault(x =>
                      x.Cells["SymbolicAdress"].Value?.ToString() == string.Empty
                     && x.Cells["FunctionText"].Value?.ToString() == string.Empty
                     && x.Index < currentPositionIndex
                     && functionDefinition == x.Cells["FunctionDefinition"].Value?.ToString());
                        break;

                    case Direction.Down:
                        properlyRow = dataGridView.Rows
                            .Cast<DataGridViewRow>()
                            .Where(z => z.Index > currentPositionIndex)
                            .Reverse()
                            .FirstOrDefault(x =>
                      x.Cells["SymbolicAdress"].Value?.ToString() == string.Empty
                     && x.Cells["FunctionText"].Value?.ToString() == string.Empty
                      && x.Index > currentPositionIndex
                     && functionDefinition == x.Cells["FunctionDefinition"].Value?.ToString());
                        break;
                }
            }
            else
            {
                switch (direction)
                {
                    case Direction.Up:
                        properlyRow = dataGridView.Rows
                            .Cast<DataGridViewRow>()
                            .Where(z => z.Index < currentPositionIndex)
                            .Reverse()
                            .FirstOrDefault(x =>
                      x.Cells["SymbolicAdress"].Value?.ToString() == string.Empty
                     && x.Cells["FunctionText"].Value?.ToString() == string.Empty
                      && x.Index < currentPositionIndex
                     && functionDefinition == x.Cells["FunctionDefinition"].Value?.ToString());
                        break;

                    case Direction.Down:
                        properlyRow = dataGridView.Rows.Cast<DataGridViewRow>().FirstOrDefault(x =>
                      x.Cells["SymbolicAdress"].Value?.ToString() == string.Empty
                     && x.Cells["FunctionText"].Value?.ToString() == string.Empty
                     && x.Index > currentPositionIndex
                     && functionDefinition == x.Cells["FunctionDefinition"].Value?.ToString());
                        break;
                }
            }
            var properlyRowIndex = properlyRow?.Index;
            return properlyRowIndex;
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
            HighlightRow(LastSelectedRow);
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            exchange_button.Enabled = false;
            if (SelectedRows.Count() == 2)
            {
                exchange_button.Enabled = true;
            }
            LastSelectedRow = e.RowIndex;
            if (true)
            {
                // dataGridView.BeginEdit(false);

            }
            //  dataGridView.DefaultCellStyle.SelectionBackColor = Color.Transparent;


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
            if (e.KeyCode == Keys.S && e.Control)
            {
                Apply_button.PerformClick();
            }
        }

        internal void Exit()
        {
            CancelButton?.PerformClick();
        }

        public void UpdateTable(List<PlcDataModelView> plcDataModelView)
        {
            PlcDataModelView = plcDataModelView;
            dataGridView.DataSource = plcDataModelView;
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (SelectedRows.Count() > 1 || SelectedRows.Count() == 0)
            {
                up_button.Enabled = false;
                dowm_button.Enabled = false;
                upper_button.Enabled = false;
                lower_button.Enabled = false;
                return;
            }
            var currentRow = SelectedRows.FirstOrDefault();//туть null
            if (currentRow == null)
            {
                return;
            }
            var functionDefinition = currentRow?.Cells["FunctionDefinition"]?.Value?.ToString();

            var emptyUpRow = TryGetEmptyIndexRow(currentRow.Index, Direction.Up, functionDefinition);
            if (!emptyUpRow.HasValue || SelectedRows.Count() != 1)
            {
                up_button.Enabled = false;
                upper_button.Enabled = false;
            }
            else
            {
                up_button.Enabled = true;
                upper_button.Enabled = true;
            }
            var emptyDownRow = TryGetEmptyIndexRow(currentRow.Index, Direction.Down, functionDefinition);
            if (!emptyDownRow.HasValue || SelectedRows.Count() != 1)
            {
                dowm_button.Enabled = false;
                lower_button.Enabled = false;
            }
            else
            {
                dowm_button.Enabled = true;
                lower_button.Enabled = true;
            }
        }

        private void properties_button_Click(object sender, EventArgs e)
        {
            var columnsName = new List<string>();
            var visibleColumnsName = new List<string>();
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                columnsName.Add(column.Name);
                if (column.Visible)
                {
                    visibleColumnsName.Add(column.Name);
                }
            }
            PropertiesForm propertiesForm = new PropertiesForm(columnsName, visibleColumnsName);
            propertiesForm.ShowDialog();
        }

        private void export_button_Click(object sender, EventArgs e)
        {
            if (TemplateName == null)
            {
                MessageBox.Show("Please select template", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            ComparingForm comparingForm = new ComparingForm(PlcDataModelView);
            comparingForm.ShowDialog();
            var dataToExport = GetProperlyRowsToImportData(PlcDataModelView);
            //ExportCsvForm exportExportCsvForm = new ExportCsvForm(dataToExport, TemplateName);
            //exportExportCsvForm.ShowDialog();
        }

        private void import_button_Click(object sender, EventArgs e)
        {
            ComparingForm comparingForm = new ComparingForm(PlcDataModelView);
            comparingForm.ShowDialog();
            //ImportCsvForm importExportCsvForm = new ImportCsvForm(TemplateName);
            //importExportCsvForm.ShowDialog();
        }

        private void UpdateDataTable(List<FromCsvModelView> csvPlcData)
        {
            if (csvPlcData == null)
            {
                throw new NullReferenceException("Нет данных для обновления");

            }
            if (csvPlcData.First().DeviceNameShort != PlcDataModelView.First().DeviceNameShort)
            {
                MessageBox.Show("Выбран неверный модуль для импорта");
                return;
            }
            var properlyDataInDataGrid = GetProperlyRowsToImportData(PlcDataModelView);

            if (csvPlcData.Count() != properlyDataInDataGrid.Count())//тут должно получиться 32 штуки
            {
                MessageBox.Show("Не найдено взаимооднозначное соответствие данных для импорта");
                return;
            }
            for (int i = 0; i < properlyDataInDataGrid.Count(); i++)
            {
                properlyDataInDataGrid[i].FunctionText = csvPlcData[i].FunctionText;  //тут надо написать перезаписать datagrid и все!
                properlyDataInDataGrid[i].SymbolicAdress = csvPlcData[i].SymbolicAdress;
                properlyDataInDataGrid[i].PLCAdress = csvPlcData[i].PLCAdress;
            }
            dataGridView.Refresh();
        }

        private List<PlcDataModelView> GetProperlyRowsToImportData(List<PlcDataModelView> plcDataModelView)
        {
            List<PlcDataModelView> result = new();
            foreach (var item in plcDataModelView)
            {
                var splittedFunctionDifinition = item?.FunctionDefinition?.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                if (splittedFunctionDifinition == null)//а вот тут надо подумать
                {
                    result.Add(item);
                    continue;
                }
                if (splittedFunctionDifinition.Contains("Источник") || splittedFunctionDifinition.Contains("Питание"))
                {
                    continue;
                }
                else
                {
                    result.Add(item);
                }
            }
            return result;
        }

        private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e == null || e.RowIndex == -1)
            {
                return;
            }
            dataGridView[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.Yellow;
        }

        private void dropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            TemplateName = dropDownList.SelectedText;
        }

        private void loadTemplate_button_Click(object sender, EventArgs e)
        {
            var pathFromRead = PathDialog.TryGetReadPath();
            if (pathFromRead == null) return;
            var fileName = PathDialog.TryGetFileName(pathFromRead);
            LoadTemplateForm loadTemplateForm = new LoadTemplateForm(pathFromRead, pathToSaveTemplate);
            loadTemplateForm.ShowDialog();
        }
        private void LoadTemplateForm_TemplateAction(object sender, Model.TemplateMembers e)
        {
            dropDownList.Items.Add(e.FileName);
            dropDownList.SelectedText = e.FileName;
        }
        private void TryDowmLoadTemplates(string pathToSaveTemplate)
        {
            var fileNames = Directory.GetFiles(pathToSaveTemplate).ToList().Select(x => Path.GetFileNameWithoutExtension(x));

            foreach (var filename in fileNames)
            {
                if (!dropDownList.Items.Contains(filename))
                    dropDownList.Items.Add(filename);
            }
        }

        private void upper_button_Click(object sender, EventArgs e)
        {
            if (SelectedRows.Count() != 1)
                return;
            InsertRowInEmptyPosition(Direction.Up, jumpThroughAll: true);
        }

        private void lower_button_Click(object sender, EventArgs e)
        {
            if (SelectedRows.Count() != 1)
                return;
            InsertRowInEmptyPosition(Direction.Down, jumpThroughAll: true);
        }

        private void dataGridView_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.V && e.Control)
                foreach (DataGridViewCell item in dataGridView.SelectedCells)
                {
                    item.Value = Clipboard.GetText();
                }
        }
    }
}


