using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.Base;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.DataModel.EObjects;
using Eplan.EplApi.HEServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ST.EplAddins.LastTerminalStrip
{
    public class ComparerMy : IComparer<Terminal>
    {
        public int Compare(Terminal x, Terminal y)
        {
            string s = string.Empty;

            try
            {
                s=(string)x?.Properties?.FUNC_PINORTERMINALNUMBER;
            }
            catch (Exception)
            {

                
            }



            string ss= string.Empty;
            try
            {
                ss = (string)y?.Properties?.FUNC_PINORTERMINALNUMBER;
            }
            catch (Exception)
            {

                
            }
            var result= s?.CompareTo(ss)??0;
            return result;
        }
    }

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

            var lastTerminals = GetLastTerminsls(currentProject);
            StorableObject[] storable = lastTerminals.ToArray();

            Search search = new Search();
            search.ClearSearchDB(currentProject);
            search.AddToSearchDB(storable);

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
            var mainTerminalsGroups = terminals.Where(terminal => terminal.IsMainTerminal == true).Select(x => x);
            var mainFunctionLastTerminalsGroups = mainTerminalsGroups
                .ToLookup(terminal => terminal.Properties.FUNC_IDENTDEVICETAG).ToList();

            List<Terminal> sorted = mainFunctionLastTerminalsGroups.Select(x => x?.OrderBy(y => y, new ComparerMy()).Last()).ToList();
         




            return sorted;
        }

        public void GetActionProperties(ref ActionProperties actionProperties)
        {

        }

    }
}
