using ST.EplAddin.UserConfigurationService.Models;
using System.Collections.ObjectModel;

namespace ST.EplAddin.UserConfigurationService.ViewModels
{
    internal class ConfigurationVM : ViewModel
    {
        public UserConfigurationShemes _userConfigurationShemes { get; set; }
        public string CurrentCatalog { get; set; }
        public string CurrentDatabase { get; set; }
        public ObservableCollection<string> AllCatalogs { get; set; }
        public ObservableCollection<string> AllDatabases { get; set; }

        public ConfigurationVM(UserConfigurationShemes userConfigurationShemes)
        {
            _userConfigurationShemes = userConfigurationShemes;

            CurrentCatalog = userConfigurationShemes.CurrentCatalog;
            CurrentDatabase = userConfigurationShemes.CurrentDatabase;
            AllCatalogs = _userConfigurationShemes.Catalogs;
            AllDatabases = userConfigurationShemes.DatabaseList;
        }
        public ConfigurationVM()
        {

        }

    }
}
