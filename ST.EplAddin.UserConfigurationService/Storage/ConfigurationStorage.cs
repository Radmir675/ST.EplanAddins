using ST.EplAddin.UserConfigurationService.Models;
using ST.EplAddin.UserConfigurationService.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ST.EplAddin.UserConfigurationService.Storage
{
    internal class ConfigurationStorage
    {
        private ObservableCollection<Scheme> _schemes = new();
        public ConfigurationStorage()
        {
            Initialize();
        }
        private void Initialize()
        {
            _schemes = JsonProvider<ObservableCollection<Scheme>>.GetData();
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
            JsonProvider<Scheme>.SaveData(_schemes);
        }
        public void Remove(List<string> names)
        {
            foreach (var name in names)
            {
                var itemToRemove = _schemes.SingleOrDefault(x => x.Name == name);
                if (itemToRemove != null)
                {
                    _schemes.Remove(itemToRemove);
                }

            }
            JsonProvider<Scheme>.SaveData(_schemes);
        }


        public ObservableCollection<Scheme> GetAll()
        {
            return _schemes;
        }
        public bool TryGetSchemeByName(string Name, out Scheme scheme)
        {
            scheme = _schemes.SingleOrDefault(x => x.Name == Name);
            return scheme != null;
        }
    }
}
