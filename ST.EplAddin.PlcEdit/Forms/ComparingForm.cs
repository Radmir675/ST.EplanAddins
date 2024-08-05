using ST.EplAddin.PlcEdit.Helpers;
using ST.EplAddin.PlcEdit.Model;
using ST.EplAddin.PlcEdit.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ST.EplAddin.PlcEdit.Forms
{
    public partial class ComparingForm : Form
    {
        public ComparisonState ComparisonSelection { get; set; }
        public List<PlcDataModelView> PlcDataModelView { get; }
        public string TemplateName { get; }
        public Import_Export_Type SelectedMode { get; }
        public List<CsvFileDataModelViews> CsvFileDataModelViews { get; set; }
        public static event EventHandler OkEvent;

        /// <summary>
        /// Использовать для импорта и экспорта (при экспорте csvFileDataModelViews может быть null)
        /// </summary>
        /// <param name="plcDataModelView">Данные из Eplan(ManagePlcForm)</param>
        /// <param name="csvFileDataModelViews">Данные из импортируемого файла</param>
        public ComparingForm(List<PlcDataModelView> plcDataModelView, string TemplateName, Import_Export_Type type, List<CsvFileDataModelViews> csvFileDataModelViews = null)
        {
            InitializeComponent();
            PlcDataModelView = plcDataModelView;
            this.TemplateName = TemplateName;
            SelectedMode = type;
            CsvFileDataModelViews = csvFileDataModelViews;
            sourceDataGridView.DataSource = PlcDataModelView;
            sourceDataGridView.Columns["DataType"].Visible = false;
            sourceDataGridView.Columns["DT"].Visible = false;
            sourceDataGridView.Columns["FunctionDefinition"].Visible = false;
            sourceDataGridView.Columns["SymbolicAdressDefined"].Visible = false;
            sourceDataGridView.Columns["FunctionType"].Visible = false;
            sourceDataGridView.Columns["TerminalId"].Visible = false;
            sourceDataGridView.Columns["DevicePinNumber"].Visible = false;
            sourceDataGridView.Columns["DevicePointDesignation"].Visible = false;

        }

        private void ComparingForm_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = Enum.GetValues(typeof(ComparisonState));
            comboBox1.SelectedIndex = 0;

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComparisonSelection = (ComparisonState)Enum.Parse(typeof(ComparisonState), comboBox1.Text);
            ReorderData(ComparisonSelection);
        }

        private void ReorderData(ComparisonState comparisonSelection)
        {
            switch (comparisonSelection)
            {
                case ComparisonState.None:
                    {
                        sourceDataGridView.DataSource = PlcDataModelView;
                        sourceDataGridView.Refresh();
                        targetDataGridView.DataSource = CsvFileDataModelViews;
                        targetDataGridView.Refresh();
                        break;
                    }
                case ComparisonState.Similarities:
                    {
                        CheckSimilaritiesOrDifferences(ComparisonState.Similarities);
                        break;
                    }
                case ComparisonState.Differences:
                    {
                        CheckSimilaritiesOrDifferences(ComparisonState.Differences);
                        break;
                    }
            }
        }

        private void CheckSimilaritiesOrDifferences(ComparisonState comparisonState)
        {
            if (PlcDataModelView != null && CsvFileDataModelViews != null)
            {
                var result = new List<PlcDataModelView>();
                var result1 = new List<CsvFileDataModelViews>();
                var isEqualDataRow = PlcDataModelView.Count == CsvFileDataModelViews.Count ? true : false;
                if (isEqualDataRow)
                {
                    for (int i = 0; i < PlcDataModelView.Count; i++)
                    {
                        switch (comparisonState)
                        {
                            case ComparisonState.Similarities:
                                {
                                    if (IsEqual(PlcDataModelView[i], CsvFileDataModelViews[i]))
                                    {
                                        result.Add(PlcDataModelView[i]);
                                        result1.Add(CsvFileDataModelViews[i]);
                                    }

                                    break;
                                }

                            case ComparisonState.Differences://индекс за пределами диапазона
                                {
                                    if (!IsEqual(PlcDataModelView[i], CsvFileDataModelViews[i]))
                                    {
                                        result.Add(PlcDataModelView[i]);
                                        result1.Add(CsvFileDataModelViews[i]);
                                    }
                                    break;
                                }
                        }
                    }
                    sourceDataGridView.DataSource = result;
                    sourceDataGridView.Refresh();
                    targetDataGridView.DataSource = result1;
                    targetDataGridView.Refresh();
                }
            }
        }

        public bool IsEqual(PlcDataModelView plcDataModelView, CsvFileDataModelViews csvFileDataModelViews)
        {
            if (!(plcDataModelView.SymbolicAdress ??= string.Empty).Equals(csvFileDataModelViews.SymbolicAdress ??= string.Empty))
            {
                return false;
            }
            if (!(plcDataModelView.FunctionText ??= string.Empty).Equals(csvFileDataModelViews.FunctionText ??= string.Empty))
            {
                return false;
            }
            if (!(plcDataModelView.PLCAdress ??= string.Empty).Equals(csvFileDataModelViews.PLCAdress ??= string.Empty))
            {
                return false;
            }
            if (!(plcDataModelView.DeviceNameShort ??= string.Empty).Equals(csvFileDataModelViews.DeviceNameShort ??= string.Empty))
            {
                MessageBox.Show("Выбран неверный модуль для импорта! Пожалуйста проверьте корректность CSV файла.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return true;
        }
        private void Upload_doc_button_Click(object sender, EventArgs e)
        {
            var path = PathDialog.TryGetReadPath();
            if (path == null)
            {
                return;
            }
            CsvConverter csvConverter = new CsvConverter(path);
            var dataFromFile = csvConverter.ReadFile();
            CsvFileDataModelViews = Mapper.ConvertDataToCsvCompare(dataFromFile);
            var dataWithTemplate = GetDataWithTemplateBorders(CsvFileDataModelViews);
            var isEqualDataRow = PlcDataModelView.Count == dataWithTemplate?.Count ? true : false;
            if (!isEqualDataRow)
            {
                MessageBox.Show("Данные для сравнения не совпадают по числу строк.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            targetDataGridView.DataSource = dataWithTemplate;
            CsvFileDataModelViews = dataWithTemplate;
            //TODO:сделать привязку а не вот это г.
        }

        private List<CsvFileDataModelViews> GetDataWithTemplateBorders(List<CsvFileDataModelViews> items)
        {
            var template = TemplatesData.GetInstance().TryGetTemplate(TemplateName);
            if (template == null)
            {
                MessageBox.Show("Шаблон не найден", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            var maxIndex = template.IndexLastRow;
            var minIndex = template.IndexFirstRow;
            var result = items.GetRange(minIndex, maxIndex - minIndex + 1);
            return result;
        }

        private void ok_button_Click(object sender, EventArgs e)
        {
            switch (SelectedMode)
            {
                case Import_Export_Type.Import:
                    var checkedRows = CsvFileDataModelViews.Where(x => x.IsChecked);
                    foreach (var checkedRow in checkedRows)
                    {
                        var elementPosition = CsvFileDataModelViews.IndexOf(checkedRow);
                        ReWriteProperties(elementPosition, checkedRow);
                    }
                    Close();
                    OkEvent?.Invoke(this, EventArgs.Empty);
                    break;

                case Import_Export_Type.Export:
                    var path = PathDialog.TryGetSavePath();
                    if (path == null)
                    {
                        return;
                    }
                    CsvConverter csvConverter = new CsvConverter(path);
                    csvConverter.SaveFile(dataToExport);//его надо переписать исходя из полученнго шаблона
                    Close();
                    break;
            }
        }

        private void ReWriteProperties(int elementPosition, CsvFileDataModelViews checkedRow)
        {
            var elementToRewrite = PlcDataModelView.ElementAt(elementPosition);
            elementToRewrite.PLCAdress = checkedRow.PLCAdress;
            elementToRewrite.SymbolicAdress = checkedRow.SymbolicAdress;
            elementToRewrite.FunctionText = checkedRow.FunctionText;
        }
    }
}
