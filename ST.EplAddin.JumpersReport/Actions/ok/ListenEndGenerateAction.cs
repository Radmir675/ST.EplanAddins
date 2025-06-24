using Eplan.EplApi.ApplicationFramework;

namespace ST.EplAddin.JumpersReport;

public class ListenEndGenerateAction
{
    public EventHandler onPostCreateEvaluation_EventHandler;

    public ListenEndGenerateAction()
    {
        onPostCreateEvaluation_EventHandler = new EventHandler("onPostCreateEvaluation");  //обновление отчета через форму Генерировать "Шестеренка" окончание
        onPostCreateEvaluation_EventHandler.EplanEvent += OnPostCreateEvaluationEvent;
    }
    private void OnPostCreateEvaluationEvent(IEventParameter eventParameter)
    {
        //using (SafetyPoint safetyPoint = SafetyPoint.Create())
        //{
        //    ActionProvider actionProvider = new ActionProvider();
        //    actionProvider.Execute();
        //    safetyPoint.Commit();
        //}
    }
}