using Eplan.EplApi.ApplicationFramework;
using System;
using EventHandler = Eplan.EplApi.ApplicationFramework.EventHandler;

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
            reportsEndListener.Sign();

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
            reportsEndListener.DeSign();
            return true;
        }
    }
    public class ReportsEndListener
    {
        public EventHandler reportsUpdateEnd_EventHandler;
        public EventHandler jumpersUpdateEnd_EventHandler;
        public EventHandler jumpersUpdateEnd1_EventHandler;

        public ReportsEndListener()
        {
            reportsUpdateEnd_EventHandler = new EventHandler("onActionEnd.String.XFgUpdateEvaluationAction");
            jumpersUpdateEnd_EventHandler = new EventHandler("onActionEnd.String.XFgEvaluateProjectAction");
            jumpersUpdateEnd1_EventHandler = new EventHandler("onActionEnd.String.EsSwitchConnectionUpdate");
            jumpersUpdateEnd_EventHandler.EplanEvent += Event2;
            jumpersUpdateEnd1_EventHandler.EplanEvent += Event3;
        }

        private void Event(IEventParameter pieventparameter)
        {
            ReportProvider reportProvider = new ReportProvider();
            reportProvider.Create();
        }
        private void Event2(IEventParameter pieventparameter)
        {
            ReportProvider reportProvider = new ReportProvider();
            reportProvider.Create();
            // var terminalsToRemove = TerminalsRepository.GetInstance().GetAllSavedTerminals();
            // terminalsToRemove.ForEach(x => x.Remove());
        }
        private void Event3(IEventParameter pieventparameter)
        {
            Console.WriteLine("cd");

        }

        public void Sign()
        {
            reportsUpdateEnd_EventHandler.EplanEvent += Event;
        }

        public void DeSign()
        {
            reportsUpdateEnd_EventHandler.EplanEvent -= Event;

        }
    }
}
