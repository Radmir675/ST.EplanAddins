using ST.EplAddin.PlcEdit.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ST.EplAddin.PlcEdit.Forms
{
    public partial class ExportCsvForm : Form
    {

        List<CsvFileDataModelView> dataFromTemplate = new List<CsvFileDataModelView>();
        private DataGridViewRow[] SelectedRows
        {
            get
            {
                return dataGridView.SelectedRows.Cast<DataGridViewRow>().ToArray();
            }
        }
        public ExportCsvForm(List<PlcDataModelView> dataToExport)
        {
            InitializeComponent();
            dataFromTemplate = Mapper.ConvertDataToCsvModel(dataToExport);
        }

        private void Export_button_Click(object sender, EventArgs e)
        {
            var path = PathDialog.TryGetSavePath();
            CsvConverter csvConverter = new CsvConverter(path);
            csvConverter.SaveFile(dataFromTemplate);//его надо переписать исходя из полученнго шаблона
        }

        private void Cancel_button_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Load_button_Click(object sender, EventArgs e)
        {
            var path = PathDialog.TryGetReadPath();
            CsvConverter csvConverter = new CsvConverter(path);
            dataFromTemplate = csvConverter.ReadFile();
            dataGridView.DataSource = dataFromTemplate;
        }

        private void ReplaceSelectedRows_button_Click(object sender, EventArgs e)
        {
            List<CsvFileDataModelView> dataToReplace = new List<CsvFileDataModelView>();
            var dataInTable = ((IEnumerable)dataGridView.DataSource).Cast<CsvFileDataModelView>().ToList();
            var indexFirstRow = SelectedRows.OrderBy(x => x.Index).First().Index;
            var indexLastRow = SelectedRows.OrderBy(x => x.Index).Last().Index;

            int j = 0;
            for (int i = 0; i < dataFromTemplate.Count; i++)
            {
                if (i >= indexFirstRow || i <= indexLastRow)
                {
                    dataFromTemplate[i] = dataFromTemplate[j];
                    j++;
                }
            }
        }
    }
}
