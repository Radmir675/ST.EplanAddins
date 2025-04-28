using Eplan.EplApi.ApplicationFramework;

namespace ST.EplAddin.JumpersReport
{
    public class AddinModule : IEplAddIn
    {
        private Eplan.EplApi.Base.TraceListener m_oTrace;
        ListenEndGenerateAction _listenEndGenerateAction;
        public bool OnUnregister()
        {
            return true;
        }

        public bool OnInit()
        {
            m_oTrace = new Eplan.EplApi.Base.TraceListener();
            System.Diagnostics.Trace.Listeners.Add(m_oTrace);
            _listenEndGenerateAction = new ListenEndGenerateAction();


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

        public bool OnExit()
        {
            return true;
        }
    }
}
