using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.HEServices;
using System;
using System.Collections.Specialized;
using Action = Eplan.EplApi.ApplicationFramework.Action;

namespace ST.Epl.Addin.SyncPage
{
    class FindCurrentPageAction : IEplAction
    {
        public static string actionName = "XEsSyncPDDsAction";
        public bool Execute(ActionCallingContext oActionCallingContext)
        {
            using (SafetyPoint safetyPoint = SafetyPoint.Create())
            {
                ActionManager oMng = new ActionManager();
                Action baseAction = oMng.FindBaseAction(this, true);
                bool resultSync = baseAction.Execute(oActionCallingContext);

                var result = Start();
                safetyPoint.Commit();
                return result;
            }

        }
        public bool Start()
        {
            SelectionSet selectionSet = new SelectionSet();
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
