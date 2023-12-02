using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EplAddin.Article_AddImageContextDialog
{
    public class ViewModel : INotifyPropertyChanged
    {
        private bool _isReadonly;
        public bool IsReadOnly
        {
            get { return _isReadonly; }
            set
            {
                if (value == _isReadonly) return;
                _isReadonly = value;
                OnPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
