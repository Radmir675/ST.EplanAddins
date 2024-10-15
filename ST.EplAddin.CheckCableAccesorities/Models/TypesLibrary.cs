using ST.EplAddin.CheckCableAccesorities.Help;
using ST.EplAddin.CheckCableAccesorities.ProductGroupEnums;
using ST.EplAddin.CheckCableAccesorities.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace ST.EplAddin.CheckCableAccesorities.Models
{
    internal class TypesLibrary : ViewModelBase
    {
        private IReadOnlyList<ProductGroupEnum> _productGroupOptions;
        private IReadOnlyList<ProductTopGroupEnum> _productGroupTopOptions;
        private IReadOnlyList<ProductSubGroupEnum> _productGroupSubOptions;
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
        public TypesLibrary()
        {
            ProductGroupOptions = EnumExtension.GetValues<ProductGroupEnum>().ToList();
            ProductGroupTopOptions = EnumExtension.GetValues<ProductTopGroupEnum>().ToList();
            ProductGroupSubOptions = EnumExtension.GetValues<ProductSubGroupEnum>().ToList();
        }
    }
}
