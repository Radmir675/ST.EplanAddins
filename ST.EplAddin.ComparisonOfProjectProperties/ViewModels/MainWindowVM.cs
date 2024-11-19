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
        public PropertiesDataStorage DataStorage { get; }
        public IReadOnlyList<ComparisonState> ComparisonStates { get; set; }
        private ComparisonState _selectedState = ComparisonState.None;
        public string PathToBaseProject
        {
            get => _pathToBaseProject;
            set
            {
                if (value == _pathToBaseProject) return;
                _pathToBaseProject = value;
                OnPropertyChanged();
            }
        }
        public ComparisonState SelectedState
        {
            get => _selectedState;
            set
            {
                _selectedState = value;
                OnPropertyChanged();
                FirstPropertiesCollectionView.Refresh();
                SecondPropertiesCollectionView.Refresh();
            }
        }

        public KeyValuePair<PropertyKey, Property> LeftListViewSelection
        {
            get => _leftListViewSelection;
            set => _leftListViewSelection = value;
        }

        public KeyValuePair<PropertyKey, Property> RightListViewSelection
        {
            get => _rightListViewSelection;
            set => _rightListViewSelection = value;
        }

        private Dictionary<PropertyKey, Property> _firstListViewProperties1;

        private Dictionary<PropertyKey, Property> _secondListViewProperties2;

        private CollectionViewSource _firstPropertiesCollection;
        private CollectionViewSource _secondPropertiesCollection;
        private readonly ChangesRecord changesRecord;

        public bool? IsFormatsOnly
        {
            get => _isFormatsOnly;
            set
            {
                if (value == _isFormatsOnly) return;
                _isFormatsOnly = value;
                OnPropertyChanged();
                FirstPropertiesCollectionView.Refresh();
                SecondPropertiesCollectionView.Refresh();
            }
        }

        public ICollectionView FirstPropertiesCollectionView => _firstPropertiesCollection?.View;

        public ICollectionView SecondPropertiesCollectionView => _secondPropertiesCollection?.View;

        private void _firstPropertiesCollection_Filter(object sender, FilterEventArgs e)
        {

            FilterData(e, _secondListViewProperties2);
            if (e.Accepted == false) return;
            FilterByFunctionName(e);
        }

        private void FilterByFunctionName(FilterEventArgs e)
        {
            if (!IsFormatsOnly.HasValue || !IsFormatsOnly.Value) return;
            if (e.Item is not KeyValuePair<PropertyKey, Property> item) return;
            if (item.Value.Name.Contains("Свойство блока"))
            {
                e.Accepted = true;
                return;
            }
            e.Accepted = false;
        }

        private void _secondPropertiesCollection_Filter(object sender, FilterEventArgs e)
        {
            FilterData(e, _firstListViewProperties1);
            if (e.Accepted == false) return;
            FilterByFunctionName(e);
        }

        private void Add(KeyValuePair<PropertyKey, Property> selection, Dictionary<PropertyKey, Property> targetCollection)
        {
            var result = targetCollection.TryGetValue(selection.Key, out Property propertyData);
            if (result)
            {
                if (propertyData.Value != selection.Value.Value)
                {
                    propertyData.Value = selection.Value.Value;
                }
            }
            else
            {
                targetCollection.Add(selection.Key, selection.Value);
            }
        }
        private void FilterData(FilterEventArgs e, Dictionary<PropertyKey, Property> collectionViewBase)
        {
            switch (SelectedState)
            {
                case ComparisonState.Difference:
                    if (e.Item is KeyValuePair<PropertyKey, Property> item)
                    {
                        if (collectionViewBase.TryGetValue(item.Key, out Property data))
                        {
                            if (data.Value != item.Value.Value)
                            {
                                e.Accepted = true;
                                break;
                            }
                        }
                    }

                    e.Accepted = false;
                    break;
                case ComparisonState.None:
                    e.Accepted = true;
                    break;
                case ComparisonState.Similarity:
                    if (e.Item is KeyValuePair<PropertyKey, Property> item1)
                    {
                        if (collectionViewBase.TryGetValue(item1.Key, out Property data))
                        {
                            if (data.Value == item1.Value.Value)
                            {
                                e.Accepted = true;
                                break;
                            }
                        }
                        e.Accepted = false;
                    }
                    e.Accepted = false;
                    break;
            }
        }


        public void GetFolderPath()
        {
            OpenFileDialog openFileDlg = new OpenFileDialog();
            openFileDlg.Filter = "Обрабатываемые проекты(*.elk)|*.elk|Базовые проекты(*.zw9)|*.zw9";
            var result = openFileDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                PathToBaseProject = openFileDlg.FileName;
            }
        }

        #region Constructors
        public MainWindowVM(PropertiesDataStorage dataStorage) : this()
        {
            DataStorage = dataStorage;
            _firstListViewProperties1 = dataStorage.GetData().FirstOrDefault();
            _secondListViewProperties2 = dataStorage.GetData().LastOrDefault();
            _firstPropertiesCollection = new CollectionViewSource
            {
                Source = _firstListViewProperties1
            };
            _secondPropertiesCollection = new CollectionViewSource
            {
                Source = _secondListViewProperties2
            };
            _firstPropertiesCollection.Filter += _firstPropertiesCollection_Filter;
            _secondPropertiesCollection.Filter += _secondPropertiesCollection_Filter;
            changesRecord = new ChangesRecord();
            changesRecord.RemoveAll();
        }
        public MainWindowVM()
        {
            ComparisonStates = EnumExtension.GetValues<ComparisonState>().ToList();
            _firstPropertiesCollection ??= new CollectionViewSource();
            _secondPropertiesCollection ??= new CollectionViewSource();
        }
        #endregion

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
                    //Add(LeftListViewSelection, _secondListViewProperties2);
                    //changesRecord.Add(LeftListViewSelection.Key);

                }, (obj) => LeftListViewSelection.Value != null);
            }
        }
        private RelayCommand toLeftSideCommand;
        public RelayCommand ToLeftSideCommand
        {
            get
            {
                return toLeftSideCommand ??= new RelayCommand(obj =>
                {

                }, (obj) => RightListViewSelection.Value != null);
            }
        }
        private RelayCommand okCommand;
        private string _pathToBaseProject = "Path";
        private KeyValuePair<PropertyKey, Property> _rightListViewSelection;
        private KeyValuePair<PropertyKey, Property> _leftListViewSelection;
        private bool? _isFormatsOnly;


        public RelayCommand OkCommand
        {
            get
            {
                return okCommand ??= new RelayCommand(obj =>
                {

                });
            }
        }
        #endregion
    }
}
