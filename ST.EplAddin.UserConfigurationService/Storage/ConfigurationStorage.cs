using ST.EplAddin.UserConfigurationService.Models;
using ST.EplAddin.UserConfigurationService.Services;
using System.Collections.ObjectModel;
using System.Linq;

namespace ST.EplAddin.UserConfigurationService.Storage
{
    internal class ConfigurationStorage
    {
        private ObservableCollection<Scheme> _schemes;
        public ConfigurationStorage()
        {
            _schemes = GetData();
        }
        public void Save(Scheme scheme)
        {
            var schemeInStorage = _schemes.FirstOrDefault(x => x.Name == scheme.Name);
            if (schemeInStorage != null)
            {
                schemeInStorage.Catalog = scheme.Catalog;
                schemeInStorage.Database = scheme.Database;
            }
            else
            {
                _schemes.Add(scheme);
                JsonProvider<Scheme>.SaveData(_schemes);
            }
        }

        public void Remove(string Name)
        {
            var itemToRemove = _schemes.SingleOrDefault(x => x.Name == Name);
            if (itemToRemove != null)
            {
                _schemes.Remove(itemToRemove);
            }
        }


        public ObservableCollection<Scheme> GetData()
        {
            var _schemes = JsonProvider<ObservableCollection<Scheme>>.GetData();
            return _schemes ??= new ObservableCollection<Scheme>();
        }
    }
}
