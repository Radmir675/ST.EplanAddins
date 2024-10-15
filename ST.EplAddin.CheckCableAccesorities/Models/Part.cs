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

        private IReadOnlyList<ProductGroupEnum> _productGroupOptions;
        private IReadOnlyList<ProductTopGroupEnum> _productGroupTopOptions;
        private IReadOnlyList<ProductSubGroupEnum> _productGroupSubOptions;
        private ProductGroupEnum _selectedProductGroup;
        private ProductTopGroupEnum _selectedProductTopGroup;
        private ProductSubGroupEnum _selectedProductSubGroup;
        public IReadOnlyList<ProductGroupEnum> ProductGroupOptions
        {
            get { return _productGroupOptions; }
            private set
            {
                _productGroupOptions = value;
                OnPropertyChanged();
            }
        }
        public IReadOnlyList<ProductTopGroupEnum> ProductGroupTopOptions
        {
            get { return _productGroupTopOptions; }
            private set
            {
                _productGroupTopOptions = value;
                OnPropertyChanged();
            }
        }
        public IReadOnlyList<ProductSubGroupEnum> ProductGroupSubOptions
        {
            get { return _productGroupSubOptions; }
            private set
            {
                _productGroupSubOptions = value;
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
            ProductGroupTopOptions = EnumExtension.GetValues<ProductTopGroupEnum>().ToList();
            ProductGroupSubOptions = EnumExtension.GetValues<ProductSubGroupEnum>().ToList();

        }
    }
}
