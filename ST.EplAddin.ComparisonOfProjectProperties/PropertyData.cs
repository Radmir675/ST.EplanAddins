namespace ST.EplAddin.ComparisonOfProjectProperties
{
    internal class PropertyData : ViewModelBase
    {
        private int _id;
        private string _value;

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

        public PropertyData(int id, string value)
        {
            _id = id;
            _value = value;
        }
    }
}
