using ST.EplAddin.UserConfigurationService.Models;
using ST.EplAddin.UserConfigurationService.Storage;
using System.Collections.ObjectModel;
using System.Linq;

namespace ST.EplAddin.UserConfigurationService.ViewModels
{
    internal class SchemesVM : ViewModel
    {
        private readonly string _catalog;
        private readonly string _database;

        public readonly ConfigurationStorage storage;

        public string Description
        {
            get => _description;
            set
            {
                if (value == _description) return;
                _description = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(OkCommand));
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                if (value == _name) return;
                _name = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(OkCommand));
            }
        }

        public SchemesVM() { }
        public SchemesVM(string catalog, string database)
        {
            _catalog = catalog;
            _database = database;
            storage = ConfigurationStorage.Instance();
            Collection = storage.GetAll();
        }

        public ObservableCollection<Scheme> Collection { get; set; } = new();

        public Scheme SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (Equals(value, _selectedItem)) return;
                _selectedItem = value;
                Name = _selectedItem?.Name;
                Description = _selectedItem?.Description;
                OnPropertyChanged();
                OnPropertyChanged(nameof(OkCommand));
            }
        }

        private RelayCommand _okCommand;
        private Scheme _selectedItem;
        private string _name;
        private string _description;

        public RelayCommand OkCommand
        {
            get
            {
                return _okCommand ??= new RelayCommand(obj =>
                {
                    var newScheme = new Scheme()
                    {
                        Catalog = _catalog,
                        Database = _database,
                        Description = Description,
                        Name = Name
                    };
                    storage.Save(newScheme);
                }, (_) => !Collection.Select(x => x.Name).Contains(Name) && !string.IsNullOrEmpty(Name));
            }
        }

    }
}
