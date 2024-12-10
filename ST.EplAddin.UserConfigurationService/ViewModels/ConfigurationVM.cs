using ST.EplAddin.UserConfigurationService.Models;
using ST.EplAddin.UserConfigurationService.Storage;
using ST.EplAddin.UserConfigurationService.Views;
using System.Collections.ObjectModel;
using System.Linq;

namespace ST.EplAddin.UserConfigurationService.ViewModels
{
    internal class ConfigurationVM : ViewModel
    {
        public UserConfigurationShemes UserConfiguration { get; set; }
        private readonly ConfigurationStorage storage;
        public string CurrentCatalog { get; set; }
        public string CurrentDatabase { get; set; }
        public string SelectedSсheme { get; set; }
        public string Description { get; set; }
        public ObservableCollection<string> AllCatalogs { get; set; }
        public ObservableCollection<string> AllDatabases { get; set; }
        public ObservableCollection<string> SсhemesCollection { get; set; }

        public ConfigurationVM(UserConfigurationShemes userConfiguration)
        {
            storage = new ConfigurationStorage();
            UserConfiguration = userConfiguration;

            CurrentCatalog = userConfiguration.CurrentCatalog;
            CurrentDatabase = userConfiguration.CurrentDatabase;
            AllCatalogs = UserConfiguration.Catalogs;
            AllDatabases = userConfiguration.DatabaseList;
            SсhemesCollection = new ObservableCollection<string>(storage.GetData().Select(x => x.Name));
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
                    var dialogResult = new SchemesView() { DataContext = new SchemesVM(CurrentCatalog, CurrentDatabase) }.ShowDialog();
                    if (dialogResult == true)
                    {
                        var newScheme = GetCurrentConfiguration();
                        storage.Save(newScheme);
                    }
                });
            }
        }

        private Scheme GetCurrentConfiguration()
        {
            var newScheme = new Scheme()
            {
                Catalog = CurrentCatalog,
                Database = CurrentDatabase,
                Description = Description,
                Name = SelectedSсheme
            };
            return newScheme;
        }

        public RelayCommand RemoveCommand
        {
            get
            {
                return _removeCommand ??= new RelayCommand(obj =>
                {
                    storage.Remove(SelectedSсheme);
                });
            }
        }
        public RelayCommand SaveCommand
        {
            get
            {
                return _saveCommand ??= new RelayCommand(obj =>
                {
                    var newScheme = GetCurrentConfiguration();
                    storage.Save(newScheme);
                });
            }
        }

        #endregion
    }
}
