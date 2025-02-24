using Eplan.EplApi.DataModel.E3D;
using ST.EplAddin.FootNote.Forms;
using ST.EplAddin.FootNote.Models;
using ST.EplAddin.FootNote.Services.Implementations;
using System.Collections.Generic;

namespace ST.EplAddin.FootNote.ViewModels
{
    public class FullPropertiesWindowVM : ViewModelBase
    {
        private readonly PropertiesStore propertiesStore;
        private PropertyStates _selectedType;
        public IReadOnlyList<PropertyStates> States { get; set; }

        public FullPropertiesWindowVM()
        { }
        public FullPropertiesWindowVM(Placement3D placement3D)
        {
            propertiesStore = new PropertiesStore(placement3D);
        }

        public PropertyStates SelectedType
        {
            get => _selectedType;
            set
            {
                if (Equals(value, _selectedType)) return;
                _selectedType = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(AllProperties));
            }
        }

        public IEnumerable<PropertyEplan> AllProperties
        {
            get
            {
                return SelectedType switch
                {
                    PropertyStates.ArticleReferenceProperty => propertiesStore.ArticleReferenceProperties,
                    PropertyStates.ArticleProperty => propertiesStore.ArticleProperties,
                    PropertyStates.Placement3DProperty => propertiesStore.Placement3DProperties,
                    _ => null
                };
            }
        }

        public PropertyEplan CurrentSelectedProperty { get; set; }
    }
}
