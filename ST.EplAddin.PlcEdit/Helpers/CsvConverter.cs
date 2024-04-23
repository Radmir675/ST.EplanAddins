using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

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

        public void SaveFile(List<CsvFileDataModelView> fileToWrite)
        {
            CsvConfiguration config = GetConfig();
            config.HasHeaderRecord = false;

            Encoding wishEncoding = Encoding.GetEncoding("windows-1251");
            using (var writer = new StreamWriter(filePath, false, wishEncoding)) //поменять тип шифрования

            using (var csvWriter = new CsvWriter(writer, config))
            {
                var headerTable = new CsvFileDataModelView()
                {
                    SymbolicAdress = "//Mapped variable",
                    BitNumber = "//Parameter name @ counter in device",
                    Unit = "//Unit",
                    FunctionText = "//Description",
                    PLCAdress = "PLCAdress",
                    DeviceNameShort = "//Device name"
                };

                csvWriter.WriteRecord(new CsvFileDataModelView("CoDeSys Mapping Export V1.2"));
                csvWriter.NextRecord();
                csvWriter.WriteRecord(headerTable);
                csvWriter.NextRecord();
                csvWriter.WriteRecord(new CsvFileDataModelView("//Important: change only first, third or fourth column in Excel or add variable name before first"));
                csvWriter.NextRecord();//что тут пока не понял
                foreach (var file in fileToWrite)
                {
                    csvWriter.WriteRecord(file);
                    csvWriter.NextRecord();
                }
            }
        }

        private string SaveStringAsANSI(string ending, Encoding encoding)
        {
            Encoding wishEncoding = Encoding.GetEncoding("windows-1251");
            byte[] encBytes = encoding.GetBytes(ending);
            byte[] utf8Bytes = Encoding.Convert(Encoding.UTF8, wishEncoding, encBytes);
            return Encoding.UTF8.GetString(utf8Bytes);
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
