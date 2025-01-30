using ST.EplAddin.PlcEdit.Helpers;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ST.EplAddin.PlcEdit.Forms
{
    public partial class ExportCsvForm : Form
    {
        private readonly List<CsvFileDataModelView> _dataToExport;
        public ExportCsvForm()
        {
            InitializeComponent();
        }

        public ExportCsvForm(List<CsvFileDataModelView> dataToExport) : this()
        {
            _dataToExport = dataToExport;
            dataGridView.DataSource = _dataToExport;
        }

        private void Export_button_Click(object sender, EventArgs e)
        {
            var path = PathDialog.TryGetSavePath();
            if (path == null) return;

            var csvConverter = new CsvConverter(path);
            csvConverter.SaveFile(_dataToExport);
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void Cancel_button_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
