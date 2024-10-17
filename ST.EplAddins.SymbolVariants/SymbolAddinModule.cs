using Eplan.EplApi.ApplicationFramework;

namespace ST.EplAddins.SymbolVariants
{
    public class SymbolAddinModule : IEplAddIn
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
            System.Windows.Forms.MessageBox.Show("Symbol addin is implemented");
            return true;
        }

        public bool OnUnregister()
        {
            return true;
        }
    }
}
