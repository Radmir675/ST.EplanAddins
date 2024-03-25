using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.DataModel.EObjects;
using Eplan.EplApi.HEServices;
using ST.EplAddin.LastTerminalStrip;
using System.Linq;


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
                var emptyTerminalStrps = dMObjectsFinder.GetTerminalStripsWithCF(new TerminalStripFilter());

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
        public void ShowEmptyTerminalSrrips(string projectName, TerminalStrip[] terminalSrtip)
        {
            LoggerForm loggerForm = new LoggerForm(projectName);
            loggerForm.Show();
            var terminalStripsName = terminalSrtip.Select(x => x.Name).ToList();
            loggerForm.ShowEmptyTerStips(terminalStripsName);
        }
    }
    public class TerminalStripFilter : ICustomFilter
    {
        public bool IsMatching(StorableObject objectToCheck)
        {
            if (objectToCheck is TerminalStrip terminalStrip)
            {

                if (!terminalStrip.Terminals.Any())
                {
                    return true;
                }

            }
            return false;
        }
    }
}
