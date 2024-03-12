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
        public InternalLogger(string project)
        {
            ProjectName = project;
            Path = System.IO.Path.Combine(@"C:\temp\EplanLogs", $"{ProjectName}.txt");
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
