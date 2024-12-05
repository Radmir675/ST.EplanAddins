using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.Base;

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
            SetPartsDatabase();
            SetDataBaseCatalogue();
            return true;
        }

        private static void SetPartsDatabase()
        {
            SchemeSetting oSchemeSetting = new SchemeSetting();
            oSchemeSetting.Init("USER.ModalDialogs.PathsScheme");
            // string strSchemeName = "Google_Drive_Config";
            string strSchemeName = "Standard";
            var s = oSchemeSetting.GetNodeHandle();
            var t = s.GetParentNode().GetNodePath();
            if (oSchemeSetting.CheckIfSchemeExists(strSchemeName))
            {
                oSchemeSetting.SetLastUsed(strSchemeName);
                var res = oSchemeSetting.GetLastUsed();
            }
        }

        private void SetDataBaseCatalogue()
        {
            SchemeSetting oSchemeSetting = new SchemeSetting();
            oSchemeSetting.Init("USER.PartSelectionGui.DataSourceScheme");
            string strSchemeName = "Google_Drive_Config";

            if (oSchemeSetting.CheckIfSchemeExists(strSchemeName))
            {
                oSchemeSetting.SetLastUsed(strSchemeName);
            }
        }

        public void GetActionProperties(ref ActionProperties actionProperties)
        {
            throw new System.NotImplementedException();
        }
    }
}
