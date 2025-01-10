using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ST.EplAddin.PlcEdit.Forms
{
    public partial class ImportCsvForm : Form
    {
        private readonly List<CsvFileDataModelView> _csvFileDataModelViews;


        public event EventHandler<List<CsvFileDataModelView>> ImportCsvDataEvent;

        public ImportCsvForm(List<CsvFileDataModelView> csvFileDataModelViews)
        {
            _csvFileDataModelViews = csvFileDataModelViews;
            InitializeComponent();
            dataGridView.DataSource = _csvFileDataModelViews;
        }


        private void Import_button_Click(object sender, EventArgs e)
        {
            ImportCsvDataEvent?.Invoke(this, _csvFileDataModelViews);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Cancel_button_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
