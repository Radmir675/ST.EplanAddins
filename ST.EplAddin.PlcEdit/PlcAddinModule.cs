using Eplan.EplApi.ApplicationFramework;
using System.Windows.Forms;

namespace ST.EplAddin.PlcEdit
{
    public class PlcAddinModule : IEplAddIn
    {
        public bool OnExit()
        {
            return true;
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
