using Eplan.EplApi.DataModel.E3D;
using ST.EplAddin.FootNote.ProperyBrowser;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ST.EplAddin.FootNote.PropertyBrowser
{
    internal class PropertiesStore
    {
        private readonly PropertiesProvider propertiesProvider;
        private List<PropertyEplan> articleReferenceProperties = new List<PropertyEplan>();
        private List<PropertyEplan> articleProperties = new List<PropertyEplan>();
        private List<PropertyEplan> placement3DProperties = new List<PropertyEplan>();

        private Task ArticleReferenceTask { get; set; }
        private Task ArticlePropertiesTask { get; set; }
        private Task Placement3DTask { get; set; }
        private Task DownLoadTask { get; set; }
        public List<PropertyEplan> ArticleReferenceProperties
        {
            get
            {
                Task.WaitAll(ArticleReferenceTask);
                return articleReferenceProperties;
            }
            set => articleReferenceProperties = value;
        }
        public List<PropertyEplan> ArticleProperties
        {
            get
            {
                Task.WaitAll(ArticlePropertiesTask);
                return articleProperties;
            }
            set => articleProperties = value;
        }
        public List<PropertyEplan> Placement3DProperties
        {
            get
            {
                Task.WaitAll(Placement3DTask);
                return placement3DProperties;
            }
            set => placement3DProperties = value;
        }
        public PropertiesStore(Placement3D placement3D)
        {
            propertiesProvider = new PropertiesProvider(placement3D);
            DownLoadAllAsync();
        }
        public void DownLoadAllAsync()
        {
            ArticleReferenceTask = Task.Run(() =>
             {
                 ArticleReferenceProperties = propertiesProvider.GetArticleReferenceProperties();
             });
            ArticlePropertiesTask = Task.Run(() =>
             {
                 ArticleProperties = propertiesProvider.GetArticleProperties();
             });
            Placement3DTask = Task.Run(() =>
             {
                 Placement3DProperties = propertiesProvider.GetPlacement3DProperties();
             });

        }
    }
}
