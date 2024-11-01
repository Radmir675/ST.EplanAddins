using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.HEServices;
using ST.EplAddin.FootNote.ProperyBrowser;
using System;
using Action = Eplan.EplApi.ApplicationFramework.Action;

namespace ST.EplAddin.Footnote
{
    class Footnote_SelectSameObjectsAction : IEplAction
    {
        public bool OnRegister(ref string Name, ref int Ordinal)
        {
            Name = "XGedSelectSameObjectsAction2";
            Ordinal = 99;
            return true;
        }
        public bool Execute(ActionCallingContext oActionCallingContext)
        {
            string function = "", cmdline = "";
            oActionCallingContext.GetParameter("function", ref function);
            oActionCallingContext.GetParameter("_cmdline", ref cmdline);

            int count = oActionCallingContext.GetParameterCount();
            string[] contextParams = oActionCallingContext.GetParameters();
            string[] contextStrings = oActionCallingContext.GetStrings();

            if (function == "PropertiesRedundant")
            {
                SelectionSet Set = new SelectionSet();
                Set.LockProjectByDefault = false;
                Set.LockSelectionByDefault = false;
                Project CurrentProject = Set.GetCurrentProject(true);
                StorableObject so = Set.GetSelectedObject(true);
                return FootnoteVerification.IsFootnoteBlock(so) ? true : false;
            }

            ActionManager oMng = new ActionManager();
            Action baseAction = oMng.FindBaseAction(this, true);
            return baseAction.Execute(oActionCallingContext);
        }

        public void GetActionProperties(ref ActionProperties actionProperties)
        {
            throw new NotImplementedException();
        }
    }
}
