using ST.EplAddin.ComparisonOfProjectProperties.Converters;
using ST.EplAddin.ComparisonOfProjectProperties.Helper;
using ST.EplAddin.ComparisonOfProjectProperties.Models;
using System.Collections.Generic;
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
            get
            {
                return _selectedState;
            }
            set
            {
                _selectedState = value;
                OnPropertyChanged();

            }
        }
        public string LeftListViewSelection { get; set; }
        public string RightListViewSelection { get; set; }
        public Dictionary<int, PropertyData> FirstListViewProperties { get; set; }
        public Dictionary<int, PropertyData> SecondListViewProperties { get; set; }

        public MainWindowVM(PropertiesDataStorage dataStorage) : this()
        {
            DataStorage = dataStorage;
            FirstListViewProperties = dataStorage.GetData().First();
            SecondListViewProperties = dataStorage.GetData().Last();
        }
        public void GetFolderPath()
        {
            FolderBrowserDialog openFileDlg = new FolderBrowserDialog();
            var result = openFileDlg.ShowDialog();
            PathToBaseProject = openFileDlg.SelectedPath;
            //TODO:настроить выбор определенных объектов и их загрузку а в целом все отлично)
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
