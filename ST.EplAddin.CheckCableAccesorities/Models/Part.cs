using ST.EplAddin.CheckCableAccesorities.ProductGroupEnums;
using ST.EplAddin.CheckCableAccesorities.ViewModels;

namespace ST.EplAddin.CheckCableAccesorities.Models
{
    internal class Part : ViewModelBase
    {
        private int number;
        private ProductGroupEnum _selectedProductGroup;
        private ProductTopGroupEnum _selectedProductTopGroup;
        private ProductSubGroupEnum _selectedProductSubGroup;

        public ProductGroupEnum ProductGroup
        {
            get { return _selectedProductGroup; }
            set
            {
                _selectedProductGroup = value;
                OnPropertyChanged();
            }
        }
        public ProductTopGroupEnum ProductTopGroup
        {
            get { return _selectedProductTopGroup; }
            set
            {
                _selectedProductTopGroup = value;
                OnPropertyChanged();
            }
        }
        public ProductSubGroupEnum ProductSubGroup
        {
            get { return _selectedProductSubGroup; }
            set
            {
                _selectedProductSubGroup = value;
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
