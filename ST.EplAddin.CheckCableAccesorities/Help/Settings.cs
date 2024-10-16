using ST.EplAddin.CheckCableAccesorities.Models;
using ST.EplAddin.CheckCableAccesorities.ProductGroupEnums;
using ST.EplAddin.CheckCableAccesorities.ViewModels;
using System.Collections.ObjectModel;

namespace ST.EplAddin.CheckCableAccesorities.Help
{
    class Settings : ViewModelBase
    {
        public Settings()
        {
            Initialize();
            datafromSettings = JsonProvider<ObservableCollection<Part>>.GetData();
        }

        private ObservableCollection<Part> Parts { get; set; } = new ObservableCollection<Part>();
        private ObservableCollection<Part> datafromSettings;

        private void Initialize()
        {
            Parts.Add(new Part(1, ProductTopGroupEnum.Electric, ProductGroupEnum.ElectricalCableConnection, ProductSubGroupEnum.Any));
            Parts.Add(new Part(2, ProductTopGroupEnum.Mechanic, ProductGroupEnum.MechanicsRoutingAccessories, ProductSubGroupEnum.MechanicsCableTubingClamp));
            Parts.Add(new Part(3, ProductTopGroupEnum.Mechanic, ProductGroupEnum.Common, ProductSubGroupEnum.Common));
            Parts.Add(new Part(4, ProductTopGroupEnum.Mechanic, ProductGroupEnum.MechanicsRoutingAccessories, ProductSubGroupEnum.MechanicsCableTubingClamp));
            Parts.Add(new Part(5, ProductTopGroupEnum.Mechanic, ProductGroupEnum.MechanicsHousing, ProductSubGroupEnum.Common));
            Parts.Add(new Part(6, ProductTopGroupEnum.Mechanic, ProductGroupEnum.MechanicsHousing, ProductSubGroupEnum.Common));
            Parts.Add(new Part(7, ProductTopGroupEnum.Mechanic, ProductGroupEnum.MechanicsHousingaccessoriesIn, ProductSubGroupEnum.Undefined));
            Parts.Add(new Part(8, ProductTopGroupEnum.Electric, ProductGroupEnum.Any, ProductSubGroupEnum.Any));
            Parts.Add(new Part(9, ProductTopGroupEnum.Mechanic, ProductGroupEnum.MechanicsHousing, ProductSubGroupEnum.MechanicsAccessories));
            Parts.Add(new Part(10, ProductTopGroupEnum.Electric, ProductGroupEnum.ElectricalWire, ProductSubGroupEnum.Common));
        }
        public ObservableCollection<Part> GetData()
        {

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
        public void ResetData()
        {
            Parts.Clear();
            Initialize();
            datafromSettings = null;
        }
    }
}
