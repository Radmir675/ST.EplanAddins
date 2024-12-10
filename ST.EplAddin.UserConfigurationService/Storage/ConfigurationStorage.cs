using ST.EplAddin.UserConfigurationService.Models;
using System.Collections.Generic;

namespace ST.EplAddin.UserConfigurationService.Storage
{
    internal class ConfigurationStorage
    {
        private List<Scheme> _schemes;
        public ConfigurationStorage()
        {
            //инициализировать
        }
        public void Save(Scheme scheme)
        {

        }

        public void Remove(string Name)
        {

        }

        public void Add()
        {

        }
        public List<Scheme> GetData()
        {
            return _schemes;
        }
    }
}
