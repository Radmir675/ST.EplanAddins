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
    public partial class Footnote_InsertInteraction : Interaction
    {
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
        /// <summary>
        /// Выбор точки на объекте
        /// </summary>
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
        }

        public bool isViewPartSelected(StorableObject storableObject)
        {
            return (storableObject != null && storableObject is ViewPart);
        }

        /// <summary>
        /// Получение ViewPart and vpartID
        /// </summary>
        /// <param name="storableObject"></param>
        public void setSource(StorableObject storableObject)
        {
            if (storableObject != null && storableObject is ViewPart vPart)
            {
                vpart = vPart;
                vpartID = vpart.ToStringIdentifier();
            }
        }
        public override RequestCode OnStart(InteractionContext oContext)
        {
            STSettings.instance.LoadSettings();
            state = State.Init;
            IsPlacementFilterActive = true;

            this.Description = "Вставить сноску";

            SelectionSet selectionSet = new SelectionSet();
            selectionSet.LockProjectByDefault = false;
            selectionSet.LockSelectionByDefault = false;
            StorableObject storableObject = selectionSet.GetSelectedObject(true);

            //TODO: тут можно сделать выноски под любые типы ст
            if (isViewPartSelected(storableObject))
            {
                Trace.WriteLine("OnStart isViewPartSelected");
                setSource(storableObject);//получили vPart
                stateSourcePoint();//выбор точки на объекте
                return RequestCode.Point;
            }
            else
            {
                Trace.WriteLine("OnStart Execute Nonselected");
                stateSelection();
                return RequestCode.Select | RequestCode.NoPreselect | RequestCode.NoMultiSelect | RequestCode.Highlite | RequestCode.IgnoreGroup;
            }
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

        public override bool OnFilterElement(StorableObject oStorableObject)
        {
            return oStorableObject is ViewPart ? true : false;
        }

        public override void OnSuccess(InteractionContext oContext)
        {
            using (SafetyPoint safetyPoint = SafetyPoint.Create())
            {
                Trace.WriteLine("OnSuccess");

                FootnoteItem note = new FootnoteItem();
                note.SetSourceObject(vpart);
                note.Create(this.Page);
                note.SetItemPoint(startPoint);// после этого начинается все заново
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
