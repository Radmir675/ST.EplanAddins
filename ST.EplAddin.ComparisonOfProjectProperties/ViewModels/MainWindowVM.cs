using ST.EplAddin.ComparisonOfProjectProperties.Converters;
using ST.EplAddin.ComparisonOfProjectProperties.Helper;
using ST.EplAddin.ComparisonOfProjectProperties.Models;
using System.Collections.Generic;
using System.Linq;

namespace ST.EplAddin.ComparisonOfProjectProperties.ViewModels
{
    internal class MainWindowVM : ViewModelBase
    {
        public Dictionary<int, PropertyData> FirstPropertiesList
        {
            get => _firstPropertiesList;
            set
            {
                if (Equals(value, _firstPropertiesList)) return;
                _firstPropertiesList = value;
                OnPropertyChanged();
            }
        }

        public Dictionary<int, PropertyData> SecondPropertiesList { get; set; }

        public IReadOnlyList<ComparisonState> ComparisonStates { get; set; }
        private ComparisonState _selectedState = ComparisonState.None;
        private Dictionary<int, PropertyData> _firstPropertiesList;

        public ComparisonState SelectedState
        {
            get
            {
                return _selectedState;
            }
            set
            {
                _selectedState = value;
                ChangeTableList();
            }
        }

        private void ChangeTableList()
        {

        }

        public MainWindowVM(Dictionary<int, PropertyData> firstPropertiesList, Dictionary<int, PropertyData> secondPropertiesList) : this()
        {
            FirstPropertiesList = firstPropertiesList;
            SecondPropertiesList = secondPropertiesList;
        }

        public MainWindowVM()
        {
            ComparisonStates = EnumExtension.GetValues<ComparisonState>().ToList();
        }
        public string SelectedObject { get; set; }

    }
}
