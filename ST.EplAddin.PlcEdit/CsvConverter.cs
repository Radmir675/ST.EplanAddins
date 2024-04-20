using CsvHelper;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;
using SaveFileDialog = System.Windows.Forms.SaveFileDialog;

namespace ST.EplAddin.PlcEdit
{
    public  class CsvConverter
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
                    var text=csvReader.Read();//посмотрим что получим
                }
            }
        }
        public void SaveFile(List<CsvFileDataView>fileToWrite) 
        {
            using (var writer = new StreamWriter(filePath,false,Encoding.ASCII)) //поменять тип шифрования
            {
                using (var csvWriter=new CsvWriter(writer,CultureInfo.InvariantCulture)) 
                {
                    csvWriter.WriteRecords(fileToWrite);
                }
            }
        }
        public string TryGetSavePath() 
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "(*.csv) | *.csv";
            if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                return null;
            // получаем выбранный файл
            string filename = saveFileDialog.FileName;
            return filename;
        }
        public string TyrGetReadPath()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "(*.csv) | *.csv";
            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                return null;
            // получаем выбранный файл
            string filename = openFileDialog.FileName;
            return filename;
        }
    }
}
