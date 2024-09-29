using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.HEServices;
using System;
using System.Linq;
using Action = Eplan.EplApi.ApplicationFramework.Action;

namespace ST.EplAddin.Footnote
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
                Block[] blocks = dmof.GetStorableObjects(null).OfType<Block>().Where(b => b.Name.Contains(FootnoteItem.FOOTNOTE_KEY)).ToArray();

                foreach (Block block in blocks)
                {
                    String name = block.Name;
                    if (name.Contains(FootnoteItem.FOOTNOTE_KEY))
                    {
                        //FootnoteItem.updateBlockProperties(block);
                        FootnoteItem note = new FootnoteItem();
                        //note.Create(block);
                        note.UpdateBlockItems(block);
                        //note.updateSubItems();
                    }
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
