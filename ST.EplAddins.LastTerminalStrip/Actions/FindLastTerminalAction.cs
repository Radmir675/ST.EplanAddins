using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.Base;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.DataModel.EObjects;
using Eplan.EplApi.DataModel.MasterData;
using Eplan.EplApi.HEServices;
using ST.EplAddin.LastTerminalStrip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;


namespace ST.EplAddins.LastTerminalStrip
{
    public class FindLastTerminalAction : IEplAction
    {
        public InternalLogger FileLoggger { get; set; }
        public List<string> WrittenLogs { get; set; } = new List<string>();
        public static string ActionName { get; set; } = "LastTerminalStrip";
        public LoggerForm LoggerForm { get; set; }
        public Progress Progress { get; set; }
        public string ProjectName { get; set; }
        public bool OnRegister(ref string Name, ref int Ordinal)
        {
            Name = ActionName;
            Ordinal = 32;
            return true;
        }
        public bool Execute(ActionCallingContext oActionCallingContext)
        {
            try
            {
                Progress = new Progress("Searching last terminals strips...");
                Progress.SetTitle("Searching last terminals strips...");
                Progress.SetAllowCancel(false);
                Progress.ShowImmediately();
                Progress.SetNeededSteps(10);

                SelectionSet selectionSet = new SelectionSet();
                Project currentProject = selectionSet.GetCurrentProject(true);
                ProjectName = currentProject.ProjectName;
                LoggerForm = new LoggerForm(ProjectName);
                FileLoggger = new InternalLogger(ProjectName);
                selectionSet.LockProjectByDefault = false;
                selectionSet.LockSelectionByDefault = false;
                using (SafetyPoint safetyPoint = SafetyPoint.Create())
                {
                    var lastTerminals = GetLastTerminals(currentProject);
                    StorableObject[] storable = lastTerminals.ToArray();

                    Search search = new Search();
                    search.ClearSearchDB(currentProject);
                    search.AddToSearchDB(storable);
                    FileLoggger.WriteFileLog(WrittenLogs);
                    LoggerForm.ShowLogs(WrittenLogs);
                    WrittenLogs.Clear();
                    ShowSearchNavigator();
                    safetyPoint.Commit();
                }
                Progress.EndPart(true);
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
            finally
            {
                Progress.EndPart(true);
            }
        }
        private void ShowSearchNavigator()
        {
            ActionManager oMng = new ActionManager();
            Eplan.EplApi.ApplicationFramework.Action baseAction = oMng.FindAction("XSeShowSearchResultsAction");
            ActionCallingContext ctx = new ActionCallingContext();
            bool result = baseAction.Execute(ctx);
        }
        private void AddFunctionDifinition()
        {
            ActionManager oMng = new ActionManager();
            Eplan.EplApi.ApplicationFramework.Action baseAction = oMng.FindAction("TerminalGuiIGfWindAddDefinition");
            ActionCallingContext ctx = new ActionCallingContext();
            bool result = baseAction.Execute(ctx);
        }
        private List<Terminal> GetLastTerminals(Project currentProject)
        {
            FunctionsFilter terminalStripsFunctionsFilter = new FunctionsFilter();
            terminalStripsFunctionsFilter.Category = Function.Enums.Category.Terminal;

            Terminal[] terminals = new DMObjectsFinder(currentProject)
                .GetTerminals(terminalStripsFunctionsFilter);
            var terminalGroups = terminals
                .ToLookup(terminal => terminal.Properties.FUNC_FULLDEVICETAG);

            TerminalStrip[] terminalStrips = FindTerminalStrips(terminalGroups, currentProject);

            Progress.SetNeededParts(1);
            Progress.BeginPart(25.0, "");
            Progress.Step(1);
            SortTerminalsByNumeric(ref terminalStrips);
            Progress.EndPart(false);
            return FindLastTerminals(ref terminalStrips);
        }
        private void SortTerminalsByNumeric(ref TerminalStrip[] terminalStrips)
        {
            DeviceService deviceService = new DeviceService();
            deviceService.SortTerminalStrips(terminalStrips, DeviceService.TerminalStripSortMethods.Numeric);
        }
        private List<Terminal> FindLastTerminals(ref TerminalStrip[] terminalStrips)
        {
            List<Terminal> record = new List<Terminal>();
            foreach (var terminalstrip in terminalStrips)
            {
                Progress.SetNeededSteps(terminalStrips.Count());
                if (terminalstrip != null && terminalstrip.Terminals != null)
                {
                    Progress.Step(1);
                    List<Terminal> TerminalOff = new List<Terminal>();
                    Progress.BeginPart(100 / (terminalStrips.Count()), terminalstrip.Name);
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
            }
            return record;
        }
        private TerminalStrip[] FindTerminalStrips(ILookup<PropertyValue, Terminal> terminalGroups, Project currentProject)
        {
            TerminalStrip[] terminalStrips = terminalGroups.Select(x =>
            {
                if (x.First().TerminalStrip == null)
                {
                    Progress.BeginPart(100 / (terminalGroups.Count()), x.First().Name);

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
                    WrittenLogs.Add(function.Name.ToString());
                    Progress.EndPart(false);
                }
                return x.First().TerminalStrip;
            }).ToArray();
            return terminalStrips;

        }
        public void GetActionProperties(ref ActionProperties actionProperties) { }
    }
}
