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
                    var description = propertyDefinition.GetAsDisplayString(value);

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
                finally { }
            }
        }
        public IEnumerable<PropertyEplan> GetArticleProperties()
        {
            var function3D = Placement3D as Function3D;
            if (function3D == null) { MessageBox.Show("Недействительный объект источника"); yield break; }
            var article = function3D.Articles.FirstOrDefault();
            if (article == null) yield break;
            var articleProperties = article.Properties;

            foreach (var property in Properties.AllArticlePropIDs)
            {
                try
                {
                    var propertyDefinition = property.Definition;
                    if (propertyDefinition.IsInternal) continue;
                    var value = articleProperties[property];
                    var name = propertyDefinition.Name;
                    var description = propertyDefinition.GetAsDisplayString(value);

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
                finally { }
            }
        }
        public IEnumerable<PropertyEplan> GetPlacement3DProperties()
        {
            var placementProperties = Placement3D.Properties;
            foreach (var property in Properties.AllPlacement3DPropIDs)
            {
                if (!placementProperties.Exists(property)) continue;
                try
                {
                    var propertyDefinition = property.Definition;
                    if (propertyDefinition.IsInternal) continue;

                    var value = placementProperties[property];
                    var name = propertyDefinition.Name;
                    var description = propertyDefinition.GetAsDisplayString(value);

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
                finally { }
            }
        }
    }
}
