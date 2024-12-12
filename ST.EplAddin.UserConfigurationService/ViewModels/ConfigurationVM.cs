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

        public ConfigurationVM(EplanConfigurationShemes eplanConfiguration)
        {

            EplanConfiguration = eplanConfiguration;
            AllCatalogs = EplanConfiguration.Catalogs;
            AllDatabases = EplanConfiguration.DatabaseList;
            var f = SetLastSelectedScheme();
            SсhemesCollection = ConfigurationStorage.Instance.GetAll();
            CurrentScheme = f;
        }

        private Scheme SetLastSelectedScheme()
        {
            var schemeName = Properties.Settings.Default.LastScheme;
            var isExist = ConfigurationStorage.Instance.TryGetSchemeByName(schemeName, out var reScheme);
            return isExist ? reScheme : SetUndefined();
        }

        private Scheme SetUndefined()
        {
            var newS = new Scheme()
            {
                Catalog = EplanConfiguration.CurrentCatalog,
                Database = EplanConfiguration.CurrentDatabase,
                Description = "",
            };
            SсhemesCollection.Add(newS);
            return newS;
        }

        public ConfigurationVM()
        {

        }

        #region Commands

        private RelayCommand _okCommand;
        private RelayCommand _createCommand;
        private RelayCommand _removeCommand;
        private RelayCommand _saveCommand;
        private Scheme _currentScheme;
        private ObservableCollection<Scheme> _sсhemesCollection = new();


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
                    var viewModel = new SchemesVM(CurrentScheme.Catalog, CurrentScheme.Database);
                    var dialogResult = new SchemesView()
                    {
                        DataContext = viewModel
                    }.ShowDialog();
                    if (dialogResult is not true) return;

                    UpdateCollectionStorage();

                    var isExist = ConfigurationStorage.Instance.TryGetSchemeByName(viewModel.Name, out Scheme newScheme);
                    if (!isExist) return;
                    CurrentScheme = newScheme;
                });
            }
        }

        private void UpdateCollectionStorage()
        {
            SсhemesCollection.Clear();
            var storageElements = ConfigurationStorage.Instance.GetAll();
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
                        ConfigurationStorage.Instance.Remove(CurrentScheme);
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
                        ConfigurationStorage.Instance.Save(CurrentScheme);
                    }
                    UpdateCollectionStorage();
                });
            }
        }

        #endregion

    }
}
