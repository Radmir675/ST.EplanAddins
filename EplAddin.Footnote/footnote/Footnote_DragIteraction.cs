using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.Base;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.EServices.Ged;
using Eplan.EplApi.HEServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Action = Eplan.EplApi.ApplicationFramework.Action;

namespace ST.EplAddin.Footnote
{

    //https://www.eplan.help/en-US/infoportal/content/api/2.8/Interactions.html
    //example of interaction attributes
    //[InteractionAttribute(Name = "FootnoteMove", NameOfBaseInteraction = "XGedMoveElementAction", Ordinal = 50, Prio = 20)]
    [InteractionAttribute(Name = "XGedIaDuplicate", NameOfBaseInteraction = "XGedIaDuplicate", Ordinal = 50, Prio = 20)]
    class Footnote_DragInteraction : Interaction
    {
        /*
        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(UInt16 virtualKeyCode);*/

        //public static System.Windows.Input.MouseButtonState LeftButton { get; }

        FootnoteItem note = null;
        PointD newposition;
        PointD oldposition;
        bool isFootnote = false;


        public override RequestCode OnStart(InteractionContext pContext)
        {

            string[] param = pContext.GetParameters();

            if (param.Count() > 3
                 && param[0] == "numberofcopies"
                 && param[1] == "toggle"
                 && param[2] == "performmove"
                 && param[3] == "object2drag")
            {

                SelectionSet s = new SelectionSet();
                s.LockProjectByDefault = false;
                s.LockSelectionByDefault = false;
                StorableObject so = s.GetSelectedObject(true);

                bool isBlock = so is Block;
                if (isBlock)
                {
                    Block bl = so as Block;
                    bool isFootnoteBlock = bl.Name.Contains(FootnoteItem.FOOTNOTE_KEY);
                    if (isFootnoteBlock)
                    {
                        note = new FootnoteItem();
                        note.Create(bl);
                        oldposition = note.notePosition;
                        isFootnote = true;

                        double x = note.itemPosition.X + note.block.Properties.INSTANCE_XCOORD.ToDouble();
                        double y = note.itemPosition.Y + note.block.Properties.INSTANCE_YCOORD.ToDouble();
                        StartPosition = new Position(new PointD(x, y));
                        //StartPosition = new Position(note.itemPosition));


                        //SetStaticCursor(bl, new PointD(0, 0));
                        if (STSettings.instance.LINECURSOR)
                        SetRubberlineCursor();

                        PromptForStatusLine = "Перетащите сноску";
                        this.Description = "Переместить сноску";
                        return RequestCode.Drag | RequestCode.Point;
                    }
                }
            }

            return base.OnStart(pContext);
        }


        public override RequestCode OnSpecialEvent(InteractionEvent pEvent)
        {
            if (pEvent.Name == "ChangeCorner") //TAB
            {
                note.INVERTDIRECTION = !note.INVERTDIRECTION;
                note.UpdateSubItems();
            }
            return base.OnSpecialEvent(pEvent);
        }

        public override RequestCode OnChar(Position oPosition, char c)
        {
            return base.OnChar(oPosition, c);
        }

        public override StorableObject[] OnDrawCursor(Position oPosition)
        {
           
            if (isFootnote && !STSettings.instance.LINECURSOR)
            {
                //FootnoteItem cursor = new FootnoteItem();
                //cursor.Create(note.block);
                /*
                double x1 = note.itemPosition.X + note.block.Properties.INSTANCE_XCOORD.ToDouble();
                double y1 = note.itemPosition.Y + note.block.Properties.INSTANCE_YCOORD.ToDouble();
                cursor.setItemPoint(new PointD(x1, y1));*/

                double x2 = oPosition.FinalPosition.X - note.block.Properties.INSTANCE_XCOORD.ToDouble();
                double y2 = oPosition.FinalPosition.Y - note.block.Properties.INSTANCE_YCOORD.ToDouble();
                note.SetNotePoint(new PointD(x2, y2));

                return new StorableObject[] { note.block };
            } else
            
            return base.OnDrawCursor(oPosition);
        }


        public override RequestCode OnStartDrag(Position oPosition)
        {
            return RequestCode.Nothing;
            return base.OnStartDrag(oPosition);
        }


        public override RequestCode OnEndDrag(bool bSuccess, Position oPosition)
        {
            if (isFootnote && bSuccess)
            {

                double x = oPosition.FinalPosition.X - note.block.Properties.INSTANCE_XCOORD.ToDouble();
                double y = oPosition.FinalPosition.Y - note.block.Properties.INSTANCE_YCOORD.ToDouble();
                newposition = new Position(new PointD(x, y));
                //newposition = oPosition.FinalPosition;

                ClearCursor();
                return RequestCode.Success;
            }

            return base.OnEndDrag(bSuccess, oPosition);
        }

        public override void OnSuccess(InteractionContext result)
        {
            if (isFootnote)
            {
                note.SetNotePoint(newposition);
                note.UpdateBlock();
                note.UpdateSubItems();
                note.Serialize();

                note.GroupWithViewPlacement();

                

                ActionManager oMng = new ActionManager();
                Action baseAction = oMng.FindAction("gedRedraw");
                baseAction.Execute(new ActionCallingContext());
                
                return;
            }
            base.OnSuccess(result);
        }

        public override void OnCancel()
        {
            if (isFootnote)
            {
                note.SetNotePoint(oldposition);
                note.GroupWithViewPlacement();
                note.Deserialize();
                return;
            }
            base.OnCancel();
        }
    }
}
