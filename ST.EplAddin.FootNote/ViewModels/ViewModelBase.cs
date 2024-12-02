using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ST.EplAddin.FootNote.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly Dictionary<string, object> _values = new();
        protected void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged();
            return true;
        }

        protected bool Set<T>(T value, [CallerMemberName] string propertyName = null!)
        {
            if (_values.TryGetValue(propertyName, out var old_value) && Equals(old_value, value)) return false;
            _values[propertyName] = value;
            OnPropertyChanged();
            return true;
        }
        protected T Get<T>([CallerMemberName] string propertyName = null!)
        {
            return _values.TryGetValue(propertyName, out var value) ? (T)value : default;
        }

    }
}
