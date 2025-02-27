using Eplan.EplApi.Base;
using Eplan.EplApi.DataModel;
using ST.EplAddin.PlcEdit.Forms;
using ST.EplAddin.PlcEdit.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private readonly IEnumerable<Function> allFunctions;

        public static event EventHandler<CustomEventArgs> ApplyEvent;
        public static event EventHandler<string> PathEvent;
        private List<PlcDataModelView> PlcDataModelView { get; set; }
        private List<int> LastSelectedRows { get; set; } = new List<int>();

        private string cellValue = string.Empty;
        CsvConverter csvConverter;
        IEnumerable<CsvFileDataModelView> allDataInFile;

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
            this.allFunctions = allFunctions;
            AddData(PlcDataModelView);
            PropertiesForm.SettingsChanged += PropertiesForm_SettingsChanged;
            PathEvent?.Invoke(this, pathToSaveTemplate);
            FastInput.Checked = Properties.Settings.Default.FastInputChecked;
            SymbolicAdressToolStripMenuItem.Checked = Properties.Settings.Default.IsRewriteSymbolicAdress;
            PLCAdressToolStripMenuItem.Checked = Properties.Settings.Default.IsRewritePLCAdress;
            deleteOverviewFunctionTextToolStripMenuItem.Checked = Properties.Settings.Default.IsDeleteOverviewFunctionText;
            RewriteFunctionsTextInImport.Checked = Properties.Settings.Default.IsRewritePLCFunctionsTextInImport;
            import_button.Enabled = false;
            export_button.Enabled = false;

        }

        private void ImportForm_ImportCsvDataEvent(object sender, List<CsvFileDataModelView> e)
        {
            ImportedData = e;
        }

        private void ComparingForm_StartRewriting()
        {
            PlcDataModelView.ForEach(x => x.PropertyChanged += ChangeCellColor);
        }
        private void ComparingForm_FinishRewriting()
        {
            PlcDataModelView.ForEach(x => x.PropertyChanged -= ChangeCellColor);
        }

        private void ChangeCellColor(object sender, PropertyChangedEventArgs e)
        {
            var item = sender as PlcDataModelView;
            var indexRow = PlcDataModelView.IndexOf(item);
            if (indexRow == -1) return;
            dataGridView[e.PropertyName, indexRow].Style.BackColor = Color.Yellow;
        }

        private void ComparingForm_OkEvent(object sender, EventArgs e)
        {
            dataGridView.EndEdit();
            dataGridView.Refresh();
            PlcDataModelView.ForEach(x => x.PropertyChanged -= ChangeCellColor);
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
                EplanSettings eplanSettings = new();
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
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    var maxWidth = column.Width;
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    column.Width = maxWidth;
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
                LastSelectedRows = rows;
                dataGridView.Refresh();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "HighlightRows", "Error", MessageBoxButtons.OK);
                Close();
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
                            x.Cells["SymbolicAdressDefined"].Value?.ToString() == string.Empty
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
                                x.Cells["SymbolicAdressDefined"].Value?.ToString() == string.Empty
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
                                x.Cells["SymbolicAdressDefined"].Value?.ToString() == string.Empty
                                && x.Cells["FunctionText"].Value?.ToString() == string.Empty
                                && x.Index < currentPositionIndex
                                && functionDefinition == x.Cells["FunctionDefinition"].Value?.ToString());
                        break;

                    case Direction.Down:
                        properlyRow = dataGridView.Rows.Cast<DataGridViewRow>().FirstOrDefault(x =>
                            x.Cells["SymbolicAdressDefined"].Value?.ToString() == string.Empty
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
            HighlightRows(LastSelectedRows);
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            LastSelectedRows.Clear();
            LastSelectedRows.Add(e.RowIndex);
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
            var exportCsvForm = new ExportCsvForm(dataToExport);
            exportCsvForm.ShowDialog();
        }

        private List<CsvFileDataModelView> GetDataToExport()
        {
            var eplanData = GetRewritingRowsData(PlcDataModelView);
            IEnumerable<CsvFileDataModelView> copiedDataOfInitialFile = allDataInFile.ToList();

            var enumerator = copiedDataOfInitialFile.GetEnumerator();
            bool IsStartFlagFound = false;

            while (IsStartFlagFound == false && enumerator.MoveNext())
            {
                if (enumerator.Current?.BitNumber?.Contains("Дискрет") ?? false)
                {
                    IsStartFlagFound = true;
                }
            }

            int i = 0;
            while (IsStartFlagFound && enumerator.MoveNext())
            {
                enumerator.Current.SymbolicAdress = eplanData[i].SymbolicAdress;
                enumerator.Current.FunctionText = eplanData[i].FunctionText;
                if (i == 31)
                {
                    IsStartFlagFound = false;
                }
                i++;
            }
            return copiedDataOfInitialFile.ToList();
        }
        private void import_button_Click(object sender, EventArgs e)
        {
            ComparingForm_StartRewriting();
            var dataToRewrite = GetRewritingRowsData(PlcDataModelView);
            if (dataToRewrite.Count == ImportedData.Count)
            {
                for (int i = 0; i < ImportedData.Count; i++)
                {
                    dataToRewrite[i].PLCAdress = ImportedData[i].PLCAdress;
                    dataToRewrite[i].SymbolicAdress = ImportedData[i].SymbolicAdress;
                    if (RewriteFunctionsTextInImport.Checked)
                    {
                        dataToRewrite[i].FunctionText = ImportedData[i].FunctionText;
                    }
                }
            }
            else
            {
                MessageBox.Show("Количество записей в загружаемом файле отличается от текущего ПЛК!'\n " +
                                "Пожалуйста, проверьте присвоение шаблонов функций ПЛК!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            dataGridView.Refresh();
            ComparingForm_FinishRewriting();
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

        private void dataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            ComparingForm_StartRewriting();
            if (e.KeyCode == Keys.V && e.Control)
            {
                InsertData();
            }
            ComparingForm_FinishRewriting();
        }
        private void InsertData()
        {
            var dataInClipBoard = Clipboard.GetText();
            string[] rowSplitter = { "\r\n" };
            char[] columnSplitter = { '\t' };

            var dataInClipboard = dataInClipBoard.Split(rowSplitter, StringSplitOptions.None);
            if (dataInClipboard[dataInClipboard.Length - 1] == string.Empty)
            {
                dataInClipboard = dataInClipboard.Take(dataInClipboard.Length - 1).ToArray();
            }
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
            LastSelectedRows = SelectedRows.Select(x => x.Index).ToList();
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
            var res = dataGridView.Columns;
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
                    x.Properties.FUNC_TYPE.ToInt() == 1 &&
                    x.Properties[20183] == overviewPLC.Properties[20183]);

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
        private void deleteOverviewFunctionTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var menuItem = sender as ToolStripMenuItem;
            menuItem.Checked = !menuItem.Checked;
            Properties.Settings.Default.IsDeleteOverviewFunctionText = menuItem.Checked;
        }
        private void dontRewriteFunctionTextsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var menuItem = sender as ToolStripMenuItem;
            menuItem.Checked = !menuItem.Checked;
            Properties.Settings.Default.IsRewritePLCFunctionsTextInImport = menuItem.Checked;
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

            csvConverter = new CsvConverter(path);

            allDataInFile = csvConverter.ReadFile();
            var dataFromFile = GetValidatedPlcData(allDataInFile).ToList();

            if (!dataFromFile.Any()) return;

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
        public IEnumerable<CsvFileDataModelView> GetValidatedPlcData(IEnumerable<CsvFileDataModelView> allDataInFile)
        {
            var enumerator = allDataInFile.GetEnumerator();
            bool IsStartFlagFound = false;

            while (IsStartFlagFound == false && enumerator.MoveNext())
            {
                if (enumerator.Current?.BitNumber?.Contains("Дискрет") ?? false)
                {
                    IsStartFlagFound = true;
                }
            }
            while (IsStartFlagFound && enumerator.MoveNext())
            {
                yield return enumerator.Current;

                if (enumerator.Current.BitNumber.Contains("Bit31"))
                {
                    yield break;
                }
            }
        }
        private void dataGridView_KeyUp(object sender, KeyEventArgs e)
        {
            ComparingForm_StartRewriting();
            switch (e.KeyCode)
            {
                case Keys.Insert:
                    InsertData();
                    break;
                case Keys.Delete:
                    {
                        foreach (var cell in SelectedCells)
                        {
                            if (cell.OwningColumn.ReadOnly == false)
                            {
                                cell.Value = string.Empty;
                            }
                        }
                        break;
                    }
            }
            ComparingForm_FinishRewriting();
        }
    }
}

