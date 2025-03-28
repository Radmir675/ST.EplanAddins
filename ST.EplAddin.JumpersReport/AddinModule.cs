using Eplan.EplApi.ApplicationFramework;

namespace ST.EplAddin.JumpersReport
{
    public class AddinModule : IEplAddIn
    {
        private Eplan.EplApi.Base.TraceListener m_oTrace;
        ReportsEndListener reportsEndListener;
        public bool OnUnregister()
        {
            return true;
        }

        public bool OnInit()
        {
            m_oTrace = new Eplan.EplApi.Base.TraceListener();
            System.Diagnostics.Trace.Listeners.Add(m_oTrace);
            reportsEndListener = new ReportsEndListener();

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
    public class ReportsEndListener
    {
        private EventHandler reportsUpdateEnd_EventHandler;
        public ReportsEndListener()
        {

            reportsUpdateEnd_EventHandler = new EventHandler("onActionEnd.String.XFgUpdateEvaluationAction");
            // reportsUpdateEnd_EventHandler.SetEvent("GenerateJumpersReportStartEvent");
            reportsUpdateEnd_EventHandler.EplanEvent += Event;
        }

        private void Event(IEventParameter pieventparameter)
        {
            ReportProvider reportProvider = new ReportProvider();
            reportProvider.Create();
        }
    }
}
