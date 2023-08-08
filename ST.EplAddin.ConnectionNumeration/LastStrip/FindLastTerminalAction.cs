using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.DataModel;
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
        public bool OnRegister(ref string Name, ref int Ordinal)
        {
            Name = "LastTerminalStrip";
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
            var s= new DMObjectsFinder(currentProject).GetConnectionsWithCF(new TerminalFilter());

            return true;
        }

        public void GetActionProperties(ref ActionProperties actionProperties)
        {
            
        }

    }
}
