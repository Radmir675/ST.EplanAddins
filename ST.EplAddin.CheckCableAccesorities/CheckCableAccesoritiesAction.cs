using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.HEServices;
using ST.EplAddin.CheckCableAccesorities.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ST.EplAddin.CheckCableAccesorities
{
    public class CheckCableAccesoritiesAction : IEplAction
    {
        public static string actionName = "CheckCableAccesorities";
        public bool Execute(ActionCallingContext oActionCallingContext)
        {
            MainWindow form = new MainWindow();
            form.ShowDialog();

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

        internal IEnumerable<string> CheckCableAccesorities(ObservableCollection<Part> parts)//checkDictionary 1-номер изделия; 2-номер enum
        {
            var allCablesInProject = GetAllCables();

            foreach (var cable in allCablesInProject)
            {
                var articles = cable.ArticleReferences;
                var articlesLength = articles.Length;
                for (int i = 0; i < articlesLength; i++) //надо проверить на максимальный i
                {
                    if (articles[i].Properties[22041].ToInt() != (int)parts[i].Type)
                    {
                        yield return cable.Name + " " + parts[i].Number;
                    }
                }
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
