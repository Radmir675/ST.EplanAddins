using ST.EplAddin.CheckCableAccesorities.Help;
using ST.EplAddin.CheckCableAccesorities.ViewModels;

namespace ST.EplAddin.CheckCableAccesorities.Models
{
    internal class Part : ViewModelBase
    {
        public int Number { get; }

        private ProductGroupType type;

        public ProductGroupType Type
        {
            get { return type; }
            set
            {
                type = value;
                OnPropertyChanged();
            }
        }
        public Part(int number, ProductGroupType type)
        {
            Number = number;
            Type = type;
        }
    }
}
