using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.Base;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.EServices.Ged;
using Eplan.EplApi.HEServices;
using System.Linq;
using Action = Eplan.EplApi.ApplicationFramework.Action;

namespace ST.EplAddin.Footnote
{

    //https://www.eplan.help/en-US/infoportal/content/api/2.8/Interactions.html
    //example of interaction attributes
    //[InteractionAttribute(Name = "FootnoteMove", NameOfBaseInteraction = "XGedMoveElementAction", Ordinal = 50, Prio = 20)]
    [InteractionAttribute(Name = "XGedIaDuplicate", NameOfBaseInteraction = "XGedIaDuplicate", Ordinal = 50, Prio = 20)]
    class Footnote_DragInteraction : Interaction
    {
        FootnoteItem note = null;
        PointD newPosition;
        PointD oldPosition;
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

                SelectionSet selectionSet = new SelectionSet();
                selectionSet.LockProjectByDefault = false;
                selectionSet.LockSelectionByDefault = false;
                StorableObject storableObject = selectionSet.GetSelectedObject(true);

                bool isBlock = storableObject is Block;
                if (isBlock)
                {
                    Block block = storableObject as Block;
                    bool isFootnoteBlock = block.Name.Contains(FootnoteItem.FOOTNOTE_KEY);
                    if (isFootnoteBlock)
                    {
                        note = new FootnoteItem();
                        note.Create(block);
                        oldPosition = note.finishPosition;
                        isFootnote = true;

                        double x = note.startPosition.X + note.block.Properties.INSTANCE_XCOORD.ToDouble();
                        double y = note.startPosition.Y + note.block.Properties.INSTANCE_YCOORD.ToDouble();
                        StartPosition = new Position(new PointD(x, y));

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
                double x2 = oPosition.FinalPosition.X - note.block.Properties.INSTANCE_XCOORD.ToDouble();
                double y2 = oPosition.FinalPosition.Y - note.block.Properties.INSTANCE_YCOORD.ToDouble();
                note.SetNotePoint(new PointD(x2, y2));

                return new StorableObject[] { note.block };
            }
            else

                return base.OnDrawCursor(oPosition);
        }

        public override RequestCode OnStartDrag(Position oPosition)
        {
            return RequestCode.Nothing;
        }

        public override RequestCode OnEndDrag(bool bSuccess, Position oPosition)
        {
            if (isFootnote && bSuccess)
            {

                double x = oPosition.FinalPosition.X - note.block.Properties.INSTANCE_XCOORD.ToDouble();
                double y = oPosition.FinalPosition.Y - note.block.Properties.INSTANCE_YCOORD.ToDouble();
                newPosition = new Position(new PointD(x, y));

                ClearCursor();
                return RequestCode.Success;
            }
            return base.OnEndDrag(bSuccess, oPosition);
        }

        public override void OnSuccess(InteractionContext result)
        {
            if (isFootnote)
            {
                note.SetNotePoint(newPosition);
                note.UpdateBlock();
                note.UpdateSubItems(note.Text);
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
                note.SetNotePoint(oldPosition);
                note.GroupWithViewPlacement();
                note.Deserialize();
                return;
            }
            base.OnCancel();
        }
    }
}
