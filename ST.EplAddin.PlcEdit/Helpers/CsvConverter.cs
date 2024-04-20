using CsvHelper;
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
        public void ReadFile()
        {
            using (var reader = new StreamReader(filePath, Encoding.ASCII))
            {
                using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var text = csvReader.Read();//посмотрим что получим
                }
            }
        }
        public void SaveFile(List<CsvFileDataModelView> fileToWrite)
        {
            using (var writer = new StreamWriter(filePath, false, Encoding.ASCII)) //поменять тип шифрования
            {
                using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csvWriter.WriteRecords(fileToWrite);
                }
            }
        }
    }
}
