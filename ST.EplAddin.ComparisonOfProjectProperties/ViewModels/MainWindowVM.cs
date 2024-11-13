using ST.EplAddin.ComparisonOfProjectProperties.Converters;
using ST.EplAddin.ComparisonOfProjectProperties.Helper;
using ST.EplAddin.ComparisonOfProjectProperties.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Forms;

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


        public void GetFolderPath()
        {
            FolderBrowserDialog openFileDlg = new FolderBrowserDialog();
            var result = openFileDlg.ShowDialog();
            var path = openFileDlg.SelectedPath;
        }
        public ICollectionView FirstPropertiesCollectionView => _firstPropertiesCollection?.View;

        public ICollectionView SecondPropertiesCollectionView => _secondPropertiesCollection?.View;

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
                    if (e.Item is KeyValuePair<int, PropertyData> data)
                    {

                        e.Accepted = true;

                    }
                    break;
                case ComparisonState.None:
                    e.Accepted = false;

                    break;
                case ComparisonState.Similarity:
                    break;
            }
        }


        public MainWindowVM()
        {
            ComparisonStates = EnumExtension.GetValues<ComparisonState>().ToList();
            _firstPropertiesCollection ??= new CollectionViewSource();
            _secondPropertiesCollection ??= new CollectionViewSource();
        }

        #region Commands
        private RelayCommand selectPathCommand;

        public RelayCommand SelectPathCommand
        {
            get
            {
                return selectPathCommand ??= new RelayCommand(obj =>
                {
                    GetFolderPath();
                });
            }
        }
        private RelayCommand toRightSideCommand;
        public RelayCommand ToRightSideCommand
        {
            get
            {
                return toRightSideCommand ??= new RelayCommand(obj =>
                {

                });
            }
        }
        private RelayCommand toLeftSideCommand;
        public RelayCommand ToLeftSideCommand
        {
            get
            {
                return toLeftSideCommand ??= new RelayCommand(obj =>
                {

                });
            }
        }
        #endregion
    }
}
