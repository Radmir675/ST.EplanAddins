using Eplan.EplApi.Base;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.DataModel.Graphics;
using Eplan.EplApi.EServices.Ged;
using Eplan.EplApi.HEServices;
using System.Diagnostics;
using System.Linq;

namespace ST.EplAddin.FootNote.FootNote
{
    /// <summary>
    /// XGedStartInteractionAction /Name:XGedFootnote
    /// https://www.eplan.help/en-US/infoportal/content/api/2.8/Interactions.html
    /// </summary>
    [Interaction(Name = "XGedFootnote", Ordinal = 50, Prio = 20)]
    public partial class Footnote_InsertInteraction : Interaction
    {
        private Footnote_InsertInteraction.State state = Footnote_InsertInteraction.State.Init;

        private PointD startPoint = new PointD(0, 0);
        private PointD endPoint = new PointD(0, 0);

        private string vpartID;
        private ViewPart vpart;
        //FootnoteItem cursor = null;
        public override bool IsAutorestartEnabled => true;

        public void StateInit()
        {
            new Edit().ClearSelection();
            this.state = Footnote_InsertInteraction.State.Init;
        }

        public void StateSelection()
        {
            this.state = Footnote_InsertInteraction.State.Selection;
            this.PromptForStatusLine = "Выберите объект пространства листа.";
        }
        /// <summary>
        /// Выбор точки на объекте
        /// </summary>
        public void StateSourcePoint()
        {
            this.state = Footnote_InsertInteraction.State.SourcePoint;
            this.PromptForStatusLine = "Выберите точку на объекте.";
        }

        public void StateTargetPoint()
        {
            this.state = Footnote_InsertInteraction.State.TargetPoint;
            this.PromptForStatusLine = "Выберите точку установки выноски.";

            //TODO: SETSTATICCURSOR
            if (STSettings.instance.LINECURSOR)
                SetRubberlineCursor();
        }

        public void StateFinished()
        {
            this.state = Footnote_InsertInteraction.State.Finished;
            this.PromptForStatusLine = "Завершено.";

            ClearCursor();
        }

        public bool IsViewPartSelected(StorableObject storableObject)
        {
            return (storableObject != null && storableObject is ViewPart);
        }

        /// <summary>
        /// Получение ViewPart and vpartID
        /// </summary>
        /// <param name="storableObject"></param>
        public void SetSource(StorableObject storableObject)
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
            state = Footnote_InsertInteraction.State.Init;
            IsPlacementFilterActive = true;

            this.Description = "Вставить сноску";

            SelectionSet selectionSet = new SelectionSet();
            selectionSet.LockProjectByDefault = false;
            selectionSet.LockSelectionByDefault = false;
            StorableObject storableObject = selectionSet.GetSelectedObject(true);

            //TODO: тут можно сделать выноски под любые типы ст
            if (IsViewPartSelected(storableObject))
            {
                Trace.WriteLine("OnStart isViewPartSelected");
                SetSource(storableObject);//получили vPart
                StateSourcePoint();//выбор точки на объекте
                return RequestCode.Point;
            }
            else
            {
                Trace.WriteLine("OnStart Execute Nonselected");
                StateSelection();
                return RequestCode.Select | RequestCode.NoPreselect | RequestCode.NoMultiSelect | RequestCode.Highlite | RequestCode.IgnoreGroup;
            }
        }

        public override RequestCode OnSelect(StorableObject[] arrSelectedObjects, SelectionContext oContext)
        {
            base.OnSelect(arrSelectedObjects, oContext);

            Trace.WriteLine("OnSelect");
            StateSourcePoint();
            SetSource(arrSelectedObjects.FirstOrDefault());

            return RequestCode.Point;
        }

        public override RequestCode OnPoint(Position oPosition)
        {
            base.OnPoint(oPosition);
            Trace.WriteLine("OnPoint");
            if (state == Footnote_InsertInteraction.State.SourcePoint)
            {
                startPoint = oPosition.FinalPosition;
                StateTargetPoint();
                return RequestCode.Point;
            }

            if (state == Footnote_InsertInteraction.State.TargetPoint)
            {
                endPoint = oPosition.FinalPosition;
                StateFinished();
                return RequestCode.Success;
            }

            return RequestCode.Stop;
        }

        public override bool OnFilterElement(StorableObject oStorableObject)
        {
            return oStorableObject is ViewPart;
        }

        public override void OnSuccess(InteractionContext oContext)
        {
            using (SafetyPoint safetyPoint = SafetyPoint.Create())
            {
                Trace.WriteLine("OnSuccess");

                var note = new FootNote.FootnoteItem(startPoint, endPoint);
                note.SetSourceObject(vpart);
                note.Create(Page);
                note.Serialize();
                note.GroupWithViewPlacement(vpart.Group as ViewPlacement);

                StateInit();

                safetyPoint.Commit();
            }
        }
    }
}
