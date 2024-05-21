using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.HEServices;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                Project CurrentProject = Set.GetCurrentProject(false);
                StorableObject so = Set.GetSelectedObject(true);

                bool isGroup = so is Group;
                bool isPlacement = so is Placement;

                Group gr = null;

                if (isGroup)
                    gr = so as Group;
                else
                    if (isPlacement)
                        gr = (so as Placement).Group;

                if (gr != null)
                {
                    bool isCommentBlock = gr.SubPlacements.Select(p => p is Eplan.EplApi.DataModel.Graphics.Comment).Any();
                    if (isCommentBlock)
                    {
                        //TODO Recursive search
                        Eplan.EplApi.DataModel.Graphics.Comment commentFromGroup = gr.SubPlacements
                            .Where(p => p is Eplan.EplApi.DataModel.Graphics.Comment)
                            .FirstOrDefault() as Eplan.EplApi.DataModel.Graphics.Comment;
                        /*
                        Action<Group> traverse = null;
                        traverse = (n) => {
                            n.SubPlacements.Where(p => p is Eplan.EplApi.DataModel.Graphics.Comment).FirstOrDefault();

                            var a = n.SubPlacements.Where(p => p is Group).ToList().ForEach(traverse); };
                        traverse(gr);
                        */
                        if (commentFromGroup != null)
                        {
                            using (LockingStep oLS = new LockingStep())
                            {
                                //+PROPUSER_DBOBJECTID { 25 / 30 / 1606317 / 0}

                                /*
                                    String OID = commentFromGroup.Properties.PROPUSER_DBOBJECTID.ToString();
                                    String[] OIDARRAY = OID.Split('/');
                                    String OIDP = OIDARRAY[1] + '/' + OIDARRAY[2] + '/' + OIDARRAY[3];
                                    StringCollection sc = new StringCollection();
                                    sc.Add(OIDP);
                                */

                                //default prop dialog
                                /*   
                                     new Edit().ClearSelection();
                                     new Edit().SelectObjects(CurrentProject.ProjectLinkFilePath, sc, true);
                                     new CommandLineInterpreter(true).Execute("GfDlgMgrActionIGfWind /function:PropertiesRedundant");
                                */

                                ///

                                CommentInsertForm2 cpf = new CommentInsertForm2();
                                cpf.SetComment(ref commentFromGroup);
                                cpf.ShowDialog();

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
