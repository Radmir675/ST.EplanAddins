using System.Collections.Generic;

namespace ST.EplAddin.ComparisonOfProjectProperties
{
    internal class MainWindowVM : ViewModelBase
    {
        public Dictionary<int, PropertyData> FirstPropertiesList { get; set; }
        public Dictionary<int, PropertyData> SecondPropertiesList { get; set; }

        public MainWindowVM(Dictionary<int, PropertyData> firstPropertiesList, Dictionary<int, PropertyData> secondPropertiesList)
        {
            FirstPropertiesList = firstPropertiesList;
            SecondPropertiesList = secondPropertiesList;
        }

        public MainWindowVM()
        {

        }
        public string SelectedObject { get; set; }

    }
}
