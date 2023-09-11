using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Eplan.EplApi.RemoteClient;

namespace ST.EplAddin.CommonLibrary
{
    class ProcessId
    {
        //при закртыии Eplan если Eplan.exe=1 то удалить все .json
        //string finish = Path.Combine(Form1.Main.textBox2.Text, array[i, 0] + " " + array[i, 1] + ".txt");
        //File.WriteAllText(finish, result);
        public int  GetEplanId() 
        {
            EplanServerData eplanServerData = new EplanServerData();
            var processId=eplanServerData.EplanProcessID;
            return processId;
        }

        public int GetProcessId() 
        {
            var processes = Process.GetProcessesByName("EPLAN.exe");
            var currentProcessId = processes.Where(x => x.Id == GetEplanId()).ToString();
            int result = int.Parse(currentProcessId);
            return result;

        }
    }
}
