using Eplan.EplApi.ApplicationFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eplan.EplApi.EServices.Ged;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.Base;
using Eplan.EplApi.DataModel.Graphics;
using Eplan.EplApi.HEServices;
using System.Diagnostics;
using Eplan.EplApi.DataModel.E3D;
using System.Windows.Forms;
using ST.EplAddin.Footnote;

namespace EplAddin.Footnote
{

    /// <summary>
    /// XGedStartInteractionAction /Name:XGedFootnote
    /// https://www.eplan.help/en-US/infoportal/content/api/2.8/Interactions.html
    /// </summary>
    [InteractionAttribute(Name = "XGedFootnote", Ordinal = 50, Prio = 20)]
    public class FootnoteInsert : InsertInteraction
    {
        private FootnoteItem note;
        private ViewPart vpart;
        private Page currentPage;

        private enum State { Init, Selection, SourcePoint, TargetPoint, Finished }
        private State state = State.Init;

        public void stateSelection()
        {
            this.PromptForStatusLine = "Выберите объект пространаства листа.";
            state = State.Selection;

        }

        public void stateSourcePoint()
        {
            this.PromptForStatusLine = "Выберите точку на объекте.";
            state = State.SourcePoint;
        }

        public bool isViewPartSelected()
        {
            SelectionSet s = new SelectionSet();
            StorableObject o = s.GetSelectedObject(true);
            return (o != null && o is ViewPart);
        }

        public void setSource(StorableObject o)
        {
            if (o != null && o is ViewPart)
            {
                vpart = o as ViewPart;
                note.setReferencedObject(vpart);
            }
        }

        public override RequestCode OnStart(InteractionContext oContext)
        {
            state = State.Init;
            IsPlacementFilterActive = true;
            
            this.PromptForStatusLine = "Выноска легенды с пространства листа";

            try
            {
                currentPage = this.Page;
                note = new FootnoteItem();
                note.Create(currentPage);
                SelectionSet s = new SelectionSet();
                StorableObject o = s.GetSelectedObject(true);

                if (isViewPartSelected())
                {
                    setSource(o);
                    stateSourcePoint();
                    return RequestCode.Point;
                }
                else
                {
                    stateSelection();
                    return RequestCode.Select | RequestCode.NoPreselect | RequestCode.NoMultiSelect | RequestCode.Highlite | RequestCode.IgnoreGroup;
                }
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.ToString(), "Error");
            }
            return RequestCode.Stop;
        }

        public override RequestCode OnSelect(StorableObject[] arrSelectedObjects, SelectionContext oContext)
        {
            stateSourcePoint();
            setSource(arrSelectedObjects.FirstOrDefault());
            return RequestCode.Point;

        }

        public override RequestCode OnPoint(Position oPosition)
        {
            if (state == State.SourcePoint)
            {
                note.setPoint(oPosition.FinalPosition);
                state = State.TargetPoint;
                this.PromptForStatusLine = "Выберите точку установки выноски.";

                //TODO: SETSTATICCURSOR
                SetRubberlineCursor();
                return RequestCode.Point;
            }
            /*
            if (state == State.TargetPoint)
            {
                note.setPoint(oPosition.FinalPosition);
                state = State.Finished;
                this.PromptForStatusLine = "Завершено.";
                ClearCursor();
                return RequestCode.Success;
            }
            */
            return RequestCode.Stop;
        }

        public override bool OnFilterElement(StorableObject oStorableObject)
        {
            if (oStorableObject is ViewPart)
            {
                return true;
            }
            return false;
        }


        public override void OnSuccess(InteractionContext oContext)
        {/*
            using (new LockingStep())
            {
                using (UndoStep undoStep = new UndoManager().CreateUndoStep())
                {
                    undoStep.SetUndoDescription("Вставить сноску");*/
                    note.GroupWithReferencedObject();
           /*     }
            }*/
        }
 
    }
}
