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
        public ObservableCollection<Scheme> SсhemesCollection { get; set; } = new();

        public ConfigurationVM(EplanConfigurationShemes eplanConfiguration)
        {
            storage = ConfigurationStorage.Instance;
            EplanConfiguration = eplanConfiguration;
            CurrentScheme = SetLastSelectedScheme();
            AllCatalogs = EplanConfiguration.Catalogs;
            AllDatabases = EplanConfiguration.DatabaseList;
            SсhemesCollection = new ObservableCollection<Scheme>(storage.GetAll());
        }

        private Scheme SetLastSelectedScheme()
        {
            var schemeName = Properties.Settings.Default.LastScheme;
            var isExist = storage.TryGetSchemeByName(schemeName, out var reScheme);
            return isExist ? reScheme : new Scheme()
            {
                Catalog = EplanConfiguration.CurrentCatalog,
                Database = EplanConfiguration.CurrentDatabase,
                Description = "",
                Name = "Не определено"
            };
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
        private Scheme _currentScheme;



        public RelayCommand OkCommand
        {
            get
            {
                return _okCommand ??= new RelayCommand(obj =>
                {
                    EplanConfiguration.CurrentCatalog = CurrentScheme.Catalog; //установка каталогов для Eplan
                    EplanConfiguration.CurrentDatabase = CurrentScheme.Database;
                    ConfigurationStorage.Instance.Save(CurrentScheme);
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
                    var viewModel = new Scheme()
                    {
                        Catalog = CurrentScheme.Catalog,
                        Database = CurrentScheme.Database
                    };
                    var dialogResult = new SchemesView()
                    {
                        DataContext = viewModel
                    }.ShowDialog();
                    if (dialogResult != null && dialogResult.Value == true)
                    {
                        UpdateCollectionStorage();

                        var isExist = storage.TryGetSchemeByName(viewModel.Name, out Scheme newScheme);
                        if (isExist)
                        {
                            CurrentScheme = newScheme;
                        }
                    }
                });
            }
        }

        private void UpdateCollectionStorage()
        {
            SсhemesCollection.Clear();
            var storageElements = storage.GetAll();
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
                        storage.Remove(CurrentScheme);
                    UpdateCollectionStorage();
                    CurrentScheme = SсhemesCollection.FirstOrDefault();
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

                    var result = MessageBox.Show(
                        "Вы действительно хотите обновить данные схему?",
                        "Обновление",
                        MessageBoxButton.OKCancel,
                        MessageBoxImage.Question);
                    if (result == MessageBoxResult.OK)
                    {
                        storage.Save(CurrentScheme);
                    }
                    UpdateCollectionStorage();
                }, (_) =>
                    SсhemesCollection.Contains(CurrentScheme) && CurrentScheme != null);
            }
        }

        #endregion

    }
}
