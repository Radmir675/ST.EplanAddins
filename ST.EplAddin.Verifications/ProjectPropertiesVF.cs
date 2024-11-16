using Eplan.EplApi.DataModel;
using Eplan.EplApi.EServices;
using System.Windows.Forms;

namespace ST.EplAddin.Verifications
{
    internal class ProjectPropertiesVF : Verification
    {
        private bool IsDone = false;
        private const int m_iMessageId = 36;
        private string PathToBaseProject;
        public override void OnRegister(ref string strName, ref int iOrdinal)
        {
            strName = "ProjectPropertiesVF";
            iOrdinal = 30;
            this.VerificationPermission = IVerification.Permission.OnlineOfflinePermitted;
            this.VerificationState = IVerification.VerificationState.OnlineOfflineState;
        }

        public override string GetMessageText()
        {
            return "Свойства текущего проекта отличаются от базового %1!s!";
        }
        public override void OnStartInspection(bool bOnline)
        {
        }

        public override void OnEndInspection()
        {
        }

        public override void Execute(StorableObject storableObject)
        {
            if (IsDone) return;

            var resultDialog = ShowFileDialog();
            if (resultDialog == false)
            {
                IsDone = true;
                return;
            }
            var projectToCompare = Project;
            var baseProject = new ProjectManager().OpenProject(PathToBaseProject);
            baseProject.Close();
            IsDone = true;
            // CheckProperties(projectToCompare);
        }

        private void CheckProperties(Project projectToCompare)
        {
            string strTmp = string.Empty;
            PropertyValue oPropValue;

            foreach (AnyPropertyId hPProp in Properties.AllProjectPropIDs)
            {
                // check if exists
                if (!projectToCompare.Properties[hPProp].IsEmpty)
                {
                    if (projectToCompare.Properties[hPProp].Definition.Type == PropertyDefinition.PropertyType.String)
                    {
                        //read string property
                        oPropValue = projectToCompare.Properties[hPProp];
                        strTmp = oPropValue.ToString();
                        var res = projectToCompare.Properties[hPProp].Definition.IsNamePart;
                    }
                }
            }
            IsDone = true;
        }

        private bool ShowFileDialog()
        {
            OpenFileDialog openFileDlg = new OpenFileDialog();
            openFileDlg.Filter = "Обрабатываемые проекты(*.elk)|*.elk|Базовые проекты(*.zw9)|*.zw9";
            var result = openFileDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                PathToBaseProject = openFileDlg.FileName;
                return true;
            }
            return false;
        }

        public override void OnRegister(ref string strCreator, ref IMessage.Region eRegion, ref int iMessageId, ref IMessage.Classification eClassification,
            ref int iOrdinal)
        {
            strCreator = "Scantronic";
            eRegion = IMessage.Region.Externals;
            iMessageId = m_iMessageId;
            eClassification = IMessage.Classification.Warning;
            iOrdinal = 20;
        }


        public override void DoHelp()
        {
        }
    }
}
