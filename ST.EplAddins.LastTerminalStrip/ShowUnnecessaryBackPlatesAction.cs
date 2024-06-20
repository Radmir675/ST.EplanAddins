using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.Base;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.DataModel.EObjects;
using Eplan.EplApi.HEServices;
using System.Collections.Generic;
using System.Linq;


namespace ST.EplAddins.LastTerminalStrip
{
    public class ShowUnnecessaryBackPlatesAction : IEplAction
    {

        public static string ActionName { get; set; } = "ShowUnnecessaryBackPlatesAction";
        public List<Terminal> LastTerminals { get; set; }
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
                var terminalStrips = dMObjectsFinder.GetTerminalStripsWithCF(new AllTerminalStripFilter());
                var backPlates = FindBackPlates(terminalStrips);

                Search search = new Search();
                search.ClearSearchDB(currentProject);
                search.AddToSearchDB(backPlates);
                ShowSearchNavigator();
                safetyPoint.Commit();
            }
            return true;
        }
        private Terminal[] FindBackPlates(TerminalStrip[] terminalStrips)
        {
            LastTerminals = FindLastTerminals(terminalStrips);
            var result = new List<Terminal>(10);
            foreach (var terminalStrip in terminalStrips)
            {
                foreach (var terminal in terminalStrip.Terminals)
                {
                    if (IsContainWrongBackPlate(terminal))
                    {
                        result.Add(terminal);
                    }
                }
            }
            return result.ToArray();
        }

        public bool IsContainWrongBackPlate(Terminal terminal)
        {
            if (terminal.IsMainTerminal)
            {

                if (terminal.Articles.Count() >= 2)
                {
                    var productSubGroup = terminal.ArticleReferences.Select(x => x.Properties.ARTICLE_PRODUCTSUBGROUP.GetDisplayString());// проверить только второе изделие главной клеммы
                    var displaySubGroupName = productSubGroup.Select(x => x.GetStringToDisplay(ISOCode.Language.L_ru_RU));

                    if (displaySubGroupName.Contains("Торцевая пластина") || displaySubGroupName.Contains("Стопор концевой"))
                    {
                        if (!LastTerminals.Contains(terminal))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
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
        public class AllTerminalStripFilter : ICustomFilter
        {
            public bool IsMatching(StorableObject objectToCheck)
            {
                if (objectToCheck is TerminalStrip terminalStrip)
                {
                    if (terminalStrip.Properties.FUNC_TYPE == 1)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        private List<Terminal> FindLastTerminals(TerminalStrip[] terminalStrips)
        {
            List<Terminal> record = new List<Terminal>();
            foreach (var terminalstrip in terminalStrips)
            {

                if (terminalstrip != null && terminalstrip.Terminals != null)
                {

                    List<Terminal> TerminalOff = new List<Terminal>();

                    foreach (Terminal terminal in terminalstrip.Terminals)
                    {
                        if (terminal.IsMainTerminal == true && terminal.Articles.Any())
                        {
                            TerminalOff.Add(terminal);
                        }
                    }
                    if (TerminalOff != null && TerminalOff.Count() >= 1)
                    {
                        var productSubGroup = TerminalOff.Last().ArticleReferences.First().Properties.ARTICLE_PRODUCTSUBGROUP.GetDisplayString();
                        var displaySubGroupName = productSubGroup.GetStringToDisplay(ISOCode.Language.L_ru_RU);
                        if (displaySubGroupName == "Клемма")
                        {
                            record.Add(TerminalOff?.Last());
                        }
                    }
                }
            }
            return record;
        }
    }
}
