using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.DataModel.Graphics;
using Eplan.EplApi.HEServices;
using System;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using static Eplan.EplApi.DataModel.Graphics.Comment;
using static Eplan.EplApi.DataModel.Placement;

namespace ST.EplAddin.Comments
{
    class CommentStateImport : IEplAction
    {
        public bool Execute(ActionCallingContext oActionCallingContext)
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
            string sTempFile;
            sTempFile = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\CommentStateExport.xml";

            XmlReader xr = XmlReader.Create(sTempFile);

            xr.ReadStartElement();
            while (xr.ReadToNextSibling("comment"))
            {
                try
                {
                    string id = xr.GetAttribute("Id");
                    Enums.ReviewStateType State = (Enums.ReviewStateType)int.Parse(xr.GetAttribute("State"));
                    Visibility visibility = (Visibility)int.Parse(xr.GetAttribute("visibility"));

                    Comment c = arrComments.Where(x => x.Properties.PROPUSER_DBOBJECTID == id).First();
                    if (c != null)
                    {
                        c.ReviewState = State;
                        c.IsSetAsVisible = visibility;
                        count++;
                    }
                }
                catch { }
            }

            xr.ReadEndElement();
            xr.Close();
            MessageBox.Show("Comments processed: " + count);
            return true;
        }

        public void GetActionProperties(ref ActionProperties actionProperties)
        {

        }

        public bool OnRegister(ref string Name, ref int Ordinal)
        {
            Name = "CommentStateImport";
            Ordinal = 20;
            return true;
        }
    }
}
