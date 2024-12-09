using ST.EplAddin.UserConfigurationService.Models;
using System.Collections.ObjectModel;
using EventHandler = System.EventHandler;

namespace ST.EplAddin.UserConfigurationService.ViewModels
{
    internal class ConfigurationVM : ViewModel
    {
        public event EventHandler OkEvent;
        public UserConfigurationShemes _userConfigurationShemes { get; set; }
        public string CurrentCatalog { get; set; }
        public string CurrentDatabase { get; set; }
        public ObservableCollection<string> AllCatalogs { get; set; }
        public ObservableCollection<string> AllDatabases { get; set; }
        private RelayCommand _okCommand;


        public ConfigurationVM(UserConfigurationShemes userConfigurationShemes)
        {
            _userConfigurationShemes = userConfigurationShemes;

            CurrentCatalog = userConfigurationShemes.CurrentCatalog;
            CurrentDatabase = userConfigurationShemes.CurrentDatabase;
            AllCatalogs = _userConfigurationShemes.Catalogs;
            AllDatabases = userConfigurationShemes.DatabaseList;
        }
        public ConfigurationVM() { }
        public RelayCommand OkCommand
        {
            get
            {
                return _okCommand ??= new RelayCommand(obj =>
                {
                    _userConfigurationShemes.CurrentCatalog = CurrentCatalog;
                    _userConfigurationShemes.CurrentDatabase = CurrentDatabase;

                });
            }
        }
    }
}
