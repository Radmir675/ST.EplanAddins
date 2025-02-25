using Eplan.EplApi.DataModel.E3D;
using ST.EplAddin.FootNote.Forms;
using ST.EplAddin.FootNote.PropertyBrowser;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ST.EplAddin.FootNote.Services.Implementations
{
    internal class PropertiesStore : IPropertiesStore
    {
        private readonly PropertiesProvider _propertiesProvider;
        public IEnumerable<PropertyEplan> ArticleReferenceProperties { get; set; }
        public IEnumerable<PropertyEplan> ArticleProperties { get; set; }
        public IEnumerable<PropertyEplan> Placement3DProperties { get; set; }
        private Task ArticleReferenceTask { get; set; }
        private Task ArticlePropertiesTask { get; set; }
        private Task Placement3DTask { get; set; }
        public PropertiesStore(Placement3D placement3D)
        {
            _propertiesProvider = new PropertiesProvider(placement3D);
            DownLoadAllAsync().Wait();
        }
        private void DownLoadAll()
        {
            ArticleReferenceProperties = _propertiesProvider.GetArticleReferenceProperties();
            ArticleProperties = _propertiesProvider.GetArticleProperties();
            Placement3DProperties = _propertiesProvider.GetPlacement3DProperties();
        }
        public async Task DownLoadAllAsync()
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
            var task = Task.WhenAll(ArticleReferenceTask, ArticlePropertiesTask, Placement3DTask);
            await task;
        }
    }


}
