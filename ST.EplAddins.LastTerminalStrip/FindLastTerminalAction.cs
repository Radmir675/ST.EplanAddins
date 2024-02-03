using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.DataModel.EObjects;
using Eplan.EplApi.DataModel.MasterData;
using Eplan.EplApi.HEServices;
using System.Collections.Generic;
using System.Linq;


namespace ST.EplAddins.LastTerminalStrip
{
    class FindLastTerminalAction : IEplAction
    {
        public static string actionName = "LastTerminalStrip";
        public bool OnRegister(ref string Name, ref int Ordinal)
        {
            Name = actionName;
            Ordinal = 32;
            return true;
        }

        public bool Execute(ActionCallingContext oActionCallingContext)
        {
            SelectionSet selectionSet = new SelectionSet();
            Project currentProject = selectionSet.GetCurrentProject(true);
            string projectName = currentProject.ProjectName;
            selectionSet.LockProjectByDefault = false;
            selectionSet.LockSelectionByDefault = false;
            using (SafetyPoint safetyPoint = SafetyPoint.Create())
            {
                var lastTerminals = GetLastTerminsls(currentProject);
                StorableObject[] storable = lastTerminals.ToArray();

                Search search = new Search();
                search.ClearSearchDB(currentProject);
                search.AddToSearchDB(storable);
                ShowSearchNavigator();
                safetyPoint.Commit();
            }

            return true;
        }
        public void ShowSearchNavigator()
        {
            ActionManager oMng = new ActionManager();
            Eplan.EplApi.ApplicationFramework.Action baseAction = oMng.FindAction("XSeShowSearchResultsAction");
            ActionCallingContext ctx = new ActionCallingContext();
            bool result = baseAction.Execute(ctx);
        }
        public void AddFunctionDifinition()
        {
            ActionManager oMng = new ActionManager();
            Eplan.EplApi.ApplicationFramework.Action baseAction = oMng.FindAction("TerminalGuiIGfWindAddDefinition");
            ActionCallingContext ctx = new ActionCallingContext();
            bool result = baseAction.Execute(ctx);
        }

        private List<Terminal> GetLastTerminsls(Project currentProject)
        {
            FunctionsFilter terminalStripsFunctionsFilter = new FunctionsFilter();
            terminalStripsFunctionsFilter.Category = Function.Enums.Category.Terminal;

            Terminal[] terminals = new DMObjectsFinder(currentProject)
                .GetTerminals(terminalStripsFunctionsFilter);
            var terminalGroups = terminals
                .ToLookup(terminal => terminal.Properties.FUNC_FULLDEVICETAG);

            TerminalStrip[] terminalStrips = terminalGroups.Select(x =>
           {
               if (x.First().TerminalStrip == null)
               {
                   string strSymbolLibName = "SPECIAL";
                   string strSymbolName = "TDEF";
                   int nVariant = 0;

                   SymbolLibrary symbolLibriary = new SymbolLibrary(currentProject, strSymbolLibName);
                   Symbol symbol = new Symbol(symbolLibriary, strSymbolName);
                   SymbolVariant symbolVariant = new SymbolVariant();
                   symbolVariant.Initialize(symbol, nVariant);

                   Function function = new Function();
                   function.Create(currentProject, symbolVariant);
                   function.Name = x.First().Properties.FUNC_FULLDEVICETAG;

               }
               return x.First().TerminalStrip;
           }).ToArray();

            DeviceService deviceService = new DeviceService();
            deviceService.SortTerminalStrips(terminalStrips, DeviceService.TerminalStripSortMethods.Numeric);

            List<Terminal> record = new List<Terminal>();
            foreach (var terminalstrip in terminalStrips)
            {
                if (terminalstrip != null && terminalstrip.Terminals != null)
                {

                    List<Terminal> TerminalOff = new List<Terminal>();
                    foreach (Terminal terminal in terminalstrip.Terminals)
                    {
                        if (terminal.IsMainTerminal == true)
                        {
                            TerminalOff.Add(terminal);
                        }
                    }
                    if (TerminalOff != null && TerminalOff.Count() >= 1)
                        record.Add(TerminalOff?.Last());
                }
                AddFunctionDifinition();
            }
            return record;
        }

        public void GetActionProperties(ref ActionProperties actionProperties)
        {

        }

    }
}
