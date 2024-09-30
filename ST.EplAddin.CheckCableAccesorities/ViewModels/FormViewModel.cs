using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ST.EplAddin.CheckCableAccesorities
{
    public class FormViewModel
    {
        private ObservableCollection<string> _warnings { get; set; }
        public ObservableCollection<Dictionary<string, string>> ProductRelations { get; set; }

        public FormViewModel(IEnumerable<string> warnings, IEnumerable<Dictionary<string, string>> productRelations)
        {
            _warnings = new ObservableCollection<string>(warnings);
            ProductRelations = new ObservableCollection<Dictionary<string, string>>(productRelations);
        }
    }
}
