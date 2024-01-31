using Eplan.EplApi.ApplicationFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST.EplAddin.ReportSorting
{

    class OriginalSortAction : IEplAction
    {
        public bool OnRegister(ref string Name, ref int Ordinal)
        {
            Name = "XLbLabelingStart_REMARK";
            //Name = "XLbLabelingStart";
            Ordinal = 99;

            return true;
        }

        public bool Execute(ActionCallingContext oActionCallingContext)
        {
            //PartsListGuiIGfWindSynchronize
            //PartSelectionGuiIGfWindonAssignData

            string function = "", cmdline = "";
            oActionCallingContext.GetParameter("function", ref function);
            oActionCallingContext.GetParameter("_cmdline", ref cmdline);

            int count = oActionCallingContext.GetParameterCount();
            string[] contextParams = oActionCallingContext.GetParameters();
            string[] contextStrings = oActionCallingContext.GetStrings();

            ActionManager oMng = new ActionManager();
            Eplan.EplApi.ApplicationFramework.Action baseAction = oMng.FindBaseAction(this, true);
            return baseAction.Execute(oActionCallingContext);

        }

        public void GetActionProperties(ref ActionProperties actionProperties)
        {
            throw new NotImplementedException();
        }


    }


}
