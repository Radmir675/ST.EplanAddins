using ST.EplAddin.CheckCableAccesorities.ProductGroupEnums;
using ST.EplAddin.CheckCableAccesorities.ViewModels;

namespace ST.EplAddin.CheckCableAccesorities.Models
{
    internal class Part : ViewModelBase
    {
        private int number;
        private ProductGroupEnum _productGroup;
        private ProductTopGroupEnum _productTopGroup;
        private ProductSubGroupEnum _productSubGroup;

        public ProductGroupEnum ProductGroup
        {
            get { return _productGroup; }
            set
            {
                _productGroup = value;
                OnPropertyChanged();
            }
        }
        public ProductTopGroupEnum ProductTopGroup
        {
            get { return _productTopGroup; }
            set
            {
                _productTopGroup = value;
                OnPropertyChanged();
            }
        }
        public ProductSubGroupEnum ProductSubGroup
        {
            get { return _productSubGroup; }
            set
            {
                _productSubGroup = value;
                OnPropertyChanged();
            }
        }
        public int Number
        {
            get { return number; }
            set
            {
                number = value;
                OnPropertyChanged();
            }
        }

        public Part(int number, ProductSubGroupEnum productSubGroup, ProductGroupEnum productGroup, ProductTopGroupEnum productTopGroup)
        {
            Number = number;
            ProductTopGroup = productTopGroup;
            ProductSubGroup = productSubGroup;
            ProductGroup = productGroup;
        }
    }
}
