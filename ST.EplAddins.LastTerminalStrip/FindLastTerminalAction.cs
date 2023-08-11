using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.DataModel.EObjects;
using Eplan.EplApi.HEServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ST.EplAddins.LastTerminalStrip
{
    class FindLastTerminalAction : IEplAction
    {
        public static string actionName = "LastTerminalStrip";
        public bool OnRegister(ref string Name, ref int Ordinal)
        {
            Name =actionName ;
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

            FunctionsFilter terminalStripsFunctionsFilter=new FunctionsFilter();

            terminalStripsFunctionsFilter.Category = Function.Enums.Category.Terminal;
            Terminal[] terminals = new DMObjectsFinder(currentProject)
                .GetTerminals(terminalStripsFunctionsFilter);
            var onlyMainTerminals = terminals.Where(z => z.IsMainTerminal == true).Select(x=>x);
            var groupsWithMainLastTerminal = onlyMainTerminals.ToLookup(x => x.Properties.FUNC_IDENTDEVICETAG).Select(x => x.Last());
          
            return true;
        }

        public void GetActionProperties(ref ActionProperties actionProperties)
        {

        }

    }
}
