using ST.EplAddin.CheckCableAccesorities.Models;
using System.Collections.ObjectModel;

namespace ST.EplAddin.CheckCableAccesorities.Help
{
    class Settings
    {
        public Settings()
        {
            Initialize();
        }

        private ObservableCollection<Part> Parts { get; set; }

        private void Initialize()
        {
            //TODO: настроить
            Parts = new ObservableCollection<Part>()
            {
                new Part(1, ProductGroupType.Common),
                new Part(2, ProductGroupType.ElectricalCableConnection),
                new Part(3, ProductGroupType.MechanicsRoutingAccessories)
            };
        }
        public ObservableCollection<Part> GetData()
        {
            var datafromSettings = JsonProvider<ObservableCollection<Part>>.GetData();
            if (datafromSettings != null)
            {
                return datafromSettings;
            }
            else
            {
                return Parts ?? new ObservableCollection<Part>();
            }
        }
        public void SaveData(ObservableCollection<Part> parts)
        {
            JsonProvider<Part>.SaveData(parts);
        }
    }
}
