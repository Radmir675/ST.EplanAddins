using Eplan.EplApi.DataModel.E3D;
using ST.EplAddin.FootNote.ProperyBrowser;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ST.EplAddin.FootNote.PropertyBrowser
{
    internal class PropertiesStore
    {
        private readonly PropertiesProvider _propertiesProvider;
        private List<PropertyEplan> _articleReferenceProperties;
        private List<PropertyEplan> _articleProperties;
        private List<PropertyEplan> _placement3DProperties;

        private Task ArticleReferenceTask { get; set; }
        private Task ArticlePropertiesTask { get; set; }
        private Task Placement3DTask { get; set; }
        public List<PropertyEplan> ArticleReferenceProperties
        {
            get
            {
                Task.WaitAll(ArticleReferenceTask);
                return _articleReferenceProperties;
            }
            set => _articleReferenceProperties = value;
        }
        public List<PropertyEplan> ArticleProperties
        {
            get
            {
                Task.WaitAll(ArticlePropertiesTask);
                return _articleProperties;
            }
            set => _articleProperties = value;
        }
        public List<PropertyEplan> Placement3DProperties
        {
            get
            {
                Task.WaitAll(Placement3DTask);
                return _placement3DProperties;
            }
            set => _placement3DProperties = value;
        }
        public PropertiesStore(Placement3D placement3D)
        {
            _propertiesProvider = new PropertiesProvider(placement3D);
            DownLoadAllAsync();
        }
        public void GetInstance()
        {

        }
        public void DownLoadAllAsync()
        {

            ArticleReferenceTask = Task.Run(() =>
            {
                ArticleReferenceProperties = _propertiesProvider.GetArticleReferenceProperties();
            });
            ArticlePropertiesTask = Task.Run(() =>
            {
                ArticleProperties = _propertiesProvider.GetArticleProperties();
            });
            Placement3DTask = Task.Run(() =>
            {
                Placement3DProperties = _propertiesProvider.GetPlacement3DProperties();
            });

        }
    }
}
