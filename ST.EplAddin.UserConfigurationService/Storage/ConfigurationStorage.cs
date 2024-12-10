using ST.EplAddin.UserConfigurationService.Models;
using System.Collections.Generic;
using System.Linq;

namespace ST.EplAddin.UserConfigurationService.Storage
{
    internal class ConfigurationStorage
    {
        private List<Scheme> _schemes;
        public ConfigurationStorage()
        {
            _schemes = new List<Scheme>();
            //инициализировать _schemes
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


        public List<Scheme> GetData()
        {
            return _schemes ?? new List<Scheme>();
        }
    }
}
