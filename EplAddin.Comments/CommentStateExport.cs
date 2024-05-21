using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.DataModel.Graphics;
using Eplan.EplApi.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eplan.EplApi.HEServices;
using static Eplan.EplApi.DataModel.Graphics.Comment;
using System.Windows.Forms;
using static Eplan.EplApi.DataModel.Placement;
using System.Xml;

namespace ST.EplAddin.Comments
{
    class CommentStateExport : IEplAction
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

            XmlWriter xw = XmlWriter.Create(sTempFile);
            xw.WriteStartDocument();
            xw.WriteStartElement("root");
            //  xw.WriteValue()
            foreach (Comment oComment in arrComments)
            {
                long id = oComment.ObjectIdentifier;
                Enums.ReviewStateType State = oComment.ReviewState;
                CommentPropertyList props = oComment.Properties;
                Visibility visibility = oComment.IsSetAsVisible;
                string IdentifyingName = oComment.Page.IdentifyingName;
                string DBOBJECTID = props.PROPUSER_DBOBJECTID.ToString();
                short color = oComment.TextColorId;
                xw.WriteStartElement("comment");
                xw.WriteAttributeString("Id", DBOBJECTID);
                xw.WriteAttributeString("State", ((int)State).ToString());
                xw.WriteAttributeString("visibility", ((int)visibility).ToString());
 
                xw.WriteEndElement();
                count++;
               
            }
            xw.WriteEndElement();
            xw.WriteEndDocument();
            // CommentStateExport.xml
            xw.Close();
            MessageBox.Show("Comments processed: " + count);
            return true;
        }

        public void GetActionProperties(ref ActionProperties actionProperties)
        {
            
        }

        public bool OnRegister(ref string Name, ref int Ordinal)
        {
            Name = "CommentStateExport";
            Ordinal = 20;
            return true;
        }
    }
}
