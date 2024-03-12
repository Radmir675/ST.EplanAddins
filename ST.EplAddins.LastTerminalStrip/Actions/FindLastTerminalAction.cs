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
    class FindLastTerminalAction : IEplAction
    {
        InternalLogger fileLoggger;
        public List<string> writtenLogs = new List<string>();
        public static string actionName = "LastTerminalStrip";
        LoggerForm loggerForm;
        Progress oProgress;
        public string projectName;
        public bool OnRegister(ref string Name, ref int Ordinal)
        {
            Name = actionName;
            Ordinal = 32;
            return true;
        }

        public bool Execute(ActionCallingContext oActionCallingContext)
        {
            try
            {
                oProgress = new Progress("Searching last terminals strips...");
                oProgress.SetTitle("Searching last terminals strips...");
                oProgress.SetAllowCancel(false);
                oProgress.ShowImmediately();
                oProgress.SetNeededSteps(10);

                SelectionSet selectionSet = new SelectionSet();
                Project currentProject = selectionSet.GetCurrentProject(true);
                projectName = currentProject.ProjectName;
                loggerForm = new LoggerForm(projectName);
                fileLoggger = new InternalLogger(projectName);
                selectionSet.LockProjectByDefault = false;
                selectionSet.LockSelectionByDefault = false;
                using (SafetyPoint safetyPoint = SafetyPoint.Create())
                {
                    var lastTerminals = GetLastTerminsls(currentProject);
                    StorableObject[] storable = lastTerminals.ToArray();

                    Search search = new Search();
                    search.ClearSearchDB(currentProject);
                    search.AddToSearchDB(storable);
                    fileLoggger.WriteFileLog(writtenLogs);
                    loggerForm.ShowLogs(writtenLogs);
                    writtenLogs.Clear();
                    ShowSearchNavigator();
                    safetyPoint.Commit();
                }
                oProgress.EndPart(true);
                return true;

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
            finally
            {
                oProgress.EndPart(true);
            }
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
                   oProgress.BeginPart(100 / (terminalGroups.Count()), x.First().Name);

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
                   LogTerminalStripName(function.Name.ToString());

                   oProgress.EndPart(false);
               }
               return x.First().TerminalStrip;
           }).ToArray();
            oProgress.SetNeededParts(1);
            oProgress.BeginPart(25.0, "");
            oProgress.Step(1);
            DeviceService deviceService = new DeviceService();
            deviceService.SortTerminalStrips(terminalStrips, DeviceService.TerminalStripSortMethods.Numeric);
            oProgress.EndPart(false);
            List<Terminal> record = new List<Terminal>();
            foreach (var terminalstrip in terminalStrips)
            {
                oProgress.SetNeededSteps(terminalStrips.Count());
                if (terminalstrip != null && terminalstrip.Terminals != null)
                {
                    oProgress.Step(1);
                    List<Terminal> TerminalOff = new List<Terminal>();
                    oProgress.BeginPart(100 / (terminalStrips.Count()), terminalstrip.Name);
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

        private void LogTerminalStripName(string terminalStripName)
        {
            writtenLogs.Add(terminalStripName);
        }

        public void GetActionProperties(ref ActionProperties actionProperties)
        {

        }

    }
}
