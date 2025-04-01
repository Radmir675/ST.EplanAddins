using Eplan.EplApi.ApplicationFramework;
using System;
using System.Collections.Generic;
using System.Linq;

//https://www.eplan.help/en-us/Infoportal/Content/api/2.9/API_REPORTS_MODIFICATION.html
namespace ST.EplAddin.JumpersReport
{
    public class Action : IEplAction
    {
        private const string ACTION_NAME = "JumpersReport";
        private string resultData;
        public static bool IsOtherReportsUpdated = false;
        public void GetActionProperties(ref ActionProperties actionProperties) { }
        public bool OnRegister(ref string Name, ref int Ordinal)
        {
            Name = ACTION_NAME;
            Ordinal = 98;
            return true;

        }
        public bool Execute(ActionCallingContext oActionCallingContext)
        {
            if (!IsOtherReportsUpdated)
            {
                return false;
            }

            int count = oActionCallingContext.GetParameterCount();
            string[] contextParams = oActionCallingContext.GetParameters();
            string[] contextStrings = oActionCallingContext.GetStrings();

            string project = "";
            string mode = "";
            string objects = "";
            string pages = "";
            string order = "";
            string filter = "";
            string mainfunction = "";
            oActionCallingContext.GetParameter("project", ref project);
            oActionCallingContext.GetParameter("mode", ref mode);
            oActionCallingContext.GetParameter("objects", ref objects);
            oActionCallingContext.GetParameter("pages", ref pages);
            oActionCallingContext.GetParameter("order", ref order);
            oActionCallingContext.GetParameter("filter", ref filter);
            oActionCallingContext.GetParameter("mainfunction", ref mainfunction);


            if (mode == "ModifyObjectList")
            {
                objects = "";

                var terminals = TerminalsRepository.GetInstance().GetData();

                if (!terminals.Any()) return false;

                List<string> resultList =
                    terminals.Where(s => s != null).Select(s => s.ToStringIdentifier()).ToList();
                resultData = String.Join(";", resultList);
                objects = resultData;
                // oActionCallingContext.AddParameter("objects", resultData);
                return true;
            }
            if (mode == "Finish")
            {
                IsOtherReportsUpdated = false;
                return true;
            }
            return false;
        }
    }
}
