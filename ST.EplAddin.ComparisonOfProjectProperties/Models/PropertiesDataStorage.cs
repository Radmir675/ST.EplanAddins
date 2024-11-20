using System.Collections.Generic;

namespace ST.EplAddin.ComparisonOfProjectProperties.Models
{
    internal class PropertiesDataStorage
    {
        public string ProjectName1 { get; set; }
        public string ProjectName2 { get; set; }
        private List<Dictionary<PropertyKey, Property>> Data { get; set; }

        public PropertiesDataStorage(Dictionary<PropertyKey, Property> data1, Dictionary<PropertyKey, Property> data2, string projectName1, string projectName2)
        {
            ProjectName1 = projectName1;
            ProjectName2 = projectName2;
            Data = new List<Dictionary<PropertyKey, Property>>() { data1, data2 };
        }
        public List<Dictionary<PropertyKey, Property>> GetData()
        {
            return Data ?? new List<Dictionary<PropertyKey, Property>>();
        }
    }
}
