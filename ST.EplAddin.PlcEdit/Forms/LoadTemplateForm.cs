using ST.EplAddin.PlcEdit.Helpers;
using ST.EplAddin.PlcEdit.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ST.EplAddin.PlcEdit.Forms
{
    public partial class LoadTemplateForm : Form
    {
        private readonly string path;
        private readonly string pathToSaveTemplate;

        public static event EventHandler<Template> TemplateAction;
        private DataGridViewRow[] SelectedRows
        {
            get
            {
                return dataGridView.SelectedRows.Cast<DataGridViewRow>().ToArray();
            }
        }
        public LoadTemplateForm(string path, string pathToSaveTemplate)
        {
            InitializeComponent();
            this.path = path;
            this.pathToSaveTemplate = pathToSaveTemplate;
        }

        private void Cancel_button_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void Select_button_Click(object sender, EventArgs e)
        {
            List<CsvFileDataModelView> csvFileDataModelViews = new List<CsvFileDataModelView>();
            var indexFirstRow = SelectedRows.OrderBy(x => x.Index).First().Index;
            var indexLastRow = SelectedRows.OrderBy(x => x.Index).Last().Index;
            var fileName = PathDialog.TryGetFileName(path);
            var fileNameWithType = PathDialog.TryGetFileNameWithType(path);
            var dataInTable = ((IEnumerable)dataGridView.DataSource).Cast<CsvFileDataModelView>().ToList();
            dataInTable.Add(new CsvFileDataModelView(string.Join(";", indexFirstRow, indexLastRow)));// информацию по поводу строчек перезаписываемых

            TemplateAction?.Invoke(this, new Template(indexFirstRow, indexLastRow, fileName));
            var fullPath = Path.Combine(pathToSaveTemplate, fileNameWithType);
            CsvConverter csvConverter = new CsvConverter(fullPath);
            csvConverter.SaveFile(dataInTable);
            Close();
        }

        private void LoadTemplateForm_Load(object sender, EventArgs e)
        {
            //CsvConverter csvConverter = new CsvConverter(path);
            //var dataFromFile = csvConverter.ReadFile();
            //dataGridView.DataSource = dataFromFile;
        }
    }
}
