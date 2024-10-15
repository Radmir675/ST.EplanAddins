using ST.EplAddin.CheckCableAccesorities.Help;
using ST.EplAddin.CheckCableAccesorities.ProductGroupEnums;
using ST.EplAddin.CheckCableAccesorities.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace ST.EplAddin.CheckCableAccesorities.Models
{
    internal class Part : ViewModelBase
    {
        private int number;
        private ProductGroupEnum productGroup;
        private ProductTopGroupEnum productTopGroup;
        private ProductSubGroupEnum productSubGroup;
        private IReadOnlyList<ProductGroupEnum> options;
        private ProductGroupEnum _selectedProductGroup;
        public IReadOnlyList<ProductGroupEnum> ProductGroupOptions
        {
            get { return options; }
            private set
            {
                options = value;
                OnPropertyChanged();
            }
        }
        public ProductGroupEnum SelectedProductGroup
        {
            get { return _selectedProductGroup; }
            set
            {
                _selectedProductGroup = value;
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
            // Initialization in constructor
            ProductGroupOptions = EnumExtension.GetValues<ProductGroupEnum>().ToList();
            // If you want to set a default.
            SelectedProductGroup = ProductGroupOptions[0];
        }
    }
}
