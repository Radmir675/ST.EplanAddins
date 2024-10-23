using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.HEServices;
using ST.EplAddin.Footnote.ProperyBrowser;
using System;
using Action = Eplan.EplApi.ApplicationFramework.Action;

namespace ST.EplAddin.Footnote
{
    class Footnote_PropertiesAction : IEplAction
    {
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
                SelectionSet selectionSet = new SelectionSet();
                selectionSet.LockProjectByDefault = false;
                selectionSet.LockSelectionByDefault = false;
                Project CurrentProject = selectionSet.GetCurrentProject(true);
                StorableObject storableObject = selectionSet.GetSelectedObject(true);
                if (storableObject is Block block)
                {

                    if (FootnoteVerification.IsFootnoteBlock(block))
                    {
                        FootnoteItem footnoteItem = new FootnoteItem();
                        footnoteItem.Create(block);
                        PropertiesDialogForm propertiesDialog = new PropertiesDialogForm();
                        propertiesDialog.SetItem(footnoteItem);
                        propertiesDialog.ShowDialog();

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
            Name = "GfDlgMgrActionIGfWind";
            Ordinal = 99;
            return true;

        }
    }
}
