using Eplan.EplApi.ApplicationFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST.EplAddins.LastTerminalStrip
{
    public class AddinModule : IEplAddIn
    {
        public bool OnExit()
        {
            throw new NotImplementedException();
        }

        public bool OnInit()
        {
            return true;
        }

        public bool OnInitGui()
        {
            return true;
        }

        public bool OnRegister(ref bool bLoadOnStart)
        {
            bLoadOnStart = true;
            System.Windows.Forms.MessageBox.Show("Last terminal addin is implemented");
            return true;
        }

        public bool OnUnregister()
        {
            return true;
        }
    }
}
