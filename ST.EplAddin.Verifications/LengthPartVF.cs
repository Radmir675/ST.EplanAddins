using Eplan.EplApi.DataModel;
using Eplan.EplApi.EServices;

namespace ST.EplAddin.Verifications
{
    internal class LengthPartVF : Verification
    {
        private const int m_iMessageId = 43;
        public override void OnRegister(ref string strName, ref int iOrdinal)
        {
            strName = "LengthPartVF";
            iOrdinal = 30;
            VerificationPermission = IVerification.Permission.OnlineOfflinePermitted;
            VerificationState = IVerification.VerificationState.OnlineOfflineState;
        }

        public override void OnStartInspection(bool bOnline)
        {
        }

        public override void OnEndInspection()
        {
        }

        public override void Execute(StorableObject oObject1)
        {
            if (oObject1 == null) return;
            if (oObject1 is ViewPart) return;




        }

        public override void OnRegister(ref string strCreator, ref IMessage.Region eRegion, ref int iMessageId, ref IMessage.Classification eClassification,
            ref int iOrdinal)
        {
            strCreator = "Scantronic";
            eRegion = IMessage.Region.Externals;
            iMessageId = m_iMessageId;
            eClassification = IMessage.Classification.Error;
            iOrdinal = 30;
        }

        public override string GetMessageText()
        {
            return "Длина %1!s! не соответствует главной функции";
        }

        public override void DoHelp()
        {
        }
    }
}
