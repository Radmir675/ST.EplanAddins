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

        private IReadOnlyList<ProductGroupEnum> options;
        private ProductGroupEnum _selectedProductGroup;
        private ProductTopGroupEnum _selectedProductTopGroup;
        private ProductSubGroupEnum _selectedProductSubGroup;
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
        public ProductTopGroupEnum SelectedProductTopGroup
        {
            get { return _selectedProductTopGroup; }
            set
            {
                _selectedProductTopGroup = value;
                OnPropertyChanged();
            }
        }
        public ProductSubGroupEnum SelectedProductSubGroup
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
            SelectedProductTopGroup = productTopGroup;
            SelectedProductSubGroup = productSubGroup;
            SelectedProductGroup = productGroup;

            // Initialization in constructor
            ProductGroupOptions = EnumExtension.GetValues<ProductGroupEnum>().ToList();

        }
    }
}
