using ST.EplAddin.ComparisonOfProjectProperties.Converters;
using ST.EplAddin.ComparisonOfProjectProperties.Helper;
using ST.EplAddin.ComparisonOfProjectProperties.Models;
using System.Collections.Generic;
using System.Linq;
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
            get
            {
                return _selectedState;
            }
            set
            {
                _selectedState = value;
                OnPropertyChanged();
                FilterData();
            }
        }
        public string LeftListViewSelection { get; set; }
        public string RightListViewSelection { get; set; }

        public Dictionary<int, PropertyData> FirstListViewProperties
        {
            get => _firstListViewProperties;
            set
            {
                if (Equals(value, _firstListViewProperties)) return;
                _firstListViewProperties = value;
                OnPropertyChanged();
            }
        }

        public Dictionary<int, PropertyData> SecondListViewProperties
        {
            get => _secondListViewProperties;
            set
            {
                if (Equals(value, _secondListViewProperties)) return;
                _secondListViewProperties = value;
                OnPropertyChanged();
            }
        }


        private void FilterData()
        {

            switch (SelectedState)
            {

                case ComparisonState.Difference:

                    break;
                case ComparisonState.None:

                    break;
                case ComparisonState.Similarity:
                    Dictionary<int, PropertyData> result = new Dictionary<int, PropertyData>();
                    foreach (var item in FirstListViewProperties)
                    {
                        if (SecondListViewProperties.TryGetValue(item.Key, out PropertyData data))
                        {
                            if (data.Value == item.Value.Value)
                            {
                                result.Add(item.Key, item.Value);
                            }
                        }
                    }
                    FirstListViewProperties = result;
                    SecondListViewProperties = result;
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
            FirstListViewProperties = dataStorage.GetData().FirstOrDefault();
            SecondListViewProperties = dataStorage.GetData().FirstOrDefault();

        }
        public MainWindowVM()
        {
            ComparisonStates = EnumExtension.GetValues<ComparisonState>().ToList();
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

                }, (obj) => RightListViewSelection != null);
            }
        }
        private RelayCommand toLeftSideCommand;
        public RelayCommand ToLeftSideCommand
        {
            get
            {
                return toLeftSideCommand ??= new RelayCommand(obj =>
                {

                }, (obj) => LeftListViewSelection != null);
            }
        }
        private RelayCommand okCommand;
        private string _pathToBaseProject = "Path";
        private Dictionary<int, PropertyData> _firstListViewProperties;
        private Dictionary<int, PropertyData> _secondListViewProperties;

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
