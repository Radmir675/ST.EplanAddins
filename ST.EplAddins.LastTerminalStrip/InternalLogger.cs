using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ST.EplAddin.LastTerminalStrip
{
    internal class InternalLogger
    {
        private string path = @"C:\temp\EplanTerminalStrips.txt";
        List<string> logsFromFile;
        public List<string> ReadFileLog()
        {
            FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate);
            using (StreamReader streamReader = new StreamReader(fileStream))
            {
                logsFromFile = new List<string>();
                while (!streamReader.EndOfStream)
                {
                    var data = streamReader.ReadLine();

                    logsFromFile.Add(data);

                }
            }
            return logsFromFile ?? new List<string>();
        }
        public void WriteFileLog(List<string> logs)
        {
            FileStream fileStream = new FileStream(path, FileMode.Append, FileAccess.Write);
            using (StreamWriter streamWriter = new StreamWriter(fileStream))
            {
                if (!logs.Any())
                {
                    streamWriter.WriteLine("Следующие определения клеммников были добавлены:");
                    foreach (string log in logs)
                    {
                        streamWriter.WriteLine(DateTime.Now + " | " + log);
                    }
                }
            }
        }
    }
}
