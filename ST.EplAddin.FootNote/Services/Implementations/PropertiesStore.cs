using Eplan.EplApi.DataModel.E3D;
using ST.EplAddin.FootNote.Forms;
using ST.EplAddin.FootNote.PropertyBrowser;
using System.Collections.Generic;

namespace ST.EplAddin.FootNote.Services.Implementations
{
    internal class PropertiesStore : IPropertiesStore
    {
        private readonly PropertiesProvider _propertiesProvider;
        public IEnumerable<PropertyEplan> ArticleReferenceProperties { get; set; }
        public IEnumerable<PropertyEplan> ArticleProperties { get; set; }
        public IEnumerable<PropertyEplan> Placement3DProperties { get; set; }
        public PropertiesStore(Placement3D placement3D)
        {
            _propertiesProvider = new PropertiesProvider(placement3D);
            DownLoadAll();
        }
        private void DownLoadAll()
        {
            ArticleReferenceProperties = _propertiesProvider.GetArticleReferenceProperties();
            ArticleProperties = _propertiesProvider.GetArticleProperties();
            Placement3DProperties = _propertiesProvider.GetPlacement3DProperties();
        }
    }


}
