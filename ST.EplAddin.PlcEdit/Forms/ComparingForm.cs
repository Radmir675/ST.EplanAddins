using ST.EplAddin.PlcEdit.Helpers;
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
        public List<CsvFileDataModelViews> CsvFileDataModelViews { get; set; }

        /// <summary>
        /// Использовать для импорта и экспорта (при экспорте csvFileDataModelViews может быть null)
        /// </summary>
        /// <param name="plcDataModelView">Данные из Eplan(ManagePlcForm)</param>
        /// <param name="csvFileDataModelViews">Данные из импортируемого файла</param>
        public ComparingForm(List<PlcDataModelView> plcDataModelView, List<CsvFileDataModelViews> csvFileDataModelViews = null)
        {
            InitializeComponent();
            PlcDataModelView = plcDataModelView;
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
            if (PlcDataModelView != null)
            {
                var result = new List<PlcDataModelView>();
                var result1 = new List<CsvFileDataModelViews>();
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

                        case ComparisonState.Differences:
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
                return false;
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
            targetDataGridView.DataSource = CsvFileDataModelViews;
        }

        private void ok_button_Click(object sender, EventArgs e)
        {
            var checkedRows = CsvFileDataModelViews.Where(x => x.IsChecked);
            foreach (var checkedRow in checkedRows)
            {
                var elementPosition = CsvFileDataModelViews.IndexOf(checkedRow);
                ReWriteProperties(elementPosition, checkedRow);
            }
            Close();
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
