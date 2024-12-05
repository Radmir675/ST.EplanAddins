using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.HEServices;

namespace ST.EplAddin.UserConfigurationService
{
    public class ConfigurationAction : IEplAction
    {

        public static string actionName = "UserConfigurationService";

        public bool OnRegister(ref string Name, ref int Ordinal)
        {
            Name = actionName;
            Ordinal = 99;
            return true;
        }

        public bool Execute(ActionCallingContext oActionCallingContext)
        {
            SelectionSet selectionSet = new SelectionSet
            {
                LockProjectByDefault = false,
                LockSelectionByDefault = false
            };

            return true;

        }

        public void GetActionProperties(ref ActionProperties actionProperties)
        {
            throw new System.NotImplementedException();
        }
    }
}
