using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.Base;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.DataModel.EObjects;
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
                safetyPoint.Commit();
            }

            return true;
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
               //if (x.First().TerminalStrip == null)
               //{
               //    return new TerminalStrip().Cr;
               //   // return new TerminalStrip().Create(currentProject);
               //}
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
