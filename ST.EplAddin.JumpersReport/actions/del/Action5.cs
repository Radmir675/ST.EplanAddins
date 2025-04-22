using Eplan.EplApi.ApplicationFramework;
using System;
using System.Collections.Generic;
using System.Linq;

//https://www.eplan.help/en-us/Infoportal/Content/api/2.9/API_REPORTS_MODIFICATION.html
namespace ST.EplAddin.JumpersReport
{
    public class Action5 : IEplAction
    {
        private const string ACTION_NAME = "FgCreateEvaluationProjectAction";
        private string resultData;
        public static bool IsOtherReportsUpdated = false;

        //AVXFgCreateEvaluationProjectAction
        //VXFgUpdateEvaluationAction
        //DMEvaluationBlock
        //VXFgCompareEvaluationsTestAction
        //AVXFgCompareEvaluations
        //AVXFgExtEvaluations
        //AVXGedEvaluationTab@@ 

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
            return false;
        }
    }
}
