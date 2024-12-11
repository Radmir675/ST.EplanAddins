using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.Base;
using ST.EplAddin.UserConfigurationService.Models;
using ST.EplAddin.UserConfigurationService.ViewModels;
using ST.EplAddin.UserConfigurationService.Views;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace ST.EplAddin.UserConfigurationService
{
    public class ConfigurationAction : IEplAction
    {
        private const string _catalogPath = "USER.ModalDialogs.PathsScheme";
        private const string _databasePath = "USER.PartSelectionGui.DataSourceScheme";
        public static string actionName = "UserConfigurationService";
        private EplanConfigurationShemes shemes = new();

        public bool OnRegister(ref string Name, ref int Ordinal)
        {
            Name = actionName;
            Ordinal = 99;
            return true;
        }

        public bool Execute(ActionCallingContext oActionCallingContext)
        {
            GetCatalogData();
            GetPartsDatabase();
            GetCurrentDatabase();
            GetCurrentCatalog();

            var dialogResult = new ConfigurationView() { DataContext = new ConfigurationVM(shemes) }.ShowDialog();
            if (!dialogResult.HasValue || dialogResult.Value != true) return false;
            SetDatabase();
            SetCatalogData();
            return true;
        }

        #region DataBase
        private void GetPartsDatabase()
        {
            var collection = new StringCollection();
            SettingNode settingNode = new SettingNode(_databasePath);
            settingNode.GetListOfNodes(ref collection, false);
            shemes.DatabaseList = new ObservableCollection<string>(collection.Cast<string>());
        }

        public void GetCurrentDatabase()
        {
            SchemeSetting oSchemeSetting = new SchemeSetting();
            oSchemeSetting.Init(_databasePath);
            shemes.CurrentDatabase = oSchemeSetting.GetLastUsed();
        }

        private void SetDatabase()
        {
            SchemeSetting oSchemeSetting = new SchemeSetting();
            oSchemeSetting.Init(_databasePath);
            if (oSchemeSetting.CheckIfSchemeExists(shemes.CurrentDatabase))
            {
                oSchemeSetting.SetLastUsed(shemes.CurrentDatabase);
            }
        }
        #endregion

        #region Catalog

        private void GetCatalogData()
        {
            var collection = new StringCollection();
            SettingNode settingNode = new SettingNode((_catalogPath));
            settingNode.GetListOfNodes(ref collection, false);
            shemes.Catalogs = new ObservableCollection<string>(collection.Cast<string>());
        }

        private void SetCatalogData()
        {
            SchemeSetting oSchemeSetting = new SchemeSetting();
            oSchemeSetting.Init(_catalogPath);
            if (oSchemeSetting.CheckIfSchemeExists(shemes.CurrentCatalog))
            {
                oSchemeSetting.SetLastUsed(shemes.CurrentCatalog);
            }
        }
        public void GetCurrentCatalog()
        {
            SchemeSetting oSchemeSetting = new SchemeSetting();
            oSchemeSetting.Init(_catalogPath);
            shemes.CurrentCatalog = oSchemeSetting.GetLastUsed();
        }
        #endregion

        public void GetActionProperties(ref ActionProperties actionProperties)
        {

        }
    }
}
