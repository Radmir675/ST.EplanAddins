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
            Parts = new ObservableCollection<Part>()
            {
                new Part(1,ProductSubGroupEnum.Electric, ProductGroupEnum.Common,ProductTopGroupEnum.Mechanic),
                new Part(2,ProductSubGroupEnum.Electric, ProductGroupEnum.ElectricalCableConnection,ProductTopGroupEnum.Electric),
                new Part(3,ProductSubGroupEnum.Electric, ProductGroupEnum.MechanicsRoutingAccessories,ProductTopGroupEnum.Electric)
            };
        }
        public ObservableCollection<Part> GetData()
        {
            //var datafromSettings = JsonProvider<ObservableCollection<Part>>.GetData();
            //if (datafromSettings != null)
            //{
            //    return datafromSettings;
            //}
            //else
            //{
            return Parts ?? new ObservableCollection<Part>();
            //}
        }
        public void SaveData(ObservableCollection<Part> parts)
        {
            JsonProvider<Part>.SaveData(parts);
        }
    }
}
