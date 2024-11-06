using Eplan.EplApi.DataModel;
using Eplan.EplApi.DataModel.E3D;
using ST.EplAddin.FootNote.ProperyBrowser;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ST.EplAddin.FootNote.PropertyBrowser
{
    internal class PropertiesProvider
    {
        public Placement3D Placement3D { get; }

        public PropertiesProvider(Placement3D placement3D)
        {
            Placement3D = placement3D;
        }
        public List<PropertyEplan> GetArticleReferenceProperties()
        {
            List<PropertyEplan> result = new();
            //TODO:нужно найти изделия и получить все свойства
            var function3D = Placement3D as Function3D;
            if (function3D == null) { MessageBox.Show("Недействительный объект источника"); return new List<PropertyEplan>(); }
            var articleReference = function3D.ArticleReferences.FirstOrDefault();

            foreach (var property in Properties.AllArticleReferencePropIDs)
            {
                try
                {
                    if (articleReference == null) return new List<PropertyEplan>();

                    var value = articleReference.Properties[property];
                    var name = property?.Definition.Name;
                    if (!string.IsNullOrEmpty(value))
                    {
                        result.Add(new PropertyEplan(name, value, property.AsInt));

                    }
                }
                catch (System.Exception)
                {
                }
            }
            return result;
        }
        public List<PropertyEplan> GetArticleProperties()
        {
            List<PropertyEplan> result = new();
            //TODO:нужно найти изделия и получить все свойства
            var function3D = Placement3D as Function3D;
            if (function3D == null) { MessageBox.Show("Недействительный объект источника"); return new List<PropertyEplan>(); }
            var article = function3D.Articles.FirstOrDefault();

            foreach (var property in Properties.AllArticlePropIDs)
            {
                try
                {
                    if (article == null) return new List<PropertyEplan>();
                    var value = article.Properties[property];
                    var name = property?.Definition.Name;
                    if (!string.IsNullOrEmpty(value))
                    {
                        result.Add(new PropertyEplan(name, value, property.AsInt));

                    }
                }
                catch (System.Exception)
                {


                }
            }

            return result;
        }
        public List<PropertyEplan> GetPlacement3DProperties()
        {
            List<PropertyEplan> result = new();
            foreach (var property in Properties.AllPlacement3DPropIDs)
            {
                try
                {
                    var value = Placement3D.Properties[property];
                    var name = property?.Definition.Name;
                    result.Add(new PropertyEplan(name, value, property.AsInt));
                }
                catch (System.Exception)
                {

                }
            }
            return result;
        }
    }
}
