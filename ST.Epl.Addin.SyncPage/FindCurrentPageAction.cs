using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.HEServices;
using System;
using System.Collections.Specialized;

namespace ST.Epl.Addin.SyncPage
{
    internal class FindCurrentPageAction : IEplAction
    {
        public static string actionName = "LastTerminalStrip";
        public bool Execute(ActionCallingContext oActionCallingContext)
        {
            SelectionSet selectionSet = new SelectionSet();
            Project currentProject = selectionSet.GetCurrentProject(true);
            var projectName = currentProject.ProjectName;
            selectionSet.LockProjectByDefault = false;
            selectionSet.LockSelectionByDefault = false;
            var currentPage = selectionSet.CurrentlyEdited;

            var identifier = currentPage.ToStringIdentifier();
            StringCollection strings = new StringCollection();
            strings.Add(identifier);
            Edit edit = new Edit();
            edit.SelectObjects(projectName, strings, true);
            edit.SetFocusToGED();
            return true;
        }

        public void GetActionProperties(ref ActionProperties actionProperties)
        {
            throw new NotImplementedException();
        }

        public bool OnRegister(ref string Name, ref int Ordinal)
        {
            Name = actionName;
            Ordinal = 32;
            return true;
        }
    }
}
