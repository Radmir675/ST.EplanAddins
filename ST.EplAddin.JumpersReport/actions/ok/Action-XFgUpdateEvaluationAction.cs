using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.DataModel;
using ST.EplAddin.JumpersReport.Providers;

//https://www.eplan.help/en-us/Infoportal/Content/api/2.9/API_REPORTS_MODIFICATION.html
namespace ST.EplAddin.JumpersReport
{
    //TODO:Разделить клеммники по местам установки
    //TODO:Нужно сортировку по местам установки еще вытащить из свойств
    public class Action_XFgUpdateEvaluationAction : IEplAction
    {
        private const string ACTION_NAME = "XFgUpdateEvaluationAction";  //Сервисные программы > Отчеты > Обновить
        private const string EPLAN_ACTION_NAME = "PROJECT.FormGeneratorGui.Templates.PxfForm_TERMINALDIAGRAM";

        public void GetActionProperties(ref ActionProperties actionProperties) { }
        public bool OnRegister(ref string Name, ref int Ordinal)
        {
            Name = ACTION_NAME;
            Ordinal = 98;
            return true;

        }
        public bool Execute(ActionCallingContext oActionCallingContext)
        {
            using (SafetyPoint safetyPoint = SafetyPoint.Create())
            {
                ActionProvider actionProvider = new ActionProvider(EPLAN_ACTION_NAME);
                actionProvider.Execute();
                safetyPoint.Commit();
            }
            ActionManager oMng = new ActionManager();
            Eplan.EplApi.ApplicationFramework.Action oBaseAction = oMng.FindBaseAction(this, true);
            string props = oBaseAction.ActionProperties.ToString();
            return oBaseAction.Execute(oActionCallingContext);
        }
    }
}
