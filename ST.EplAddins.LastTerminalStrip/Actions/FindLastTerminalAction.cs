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
    public partial class FindLastTerminalAction : IEplAction
    {
        public InternalLogger FileLoggger { get; set; }
        public List<string> WrittenLogs { get; set; } = new List<string>();
        public static string ActionName { get; set; } = "LastTerminalStrip";
        public LoggerForm LoggerForm { get; set; }
        public Progress Progress { get; set; }
        public string ProjectName { get; set; }
        public Project CurrentProject { get; set; }
        public Terminal[] EmptyTerminals { get; set; }
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
                selectionSet.LockProjectByDefault = false;
                selectionSet.LockSelectionByDefault = false;
                CurrentProject = selectionSet.GetCurrentProject(true);
                ProjectName = CurrentProject.ProjectName;
                LoggerForm = new LoggerForm(CurrentProject);
                LoggerForm.AccountHandler += ShowSearch;
                FileLoggger = new InternalLogger(CurrentProject);
                using (SafetyPoint safetyPoint = SafetyPoint.Create())
                {
                    var lastTerminals = GetLastTerminals(CurrentProject);
                    StorableObject[] storable = lastTerminals.ToArray();

                    Search search = new Search();
                    search.ClearSearchDB(CurrentProject);
                    search.AddToSearchDB(storable);
                    FileLoggger.WriteFileLog(WrittenLogs);
                    LoggerForm.ShowLogs(WrittenLogs);
                    EmptyTerminals = GetEpmtyTerminalStrips(lastTerminals);
                    LoggerForm.EmptyTerminalStrips = EmptyTerminals.ToList();
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
            deviceService.SortTerminalStrips(terminalStrips, DeviceService.TerminalStripSortMethods.Default);
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

        private TerminalStrip[] FindTerminalStrips(ILookup<PropertyValue, Terminal> terminalGroups, Project currentProject)
        {
            DMObjectsFinder DMObjectsFinder = new DMObjectsFinder(currentProject);
            var existingTerminalStrips = DMObjectsFinder.GetTerminalStripsWithCF(new MultyLineTerminalStripFilter()).Select(x => x.Name).ToList();
            TerminalStrip[] terminalStrips = terminalGroups.Select(x =>
            {
                if (!existingTerminalStrips.Contains(x.FirstOrDefault()?.TerminalStrip?.Name)//проверяем соедржится ли в данном клеммном ряду многополюсное определение клеммника
                     && x.First().Properties.FUNC_FULLDEVICETAG != "+"
                     && x.Any(z => z?.Articles?.Count() > 0))//есть ли у этих клемм хотя бы 1 изделие с артикулом
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
        private Terminal[] GetEpmtyTerminalStrips(List<Terminal> terminals)
        {
            var result = terminals.Where(x => x.Articles.Length < 2).ToArray();
            return result;
        }
        public void GetActionProperties(ref ActionProperties actionProperties) { }
        public void ShowSearch()
        {
            Search search = new Search();
            search.ClearSearchDB(CurrentProject);
            search.AddToSearchDB(EmptyTerminals);
        }
    }
}
