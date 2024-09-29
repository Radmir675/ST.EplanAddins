using Eplan.EplApi.DataModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ST.EplAddin.LastTerminalStrip
{
    public class InternalLogger
    {
        public string Path { get; }
        public string ProjectName { get; }
        public List<string> LogsFromFile { get; set; }
        public InternalLogger(Project project)
        {
            ProjectName = project.ProjectName;
            Path = GetPath(project);
        }
        private string GetPath(Project project)
        {
            using (LockingStep lockingStep = new LockingStep())
            {
                string path = project.ProjectDirectoryPath;
                string fullPath = System.IO.Path.Combine(path, $"{project.ProjectName}.txt");
                return fullPath;
            }
        }
        public List<string> ReadFileLog()
        {
            FileStream fileStream = new FileStream(Path, FileMode.OpenOrCreate);
            using (StreamReader streamReader = new StreamReader(fileStream))
            {
                LogsFromFile = new List<string>();
                while (!streamReader.EndOfStream)
                {
                    var data = streamReader.ReadLine();
                    LogsFromFile.Add(data);
                }
            }
            return LogsFromFile ?? new List<string>();
        }
        public void WriteFileLog(List<string> logs)
        {
            new System.IO.FileInfo(Path).Directory.Create();
            FileStream fileStream = new FileStream(Path, FileMode.Append, FileAccess.Write);
            using (StreamWriter streamWriter = new StreamWriter(fileStream))
            {
                if (logs.Any())
                {
                    streamWriter.WriteLine("Следующие определения клеммников были добавлены:");
                }
                foreach (string log in logs)
                {
                    streamWriter.WriteLine(DateTime.Now + " | " + log);
                }
            }
        }
    }
}
