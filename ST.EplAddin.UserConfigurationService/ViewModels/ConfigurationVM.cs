using ST.EplAddin.UserConfigurationService.Models;
using ST.EplAddin.UserConfigurationService.Storage;
using ST.EplAddin.UserConfigurationService.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace ST.EplAddin.UserConfigurationService.ViewModels
{
    internal class ConfigurationVM : ViewModel
    {
        public EplanConfigurationShemes EplanConfiguration { get; set; }
        private readonly ConfigurationStorage storage;
        public string CurrentCatalog { get; set; }
        public string CurrentDatabase { get; set; }
        public string Description { get; set; }

        public string SelectedSсheme
        {
            get => _selectedSсheme;
            set
            {
                _selectedSсheme = value;
                if (!string.IsNullOrEmpty(value))
                {
                    UpdateSchemeSettings(value);
                }
            }
        }

        private void UpdateSchemeSettings(string name)
        {
            var result = storage.TryGetSchemeByName(name, out var scheme);
            if (result == false)
            {
                //storage.Remove(name);
                //CurrentCatalog = EplanConfiguration.CurrentCatalog;
                //CurrentDatabase = EplanConfiguration.CurrentDatabase;
                return;
            }
            CurrentCatalog = scheme.Catalog;
            CurrentDatabase = scheme.Database;
            Description = scheme?.Description ?? "";
        }

        public ObservableCollection<string> AllCatalogs { get; set; }
        public ObservableCollection<string> AllDatabases { get; set; }
        public ObservableCollection<string> SсhemesCollection { get; set; }

        public ConfigurationVM(EplanConfigurationShemes eplanConfiguration)
        {
            storage = new ConfigurationStorage();
            EplanConfiguration = eplanConfiguration;

            SelectedSсheme = Properties.Settings.Default.LastScheme;
            UpdateSchemeSettings(SelectedSсheme);


            AllCatalogs = EplanConfiguration.Catalogs;
            AllDatabases = eplanConfiguration.DatabaseList;
            SсhemesCollection = new ObservableCollection<string>(storage.GetAll().Select(x => x.Name));
        }

        public ConfigurationVM()
        {
            storage = new ConfigurationStorage();
        }

        #region Commands

        private RelayCommand _okCommand;
        private RelayCommand _createCommand;
        private RelayCommand _removeCommand;
        private RelayCommand _saveCommand;
        private string _selectedSсheme;

        public RelayCommand OkCommand
        {
            get
            {
                return _okCommand ??= new RelayCommand(obj =>
                {
                    EplanConfiguration.CurrentCatalog = CurrentCatalog;
                    EplanConfiguration.CurrentDatabase = CurrentDatabase;
                    Properties.Settings.Default.LastScheme = SelectedSсheme;
                });
            }
        }
        public RelayCommand CreateCommand
        {
            get
            {
                return _createCommand ??= new RelayCommand(obj =>
                {
                    var viewModel = new SchemesVM(CurrentCatalog, CurrentDatabase);
                    var dialogResult = new SchemesView()
                    {
                        DataContext = viewModel
                    }.ShowDialog();
                    if (dialogResult != null && dialogResult.Value == true)
                    {
                        SelectedSсheme = viewModel.Name;

                    }
                });
            }
        }

        public RelayCommand RemoveCommand
        {
            get
            {
                return _removeCommand ??= new RelayCommand(obj =>
                {
                    storage.Remove(SelectedSсheme);
                }, (_) => SelectedSсheme != null);
                //над обновить данные
            }
        }
        public RelayCommand SaveCommand
        {
            get
            {
                return _saveCommand ??= new RelayCommand(obj =>
                {
                    var newScheme = GetCurrentConfiguration();
                    var result = MessageBox.Show(
                        "Вы действительно хотите обновить данные схему?",
                        "Обновление",
                        MessageBoxButton.OKCancel,
                        MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                        storage.Save(newScheme);
                });
            }
        }

        #endregion
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
    }
}
