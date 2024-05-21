using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.DataModel.Graphics;
using Eplan.EplApi.HEServices;
using System.Linq;
using System.Windows.Forms;

namespace ST.EplAddin.Comments
{
    public class CommentRemoveAllAccepted : IEplAction
    {
        public bool Execute(ActionCallingContext oActionCallingContext)
        {
            int count = oActionCallingContext.GetParameterCount();
            if (count != 3)
                return false;

            string action = string.Empty;
            oActionCallingContext.GetParameter("0", ref action);

            string type = string.Empty;
            oActionCallingContext.GetParameter("1", ref type);

            Comment.Enums.ReviewStateType reviewState = Comment.Enums.ReviewStateType.ReviewNone;
            switch (type)
            {
                case "0":
                case "ReviewNone": reviewState = Comment.Enums.ReviewStateType.ReviewNone; break;
                case "1":
                case "Accepted": reviewState = Comment.Enums.ReviewStateType.Accepted; break;
                case "2":
                case "Rejected": reviewState = Comment.Enums.ReviewStateType.Rejected; break;
                case "3":
                case "Cancelled": reviewState = Comment.Enums.ReviewStateType.Cancelled; break;
                case "4":
                case "Completed": reviewState = Comment.Enums.ReviewStateType.Completed; break;
            }

            switch (action)
            {
                case "/hide":
                    return CommentSetVisibility(reviewState, false);

                case "/show":
                    return CommentSetVisibility(reviewState, true);

                case "/remove":
                    return CommentRemove(reviewState);
            }

            return false;
        }

        public void GetActionProperties(ref ActionProperties actionProperties)
        {
        }

        public bool OnRegister(ref string Name, ref int Ordinal)
        {
            Name = "CommentManager";
            Ordinal = 20;
            return true;
        }

        public bool CommentRemove(Comment.Enums.ReviewStateType type)
        {
            SelectionSet Set = new SelectionSet();
            Project CurrentProject = Set.GetCurrentProject(true);
            if (CurrentProject == null) return false;
            string ProjectName = CurrentProject.ProjectName;
            int count = 0;
            //find all comment objects in project
            DMObjectsFinder oFinder = new DMObjectsFinder(CurrentProject);
            Comment[] arrComments = oFinder.GetStorableObjects(null).OfType<Comment>().ToArray();

            //Work on all found comments
            foreach (Comment oComment in arrComments)
            {
                if (oComment.ReviewState == type)
                {
                    if (oComment.Group == null)
                        oComment.Remove();
                    else
                    {
                        Placement[] groupPlacements = oComment.Group.SubPlacements;
                        foreach (Placement oPlacement in groupPlacements)
                            oPlacement.Remove();
                    }
                    count++;
                }
            }
            MessageBox.Show("Comments removed: " + count);
            return true;
        }

        public bool CommentSetVisibility(Comment.Enums.ReviewStateType type, bool visible)
        {
            SelectionSet Set = new SelectionSet();
            Project CurrentProject = Set.GetCurrentProject(true);
            if (CurrentProject == null) return false;
            string ProjectName = CurrentProject.ProjectName;
            int count = 0;
            //find all comment objects in project
            DMObjectsFinder oFinder = new DMObjectsFinder(CurrentProject);
            Comment[] arrComments = oFinder.GetStorableObjects(null).OfType<Comment>().ToArray();

            //Work on all found comments
            foreach (Comment oComment in arrComments)
            {
                if (oComment.ReviewState == type)
                {
                    if (oComment.Group == null)
                        oComment.IsSetAsVisible = (visible) ? Placement.Visibility.ByLayer : Placement.Visibility.Invisible;
                    else
                    {
                        Placement[] groupPlacements = oComment.Group.SubPlacements;
                        foreach (Placement oPlacement in groupPlacements)
                            oPlacement.IsSetAsVisible = (visible) ? Placement.Visibility.ByLayer : Placement.Visibility.Invisible;
                    }
                    count++;
                }
            }
            MessageBox.Show("Comments processed: " + count);
            return true;
        }
    }
}

