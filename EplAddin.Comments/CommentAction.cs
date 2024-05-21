using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.DataModel.Graphics;
using Eplan.EplApi.HEServices;
using System;
using System.Linq;
using Action = Eplan.EplApi.ApplicationFramework.Action;

namespace ST.EplAddin.Comments
{
    class CommentAction_PropertiesActionXGed : IEplAction
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
                Project currentProject = Set.GetCurrentProject(false);
                StorableObject storableObject = Set.GetSelectedObject(true);

                bool isGroup = storableObject is Group;
                bool isPlacement = storableObject is Placement;

                Group group = null;

                if (isGroup)
                    group = storableObject as Group;
                else
                    if (isPlacement)
                    group = (storableObject as Placement).Group;

                if (group != null)
                {
                    bool isCommentBlock = group.SubPlacements.Select(p => p is Comment).Any();
                    if (isCommentBlock)
                    {
                        //TODO Recursive search
                        Comment commentFromGroup = group.SubPlacements
                             .Where(p => p is Comment)
                             .FirstOrDefault() as Comment;

                        if (commentFromGroup != null)
                        {
                            using (LockingStep oLS = new LockingStep())
                            {
                                CommentPropertyForm commentsInsertForm = new CommentPropertyForm();
                                commentsInsertForm.SetComment(ref commentFromGroup);
                                commentsInsertForm.ShowDialog();

                                return true;
                            }
                        }
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
