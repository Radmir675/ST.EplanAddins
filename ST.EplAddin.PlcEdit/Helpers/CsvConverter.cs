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

        public IEnumerable<CsvFileDataModelView> ReadFile()
        {
            CsvConfiguration config = GetConfig();
            try
            {
                using (var reader = new StreamReader(filePath))
                using (var csvReader = new CsvReader(reader, config))
                {
                    var encoding = reader.CurrentEncoding;
                    while (csvReader.Read())
                    {
                        var module = new CsvFileDataModelView()
                        {
                            SymbolicAdress = csvReader.GetField(0),
                            BitNumber = csvReader.GetField(1),
                            Unit = csvReader.GetField(2),
                            FunctionText = csvReader?.GetField(3),
                            PLCAdress = csvReader?.GetField(4),
                            DeviceNameShort = csvReader.GetField(5),
                        };
                        yield return module;
                    }
                }
            }
            finally
            {
                //TODO:посмотреть что делать с блоком finnaly
            }
            //catch (System.Exception e)
            //{
            //    MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }



        public void SaveFile(List<CsvFileDataModelView> fileToWrite)
        {
            try
            {
                CsvConfiguration config = GetConfig();
                using (var writer = new StreamWriter(filePath, false, Encoding.UTF8))
                using (var csvWriter = new CsvWriter(writer, config))
                {
                    foreach (var file in fileToWrite)
                    {
                        csvWriter.WriteRecord(file);
                        csvWriter.NextRecord();
                    }
                }
                MessageBox.Show("Files are recorded!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (System.Exception e)
            {
                MessageBox.Show($"{e}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public (int, int) ReadAdditionalInformation()
        {
            string[] parsedNums = new string[2];

            CsvConfiguration config = GetConfig();
            using (var reader = new StreamReader(filePath))
            using (var csvReader = new CsvReader(reader, config))
            {
                var encoding = reader.CurrentEncoding;
                csvReader.Read();
                while (csvReader.Read())
                {
                    parsedNums = csvReader.GetField(0).Split(';');
                }
            }


            var IsParseFirst = int.TryParse(parsedNums[0], out int minRow);
            var IsParseSecond = int.TryParse(parsedNums[1], out int maxRow);


            return (minRow, maxRow);
        }

        private CsvConfiguration GetConfig()
        {
            return new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";",
                TrimOptions = TrimOptions.None,
                BadDataFound = null,
                MissingFieldFound = null

            };
        }
    }
}
