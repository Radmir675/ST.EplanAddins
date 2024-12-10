using ST.EplAddin.UserConfigurationService.Models;
using ST.EplAddin.UserConfigurationService.Storage;
using System.Collections.ObjectModel;

namespace ST.EplAddin.UserConfigurationService.ViewModels
{
    internal class ConfigurationVM : ViewModel
    {
        public UserConfigurationShemes UserConfiguration { get; set; }
        private readonly ConfigurationStorage storage;
        public string CurrentCatalog { get; set; }
        public string CurrentDatabase { get; set; }
        public string SelectedSheme { get; set; }
        public ObservableCollection<string> AllCatalogs { get; set; }
        public ObservableCollection<string> AllDatabases { get; set; }
        public ObservableCollection<string> ShemesCollection { get; set; }

        public ConfigurationVM(UserConfigurationShemes userConfiguration)
        {
            storage = new ConfigurationStorage();
            UserConfiguration = userConfiguration;

            CurrentCatalog = userConfiguration.CurrentCatalog;
            CurrentDatabase = userConfiguration.CurrentDatabase;
            AllCatalogs = UserConfiguration.Catalogs;
            AllDatabases = userConfiguration.DatabaseList;
            ShemesCollection = storage.GetData();

        }
        public ConfigurationVM() { }

        #region Commands

        private RelayCommand _okCommand;
        private RelayCommand _createCommand;
        private RelayCommand _removeCommand;
        private RelayCommand _saveCommand;
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
        public RelayCommand CreateCommand
        {
            get
            {
                return _createCommand ??= new RelayCommand(obj =>
                {
                    storage.Add();
                });
            }
        }
        public RelayCommand RemoveCommand
        {
            get
            {
                return _removeCommand ??= new RelayCommand(obj =>
                {
                    storage.Remove(SelectedSheme);
                });
            }
        }
        public RelayCommand SaveCommand
        {
            get
            {
                return _saveCommand ??= new RelayCommand(obj =>
                {
                    storage.Save();
                });
            }
        }

        #endregion
    }
}
