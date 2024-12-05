using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.Base;
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

            SchemeSetting oSchemeSetting = new SchemeSetting();
            oSchemeSetting.Init("USER.ModalDialogs.PathsScheme");
            string strSchemeName = "Google_Drive_Config";
            // string strSchemeName1 = "Standart";
            var reas = oSchemeSetting.GetName();
            var t = oSchemeSetting.GetLocalizedNameSettingPath();
            if (oSchemeSetting.CheckIfSchemeExists(strSchemeName))
            {
                oSchemeSetting.SetScheme("Google_Drive_Config");
            }


            SetDataBaseCatalogue();

            return true;

        }

        private void SetDataBaseCatalogue()
        {
            SchemeSetting oSchemeSetting = new SchemeSetting();
            oSchemeSetting.Init("USER.PartSelectionGui.DataSourceScheme");
            string strSchemeName = "Google_Drive_Config";

            if (oSchemeSetting.CheckIfSchemeExists(strSchemeName))
            {
                oSchemeSetting.SetScheme(strSchemeName);
            }
        }

        public void GetActionProperties(ref ActionProperties actionProperties)
        {
            throw new System.NotImplementedException();
        }
    }
}
