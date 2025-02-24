using Eplan.EplApi.Base;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.DataModel.E3D;
using ST.EplAddin.FootNote.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ST.EplAddin.FootNote.FootNote
{
    public partial class FootnoteItem
    {

        /// <summary>
        /// Получить значение свойства исходного объекта
        /// </summary>
        /// <param name="placement3D">Исходный объект</param>
        /// <returns></returns>
        public string GetSourceObjectProperty(Placement3D placement3D)
        {
            logger.Debug("Placement3D");
            var result = "-1"; //default result

            if (placement3D == null) return result;
            switch (PROPERTYID)
            {
                case PropertiesList.AllAvailableProperties:
                    PropertySelectDialogForm propertySelectDialogForm = new PropertySelectDialogForm(placement3D);
                    propertySelectDialogForm.ShowDialog();
                    if (propertySelectDialogForm.DialogResult == DialogResult.OK)
                        result = "-1";
                    break;

                case PropertiesList.User_defined:
                    if (IsUserTextUpdated)
                    {
                        var propertiesId = GetPropID(USERTEXT);
                        var validPropertiesText = GetValidPropertiesText(placement3D, propertiesId).ToList();
                        if (!validPropertiesText.Any())
                        {
                            result = USERTEXT;
                            break;
                        }
                        for (var i = 0; i < validPropertiesText.Count; i++)
                        {
                            result = USERTEXT.Replace($"{{{propertiesId[i].ToString()}}}", validPropertiesText[i]);
                        }
                        break;
                    }
                    if (label != null)
                    {
                        result = label.Contents?.GetStringToDisplay(ISOCode.Language.L_ru_RU);
                        break;
                    }
                    else
                    {


                        Footnote_CustomTextForm form = new Footnote_CustomTextForm(placement3D);
                        form.ShowDialog();
                        if (form.DialogResult == DialogResult.OK)
                        {
                            USERTEXT = form.GetUserText();
                            var propertiesId = GetPropID(USERTEXT);
                            var validPropertiesText = GetValidPropertiesText(placement3D, propertiesId).ToList();
                            if (!validPropertiesText.Any())
                            {
                                result = USERTEXT;
                            }
                            for (var i = 0; i < validPropertiesText.Count; i++)
                            {
                                result = USERTEXT.Replace($"{{{propertiesId[i].ToString()}}}", validPropertiesText[i]);
                            }
                        }
                        form.Close();
                    }
                    break;

                case PropertiesList.P20450:
                    result = placement3D.Properties[20450].ToInt().ToString();
                    break;

                case PropertiesList.P20008:
                    result = placement3D.Properties[20008].ToString();
                    break;

                case PropertiesList.P20487:
                    Function3D function3D = placement3D as Function3D;
                    if (function3D == null) { MessageBox.Show("Недействительный объект источника"); break; }

                    ArticleReference articleReference = function3D.ArticleReferences.FirstOrDefault();
                    if (articleReference == null || articleReference.IsTransient)
                    {
                        //Попытка найти номер в связанных элементах
                        var arr = function3D.CrossReferencedObjectsAll.Where(a => (a as Function3D).ArticleReferences.Count() > 0);
                        StorableObject arrItem = null;
                        if (arr != null)
                        {
                            arrItem = arr.FirstOrDefault();
                            articleReference = (arrItem as Function3D).ArticleReferences.FirstOrDefault();
                        }
                    }

                    if (articleReference == null || articleReference.IsTransient)
                    { MessageBox.Show("Недействительная ссылка изделия объекта источника"); break; }

                    if (articleReference.Properties.Exists(20487) == false) { MessageBox.Show("Несуществующее свойство ссылки изделия объекта источника"); break; }

                    if (articleReference.Properties[20487].IsEmpty)
                    {
                        //MessageBox.Show("Пустой Номер позиции");
                        result = "-1";
                        break;
                    }

                    result = articleReference.Properties[20487].ToInt().ToString();
                    break;
            }
            return result;
        }

        private IEnumerable<string> GetValidPropertiesText(Placement3D placement3D, List<int> propertiesId)
        {
            //TODO:переписать под разные типы свойств
            logger.Debug("");
            return new List<string>();
            //foreach (var property in propertiesId)
            //{
            //    using (PropertyDefinition propertyDefinition = new PropertyDefinition(property))
            //    {
            //        bool IsIndexed = propertyDefinition.IsIndexed;
            //        if (IsIndexed == false)
            //        {
            //            var propertyText = placement3D.Properties[property].ToString(ISOCode.Language.L_ru_RU);
            //            yield return propertyText;
            //        }
            //    }
            //}
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

