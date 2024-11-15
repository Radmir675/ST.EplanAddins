using Eplan.EplApi.DataModel;
using Eplan.EplApi.EServices;

namespace ST.EplAddin.Verifications
{
    internal class ProjectPropertiesVF : Verification
    {
        private const int m_iMessageId = 36;
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

        public override void Execute(StorableObject oObject1)
        {

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
