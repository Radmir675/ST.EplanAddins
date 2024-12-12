using ST.EplAddin.UserConfigurationService.Models;
using ST.EplAddin.UserConfigurationService.Storage;
using System.Collections.ObjectModel;

namespace ST.EplAddin.UserConfigurationService.ViewModels
{
    internal class SchemesVM : ViewModel
    {
        private readonly string _catalog;
        private readonly string _database;
        public string Description { get; set; }
        public string Name { get; set; }

        public ObservableCollection<Scheme> elements { get; set; }

        public SchemesVM()
        {

        }
        public SchemesVM(string catalog, string database)
        {
            _catalog = catalog;
            _database = database;
            elements = ConfigurationStorage.Instance.GetAll();
        }

        public ObservableCollection<Scheme> Collection { get; set; } = new();

        public Scheme SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (Equals(value, _selectedItem)) return;
                _selectedItem = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand _okCommand;
        private Scheme _selectedItem;

        public RelayCommand OkCommand
        {
            get
            {
                return _okCommand ??= new RelayCommand(obj =>
                {
                    var configurationStorage = ConfigurationStorage.Instance;
                    var newScheme = new Scheme()
                    {
                        Catalog = _catalog,
                        Database = _database,
                        Description = Description,
                        Name = Name
                    };
                    configurationStorage.Save(newScheme);
                }, (_) => ConfigurationStorage.Instance.TryGetSchemeByName(Name, out Scheme sheme));
            }
        }

    }
}
