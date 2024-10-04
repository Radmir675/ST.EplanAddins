using ST.EplAddin.CheckCableAccesorities.Help;
using ST.EplAddin.CheckCableAccesorities.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace ST.EplAddin.CheckCableAccesorities.ViewModels
{
    class MainWindowVM : ViewModelBase
    {
        private readonly Settings settings;
        public ObservableCollection<Part> PartsData { get; set; }
        public Part SelectedPart
        {
            get => selectedPart;
            set
            {
                selectedPart = value;
                OnPropertyChanged();
            }
        }
        private Part selectedPart;

        private RelayCommand checkProducts;
        private RelayCommand addNewPart;
        private RelayCommand removeSelectedPart;

        public MainWindowVM()
        {
            settings = new Settings();
            PartsData = settings.GetData();
        }
        public RelayCommand CheckProducts
        {
            get
            {
                return checkProducts ??
                    (
                    checkProducts = new RelayCommand(obj =>
                    {
                        CheckCableAccesoritiesAction checkCheckCableAccesoritiesAction = new CheckCableAccesoritiesAction();
                        var data = checkCheckCableAccesoritiesAction.CheckCableAccesorities(null);
                        //запустить событие по проверке
                    }));
            }
        }
        public RelayCommand AddNewPart
        {
            get
            {
                return addNewPart ??
                    (
                    addNewPart = new RelayCommand(obj =>
                    {
                        PartsData.Add(new Part(PartsData.LastOrDefault()?.Number + 1 ?? 1, ProductGroupType.Undefined));
                    }));
            }
        }
        public RelayCommand RemoveSelectedPart
        {
            get
            {
                return removeSelectedPart ??
                    (
                    removeSelectedPart = new RelayCommand(obj =>
                    {
                        if (obj is Part part)
                        {
                            var partIndex = PartsData.IndexOf(part);
                            PartsData.Remove(part);
                            if (partIndex > 0)
                            {
                                SelectedPart = PartsData[partIndex - 1];
                            }
                        }
                    },
                    (obj) => SelectedPart != null));
            }
        }



    }
}
