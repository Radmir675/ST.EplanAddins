using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.Base;
using System.Collections.Specialized;

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
            var collection = new StringCollection();

            SchemeSetting oSchemeSetting = new SchemeSetting();
            oSchemeSetting.Init("USER.PartSelectionGui.DataSourceScheme");
            var count = oSchemeSetting.GetCount();
            SettingNode settingNode = new SettingNode("USER.PartSelectionGui.DataSourceScheme");
            settingNode.GetListOfNodes(ref collection, false);







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
