using Eplan.EplApi.Base;
using Eplan.EplApi.DataModel;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Function3D = Eplan.EplApi.DataModel.E3D.Function3D;
using Placement3D = Eplan.EplApi.DataModel.E3D.Placement3D;

namespace ST.EplAddin.FootNote.FootNote
{
    public partial class FootnoteItem
    {

        /// <summary>
        /// Получить значение свойства исходного объекта
        /// </summary>
        /// <param name="placement3D">Исходный объект</param>
        /// <returns></returns>
        ///
        private const string default_result = "-1";
        public string GetSourceObjectProperty(Placement3D placement3D)
        {
            logger.Debug("Placement3D");
            string result = default_result;
            if (placement3D == null) return default_result;
            switch (PROPERTYID)
            {
                case PropertiesList.User_defined:
                    if (IsUserTextUpdated)
                    {
                        result = GetResult(placement3D);
                        break;
                    }
                    if (label != null)
                    {
                        result = label.Contents?.GetStringToDisplay(ISOCode.Language.L_ru_RU);
                        break;
                    }

                    Footnote_CustomTextForm form = new Footnote_CustomTextForm(placement3D);
                    form.ShowDialog();
                    if (form.DialogResult == DialogResult.OK)
                    {
                        USERTEXT = form.GetUserText();
                        result = GetResult(placement3D);
                    }
                    form.Close();
                    break;
            }
            return result;
        }

        private string GetResult(Placement3D placement3D)
        {
            string result = default_result;
            var propertiesId = GetPropID(USERTEXT);
            if (propertiesId.Any())
            {
                var validPropertiesText = GetValidPropertiesText(placement3D, propertiesId).ToList();
                for (var i = 0; i < validPropertiesText.Count; i++)
                {
                    result = USERTEXT.Replace($"{{{propertiesId[i].ToString()}}}", validPropertiesText[i]);
                    return result;
                }
            }
            return string.IsNullOrEmpty(USERTEXT) ? result : USERTEXT;
        }

        private IEnumerable<string> GetValidPropertiesText(Placement3D placement3D, List<int> propertiesId)
        {
            logger.Debug("");
            foreach (var property in propertiesId)
            {
                if (placement3D.Properties.Exists(property))
                {
                    //TODO:надо доделать свойства
                    AnyPropertyId wd = new AnyPropertyId();
                    wd.SetPropertyId(property);
                    if (wd.Definition.IsIndexed)
                    {
                        yield return placement3D.Properties[property][1].ToString(ISOCode.Language.L_ru_RU);
                    }
                    else
                    {
                        yield return placement3D.Properties[property].ToString(ISOCode.Language.L_ru_RU);
                    }
                }

                else if (placement3D is Function3D function3D)
                {
                    if (function3D.ArticleReferences.First().Properties.Exists(property))
                    {
                        yield return function3D.ArticleReferences.First().Properties[property].ToString(ISOCode.Language.L_ru_RU);
                    }
                    else if (function3D.Articles.First().Properties.Exists(property))
                    {
                        yield return function3D.Articles.First().Properties[property];
                    }
                }
            }
        }

        private List<int> GetPropID(string inputText)
        {
            logger.Debug("");
            string pattern = @"(?<=\{).*?(?=\})";//{215421}слова{12211}
            Regex regex = new Regex(pattern);
            MatchCollection collection = regex.Matches(inputText);

            var result = collection
                    .Cast<Match>()
                    .Select(s => new { Success = int.TryParse(s.Value, out var value), value })
                    .Where(pair => pair.Success)
                    .Select(pair => pair.value).ToList();
            return result;
        }
    }
}

