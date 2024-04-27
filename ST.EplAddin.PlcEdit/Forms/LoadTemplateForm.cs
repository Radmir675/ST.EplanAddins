using ST.EplAddin.PlcEdit.Helpers;
using ST.EplAddin.PlcEdit.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ST.EplAddin.PlcEdit.Forms
{
    public partial class LoadTemplateForm : Form
    {
        private readonly string path;

        public static event EventHandler<TemplateMembers> TemplateActioon;
        public const string TemplatePath = "";// взять данные из action 
        private DataGridViewRow[] SelectedRows
        {
            get
            {
                return dataGridView.SelectedRows.Cast<DataGridViewRow>().ToArray();
            }
        }
        public LoadTemplateForm(string path)
        {
            InitializeComponent();
            this.path = path;
        }

        private void Cancel_button_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void Select_button_Click(object sender, EventArgs e)
        {
            List<CsvFileDataModelView> csvFileDataModelViews = new List<CsvFileDataModelView>();
            var dataInTable = ((IEnumerable)dataGridView.DataSource).Cast<CsvFileDataModelView>().ToList();
            var indexFirstRow = SelectedRows.OrderBy(x => x.Index).First().Index;
            var indexLastRow = SelectedRows.OrderBy(x => x.Index).Last().Index;
            var changableRows = (indexFirstRow, indexLastRow - indexFirstRow + 1);
            var fileName = PathDialog.TryGetFileName(path);

            TemplateActioon?.Invoke(this, new TemplateMembers(indexFirstRow, indexLastRow, fileName));

            CsvConverter csvConverter = new CsvConverter(path);
            csvConverter.SaveFile(dataInTable);//куда то надо записать информацию по поводу строчек перезаписываемых

            this.Close();
        }



        private void LoadTemplateForm_Load(object sender, EventArgs e)
        {
            CsvConverter csvConverter = new CsvConverter(path);
            var dataFromFile = csvConverter.ReadFile();
            dataGridView.DataSource = dataFromFile;
        }
    }
}
