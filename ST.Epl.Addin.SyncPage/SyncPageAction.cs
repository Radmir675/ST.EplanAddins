using Eplan.EplApi.DataModel;
using Eplan.EplApi.HEServices;
using System.Collections.Specialized;

namespace ST.EplAddin.SyncPage
{
    internal class SyncPageAction
    {
        public bool Execute()
        {
            SelectionSet selectionSet = new SelectionSet();
            Project currentProject = selectionSet.GetCurrentProject(true);

            selectionSet.LockProjectByDefault = false;
            selectionSet.LockSelectionByDefault = false;
            var currentPage = selectionSet.CurrentlyEdited;
            var fulLinkProject = currentPage.Project.ProjectLinkFilePath;

            var identifier = currentPage.ToStringIdentifier();
            StringCollection strings = new StringCollection();
            strings.Add(identifier);
            Edit edit = new Edit();
            edit.SelectObjects(fulLinkProject, strings, true);
            StorableObject[] storableObject = new StorableObject[1] { currentPage };
            edit.SynchronizeObjectsToNavigators(storableObject);
            var isFocused = edit.SetFocusToGED();
            return true;
        }
    }
}
