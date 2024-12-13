using System.Collections.ObjectModel;

namespace ST.EplAddin.UserConfigurationService.Models
{
    internal class EplanConfigurationShemes
    {
        public ObservableCollection<string> Catalogs { get; set; }
        public ObservableCollection<string> DatabaseList { get; set; }
        public string CurrentCatalog { get; set; }
        public string CurrentDatabase { get; set; }

    }
}
