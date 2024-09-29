using ST.EplAddin.PlcEdit.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ST.EplAddin.PlcEdit.Forms
{
    public partial class ImportCsvForm : Form
    {
        public static event EventHandler<List<CsvFileDataModelView>> ImportCsvData;
        public static event EventHandler<List<CsvFileDataModelView>> ExportCsvData;
        private DataGridViewRow[] SelectedRows
        {
            get
            {
                return dataGridView.SelectedRows.Cast<DataGridViewRow>().ToArray();
            }
        }
        public ImportCsvForm()
        {
            InitializeComponent();
        }

        private void Import_button_Click(object sender, EventArgs e)
        {
            List<CsvFileDataModelView> csvFileDataModelViews = new List<CsvFileDataModelView>();
            var dataInTable = ((IEnumerable)dataGridView.DataSource).Cast<CsvFileDataModelView>().ToList();
            var indexFirstRow = SelectedRows.OrderBy(x => x.Index).First().Index;
            var indexLastRow = SelectedRows.OrderBy(x => x.Index).Last().Index;
            var selectedData = dataInTable.Skip(indexFirstRow)//прибавил плюс один так как индекс начинается с нуля
                                          .Take(indexLastRow - indexFirstRow + 1)
                                          .ToList();
            ImportCsvData?.Invoke(this, selectedData);
            this.Close();
        }

        private void Cancel_button_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Load_button_Click(object sender, EventArgs e)
        {
            var path = PathDialog.TryGetReadPath();
            if (path == null)
            {
                return;
            }
            CsvConverter csvConverter = new CsvConverter(path);
            var dataFromFile = csvConverter.ReadFile();
            dataGridView.DataSource = dataFromFile;
        }
    }
}
