using Eplan.EplApi.ApplicationFramework;

namespace ST.Epl.Addin.SyncPage
{
    public class AddinModule : IEplAddIn
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
            return true;
        }

        public bool OnUnregister()
        {
            return true;
        }
    }
}
