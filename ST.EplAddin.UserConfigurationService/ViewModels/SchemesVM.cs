using ST.EplAddin.UserConfigurationService.Models;
using System.Collections.ObjectModel;

namespace ST.EplAddin.UserConfigurationService.ViewModels
{
    internal class SchemesVM : ViewModel
    {
        public ObservableCollection<Scheme> Collection { get; set; } = new();
        public Scheme SelectedItem { get; set; }
        public RelayCommand OkCommand { get; set; }

    }
}
