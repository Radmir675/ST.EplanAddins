using Eplan.EplApi.ApplicationFramework;
using EventHandler = Eplan.EplApi.ApplicationFramework.EventHandler;

namespace ST.EplAddin.JumpersReport.Actions.ok
{
    internal class JumpersReportEndListener
    {
        public EventHandler onPostCreateEvaluation_EventHandler;
        public JumpersReportEndListener()
        {
            //onPostCreateEvaluation_EventHandler = new EventHandler("onActionEnd.String.JumpersReport1");
            //onPostCreateEvaluation_EventHandler.EplanEvent += OnPostCreateEvaluationEvent;
        }
        private void OnPostCreateEvaluationEvent(IEventParameter eventParameter)
        {

        }
    }
}
