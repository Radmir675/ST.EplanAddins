using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.HEServices;

namespace ST.EplAddin.Tests
{
    public class Action : IEplAction
    {
        public static string actionName = "EplanTests";
        public bool OnRegister(ref string Name, ref int Ordinal)
        {
            Name = actionName;
            Ordinal = 99;
            return true;
        }

        public bool Execute(ActionCallingContext oActionCallingContext)
        {
            SelectionSet selection = new SelectionSet();
            var project = selection.GetCurrentProject(true);
            project.Properties.PROJ_INSTALLATIONNAME = "cdc";

            return true;
        }


        public void GetActionProperties(ref ActionProperties actionProperties)
        {

        }
    }
}
