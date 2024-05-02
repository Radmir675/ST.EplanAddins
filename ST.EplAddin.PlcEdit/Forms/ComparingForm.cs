using ST.EplAddin.PlcEdit.Helpers;
using ST.EplAddin.PlcEdit.View;
using System;
using System.Collections.Generic;
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
                        break;
                    }
                case ComparisonState.Similarities:
                    {
                        break;
                    }
                case ComparisonState.Differences:
                    {
                        break;
                    }
            }
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

        }
    }
}
