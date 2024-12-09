using ST.EplAddin.UserConfigurationService.Models;
using System.Collections.ObjectModel;

namespace ST.EplAddin.UserConfigurationService.ViewModels
{
    internal class ConfigurationVM : ViewModel
    {
        public UserConfigurationShemes UserConfiguration { get; set; }
        public string CurrentCatalog { get; set; }
        public string CurrentDatabase { get; set; }
        public ObservableCollection<string> AllCatalogs { get; set; }
        public ObservableCollection<string> AllDatabases { get; set; }
        private RelayCommand _okCommand;


        public ConfigurationVM(UserConfigurationShemes userConfiguration)
        {
            UserConfiguration = userConfiguration;

            CurrentCatalog = userConfiguration.CurrentCatalog;
            CurrentDatabase = userConfiguration.CurrentDatabase;
            AllCatalogs = UserConfiguration.Catalogs;
            AllDatabases = userConfiguration.DatabaseList;
        }
        public ConfigurationVM() { }
        public RelayCommand OkCommand
        {
            get
            {
                return _okCommand ??= new RelayCommand(obj =>
                {
                    UserConfiguration.CurrentCatalog = CurrentCatalog;
                    UserConfiguration.CurrentDatabase = CurrentDatabase;
                });
            }
        }
    }
}
