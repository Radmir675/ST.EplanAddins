using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.Base;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.HEServices;
using ST.EplAddin.CheckCableAccesorities.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ST.EplAddin.CheckCableAccesorities
{
    public class CheckCableAccesoritiesAction : IEplAction
    {
        public static string actionName = "CheckCableAccesorities";
        public bool Execute(ActionCallingContext oActionCallingContext)
        {
            MainWindow form = new MainWindow();
            form.Show();

            return true;
        }

        private Function[] GetAllCables()
        {
            SelectionSet selectionSet = new SelectionSet();
            selectionSet.LockProjectByDefault = false;
            selectionSet.LockSelectionByDefault = false;
            Project currentProject = selectionSet.GetCurrentProject(true);
            var projectName = currentProject.ProjectName;

            DMObjectsFinder dMObjectsFinder = new DMObjectsFinder(currentProject);
            var allCablesInProject = dMObjectsFinder.GetCablesWithFilterScheme(null);
            return allCablesInProject;
        }

        internal IEnumerable<ErrorDataCable> CheckCableAccesorities(ObservableCollection<Part> parts)
        {
            var allCablesInProject = GetAllCables();

            foreach (var cable in allCablesInProject)
            {
                var articles = cable.ArticleReferences;
                var articlesLength = articles.Length;
                try
                {
                    if (articles.Any())
                    {
                        for (int i = 0; i < parts.Count; i++)
                        {
                            var currentProperlyArticle = articles.FirstOrDefault(article => article.ReferencePos == parts[i].Number);
                            if (currentProperlyArticle == null)
                            {
                            }
                            else
                            {
                                if (currentProperlyArticle?.Properties[22041].ToInt() != (int)parts[i].ProductGroup && (int)parts[i].ProductGroup != 999)
                                {
                                    var typeName = currentProperlyArticle.Properties[22041].GetDisplayString().GetStringToDisplay(ISOCode.Language.L_ru_RU);
                                    yield return new ErrorDataCable(cable.Name, parts[i].Number, currentProperlyArticle.Properties[22041], typeName, "Группа продуктов");

                                }
                                if (currentProperlyArticle?.Properties[22138].ToInt() != (int)parts[i].ProductTopGroup && (int)parts[i].ProductTopGroup != 999)
                                {
                                    var typeName = currentProperlyArticle.Properties[22138].GetDisplayString().GetStringToDisplay(ISOCode.Language.L_ru_RU);
                                    yield return new ErrorDataCable(cable.Name, parts[i].Number, currentProperlyArticle.Properties[22138], typeName, "Главная группа продуктов");
                                }
                                if (currentProperlyArticle?.Properties[22028].ToInt() != (int)parts[i].ProductSubGroup && (int)parts[i].ProductSubGroup != 999)
                                {
                                    var typeName = currentProperlyArticle.Properties[22028].GetDisplayString().GetStringToDisplay(ISOCode.Language.L_ru_RU);
                                    yield return new ErrorDataCable(cable.Name, parts[i].Number, currentProperlyArticle.Properties[22028], typeName, "Подгруппа продуктов");
                                }
                            }

                        }

                    }
                }
                finally { }
            }
        }
        public void GetActionProperties(ref ActionProperties actionProperties)
        {

        }

        public bool OnRegister(ref string Name, ref int Ordinal)
        {
            Name = actionName;
            Ordinal = 99;
            return true;
        }
    }
}
