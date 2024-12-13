using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ST.EplAddin.PlcEdit.Model
{
    public class TemplatesData
    {
        private static List<Template> Templates { get; set; } = new();
        private static string SavePath { get; set; }
        private TemplatesData() { }
        static TemplatesData()
        {
            ManagePlcForm.PathEvent += ManagePlcForm_PathEvent;
        }
        private static void ManagePlcForm_PathEvent(object sender, string e)
        {
            SavePath = e;
            ScanTemplates();
        }

        private static TemplatesData Instance { get; set; }
        public static TemplatesData GetInstance()
        {

            Instance ??= new TemplatesData();
            return Instance;
        }
        public void Add(Template template)
        {
            Templates.Add(template);
        }
        public List<Template> GetTemplates()
        {
            return Templates;
        }
        private static void ScanTemplates()
        {
            Templates.Clear();
            var files = Directory.GetFiles(SavePath);
            foreach (var file in files)
            {
                CsvConverter csvConverter = new CsvConverter(file);
                var (minRow, maxRow) = csvConverter.ReadAdditionalInformation();
                var fileName = Path.GetFileNameWithoutExtension(file);
                Templates.Add(new Template(minRow, maxRow, fileName));
            }
        }
        public Template TryGetByName(string selectedTemplateName)
        {
            var result = Templates.FirstOrDefault(x => x.FileName == selectedTemplateName);
            return result;
        }
        public string TryGetPath(string templateName)
        {
            var result = Directory.GetFiles(SavePath, $"{templateName}*").FirstOrDefault();
            return result;
        }

        public bool Rename(string templateName, string newName)
        {
            try
            {
                var oldPath = TryGetPath(templateName);
                var newPath = Path.Combine(SavePath, newName);
                File.Move(oldPath, newPath);
                ScanTemplates();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public bool Delete(string templateName)
        {
            try
            {
                string path = TryGetPath(templateName);
                FileInfo fileInfo = new FileInfo(path);
                if (fileInfo.Exists)
                {
                    fileInfo.Delete();
                }
                ScanTemplates();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
