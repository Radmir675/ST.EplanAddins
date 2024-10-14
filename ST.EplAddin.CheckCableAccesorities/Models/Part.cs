using ST.EplAddin.CheckCableAccesorities.Help;
using ST.EplAddin.CheckCableAccesorities.ViewModels;

namespace ST.EplAddin.CheckCableAccesorities.Models
{
    internal class Part : ViewModelBase
    {
        private int number;
        private ProductGroupEnum productGroup;
        private ProductTopGroupEnum productTopGroup;
        private ProductSubGroupEnum productSubGroup;

        public int Number
        {
            get { return number; }
            set
            {
                number = value;
                OnPropertyChanged();
            }
        }
        public ProductGroupEnum ProductGroup
        {
            get { return productGroup; }
            set
            {
                productGroup = value;
                OnPropertyChanged();
            }
        }
        public ProductTopGroupEnum ProductTopGroup
        {
            get { return productTopGroup; }
            set
            {
                productTopGroup = value;
                OnPropertyChanged();
            }
        }
        public ProductSubGroupEnum ProductSubGroup
        {
            get { return productSubGroup; }
            set
            {
                productSubGroup = value;
                OnPropertyChanged();
            }
        }
        public Part(int number, ProductSubGroupEnum productSubGroup, ProductGroupEnum productGroup, ProductTopGroupEnum productTopGroup)
        {
            Number = number;
            ProductGroup = productGroup;
            ProductTopGroup = productTopGroup;
            ProductSubGroup = productSubGroup;
        }
    }
}
