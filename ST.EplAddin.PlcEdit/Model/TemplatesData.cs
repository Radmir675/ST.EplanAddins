using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ST.EplAddin.PlcEdit.Model
{
    public class TemplatesData
    {
        private static List<Template> Templates { get; set; } = new();
        private static string SavePath { get; set; }
        private TemplatesData()
        {

        }

        static TemplatesData()
        {
            ManagePlcForm.PathEvent += ManagePlcForm_PathEvent;
        }
        private static void ManagePlcForm_PathEvent(object sender, string e)
        {
            SavePath = e;
            ReadFilesFromFolder();
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
            ReadFilesFromFolder();
            return Templates;
        }
        private static void ReadFilesFromFolder()
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
        public Template TryGetTemplateByName(string selectedTemplateName)
        {
            var result = Templates.FirstOrDefault(x => x.FileName == selectedTemplateName);
            return result;
        }
        public string TryGetTemplatePath(string templateName)
        {
            var result = Directory.GetFiles(SavePath, $"{templateName}*").FirstOrDefault();
            return result;
        }

    }
}
