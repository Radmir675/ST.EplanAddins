using System.Collections.ObjectModel;

namespace ST.EplAddin.UserConfigurationService.Models
{
    internal class UserConfigurationShemes
    {
        public ObservableCollection<string> Catalogs { get; set; }
        public ObservableCollection<string> DatabaseList { get; set; }
        public string CurrentCatalog;
        public string CurrentDatabase;

    }
}
