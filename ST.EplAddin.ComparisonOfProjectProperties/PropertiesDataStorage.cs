using ST.EplAddin.ComparisonOfProjectProperties.Models;
using System.Collections.Generic;

namespace ST.EplAddin.ComparisonOfProjectProperties
{
    internal class PropertiesDataStorage
    {
        public string ProjectName1 { get; set; } = "Система 1";
        public string ProjectName2 { get; set; } = "Система 2";
        private List<Dictionary<int, PropertyData>> Data { get; set; }

        public PropertiesDataStorage(Dictionary<int, PropertyData> data1, Dictionary<int, PropertyData> data2, string projectName1, string projectName2)
        {
            ProjectName1 = projectName1;
            ProjectName2 = projectName2;
            Data = new List<Dictionary<int, PropertyData>>() { data1, data2 };
        }
        public List<Dictionary<int, PropertyData>> GetData()
        {
            return Data ?? new List<Dictionary<int, PropertyData>>();
        }
    }
}
