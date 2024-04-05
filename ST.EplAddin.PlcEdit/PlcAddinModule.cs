using Eplan.EplApi.ApplicationFramework;
using System;
using System.Windows.Forms;

namespace ST.EplAddins.SymbolVariants
{
    public class PlcAddinModule : IEplAddIn
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
            MessageBox.Show("Plc edit addin is implemented");
            return true;
        }

        public bool OnUnregister()
        {
            return true;
        }
    }
}
