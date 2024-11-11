using System.Collections.Generic;

namespace ST.EplAddin.ComparisonOfProjectProperties
{
    internal class PropertiesDataStorage
    {
        private List<Dictionary<int, string>> Data { get; set; }


        public PropertiesDataStorage(Dictionary<int, string> data1, Dictionary<int, string> data2)
        {
            Data = new List<Dictionary<int, string>>() { data1, data2 };
        }
        public List<Dictionary<int, string>> GetData()
        {
            return Data ?? new List<Dictionary<int, string>>();
        }
    }
}
