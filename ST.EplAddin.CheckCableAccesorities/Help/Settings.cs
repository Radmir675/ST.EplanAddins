using ST.EplAddin.CheckCableAccesorities.Models;
using System.Collections.Generic;

namespace ST.EplAddin.CheckCableAccesorities.Help
{
    class Settings
    {
        private List<Part> parts;
        private List<Part> Parts
        {
            get
            {
                Initialize();
                return parts;
            }
            set
            {
                parts = value;
            }
        }
        private void Initialize()
        {
            parts = new List<Part>()
            {
                new Part(1, ProductGroupType.Common),
                new Part(2, ProductGroupType.ElectricalCableConnection),
                new Part(3, ProductGroupType.MechanicsRoutingAccessories)
            };

        }
        public List<Part> GetData()
        {
            //тут надо или получить данные из Eplan или если там пусто то проинициализировать самим
            return Parts;
        }
        public void SetData(List<Part> parts)
        {
            //надо вызывать метод и все сохранить в БД Eplan
        }
    }
}
