using ST.EplAddin.UserConfigurationService.Models;
using ST.EplAddin.UserConfigurationService.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace ST.EplAddin.UserConfigurationService.Storage
{
    public class ConfigurationStorage
    {
        private ObservableCollection<Scheme> _schemes;
        private static ConfigurationStorage _instance;
        private static object syncRoot = new Object();
        public static ConfigurationStorage Instance()
        {
            if (_instance == null)
            {
                lock (syncRoot)
                {
                    if (_instance == null)
                        _instance = new ConfigurationStorage();
                }
            }
            return _instance;
        }

        private ConfigurationStorage()
        {
            Initialize();
        }
        private void Initialize()
        {
            _schemes = JsonProvider<ObservableCollection<Scheme>>.GetData() ?? new ObservableCollection<Scheme>();
        }
        public void Save(Scheme scheme)
        {
            var schemeInStorage = _schemes.FirstOrDefault(x => x.Name == scheme.Name);
            if (schemeInStorage != null)
            {
                schemeInStorage.Description = scheme.Description;
                schemeInStorage.Catalog = scheme.Catalog;
                schemeInStorage.Database = scheme.Database;

            }
            else if (!string.IsNullOrEmpty(scheme.Name) && !_schemes.Contains(scheme))
            {
                _schemes.Add(scheme);
            }

            JsonProvider<Scheme>.SaveData(_schemes);
        }

        public void Remove(string Name)
        {
            var itemToRemove = _schemes.FirstOrDefault(x => x.Name == Name);
            if (itemToRemove != null)
            {
                _schemes.Remove(itemToRemove);
            }
            JsonProvider<Scheme>.SaveData(_schemes);
        }
        public void Remove(Scheme scheme)
        {
            if (scheme != null)
            {
                _schemes.Remove(scheme);
            }
            JsonProvider<Scheme>.SaveData(_schemes);
        }

        public ObservableCollection<Scheme> GetAll()
        {
            return _schemes;
        }
        public bool TryGetSchemeByName(string Name, out Scheme scheme)
        {
            scheme = _schemes.FirstOrDefault(x => x.Name == Name);
            return scheme != null;
        }
    }
}
