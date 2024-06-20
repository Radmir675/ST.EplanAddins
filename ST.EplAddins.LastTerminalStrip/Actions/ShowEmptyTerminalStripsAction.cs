using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.HEServices;


namespace ST.EplAddins.LastTerminalStrip
{
    public class ShowEmptyTerminalStripsAction : IEplAction
    {

        public static string ActionName { get; set; } = "ShowEmptyTerminalStrips";
        public bool OnRegister(ref string Name, ref int Ordinal)
        {
            Name = ActionName;
            Ordinal = 32;
            return true;
        }

        public bool Execute(ActionCallingContext oActionCallingContext)
        {
            using (SafetyPoint safetyPoint = SafetyPoint.Create())
            {
                SelectionSet selectionSet = new SelectionSet();
                Project currentProject = selectionSet.GetCurrentProject(true);
                var projectName = currentProject.ProjectName;
                DMObjectsFinder dMObjectsFinder = new DMObjectsFinder(currentProject);
                var emptyTerminalStrps = dMObjectsFinder.GetTerminalStripsWithCF(new EmptyArticleTerminalStripFilter());

                Search search = new Search();
                search.ClearSearchDB(currentProject);
                search.AddToSearchDB(emptyTerminalStrps);
                ShowSearchNavigator();
                safetyPoint.Commit();
            }
            return true;
        }
        private void ShowSearchNavigator()
        {
            ActionManager oMng = new ActionManager();
            Eplan.EplApi.ApplicationFramework.Action baseAction = oMng.FindAction("XSeShowSearchResultsAction");
            ActionCallingContext ctx = new ActionCallingContext();
            bool result = baseAction.Execute(ctx);
        }
        public void GetActionProperties(ref ActionProperties actionProperties)
        {

        }
    }
}
