using ST.EplAddin.CheckCableAccesorities.Models;
using ST.EplAddin.CheckCableAccesorities.ProductGroupEnums;
using ST.EplAddin.CheckCableAccesorities.ViewModels;
using System.Collections.ObjectModel;

namespace ST.EplAddin.CheckCableAccesorities.Help
{
    class Settings : ViewModelBase
    {
        private ObservableCollection<Part> parts;

        public Settings()
        {
            Initialize();
        }

        private ObservableCollection<Part> Parts { get; set; } = new ObservableCollection<Part>();


        private void Initialize()
        {

            var data = new ObservableCollection<Part>()
            {
                new Part(1,ProductSubGroupEnum.Electric, ProductGroupEnum.Common,ProductTopGroupEnum.Undefined),
                new Part(2,ProductSubGroupEnum.Electric, ProductGroupEnum.ElectricalCableConnection,ProductTopGroupEnum.Electric),
                new Part(3,ProductSubGroupEnum.Electric, ProductGroupEnum.MechanicsRoutingAccessories,ProductTopGroupEnum.Electric)
            };

            Parts.Add(new Part(1, ProductSubGroupEnum.Electric, ProductGroupEnum.Common, ProductTopGroupEnum.Undefined));
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
        public void ResetData()
        {
            Parts.Clear();
            Initialize();
        }
    }
}
