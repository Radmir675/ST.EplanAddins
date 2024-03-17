using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.Base;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.HEServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Action = Eplan.EplApi.ApplicationFramework.Action;

namespace ST.EplAddin.Footnote
{
    class FootnoteMoveElementAction : IEplAction
    {
        FootnoteItem note = null;
        PointD newposition;
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
                bool isFootnoteBlock = bl.Name.Contains(FootnoteItem.FOOTNOTE_KEY);
                if (isFootnoteBlock)
                {
                    note = new FootnoteItem();
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

                int a = 0;
                //note.itemPosition;// = new PointPosition;
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
