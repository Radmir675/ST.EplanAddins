using ST.EplAddin.CheckCableAccesorities.Help;
using ST.EplAddin.CheckCableAccesorities.Models;
using ST.EplAddin.CheckCableAccesorities.ProductGroupEnums;
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
        public ErrorDataCable SelectedMessage
        {
            get => selectedMessage;
            set { selectedMessage = value; OnPropertyChanged(); }
        }
        public ObservableCollection<ErrorDataCable> Message
        {
            get => message;
            set
            {
                message = value;
                OnPropertyChanged();
            }
        }
        private Part selectedPart;
        private RelayCommand checkProducts;
        private RelayCommand addNewPart;
        private RelayCommand removeSelectedPart;
        private RelayCommand saveSettings;
        private RelayCommand resetCommand;
        private ErrorDataCable selectedMessage;
        private ObservableCollection<ErrorDataCable> message;

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
                        Message = new ObservableCollection<ErrorDataCable>(checkCheckCableAccesoritiesAction.CheckCableAccesorities(PartsData));
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
                        PartsData.Add(new Part(PartsData.LastOrDefault()?.Number + 1 ?? 1,
                            ProductTopGroupEnum.Undefined,
                            ProductGroupEnum.Undefined,
                            ProductSubGroupEnum.Undefined));
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
        public RelayCommand SaveSettings
        {
            get
            {
                return saveSettings ??
                    (
                    saveSettings = new RelayCommand(obj =>
                    {
                        settings.SaveData(PartsData);
                    }));
            }
        }
        public RelayCommand ResetCommand
        {
            get
            {
                return resetCommand ??
                    (
                    resetCommand = new RelayCommand(obj =>
                    {
                        settings.ResetData();

                    }));
            }
        }
    }
}
