using Eplan.EplApi.ApplicationFramework;

//https://www.eplan.help/en-us/Infoportal/Content/api/2.9/API_REPORTS_MODIFICATION.html
namespace ST.EplAddin.JumpersReport
{
    public class Action_GfDlgMgrActionIGfWind : IEplAction
    {
        private const string ACTION_NAME = "GfDlgMgrActionIGfWind"; // /function:EvaluateTemplate //Контекстное меню обновить в генераторе отчетов

        public void GetActionProperties(ref ActionProperties actionProperties) { }
        public bool OnRegister(ref string Name, ref int Ordinal)
        {
            Name = ACTION_NAME;
            Ordinal = 98;
            return true;

        }
        public bool Execute(ActionCallingContext oActionCallingContext)
        {
            string functionName = "";
            int count = oActionCallingContext.GetParameterCount();
            string[] contextParams = oActionCallingContext.GetParameters();
            string[] contextStrings = oActionCallingContext.GetStrings();

            oActionCallingContext.GetParameter("function", ref functionName);
            if (functionName == "EvaluateTemplate")
            {
                int a = 0;
            }

            ActionManager oMng = new ActionManager();
            Eplan.EplApi.ApplicationFramework.Action oBaseAction = oMng.FindBaseAction(this, true);
            string props = oBaseAction.ActionProperties.ToString();
            return oBaseAction.Execute(oActionCallingContext);
        }
    }
}
