using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.HEServices;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ST.EplAddin.Tests
{
    public class Action : IEplAction
    {
        public static string actionName = "EplanTests";

        public bool OnRegister(ref string Name, ref int Ordinal)
        {
            Name = actionName;
            Ordinal = 99;
            return true;
        }

        public bool Execute(ActionCallingContext oActionCallingContext)
        {
            SelectionSet selectionSet = new SelectionSet();
            selectionSet.LockProjectByDefault = false;
            var currentProject = selectionSet.GetCurrentProject(true);
            //теперь найдем все места установки
            var projectLocations = GetProjectLocations(currentProject);

            //теперь найдем все идентификаторы проекта
            var projectExistingtypes = ProjectExistingtypes(currentProject);
            foreach (var location in projectLocations)
            {
                //тут мы нашли все виды документов
                var pagesType = PagesType(currentProject, location);

                //тут мы найдем есть ли такие изделия в данном месте установки
                var allArtRefParts = AllArtRefParts(currentProject, projectExistingtypes, location);

                var wringIdentify = GetWrongTypes(pagesType, allArtRefParts).ToList();

                MessageBox.Show(
                    $"Пожалуйста проверьте следующие идентификаторы (изделия) в разделе документация {string.Join(", ", wringIdentify)}  в месте установки {location}", "Ошибки в спецификации", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            return true;
        }

        private IEnumerable<string> GetProjectLocations(Project currentProject)
        {
            DMObjectsFinder oFinder = new DMObjectsFinder(currentProject);
            var pages = oFinder.GetPages(null);
            var pagesType = pages.Select(x => x.Properties.DESIGNATION_FULLLOCATION)
                .Distinct().Select(x => x.ToString()).Where(x => !string.IsNullOrEmpty(x));
            return pagesType;
        }

        private IEnumerable<string> GetWrongTypes(IEnumerable<string> pagesType, List<string> allArtRefParts)
        {
            var common = pagesType.Intersect(allArtRefParts).ToList();
            var first = pagesType.Except(common);
            var second = allArtRefParts.Except(common);
            var missingPartNo = first.Concat(second);
            return missingPartNo;
        }

        private List<string> AllArtRefParts(Project currentProject, List<string> projectExistingtypes, string fullLocation)
        {
            DMObjectsFinder finder = new DMObjectsFinder(currentProject);
            var allArtRefParts = finder.GetArticleReferencesWithCF(new ArticleReferenceFilter(projectExistingtypes, fullLocation))
                .Select(x => x.PartNr.ToString())
                .ToList();
            return allArtRefParts;
        }

        private List<string> ProjectExistingtypes(Project currentProject)
        {
            var projectExistingtypes = currentProject.GetLocations(Project.Hierarchy.Document).ToList();
            return projectExistingtypes;
        }

        private static IEnumerable<string> PagesType(Project currentProject, string fullLocation)
        {
            DMObjectsFinder oFinder = new DMObjectsFinder(currentProject);
            PagesFilter oPagesFilter = new PagesFilter();
            PagePropertyList oPagePropertyList = new PagePropertyList();
            oPagePropertyList.DESIGNATION_FULLLOCATION = fullLocation;
            oPagesFilter.SetFilteredPropertyList(oPagePropertyList);
            var pages = oFinder.GetPages(oPagesFilter);
            var pagesType = pages.Select(x => x.Properties.DESIGNATION_DOCTYPE)
                .Distinct().Select(x => x.ToString())
                .Where(x => x != "СП");
            return pagesType;
        }

        public void GetActionProperties(ref ActionProperties actionProperties)
        {

        }

        public class ArticleReferenceFilter : ICustomFilter
        {
            private readonly List<string> _docTypes;
            private readonly string _fullLocation;

            public ArticleReferenceFilter(List<string> docTypes, string fullLocation)
            {
                _docTypes = docTypes;
                _fullLocation = fullLocation;
            }
            public bool IsMatching(StorableObject objectToCheck)
            {
                var artilceReference = objectToCheck as ArticleReference;
                if (artilceReference.Properties.DESIGNATION_FULLLOCATION.ToString() != _fullLocation) return false;
                if (artilceReference.Count <= 0) return false;

                var pathNo = artilceReference.Properties.ARTICLEREF_PARTNO.ToString();
                if (_docTypes.Contains(pathNo))
                {
                    return true;
                }
                return false;
            }
        }
    }
}
