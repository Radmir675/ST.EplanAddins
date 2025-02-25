using Eplan.EplApi.Base;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.DataModel.E3D;
using ST.EplAddin.FootNote.Forms;
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
        public IEnumerable<PropertyEplan> GetArticleReferenceProperties()
        {
            var function3D = Placement3D as Function3D;
            if (function3D == null)
            {
                MessageBox.Show("Недействительный объект источника");
                yield break;
            }

            var articleReference = function3D.ArticleReferences.FirstOrDefault();
            if (articleReference == null) yield break;

            var articleReference_Props = articleReference.Properties;
            foreach (var property in Properties.AllArticleReferencePropIDs)
            {
                try
                {
                    var propertyDefinition = property.Definition;
                    if (propertyDefinition.IsInternal) continue;
                    var value = articleReference_Props[property];
                    var name = propertyDefinition.Name;
                    var description = "pdfg";

                    if (propertyDefinition.IsIndexed)
                    {
                        foreach (var ind in value.Indexes)
                        {
                            var idxVal = value[ind];


                            if (!idxVal.IsEmpty)
                            {
                                yield return new PropertyEplan(name + "_" + ind, idxVal.ToString(ISOCode.Language.L_ru_RU), property.AsInt, description);
                            }
                        }
                        continue;
                    }

                    if (propertyDefinition.IsIndexed == false)
                    {
                        if (!value.IsEmpty)
                        {
                            yield return new PropertyEplan(name, value.ToString(ISOCode.Language.L_ru_RU), property.AsInt, description);
                        }
                    }
                }
                finally
                {

                }
            }

        }
        public List<PropertyEplan> GetArticleProperties()
        {
            List<PropertyEplan> result = new();
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
