using ST.EplAddin.ComparisonOfProjectProperties.Helper;

namespace ST.EplAddin.ComparisonOfProjectProperties.Models
{
    internal class PropertyData : ViewModelBase
    {
        public string DefinitionName
        {
            get => _definitionName;
            private set
            {
                if (value == _definitionName) return;
                _definitionName = value;
                OnPropertyChanged();
            }
        }

        private int _id;
        private string _value;
        private string _definitionName;

        public int Id
        {
            get => _id;
            set
            {
                if (value == _id) return;
                _id = value;
                OnPropertyChanged();
            }
        }

        public string Value
        {
            get => _value;
            set
            {
                if (value == _value) return;
                _value = value;
                OnPropertyChanged();
            }
        }

        public PropertyData(int id, string value, string definitionName)
        {
            DefinitionName = definitionName;
            _id = id;
            _value = value;
        }
    }
}
