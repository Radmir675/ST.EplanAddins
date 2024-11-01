using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.HEServices;
using ST.EplAddin.FootNote.ProperyBrowser;
using System;
using System.Linq;
using Action = Eplan.EplApi.ApplicationFramework.Action;

namespace ST.EplAddin.FootNote
{
    class Footnote_UpdateAction : IEplAction
    {
        public bool Execute(ActionCallingContext oActionCallingContext)
        {
            int count = oActionCallingContext.GetParameterCount();
            string[] contextParams = oActionCallingContext.GetParameters();
            string[] contextStrings = oActionCallingContext.GetStrings();

            string paramValue = "";
            oActionCallingContext.GetParameter("_cmdline", ref paramValue);

            if (paramValue == "XFgUpdateEvaluationAction")
            {
                SelectionSet Set = new SelectionSet();
                Set.LockProjectByDefault = false;
                Set.LockSelectionByDefault = false;
                Project CurrentProject = Set.GetCurrentProject(true);

                DMObjectsFinder dmof = new DMObjectsFinder(CurrentProject);
                Block[] blocks = dmof.GetStorableObjects(null).OfType<Block>().Where(b => FootnoteVerification.IsFootnoteBlock(b)).ToArray();

                foreach (Block block in blocks)
                {
                    FootnoteItem note = new FootnoteItem();
                    note.UpdateBlockItems(block);
                }
            }
            ActionManager oMng = new ActionManager();
            Action oBaseAction = oMng.FindBaseAction(this, true);

            string props = oBaseAction.ActionProperties.ToString();
            return oBaseAction.Execute(oActionCallingContext);
        }

        public void GetActionProperties(ref ActionProperties actionProperties)
        {
            throw new NotImplementedException();
        }

        public bool OnRegister(ref string Name, ref int Ordinal)
        {
            Name = "XFgUpdateEvaluationAction";
            Ordinal = 99;
            return true;
        }
    }
}
