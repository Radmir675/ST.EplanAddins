using ST.EplAddin.PlcEdit.Helpers;
using ST.EplAddin.PlcEdit.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;

namespace ST.EplAddin.PlcEdit.Forms
{
    public partial class ComparingForm : Form
    {
        public ComparisonState ComparisonSelection { get; set; }
        public List<PlcDataModelView> PlcDataModelView { get; }

        public ExchangeMode SelectedMode { get; }
        public List<CsvFileDataModelViews> CsvFileDataModelViews { get; set; }
        public static event EventHandler OkEvent;
        public static event EventHandler StartRewriting;
        private bool IsFileUploaded = false;

        /// <summary>
        /// Использовать для импорта и экспорта (при экспорте csvFileDataModelViews может быть null)
        /// </summary>
        /// <param name="plcDataModelView">Данные из Eplan(ManagePlcForm)</param>
        /// <param name="csvFileDataModelViews">Данные из импортируемого файла</param>
        public ComparingForm(List<PlcDataModelView> plcDataModelView, ExchangeMode type, List<CsvFileDataModelViews> csvFileDataModelViews = null)
        {
            InitializeComponent();
            PlcDataModelView = plcDataModelView;
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
            if (SelectedMode == ExchangeMode.Export)
            {
            }
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

            return true;
        }


        private void ok_button_Click(object sender, EventArgs e)
        {
            if (IsFileUploaded == false && SelectedMode == ExchangeMode.Import)
            {
                MessageBox.Show("Пожалуйста выберите файл для импорта", "InFormation", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                switch (SelectedMode)
                {
                    case ExchangeMode.Import:
                        var checkedRows = CsvFileDataModelViews.Where(x => x.IsChecked);
                        StartRewriting?.Invoke(this, EventArgs.Empty);
                        foreach (var checkedRow in checkedRows)
                        {
                            var elementPosition = CsvFileDataModelViews.IndexOf(checkedRow);
                            ReWriteProperties(elementPosition, checkedRow);
                        }
                        Close();
                        OkEvent?.Invoke(this, EventArgs.Empty);
                        break;

                    case ExchangeMode.Export:
                        var path = PathDialog.TryGetSavePath();
                        if (path == null)
                        {
                            return;
                        }
                        CsvConverter csvConverter = new CsvConverter(path);
                        var dataToExport = GetDataToExport();
                        if (dataToExport == null)
                        {
                            Close();
                            break;
                        }
                        csvConverter.SaveFile(dataToExport);
                        Close();
                        break;
                }
            }
        }

        private List<CsvFileDataModelView> GetDataToExport()
        {
            //var template = TemplatesData.GetInstance().TryGetByName(TemplateName);
            //if (template == null)
            //{
            //    MessageBox.Show("Шаблон не найден");
            //    return null;
            //}
            //var templatePath = TemplatesData.GetInstance().TryGetPath(TemplateName);
            //if (string.IsNullOrEmpty(templatePath))
            //{
            //    MessageBox.Show("Фаил не найден!");
            //    return null;
            //}

            //CsvConverter csvConverter = new CsvConverter(templatePath);
            //var dataFromCSVFile = csvConverter.ReadFile();//полный данными фаил.
            //var eplanDataInCSVFormat = Mapper.ConvertDataToCsvModel(PlcDataModelView);
            //for (int i = 0; i < dataFromCSVFile.Count; i++)
            //{
            //    if (i >= template.IndexFirstRow && i < template.IndexLastRow + 1)
            //    {
            //        dataFromCSVFile[i] = eplanDataInCSVFormat[i - template.IndexFirstRow];
            //    }
            //}
            //foreach (var data in dataFromCSVFile)
            //{
            //    if (!string.IsNullOrEmpty(data.DeviceNameShort) && !data.DeviceNameShort.Contains("//"))
            //    {
            //        data.DeviceNameShort = eplanDataInCSVFormat[0].DeviceNameShort;
            //    }
            //}
            //dataFromCSVFile.Remove(dataFromCSVFile.Last());
            ////dataFromCSVFile[template.IndexFirstRow - 1].SymbolicAdress = $"{eplanDataInCSVFormat[0].DeviceNameShort}";
            //dataFromCSVFile.Last().SymbolicAdress = $"xHwError_{eplanDataInCSVFormat[0].DeviceNameShort}";

            //var dataToExport = dataFromCSVFile;//тут должна быть замена в соотвествии с шаблоном и выбранными ячейками
            //return dataToExport;
            return null;
        }

        private void ReWriteProperties(int elementPosition, CsvFileDataModelViews checkedRow)
        {
            var elementToRewrite = PlcDataModelView.ElementAt(elementPosition);

            elementToRewrite.PLCAdress = checkedRow.PLCAdress;
            elementToRewrite.SymbolicAdress = checkedRow.SymbolicAdress;
            elementToRewrite.FunctionText = checkedRow.FunctionText;
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox.Checked)
            {
                CsvFileDataModelViews?.ForEach(x => x.IsChecked = true);
            }
            else
            {
                CsvFileDataModelViews?.ForEach(x => x.IsChecked = false);
            }
            targetDataGridView?.Refresh();
        }
    }
}
