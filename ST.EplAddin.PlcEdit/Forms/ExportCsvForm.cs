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

        List<CsvFileDataModelView> dataFromEplan = new List<CsvFileDataModelView>();
        List<CsvFileDataModelView> dataToExport = new List<CsvFileDataModelView>();
        private DataGridViewRow[] SelectedRows
        {
            get
            {
                return dataGridView.SelectedRows.Cast<DataGridViewRow>().ToArray();
            }
        }
        public ExportCsvForm(List<PlcDataModelView> dataFromEplan)
        {
            InitializeComponent();
            this.dataFromEplan = Mapper.ConvertDataToCsvModel(dataFromEplan);
        }

        private void Export_button_Click(object sender, EventArgs e)
        {
            var path = PathDialog.TryGetSavePath();
            if (path == null)
            {
                return;
            }
            CsvConverter csvConverter = new CsvConverter(path);
            csvConverter.SaveFile(dataToExport);//его надо переписать исходя из полученнго шаблона
            this.Close();
        }

        private void Cancel_button_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoadTemplate_button_Click(object sender, EventArgs e)
        {
            var path = PathDialog.TryGetReadPath();
            if (path == null)
            {
                return;
            }
            CsvConverter csvConverter = new CsvConverter(path);
            dataToExport = csvConverter.ReadFile();
            dataGridView.DataSource = dataToExport;
        }

        private void ReplaceSelectedRows_button_Click(object sender, EventArgs e)
        {
            var dataInTable = ((IEnumerable)dataGridView.DataSource).Cast<CsvFileDataModelView>().ToList();
            var indexFirstRow = SelectedRows.OrderBy(x => x.Index).First().Index;
            var indexLastRow = SelectedRows.OrderBy(x => x.Index).Last().Index;

            int j = 0;
            for (int i = 0; i < dataToExport.Count; i++)
            {
                if (i >= indexFirstRow && i <= indexLastRow)
                {
                    dataToExport[i] = dataFromEplan[j];
                    j++;
                }
            }
            dataGridView.Refresh();
        }
    }
}
