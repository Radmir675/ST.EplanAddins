using Eplan.EplApi.ApplicationFramework;

namespace ST.EplAddin.JumpersReport
{
    public class Action : IEplAction
    {
        public static string actionName = "JumpersReport";

        public void GetActionProperties(ref ActionProperties actionProperties)
        {

        }
        public bool OnRegister(ref string Name, ref int Ordinal)
        {
            Name = actionName;
            Ordinal = 99;
            return true;
        }

        public bool Execute(ActionCallingContext oActionCallingContext)
        {
            int count = oActionCallingContext.GetParameterCount();
            string[] contextParams = oActionCallingContext.GetParameters();
            string[] contextStrings = oActionCallingContext.GetStrings();

            string project = "";
            string mode = "";
            string objects = "";
            string pages = "";
            string order = "";
            string filter = "";

            oActionCallingContext.GetParameter("project", ref project);
            oActionCallingContext.GetParameter("mode", ref mode);
            oActionCallingContext.GetParameter("objects", ref objects);
            oActionCallingContext.GetParameter("pages", ref pages);
            oActionCallingContext.GetParameter("order", ref order);
            oActionCallingContext.GetParameter("filter", ref filter);

            if (mode == "ModifyObjectList")
            {

            }

            return true;
        }




    }
}
