using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.HEServices;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ST.EplAddin.DocumentPartsCheck
{
    public class Action : IEplAction
    {
        public static string actionName = "DocumentsPartCheck";

        public void GetActionProperties(ref ActionProperties actionProperties)
        {

        }
        public bool OnRegister(ref string Name, ref int Ordinal)
        {
            Name = actionName;
            Ordinal = 99;
            return true;
        }

        public bool Execute(ActionCallingContext oActionCallingContext)
        {
            FindAllMissingParts();
            return true;
        }

        private void FindAllMissingParts()
        {
            SelectionSet selectionSet = new SelectionSet
            {
                LockProjectByDefault = false
            };
            var currentProject = selectionSet.GetCurrentProject(true);
            //теперь найдем все места установки
            var projectLocations = GetProjectLocations(currentProject);

            //теперь найдем все идентификаторы проекта
            var projectExistingTypes = GetProjectExistingTypes(currentProject);
            foreach (var location in projectLocations)
            {
                //тут мы нашли все виды документов
                var pagesType = PagesType(currentProject, location);

                //тут мы найдем есть ли такие изделия в данном месте установки
                var allArtRefParts = GetAllArtRefParts(currentProject, projectExistingTypes, location);

                var wringIdentify = GetWrongTypes(pagesType, allArtRefParts).ToList();
                if (wringIdentify.Any())
                {
                    MessageBox.Show(
                        $"Пожалуйста проверьте следующие идентификаторы (изделия) в разделе документация {string.Join(", ", wringIdentify)}  в месте установки {location}", "Ошибки в спецификации", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else
                {
                    MessageBox.Show($"Для {location} проверка прошла успешно!", "Проверка", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
        }

        private IEnumerable<string> GetProjectLocations(Project currentProject)
        {
            DMObjectsFinder oFinder = new DMObjectsFinder(currentProject);
            var pages = oFinder.GetPages(null);
            var pagesType = pages.Select(x => x.Properties.DESIGNATION_FULLLOCATION)
                .Distinct().Select(x => x.ToString());
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

        private List<string> GetAllArtRefParts(Project currentProject, List<string> projectExistingTypes, string fullLocation)
        {
            DMObjectsFinder finder = new DMObjectsFinder(currentProject);
            var allArtRefParts = finder.GetArticleReferencesWithCF(new ArticleReferenceFilter(projectExistingTypes, fullLocation))
                .Select(x => x.PartNr.ToString())
                .ToList();
            return allArtRefParts;
        }

        private List<string> GetProjectExistingTypes(Project currentProject)
        {
            var projectExistingTypes = currentProject.GetLocations(Project.Hierarchy.Document).ToList();
            return projectExistingTypes;
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
                .Where(x => x != "СП" && x != "");
            return pagesType;
        }


        public class ArticleReferenceFilter(List<string> docTypes, string fullLocation) : ICustomFilter
        {
            public bool IsMatching(StorableObject objectToCheck)
            {
                var articleReference = objectToCheck as ArticleReference;
                if (articleReference.Properties.DESIGNATION_FULLLOCATION.ToString() != fullLocation) return false;
                if (articleReference.Count <= 0) return false;

                var pathNo = articleReference.Properties.ARTICLEREF_PARTNO.ToString();
                return docTypes.Contains(pathNo);
            }
        }
    }
}
