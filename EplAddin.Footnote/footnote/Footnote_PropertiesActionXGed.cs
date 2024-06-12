using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.HEServices;
using System;
using Action = Eplan.EplApi.ApplicationFramework.Action;

namespace ST.EplAddin.Footnote
{

    class Footnote_PropertiesActionXGed : IEplAction
    {
        public bool Execute(ActionCallingContext oActionCallingContext)
        {
            string function = "", cmdline = "";
            oActionCallingContext.GetParameter("specialGroupHandling", ref function);
            oActionCallingContext.GetParameter("_cmdline", ref cmdline);

            int count = oActionCallingContext.GetParameterCount();
            string[] contextParams = oActionCallingContext.GetParameters();
            string[] contextStrings = oActionCallingContext.GetStrings();

            if (cmdline == "XGedEditPropertiesAction /specialGroupHandling:1")
            {
                SelectionSet Set = new SelectionSet();
                Set.LockProjectByDefault = false;
                Set.LockSelectionByDefault = false;
                Project CurrentProject = Set.GetCurrentProject(true);
                StorableObject so = Set.GetSelectedObject(true);

                bool isBlock = so is Block;
                if (isBlock)
                {
                    Block block = so as Block;
                    bool isFootnoteBlock = block.Name.Contains(FootnoteItem.FOOTNOTE_KEY);
                    if (isFootnoteBlock)
                    {
                        FootnoteItem note = new FootnoteItem();
                        note.Create(block);
                        PropertiesDialogForm propertiesDialogForm = new PropertiesDialogForm();
                        propertiesDialogForm.SetItem(note);
                        propertiesDialogForm.ShowDialog();
                        return true;
                    }
                }
            }
            ActionManager oMng = new ActionManager();
            Action baseAction = oMng.FindBaseAction(this, true);
            return baseAction.Execute(oActionCallingContext);
        }

        public void GetActionProperties(ref ActionProperties actionProperties)
        {
            throw new NotImplementedException();
        }

        public bool OnRegister(ref string Name, ref int Ordinal)
        {
            Name = "XGedEditPropertiesAction";
            Ordinal = 99;
            return true;

        }
    }
}
