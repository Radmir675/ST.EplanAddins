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
