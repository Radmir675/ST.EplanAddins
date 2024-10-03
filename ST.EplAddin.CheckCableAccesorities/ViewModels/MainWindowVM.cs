using ST.EplAddin.CheckCableAccesorities.Help;
using ST.EplAddin.CheckCableAccesorities.Models;
using System.Collections.Generic;

namespace ST.EplAddin.CheckCableAccesorities.ViewModels
{
    class MainWindowVM : ViewModelBase
    {
        public List<Part> PartsData { get => new Settings().GetData(); }

        private RelayCommand checkProducts;

        public MainWindowVM()
        {

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
        //    public IEnumerable<KeyValuePair<ProductGroupType, string>> EnumValues =>
        //Enum.GetValues(typeof(ProductGroupType))
        //    .Cast<ProductGroupType>()
        //    .Select(e => new KeyValuePair<ProductGroupType, string>(e, e.GetDescription()));


    }
}
