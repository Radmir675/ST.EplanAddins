using ST.EplAddin.CheckCableAccesorities.Help;
using ST.EplAddin.CheckCableAccesorities.Models;
using System.Collections.ObjectModel;

namespace ST.EplAddin.CheckCableAccesorities.ViewModels
{
    class MainWindowVM : ViewModelBase
    {
        private readonly Settings settings;
        public ObservableCollection<Part> PartsData { get => settings.GetData(); }

        private RelayCommand checkProducts;
        private RelayCommand addNewPart;


        public MainWindowVM()
        {
            settings = new Settings();
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
                        settings.AddNewPart();
                    }));
            }
        }



    }
}
