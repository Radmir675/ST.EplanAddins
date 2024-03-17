using Eplan.EplApi.Base;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.DataModel.Graphics;
using Eplan.EplApi.EServices.Ged;
using Eplan.EplApi.HEServices;
using System.Diagnostics;
using System.Linq;

namespace ST.EplAddin.Footnote
{

    /// <summary>
    /// XGedStartInteractionAction /Name:XGedFootnote
    /// https://www.eplan.help/en-US/infoportal/content/api/2.8/Interactions.html
    /// </summary>
    [InteractionAttribute(Name = "XGedFootnote", Ordinal = 50, Prio = 20)]
    public class Footnote_InsertInteraction : Interaction
    {
        private enum State { Init, Selection, SourcePoint, TargetPoint, Finished }
        private State state = State.Init;

        private PointD startPoint = new PointD(0, 0);
        private PointD endPoint = new PointD(0, 0);

        private string vpartID;
        private ViewPart vpart;
        //FootnoteItem cursor = null;

        public override bool IsAutorestartEnabled => true;

        public void stateInit()
        {
            new Edit().ClearSelection();
            this.state = State.Init;
        }

        public void stateSelection()
        {
            this.state = State.Selection;
            this.PromptForStatusLine = "Выберите объект пространаства листа.";
        }

        public void stateSourcePoint()
        {
            this.state = State.SourcePoint;
            this.PromptForStatusLine = "Выберите точку на объекте.";
        }

        public void stateTargetPoint()
        {
            this.state = State.TargetPoint;
            this.PromptForStatusLine = "Выберите точку установки выноски.";

            //TODO: SETSTATICCURSOR
            if (STSettings.instance.LINECURSOR)
                SetRubberlineCursor();
        }

        public void stateFinished()
        {
            this.state = State.Finished;
            this.PromptForStatusLine = "Завершено.";

            ClearCursor();
            /*
            if (!STSettings.instance.LINECURSOR && cursor != null)
            {
                cursor.block.Remove();
                cursor = null;
            }*/
        }

        public bool isViewPartSelected()
        {
            SelectionSet s = new SelectionSet();
            s.LockProjectByDefault = false;
            s.LockSelectionByDefault = false;
            StorableObject o = s.GetSelectedObject(true);
            return (o != null && o is ViewPart);
        }

        public void setSource(StorableObject o)
        {
            if (o != null && o is ViewPart)
            {
                vpartID = (o as ViewPart).ToStringIdentifier();
                vpart = (o as ViewPart);
            }

        }

        public override RequestCode OnStart(InteractionContext oContext)
        {
            STSettings.instance.LoadSettings();
            state = State.Init;
            IsPlacementFilterActive = true;

            this.Description = "Вставить сноску";

            SelectionSet s = new SelectionSet();
            s.LockProjectByDefault = false;
            s.LockSelectionByDefault = false;
            StorableObject o = s.GetSelectedObject(true);

            if (isViewPartSelected())
            {
                Trace.WriteLine("OnStart isViewPartSelected");
                setSource(o);
                stateSourcePoint();
                return RequestCode.Point;
            }
            else
            {
                Trace.WriteLine("OnStart Execute Nonselected");
                stateSelection();
                return RequestCode.Select | RequestCode.NoPreselect | RequestCode.NoMultiSelect | RequestCode.Highlite | RequestCode.IgnoreGroup;
            }

            return RequestCode.Stop;
        }

        public override RequestCode OnSelect(StorableObject[] arrSelectedObjects, SelectionContext oContext)
        {
            base.OnSelect(arrSelectedObjects, oContext);

            Trace.WriteLine("OnSelect");
            stateSourcePoint();
            setSource(arrSelectedObjects.FirstOrDefault());

            return RequestCode.Point;
        }

        public override RequestCode OnPoint(Position oPosition)
        {
            base.OnPoint(oPosition);
            Trace.WriteLine("OnPoint");
            if (state == State.SourcePoint)
            {
                startPoint = oPosition.FinalPosition;
                stateTargetPoint();
                return RequestCode.Point;
            }

            if (state == State.TargetPoint)
            {
                endPoint = oPosition.FinalPosition;
                stateFinished();
                return RequestCode.Success;
            }

            return RequestCode.Stop;
        }

        /*public override StorableObject[] OnDrawCursor(Position oPosition)
        {
            if (!STSettings.instance.LINECURSOR && this.state == State.TargetPoint)
            {

                Pen penline = new Pen();
                penline.ColorId = 0;
                penline.StyleId = 0;
                penline.StyleFactor = -16002;
                penline.Width = STSettings.instance.LINEWIDTH;
                penline.LineEndType = 0;

                Line noteline = new Line();
                noteline.CreateTransient(this.Project);
                //noteline.Create(currentPage);
                noteline.Pen = penline;

                PointD notePosition = oPosition.FinalPosition;
                PointD itemPosition = startPoint;
                double textwidth = 10;
                bool INVERTDIRECTION = false;

                noteline.StartPoint = notePosition;

                PointD endpoint = notePosition;


                if (notePosition.X > itemPosition.X ^ INVERTDIRECTION)
                {
                    endpoint.X += textwidth;
                    noteline.EndPoint = endpoint;
                }
                else
                {
                    endpoint.X -= textwidth;
                    noteline.EndPoint = endpoint;
                }
                

                //if (cursor == null)
                //{
                //    cursor = new FootnoteItem();
               //     cursor.Create(this.Page);
                ////    cursor.createSubItems();
                 //   cursor.setReferencedObject(vpartID);
                //    cursor.updateSubItems();
                //    cursor.setItemPoint(startPoint);
                //}
                //

                //cursor.setNotePoint(oPosition.FinalPosition);
                //cursor.GroupWithReferencedObject();
                //
                
                //double x1 = startPoint.X + cursor.block.Properties.INSTANCE_XCOORD.ToDouble();
                //double y1 = startPoint.Y + cursor.block.Properties.INSTANCE_YCOORD.ToDouble();
                //cursor.setItemPoint(new PointD(x1, y1));

               // double x2 = oPosition.FinalPosition.X - cursor.block.Properties.INSTANCE_XCOORD.ToDouble();
               // double y2 = oPosition.FinalPosition.Y - cursor.block.Properties.INSTANCE_YCOORD.ToDouble();
               // cursor.setNotePoint(new PointD(x2, y2));
                
                return new StorableObject[] { noteline };
            }
            else

                return base.OnDrawCursor(oPosition);
        
        }*/

        public override bool OnFilterElement(StorableObject oStorableObject)
        {

            if (oStorableObject is ViewPart)
                return true;
            return false;
        }

        public override void OnSuccess(InteractionContext oContext)
        {
            using (SafetyPoint safetyPoint = SafetyPoint.Create())
            {
                Trace.WriteLine("OnSuccess");

                FootnoteItem note = new FootnoteItem(); ;
                note.Create(this.Page);
                note.SetSourceObject(vpart);
                note.SetItemPoint(startPoint);
                note.SetNotePoint(endPoint);

                note.UpdateBlock();
                note.UpdateSubItems();
                note.Serialize();
                note.GroupWithViewPlacement(vpart.Group as ViewPlacement);

                stateInit();

                safetyPoint.Commit();
            }
        }
    }
}
