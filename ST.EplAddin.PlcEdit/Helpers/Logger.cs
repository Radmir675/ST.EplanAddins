using System.Collections.Generic;

namespace ST.EplAddin.PlcEdit.Helpers
{
    public class Logger
    {
        private List<string> messages = new();
        public void Log(string message)
        {
            messages.Add(message);
        }
        public List<string> GetMessages()
        {
            return messages;
        }
    }
}
