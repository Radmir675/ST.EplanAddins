using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.Base;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.DataModel.EObjects;
using Eplan.EplApi.DataModel.MasterData;
using Eplan.EplApi.HEServices;
using System;
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
        
        private static void ActionCallingContext()
        {
            ActionManager actionManager = new ActionManager();
            Eplan.EplApi.ApplicationFramework.Action findLastAction = actionManager.FindAction("XSeAddToSearchDBAction");
            if (findLastAction != null)
            {
                ActionCallingContext ctx = new ActionCallingContext();
                bool bRet = findLastAction.Execute(ctx);
                if (bRet)
                {
                    new Decider().Decide(EnumDecisionType.eOkDecision, "The Action " + findLastAction + " ended successfully!", "", EnumDecisionReturn.eOK, EnumDecisionReturn.eOK);
                }
                else
                {
                    new Decider().Decide(EnumDecisionType.eOkDecision, "The Action " + findLastAction + " ended with errors!", "", EnumDecisionReturn.eOK, EnumDecisionReturn.eOK);
                }
            }
        }

        private List<Terminal> GetLastTerminsls(Project currentProject)
        {
            FunctionsFilter terminalStripsFunctionsFilter = new FunctionsFilter();
            terminalStripsFunctionsFilter.Category = Function.Enums.Category.Terminal;


            Terminal[] terminals = new DMObjectsFinder(currentProject)
                .GetTerminals(terminalStripsFunctionsFilter);
            var terminalGroups = terminals
                .ToLookup(terminal => terminal.Properties.FUNC_IDENTDEVICETAG);

            //TODO:если нет определения клеммника то создать

            TerminalStrip[] terminalStrips = terminalGroups.Select(x =>
           {
               if (x.First().TerminalStrip == null)
               {
                  
                   //SymbolLibrary s = new SymbolLibrary(currentProject,"SPECIAL");
                   //Symbol symbol = new Symbol(s, 6);
                   //SymbolVariant symbolVariant = new SymbolVariant(symbol,0);
                   
                   
                   //TerminalStrip terminalStrip = new TerminalStrip();
                   //terminalStrip.Create(currentProject, symbolVariant);

                   

                   ////return new TerminalStrip().Create(currentProject,);
                   //// return new TerminalStrip().Create(currentProject);
                  AddFunctionDifinition();
               }
               return x.First().TerminalStrip;
           }).ToArray();

            DeviceService deviceService = new DeviceService();
          //TODO:убрал пока на всякий случай
          //deviceService.SortTerminalStrips(terminalStrips, DeviceService.TerminalStripSortMethods.Default);
            deviceService.SortTerminalStrips(terminalStrips, DeviceService.TerminalStripSortMethods.Numeric);
            //потом взять из по свойству #20809 последнюю главную клемму;

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

            }
            return record;
        }

        public void GetActionProperties(ref ActionProperties actionProperties)
        {

        }

    }
}
