using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.HEServices;
using ST.EplAddin.FootNote.ProperyBrowser;
using System;
using Action = Eplan.EplApi.ApplicationFramework.Action;

namespace ST.EplAddin.FootNote.FootNote
{
    class FootnoteMoveElementAction : IEplAction
    {
        FootNote.FootnoteItem note = null;
        bool isFootnote = false;

        public bool Execute(ActionCallingContext oActionCallingContext)
        {
            SelectionSet s = new SelectionSet();
            s.LockProjectByDefault = false;
            s.LockSelectionByDefault = false;
            StorableObject so = s.GetSelectedObject(true);

            bool isBlock = so is Block;
            if (isBlock && false)
            {
                Block bl = so as Block;
                bool isFootnoteBlock = FootnoteVerification.IsFootnoteBlock(bl);
                if (isFootnoteBlock)
                {
                    note = new FootNote.FootnoteItem();
                    note.Create(bl);
                    isFootnote = true;
                }
            }

            ActionManager oMng = new ActionManager();
            Action oAction1 = oMng.FindBaseAction(this, true);
            bool result = oAction1.Execute(oActionCallingContext);
            if (isFootnote)
            {
                int count = oActionCallingContext.GetParameterCount();
                string[] contextParams = oActionCallingContext.GetParameters();
                string[] contextStrings = oActionCallingContext.GetStrings();
            }
            return result;

        }

        public void GetActionProperties(ref ActionProperties actionProperties)
        {
            throw new NotImplementedException();
        }

        public bool OnRegister(ref string Name, ref int Ordinal)
        {
            Name = "XGedMoveElementAction1";
            Ordinal = 99;
            return true;
        }
    }
}
