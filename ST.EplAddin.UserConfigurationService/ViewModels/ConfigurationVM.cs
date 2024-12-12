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
        public Scheme CurrentScheme
        {
            get => _currentScheme;
            set
            {
                if (Equals(value, _currentScheme)) return;
                _currentScheme = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<string> AllCatalogs { get; set; }
        public ObservableCollection<string> AllDatabases { get; set; }
        public ObservableCollection<Scheme> SсhemesCollection
        {
            get => _sсhemesCollection;
            set
            {
                if (Equals(value, _sсhemesCollection)) return;
                _sсhemesCollection = value;
                OnPropertyChanged();

            }
        }
        public ConfigurationVM() { }
        public ConfigurationVM(EplanConfigurationShemes eplanConfiguration)
        {
            storage = ConfigurationStorage.Instance();
            EplanConfiguration = eplanConfiguration;
            AllCatalogs = EplanConfiguration.Catalogs;
            AllDatabases = EplanConfiguration.DatabaseList;
            SсhemesCollection = storage.GetAll();
            CurrentScheme = SetLastSelectedScheme();
        }

        private Scheme SetLastSelectedScheme()
        {
            var schemeName = Properties.Settings.Default.LastScheme;
            var isExist = storage.TryGetSchemeByName(schemeName, out var reScheme);
            if (EplanConfiguration.CurrentCatalog != reScheme?.Catalog || EplanConfiguration.CurrentDatabase != reScheme?.Database)
            {
                isExist = false;
            }
            return isExist ? reScheme : SetUndefined();
        }

        private Scheme SetUndefined()
        {
            var newS = new Scheme()
            {
                Catalog = EplanConfiguration.CurrentCatalog,
                Database = EplanConfiguration.CurrentDatabase,
                Description = "",
                Name = "Не определено"
            };
            // SсhemesCollection.Add(newS);
            return newS;
        }
        #region Commands

        private RelayCommand _okCommand;
        private RelayCommand _createCommand;
        private RelayCommand _removeCommand;
        private RelayCommand _saveCommand;
        private Scheme _currentScheme;
        private ObservableCollection<Scheme> _sсhemesCollection = new();
        private readonly ConfigurationStorage storage;

        public RelayCommand OkCommand
        {
            get
            {
                return _okCommand ??= new RelayCommand(obj =>
                {
                    EplanConfiguration.CurrentCatalog = CurrentScheme.Catalog; //установка каталогов для Eplan
                    EplanConfiguration.CurrentDatabase = CurrentScheme.Database;
                    Properties.Settings.Default.LastScheme = CurrentScheme.Name;
                });
            }
        }
        public RelayCommand CreateCommand
        {
            get
            {
                return _createCommand ??= new RelayCommand(obj =>
                {
                    var viewModel = new SchemesVM(CurrentScheme.Catalog, CurrentScheme.Database);
                    var dialogResult = new SchemesView()
                    {
                        DataContext = viewModel
                    }.ShowDialog();
                    if (dialogResult is not true) return;

                    UpdateCollectionStorage();

                    var isExist = storage.TryGetSchemeByName(viewModel.Name, out Scheme newScheme);
                    if (!isExist) return;
                    CurrentScheme = newScheme;
                });
            }
        }

        private void UpdateCollectionStorage()
        {
            SсhemesCollection = storage.GetAll();
            if (SсhemesCollection.Count == 0)
            {
                SetUndefined();
            }
        }

        public RelayCommand RemoveCommand
        {
            get
            {
                return _removeCommand ??= new RelayCommand(obj =>
                {
                    var result = MessageBox.Show(
                        "Вы действительно хотите удалить данную схему?",
                        "Обновление",
                        MessageBoxButton.OKCancel,
                        MessageBoxImage.Question);
                    if (result == MessageBoxResult.OK)
                        storage.Remove(CurrentScheme);
                    UpdateCollectionStorage();
                    CurrentScheme = SсhemesCollection?.FirstOrDefault();
                    if (CurrentScheme == null)
                    {
                        CurrentScheme = SetUndefined();
                    }
                }, (_) =>
                    SсhemesCollection.Contains(CurrentScheme) && CurrentScheme != null);
            }
        }
        public RelayCommand SaveCommand
        {
            get
            {
                return _saveCommand ??= new RelayCommand(obj =>
                {
                    var currentSchemeName = CurrentScheme.Name;
                    storage.Save(CurrentScheme);
                    UpdateCollectionStorage();
                    storage.TryGetSchemeByName(currentSchemeName, out Scheme newScheme);
                    CurrentScheme = newScheme;
                }, (_) => SсhemesCollection.Any(x => x.Name == CurrentScheme?.Name));
            }
        }

        #endregion

    }
}
