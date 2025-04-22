using Eplan.EplApi.ApplicationFramework;
using System;
using System.Collections.Generic;
using System.Linq;

//https://www.eplan.help/en-us/Infoportal/Content/api/2.9/API_REPORTS_MODIFICATION.html
namespace ST.EplAddin.JumpersReport
{
    public class Action_XFgEvaluateProjectAction : IEplAction
    {
        private const string ACTION_NAME = "XFgEvaluateProjectAction"; //Сервисные программы > Отчеты > Генерация отчета по проекту
        private string resultData;
        public static bool IsOtherReportsUpdated = false;

        //AVXFgCreateEvaluationProjectAction
        //VXFgUpdateEvaluationAction
        //DMEvaluationBlock
        //VXFgCompareEvaluationsTestAction
        //AVXFgCompareEvaluations
        //AVXFgExtEvaluations
        //AVXGedEvaluationTab@@ 
        //GfDlgMgrActionIGfWind /function:EvaluateTemplate

        public void GetActionProperties(ref ActionProperties actionProperties)
        {
            var PROP = new ActionParameterProperties();
            var t = new ActionParameterProperties();
            t.Set("STReport");
            actionProperties.AddParameter(t);

        }
        public bool OnRegister(ref string Name, ref int Ordinal)
        {
            Name = ACTION_NAME;
            Ordinal = 98;
            return true;

        }
        public bool Execute(ActionCallingContext oActionCallingContext)
        {
            ActionManager oMng = new ActionManager();
            Eplan.EplApi.ApplicationFramework.Action oBaseAction = oMng.FindBaseAction(this, true);
            string props = oBaseAction.ActionProperties.ToString();
            return oBaseAction.Execute(oActionCallingContext);
        }
    }
}
