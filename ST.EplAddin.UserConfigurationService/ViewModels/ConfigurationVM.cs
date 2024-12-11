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

        public string CurrentCatalog
        {
            get => _currentCatalog;
            set
            {
                if (value == _currentCatalog) return;
                _currentCatalog = value;
                OnPropertyChanged();
                //OnPropertyChanged(nameof(OkCommand));
                //OnPropertyChanged(nameof(CreateCommand));
            }
        }

        public string CurrentDatabase
        {
            get => _currentDatabase;
            set
            {
                if (value == _currentDatabase) return;
                _currentDatabase = value;
                OnPropertyChanged();
                //OnPropertyChanged(nameof(OkCommand));
                //OnPropertyChanged(nameof(CreateCommand));
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                if (value == _description) return;
                _description = value;
                OnPropertyChanged();
            }
        }

        public string SelectedSсheme
        {
            get => _selectedSсheme;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _selectedSсheme = value;
                    UpdateSchemeSettings(value);
                    OnPropertyChanged();
                }
            }
        }

        private void UpdateSchemeSettings(string name)
        {
            var result = storage.TryGetSchemeByName(name, out var scheme);
            if (result == false)
            {
                _selectedSсheme = "Не определено";
                CurrentCatalog = EplanConfiguration.CurrentCatalog;
                CurrentDatabase = EplanConfiguration.CurrentDatabase;
                Description = "";
                return;
            }

            CurrentCatalog = scheme.Catalog;
            CurrentDatabase = scheme.Database;
            Description = scheme?.Description ?? "";
        }

        public ObservableCollection<string> AllCatalogs { get; set; }
        public ObservableCollection<string> AllDatabases { get; set; }
        public ObservableCollection<string> SсhemesCollection { get; set; } = new();

        public ConfigurationVM(EplanConfigurationShemes eplanConfiguration)
        {
            storage = ConfigurationStorage.Instance;
            EplanConfiguration = eplanConfiguration;
            SelectedSсheme = Properties.Settings.Default.LastScheme;
            AllCatalogs = EplanConfiguration.Catalogs;
            AllDatabases = eplanConfiguration.DatabaseList;
            SсhemesCollection = new ObservableCollection<string>(storage.GetAll().Select(x => x.Name));
        }

        public ConfigurationVM()
        {
            storage = ConfigurationStorage.Instance;
        }

        #region Commands

        private RelayCommand _okCommand;
        private RelayCommand _createCommand;
        private RelayCommand _removeCommand;
        private RelayCommand _saveCommand;
        private string _selectedSсheme;
        private string _description;
        private string _currentCatalog;
        private string _currentDatabase;

        public RelayCommand OkCommand
        {
            get
            {
                return _okCommand ??= new RelayCommand(obj =>
                {
                    EplanConfiguration.CurrentCatalog = CurrentCatalog;
                    EplanConfiguration.CurrentDatabase = CurrentDatabase;
                    ConfigurationStorage.Instance.Save(GetCurrentConfiguration());
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
                        UpdateCollectionStorage();
                        SelectedSсheme = viewModel.Name;
                    }
                });
            }
        }

        private void UpdateCollectionStorage()
        {
            SсhemesCollection.Clear();
            var storageElements = storage.GetAll().Select(x => x.Name);
            foreach (var item in storageElements)
            {
                SсhemesCollection.Add(item);
            }
        }

        public RelayCommand RemoveCommand
        {
            get
            {
                return _removeCommand ??= new RelayCommand(obj =>
                {


                    var result = MessageBox.Show(
                        "Вы действительно хотите обновить данные схему?",
                        "Обновление",
                        MessageBoxButton.OKCancel,
                        MessageBoxImage.Question);
                    if (result == MessageBoxResult.OK)
                        storage.Remove(SelectedSсheme);
                    UpdateCollectionStorage();
                    SelectedSсheme = SсhemesCollection.FirstOrDefault();
                }, (_) =>
                    SсhemesCollection.Contains(SelectedSсheme) && !string.IsNullOrEmpty(SelectedSсheme));

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
                    if (result == MessageBoxResult.OK)
                    {
                        storage.Save(newScheme);
                    }
                    UpdateCollectionStorage();
                    SelectedSсheme = newScheme.Name;
                }, (_) =>
                    SсhemesCollection.Contains(SelectedSсheme) && !string.IsNullOrEmpty(SelectedSсheme));
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
