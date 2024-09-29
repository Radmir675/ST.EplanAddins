using Microsoft.VisualStudio.TestTools.UnitTesting;
using ST.EplAddin.PlcEdit;
using ST.EplAddin.PlcEdit.Forms;
using System.Collections.Generic;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void StartFormManage()
        {
            PlcDataModelView model = new PlcDataModelView();
            model.DeviceNameShort = "cdc";
            model.FunctionText = "Привет мир!";
            PlcDataModelView modeli = new PlcDataModelView();
            modeli.DeviceNameShort = "cd1c";
            modeli.FunctionText = "Привет мир5!";
            var ss = new List<PlcDataModelView>();
            ss.Add(modeli);
            ss.Add(model);
            ManagePlcForm managePlcForm = new ManagePlcForm(ss);
            managePlcForm.ShowDialog();
        }
        [TestMethod]
        public void LoadCsv()
        {
            ImportCsvForm importExportCsvForm = new ImportCsvForm();
            importExportCsvForm.ShowDialog();
        }
        [TestMethod]
        public void SaveCsv()
        {

            var path = @"C:\Users\biktimirov.rr\Desktop\Фаил.csv";
            CsvConverter csvConverter = new CsvConverter(path);

            List<CsvFileDataModelView> lines = new List<CsvFileDataModelView>(2);
            CsvFileDataModelView csvFileDataModelView = new CsvFileDataModelView();
            csvFileDataModelView.FunctionText = "Test";
            csvFileDataModelView.PLCAdress = "Bit1";
            CsvFileDataModelView csvFileDataModelView1 = new CsvFileDataModelView();
            csvFileDataModelView1.FunctionText = "Test1";
            csvFileDataModelView1.PLCAdress = "Bit2";


            lines.Add(csvFileDataModelView);
            lines.Add(csvFileDataModelView1);
            csvConverter.SaveFile(lines);
            Assert.IsNotNull(lines);
        }
        [TestMethod]
        public void StartMainWindows()
        {
            List<PlcDataModelView> list = new List<PlcDataModelView>();
            PlcDataModelView plcDataModelView = new PlcDataModelView();
            plcDataModelView.FunctionText = "Test";
            PlcDataModelView plcDataModelView1 = new PlcDataModelView();
            plcDataModelView1.FunctionText = "Test1";
            list.Add(plcDataModelView);
            list.Add(plcDataModelView1);

            var pathSaveTemplate = TryGetPathTemplateSaves();
            if (string.IsNullOrEmpty(pathSaveTemplate))
            {
                return;
            }
            ManagePlcForm managePlcForm = new ManagePlcForm(list, pathSaveTemplate);
            managePlcForm.ShowDialog();
        }

        private string TryGetPathTemplateSaves()
        {
            //@"C:\Users\biktimirov.rr\Desktop\Scantronic"

            var path =;

            return path;
        }

        public List<PlcDataModelView> Init()
        {

            List<PlcDataModelView> list = new List<PlcDataModelView>();
            PlcDataModelView plcDataModelView = new PlcDataModelView();
            plcDataModelView.FunctionText = "Test";
            plcDataModelView.DeviceNameShort = "S1_1A4";
            PlcDataModelView plcDataModelView1 = new PlcDataModelView();
            plcDataModelView1.FunctionText = "Test1";
            plcDataModelView1.DeviceNameShort = "S1_1A4";
            list.Add(plcDataModelView);
            list.Add(plcDataModelView1);
            return list;
        }
        [TestMethod]
        public void TryReplaceDataInMainViewForm()
        {
            //ManagePlcForm managePlcForm = new ManagePlcForm(Init());
            //managePlcForm.ShowDialog();

        }
        [TestMethod]
        public void ExportCsv()
        {
            var path = @"C:\Users\biktimirov.rr\Desktop\PathToTemplate";
            ManagePlcForm managePlcForm = new ManagePlcForm(Init(), path);
            managePlcForm.ShowDialog();

        }
        [TestMethod]
        public void StartComparis()
        {
            ComparingForm comparingForm = new ComparingForm(Init());
            comparingForm.ShowDialog();

        }
    }
}
