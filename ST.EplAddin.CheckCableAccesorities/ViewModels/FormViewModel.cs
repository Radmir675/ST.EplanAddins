using ST.EplAddin.CheckCableAccesorities.View.ViewModel;

namespace ST.EplAddin.CheckCableAccesorities
{
    public class FormViewModel
    {
        private RelayCommand checkProducts;
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

    }
}
