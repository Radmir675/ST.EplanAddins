using ST.EplAddin.ComparisonOfProjectProperties.Converters;
using ST.EplAddin.ComparisonOfProjectProperties.Helper;
using ST.EplAddin.ComparisonOfProjectProperties.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;

namespace ST.EplAddin.ComparisonOfProjectProperties.ViewModels
{
    internal class MainWindowVM : ViewModelBase
    {
        private CollectionViewSource _firstPropertiesCollection;
        private CollectionViewSource _secondPropertiesCollection;

        public IReadOnlyList<ComparisonState> ComparisonStates { get; set; }
        private ComparisonState _selectedState = ComparisonState.None;

        public ComparisonState SelectedState
        {
            get
            {
                return _selectedState;
            }
            set
            {
                _selectedState = value;
                OnPropertyChanged();
                _secondPropertiesCollection.View.Refresh();
            }
        }



        public ICollectionView FirstPropertiesCollectionView => _firstPropertiesCollection?.View ?? CollectionViewSource.GetDefaultView(_firstPropertiesCollection.Source);

        public ICollectionView SecondPropertiesCollectionView => _secondPropertiesCollection.View;

        public MainWindowVM(Dictionary<int, PropertyData> firstPropertiesList, Dictionary<int, PropertyData> secondPropertiesList) : this()
        {

            _firstPropertiesCollection = new CollectionViewSource
            {
                Source = firstPropertiesList
            };
            _secondPropertiesCollection = new CollectionViewSource
            {
                Source = secondPropertiesList
            };
            _firstPropertiesCollection.Filter += _firstPropertiesCollection_Filter;
            _secondPropertiesCollection.Filter += _secondPropertiesCollection_Filter;
        }
        private void _firstPropertiesCollection_Filter(object sender, FilterEventArgs e)
        {

            e.Accepted = true;

        }

        private void _secondPropertiesCollection_Filter(object sender, FilterEventArgs e)
        {
            switch (SelectedState)
            {
                case ComparisonState.Difference:
                    if (e.Item is Dictionary<int, PropertyData> data)
                    {
                        e.Accepted = true;

                    }
                    break;
                case ComparisonState.None:
                    e.Accepted = true;

                    break;
                case ComparisonState.Similarity:
                    break;
            }
        }


        public MainWindowVM()
        {
            ComparisonStates = EnumExtension.GetValues<ComparisonState>().ToList();
        }
    }
}
