using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;

namespace ST.EplAddin.PlcEdit
{
    public class CsvConverter
    {
        private readonly string filePath;

        public CsvConverter(string filePath)
        {
            this.filePath = filePath;
        }

        public List<CsvFileDataModelView> ReadFile()
        {
            CsvConfiguration config = GetConfig();
            List<CsvFileDataModelView> lines = new List<CsvFileDataModelView>(50);
            try
            {
                using (var reader = new StreamReader(filePath))
                using (var csvReader = new CsvReader(reader, config))
                {
                    var encoding = reader.CurrentEncoding;
                    csvReader.Read();
                    while (csvReader.Read())
                    {
                        var module = new CsvFileDataModelView()
                        {
                            SymbolicAdress = csvReader.GetField(0),
                            BitNumber = csvReader.GetField(1),
                            FunctionText = csvReader?.GetField(3),
                            PLCAdress = csvReader?.GetField(4),
                            DeviceNameShort = csvReader.GetField(5),
                        };
                        lines.Add(module);
                    }
                }
                return lines;

            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return lines;
        }
        public void SaveFile(List<CsvFileDataModelView> fileToWrite)
        {
            CsvConfiguration config = GetConfig();
            using (var writer = new StreamWriter(filePath, true, Encoding.UTF8)) //поменять тип шифрования
            using (var csvWriter = new CsvWriter(writer, config))
            {
                foreach (var file in fileToWrite)
                {
                    csvWriter.WriteRecord(file);
                    csvWriter.NextRecord();
                }
            }
            MessageBox.Show("Files are recorded!", "Mesage", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private CsvConfiguration GetConfig()
        {
            return new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";",
                BadDataFound = null,
                MissingFieldFound = null
            };
        }
    }
}
