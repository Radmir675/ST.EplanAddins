using Eplan.EplApi.DataModel;
using Eplan.EplApi.EServices;

namespace ST.EplAddin.Verifications
{
    internal class TemplateCableCoreCountVF : Verification
    {
        private const int m_iMessageId = 40;
        public override void OnRegister(ref string strName, ref int iOrdinal)
        {
            strName = "TemplateCableCoreCountVF";
            iOrdinal = 30;
            VerificationPermission = IVerification.Permission.OnlineOfflinePermitted;
            VerificationState = IVerification.VerificationState.OnlineOfflineState;
        }

        public override void OnStartInspection(bool bOnline) { }

        public override void OnEndInspection() { }

        public override void Execute(StorableObject oObject1)
        {

        }

        public override void OnRegister(ref string strCreator, ref IMessage.Region eRegion, ref int iMessageId, ref IMessage.Classification eClassification,
            ref int iOrdinal)
        {
            strCreator = "Scantronic";
            eRegion = IMessage.Region.Externals;
            iMessageId = m_iMessageId;
            eClassification = IMessage.Classification.Error;
            iOrdinal = 20;
        }

        public override string GetMessageText()
        {
            return "В кабеле %1!s! не присвоены жилы";
        }

        public override void DoHelp() { }
    }
}
