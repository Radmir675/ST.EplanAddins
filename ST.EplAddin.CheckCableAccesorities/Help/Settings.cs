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
                new Part(1, ProductGroupType.Common),
                new Part(2, ProductGroupType.ElectricalCableConnection),
                new Part(3, ProductGroupType.MechanicsRoutingAccessories)
            };
        }
        public ObservableCollection<Part> GetData()
        {
            //тут надо или получить данные из Eplan или если там пусто то проинициализировать самим
            return new ObservableCollection<Part>(Parts);
        }
        public void AddNewPart()
        {
            Parts.Add(new Part(4, ProductGroupType.Undefined));
        }
        public void SeveData(ObservableCollection<Part> parts)
        {
            //надо вызывать метод и все сохранить в БД Eplan
        }
    }
}
