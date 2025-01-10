using Eplan.EplApi.Base;
using Eplan.EplApi.DataModel;
using ST.EplAddin.PlcEdit.Forms;
using ST.EplAddin.PlcEdit.Helpers;
using ST.EplAddin.PlcEdit.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Application = System.Windows.Forms.Application;
using MessageBox = System.Windows.Forms.MessageBox;

namespace ST.EplAddin.PlcEdit
{
    public partial class ManagePlcForm : Form
    {
        private List<CsvFileDataModelView> ImportedData = new();
        private bool IsFileUploaded = false;
        private readonly string pathToSaveTemplate;
        private readonly IEnumerable<Function> allFunctions;

        public static event EventHandler<CustomEventArgs> ApplyEvent;
        public static event EventHandler<string> PathEvent;
        public static event EventHandler<IEnumerable<StorableObject>> ShowSearch;
        private List<PlcDataModelView> PlcDataModelView { get; set; }
        private List<int> LastSelectedRow { get; set; } = new List<int>();
        public List<Template> Templates { get; set; }

        private string cellValue = string.Empty;

        public DataGridViewRow[] SelectedRows
        {
            get
            {
                return dataGridView.SelectedCells.Cast<DataGridViewCell>()
                    .Select(c => c.OwningRow).Distinct().ToArray();
            }
        }

        public DataGridViewCell[] SelectedCells
        {
            get { return dataGridView.SelectedCells.Cast<DataGridViewCell>().OrderBy(x => x.RowIndex).ToArray(); }
        }

        public ManagePlcForm(List<PlcDataModelView> plcDataModelView, string pathToSaveTemplate,
            IEnumerable<Function> allFunctions)
        {
            InitializeComponent();
            PlcDataModelView = plcDataModelView;
            this.pathToSaveTemplate = pathToSaveTemplate;
            this.allFunctions = allFunctions;
            AddData(PlcDataModelView);
            PropertiesForm.SettingsChanged += PropertiesForm_SettingsChanged;




            ComparingForm.StartRewriting += ComparingForm_StartRewriting; //TODO:тут надо посмотреть
            TemplatesData.GetInstance();
            PathEvent?.Invoke(this, pathToSaveTemplate);
            Templates = TemplatesData.GetInstance().GetTemplates();
            FastInput.Checked = Properties.Settings.Default.FastInputChecked;
            SymbolicAdressToolStripMenuItem.Checked = Properties.Settings.Default.IsRewriteSymbolicAdress;
            PLCAdressToolStripMenuItem.Checked = Properties.Settings.Default.IsRewritePLCAdress;
            import_button.Enabled = false;
            export_button.Enabled = false;
        }

        private void ImportForm_ImportCsvDataEvent(object sender, List<CsvFileDataModelView> e)
        {

            ImportedData = e;
        }

        private void ComparingForm_StartRewriting(object sender, EventArgs e)
        {
            PlcDataModelView.ForEach(x => x.PropertyChanged += ChangeCellColor);
        }

        private void ChangeCellColor(object sender, PropertyChangedEventArgs e)
        {
            var item = sender as PlcDataModelView;
            var indexRow = PlcDataModelView.IndexOf(item);
            //var indexColumn = dataGridView.Columns[e.PropertyName]?.Index;
            if (indexRow == -1) return;
            dataGridView[e.PropertyName, indexRow].Style.BackColor = Color.Yellow;
        }

        private void ComparingForm_OkEvent(object sender, EventArgs e)
        {
            dataGridView.EndEdit();
            dataGridView.Refresh();
            PlcDataModelView.ForEach(x => x.PropertyChanged -= ChangeCellColor);
        }

        private void ImportCsvFormImportCsvDataEvent(object sender, List<CsvFileDataModelView> e)
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

            ChangeColorDisableColumns();
            GetDefaultColumnSetting();
            UpdateButtonsState();
            Size = Properties.Settings.Default.FormSize;
            var initialPointLocation = Properties.Settings.Default?.FormLocation;
            if (initialPointLocation.Value.X != 0 && initialPointLocation.Value.Y != 0)
            {
                Location = initialPointLocation.Value;
            }

            ChangeButtonsView();
        }

        private void ChangeButtonsView()
        {
            if (!IsFileUploaded) return;
            import_button.Enabled = true;
            export_button.Enabled = true;
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
            }
        }

        private void ChangeColorDisableColumns()
        {
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                if (column.ReadOnly == true)
                {
                    column.DefaultCellStyle = null;
                    column.DefaultCellStyle.BackColor = Color.FromArgb(169, 169, 169);
                }
                else if (column.ReadOnly == false)
                {
                    column.DefaultCellStyle = null;
                    column.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                }
            }
        }

        private void AddData(List<PlcDataModelView> data)
        {
            dataGridView.DataSource = data;
        }

        private void up_button_Click(object sender, EventArgs e)
        {
            if (SelectedRows.Any())
            {
                InsertRowsToEmptyPositions(Direction.Up, 2);
                dataGridView_MouseUp(this, null);
            }
        }

        private void dowm_button_Click(object sender, EventArgs e)
        {
            if (SelectedRows.Any())
            {
                InsertRowsToEmptyPositions(Direction.Down, 3);
                dataGridView_MouseUp(this, null);
            }
        }

        private void exchange_button_Click(object sender, EventArgs e)
        {
            if (SelectedRows.Count() == 2)
            {
                ExchangePositions();
                dataGridView_MouseUp(this, null);
            }
        }

        private void upper_button_Click(object sender, EventArgs e)
        {
            if (SelectedRows.Any())
            {
                InsertRowsToEmptyPositions(Direction.Up, 2, jumpThroughAll: true);
                dataGridView_MouseUp(this, null);
            }
        }

        private void lower_button_Click(object sender, EventArgs e)
        {

            if (SelectedRows.Any())
            {
                InsertRowsToEmptyPositions(Direction.Down, 2, jumpThroughAll: true);
                dataGridView_MouseUp(this, null);
            }
        }

        private void HighlightRows(List<int> rows)
        {
            dataGridView.ClearSelection();
            try
            {
                rows.ForEach(rowIndex => dataGridView.Rows[rowIndex].Selected = true);
                LastSelectedRow = rows;
                dataGridView.Refresh();

            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }
        }

        private void InsertRowsToEmptyPositions(Direction direction, int selectedRows, bool jumpThroughAll = false)
        {
            // var currentIndexRows = dataGridView.SelectedCells.Cast<DataGridViewCell>().Select(x => x.RowIndex).ToList();
            var currentIndexRows = SelectedRows.Select(x => x.Index).ToList();
            switch (direction)
            {
                case Direction.Up:
                    currentIndexRows = currentIndexRows.OrderBy(x => x).ToList();
                    break;
                case Direction.Down:
                    currentIndexRows = currentIndexRows.OrderByDescending(x => x).ToList();
                    break;
            }

            List<int> rows = new();
            foreach (var currentIndexRow in currentIndexRows)
            {
                var currentRow = dataGridView.Rows[currentIndexRow];
                var functionDefinition = currentRow.Cells["FunctionDefinition"].Value.ToString();
                var targetIndexRow =
                    TryGetEmptyIndexRow(currentRow.Index, direction, functionDefinition,
                        jumpThroughAll); // задать смещение 
                if (targetIndexRow == null)
                {
                    return;
                }

                AssignDataToTargetRow(currentIndexRow, targetIndexRow.Value);
                rows.Add(targetIndexRow.Value);
            }

            HighlightRows(rows);
        }

        private List<DataGridViewRow> GetRowsWithIndex(DataGridViewRowCollection rows, List<int> currentIndexRows)
        {
            List<DataGridViewRow> result = new();
            foreach (DataGridViewRow row in rows)
            {
                if (currentIndexRows.Contains(row.Index))
                {
                    result.Add(row);
                }
            }

            return result;
        }

        private void ExchangePositions()
        {
            var rowsIndex = dataGridView.SelectedCells.Cast<DataGridViewCell>().Select(c => c.RowIndex).Distinct()
                .ToArray();
            var currentIndexRow = rowsIndex[0];
            var targetIndexRow = rowsIndex[1];
            if (currentIndexRow != targetIndexRow)
            {
                AssignDataToTargetRow(currentIndexRow, targetIndexRow);
                HighlightRows(rowsIndex.ToList());
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

        private int? TryGetEmptyIndexRow(int currentPositionIndex, Direction direction, string functionDefinition,
            bool jumpThroughAll = false)
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
            HighlightRows(LastSelectedRow);
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            LastSelectedRow.Clear();
            LastSelectedRow.Add(e.RowIndex);
            if (FastInput.Checked)
            {
                dataGridView.BeginEdit(false);
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

        private bool IsEqualDefinitions(DataGridViewRow firstSelectedRow, DataGridViewRow secondSelectedRow)
        {
            var firstFunctionDefinition = firstSelectedRow?.Cells["FunctionDefinition"]?.Value?.ToString();
            var secondFunctionDefinition = secondSelectedRow?.Cells["FunctionDefinition"]?.Value?.ToString();
            return firstFunctionDefinition == secondFunctionDefinition;
        }

        private bool IsEqualDefinitions(List<DataGridViewRow> rows)
        {
            return rows.TrueForAll(z => z?.Cells["FunctionDefinition"]?.Value?.ToString()
                                        == rows.FirstOrDefault()?.Cells["FunctionDefinition"]?.Value?.ToString());
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
            var dataToExport = GetDataToExport();
            var exportCsvForm = new ExportCsvForm(dataToExport.ToList());
            exportCsvForm.ShowDialog();
            if (exportCsvForm.DialogResult == DialogResult.OK)
            {
                MessageBox.Show("Данные успешно записаны!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private CsvFileDataModelView[] GetDataToExport()
        {
            var eplanData = GetRewritingRowsData(PlcDataModelView);
            CsvFileDataModelView[] array = new CsvFileDataModelView[eplanData.Count];
            if (eplanData.Count == ImportedData.Count)
            {
                ImportedData.CopyTo(array);
                for (var i = 0; i < array.Length; i++)
                {
                    array[i].SymbolicAdress = eplanData[i].SymbolicAdress;
                    array[i].FunctionText = eplanData[i].FunctionText;
                }
            }
            return array;
        }

        private void import_button_Click(object sender, EventArgs e)
        {
            //TODO: импортировать данные нужно 
            var dataToImport = GetRewritingRowsData(PlcDataModelView);
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

            var properlyDataInDataGrid = GetRewritingRowsData(PlcDataModelView);

            if (csvPlcData.Count() != properlyDataInDataGrid.Count()) //тут должно получиться 32 штуки
            {
                MessageBox.Show("Не найдено взаимооднозначное соответствие данных для импорта");
                return;
            }

            for (int i = 0; i < properlyDataInDataGrid.Count(); i++)
            {
                properlyDataInDataGrid[i].FunctionText =
                    csvPlcData[i].FunctionText; //тут надо написать перезаписать datagrid и все!
                properlyDataInDataGrid[i].SymbolicAdress = csvPlcData[i].SymbolicAdress;
                properlyDataInDataGrid[i].PLCAdress = csvPlcData[i].PLCAdress;
            }

            dataGridView.Refresh();
        }

        private List<PlcDataModelView> GetRewritingRowsData(List<PlcDataModelView> plcDataModelView)
        {
            List<PlcDataModelView> result = new();
            foreach (var item in plcDataModelView)
            {
                var splittedFunctionDifinition =
                    item?.FunctionDefinition?.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                if (splittedFunctionDifinition == null) //а вот тут надо подумать
                {
                    result.Add(item);
                    continue;
                }

                if (splittedFunctionDifinition.Contains("Источник") || splittedFunctionDifinition.Contains("Питание"))
                {
                    continue;
                }
                result.Add(item);
            }
            return result;
        }



        private void loadTemplate_button_Click(object sender, EventArgs e)
        {
            var pathFromRead = PathDialog.TryGetReadPath();
            if (pathFromRead == null) return;
            var fileName = PathDialog.TryGetFileName(pathFromRead);
            LoadTemplateForm loadTemplateForm = new LoadTemplateForm(pathFromRead, pathToSaveTemplate);
            loadTemplateForm.ShowDialog();
        }

        private void dataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.V && e.Control)
            {
                InsertData();
            }

            if (e.KeyCode == Keys.Delete)
            {
                foreach (var cell in SelectedCells)
                {
                    if (cell.OwningColumn.ReadOnly == false)
                    {
                        cell.Value = string.Empty;
                    }
                }

            }
        }

        private void InsertData()
        {
            var dataInClipBoard = Clipboard.GetText();
            string[] rowSplitter = { "\r\n" };
            char[] columnSplitter = { '\t' };

            var dataInClipboard = dataInClipBoard.Split(rowSplitter, StringSplitOptions.None);
            int firstSelectedRowIndex = dataGridView.SelectedCells[0].RowIndex;
            int firstSelectedColumnIndex = dataGridView.SelectedCells[0].ColumnIndex;

            var selectedCellsCount = dataGridView.SelectedCells.Count;

            if (selectedCellsCount == 1)
            {
                int j = 0;
                for (int i = firstSelectedRowIndex; i < dataInClipboard.Count() + firstSelectedRowIndex; i++)
                {
                    if (i < dataGridView.Rows.Count)
                    {
                        if (dataGridView[firstSelectedColumnIndex, i].ReadOnly == false)
                        {
                            dataGridView[firstSelectedColumnIndex, i].Value = dataInClipboard[j];
                        }

                        j++;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            else
            {
                if (dataInClipboard.Count() == 1)
                {
                    foreach (DataGridViewCell item in dataGridView.SelectedCells)
                    {
                        if (item.OwningColumn.ReadOnly == false)
                        {
                            item.Value = dataInClipBoard;
                        }
                    }
                }

                else if (dataInClipboard.Count() == SelectedCells.Count()
                         && SelectedCells.All(x => x.ColumnIndex == firstSelectedColumnIndex))
                {
                    int j = 0;

                    foreach (var item in SelectedCells)
                    {
                        if (item.OwningColumn.ReadOnly == false)
                        {
                            item.Value = dataInClipboard[j];
                            j++;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Индекс массива вышел за пределы диапазона", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
        }


        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            Properties.Settings.Default.FastInputChecked = FastInput.Checked;
        }

        private void dataGridView_MouseUp(object sender, MouseEventArgs e)
        {
            UpdateButtonsState();
            LastSelectedRow = SelectedRows.Select(x => x.Index).ToList();
        }

        private void UpdateButtonsState()
        {
            up_button.Enabled = false;
            dowm_button.Enabled = false;
            upper_button.Enabled = false;
            lower_button.Enabled = false;
            exchange_button.Enabled = false;

            var firstSelectedRow = SelectedRows.FirstOrDefault();
            var secondSelectedRow = SelectedRows.LastOrDefault();

            var functionDefinition =
                firstSelectedRow?.Cells["FunctionDefinition"]?.Value?.ToString() ?? "Не определено";

            if (firstSelectedRow != null)
            {
                var emptyUpRow = TryGetEmptyIndexRow(firstSelectedRow.Index, Direction.Up, functionDefinition);
                var emptyDownRow = TryGetEmptyIndexRow(firstSelectedRow.Index, Direction.Down, functionDefinition);

                if (SelectedRows.Count() == 2 && IsEqualDefinitions(firstSelectedRow, secondSelectedRow))
                {
                    exchange_button.Enabled = true;
                }

                if (emptyUpRow.HasValue && IsEqualDefinitions(SelectedRows.ToList()))
                {
                    up_button.Enabled = true;
                    upper_button.Enabled = true;
                }

                if (emptyDownRow.HasValue && IsEqualDefinitions(SelectedRows.ToList()))
                {
                    dowm_button.Enabled = true;
                    lower_button.Enabled = true;
                }
            }
        }

        private void reviewPLC_button_Click(object sender, EventArgs e)
        {
            Logger logger = new();
            var overviewPLCs = allFunctions?.Where(x => x.Properties.FUNC_TYPE == 3);
            var result = new List<Function>();
            foreach (var overviewPLC in overviewPLCs)
            {
                var multyLinePLC = allFunctions.FirstOrDefault(x =>
                    x.Properties.FUNC_FULLNAME == overviewPLC.Properties.FUNC_FULLNAME &&
                    x.Properties.FUNC_TYPE.ToInt() == 1);

                var functiomText = overviewPLC.Properties.FUNC_TEXT.GetDisplayString()
                    .GetStringToDisplay(ISOCode.Language.L_ru_RU);
                var IsFunctionTextEmpty = string.IsNullOrEmpty(functiomText);
                if (!IsFunctionTextEmpty)
                {
                    var text = $"Данная функция {overviewPLC.Name} содержит функциональный текст на обзоре ПЛК";
                    logger.Log(text);
                }

                var symbolicAdress =
                    overviewPLC.Properties.FUNC_PLCSYMBOLICADDRESS_MANUAL.ToString(ISOCode.Language.L_ru_RU);
                var IsSymbolicAdressEmpty = string.IsNullOrEmpty(symbolicAdress);
                //if (!IsSymbolicAdressEmpty)
                //{
                //    var text = $"Данная функция {overviewPLC.Name} содержит символический адрес на обзоре ПЛК";
                //    logger.Log(text);
                //}

                bool IsAdressEqualInDifferentRepresentations;
                if (multyLinePLC != null)
                {
                    IsAdressEqualInDifferentRepresentations =
                        multyLinePLC.Properties.FUNC_PLCADDRESS.ToString(ISOCode.Language.L_ru_RU) ==
                        overviewPLC.Properties.FUNC_PLCADDRESS.ToString(ISOCode.Language.L_ru_RU);
                }
                else
                {
                    IsAdressEqualInDifferentRepresentations = true;
                }

                if (!IsAdressEqualInDifferentRepresentations)
                {
                    var text = $"Адрес на функции {overviewPLC.Name} отличается на разных представлениях";
                    logger.Log(text);
                }
                //    if (!IsFunctionTextEmpty || !IsSymbolicAdressEmpty || !IsAdressEqualInDifferentRepresentations)
                //    {
                //        result.Add(overviewPLC);
                //    }
                //}
                //MessageBox.Show(@"Результаты проверки отображены во вкладке ""Поиск"" ");
                //ShowSearch?.Invoke(this, result.Cast<StorableObject>());
            }

            var loggerResult = logger.GetMessages();
            if (loggerResult.Any())
            {
                LoggerForm loggerForm = new LoggerForm(loggerResult);
                loggerForm.ShowDialog();
                // loggerForm.FormClosed += (sender, e) =>;

            }
        }

        private void ManagePlcForm_Shown(object sender, EventArgs e)
        {
            reviewPLC_button_Click(this, new EventArgs());
        }

        private void rewriteOverviewAdressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var menuItem = sender as ToolStripMenuItem;
            menuItem.Checked = !menuItem.Checked;
            Properties.Settings.Default.IsRewritePLCAdress = menuItem.Checked;
        }

        private void rewriteOwerviewSymbolicAdressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var menuItem = sender as ToolStripMenuItem;
            menuItem.Checked = !menuItem.Checked;
            Properties.Settings.Default.IsRewriteSymbolicAdress = menuItem.Checked;
        }

        private void ManagePlcForm_ResizeEnd(object sender, EventArgs e)
        {
            Properties.Settings.Default.FormSize = Size;
        }

        private void ManagePlcForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.FormLocation = this.Location;
        }

        private string GetValue(DataGridViewCellEventArgs e)
        {
            return dataGridView[e.ColumnIndex, e.RowIndex]?.Value?.ToString() ?? string.Empty;
        }

        private string GetValue(DataGridViewCellCancelEventArgs e)
        {
            return dataGridView[e.ColumnIndex, e.RowIndex]?.Value?.ToString() ?? string.Empty;
        }


        private void dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e == null || e.RowIndex == -1) return;

            var currentCellValue = GetValue(e);
            if (currentCellValue != cellValue)
            {
                dataGridView[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.Yellow;
            }
        }

        private void dataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            cellValue = GetValue(e);
        }

        private void sourceFile_button_Click(object sender, EventArgs e)
        {
            var path = PathDialog.TryGetReadPath();
            if (path == null) return;
            CsvConverter csvConverter = new CsvConverter(path);
            var dataFromFile = csvConverter.ReadFile();
            if (!dataFromFile.Any()) return;

            //var CsvFileDataModelViews = Mapper.ConvertDataToCsvCompare(dataFromFile);

            if (!(PlcDataModelView[0].DeviceNameShort ??= string.Empty).Equals(
                    dataFromFile[0].DeviceNameShort ??= string.Empty))
            {
                MessageBox.Show("Выбран неверный модуль для импорта! Пожалуйста проверьте корректность CSV файла.",
                    "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            var importForm = new ImportCsvForm(dataFromFile);
            importForm.ImportCsvDataEvent += ImportForm_ImportCsvDataEvent;
            importForm.ShowDialog();
            if (importForm.DialogResult == DialogResult.OK)
            {
                if (ImportedData.Any())
                {
                    MessageBox.Show("Данные успешно загружены!");

                    IsFileUploaded = true;
                    ChangeButtonsView();
                }
            }
            importForm.ImportCsvDataEvent -= ImportForm_ImportCsvDataEvent;
        }
    }
}

