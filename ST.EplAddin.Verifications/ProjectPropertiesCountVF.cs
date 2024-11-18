using Eplan.EplApi.DataModel;
using Eplan.EplApi.EServices;
using System;

namespace ST.EplAddin.Verifications
{
    internal class ProjectPropertiesCountVF : Verification
    {
        private const int m_iMessageId = 37;
        public override void OnRegister(ref string strName, ref int iOrdinal)
        {
            strName = "ProjectPropertiesCountVF";
            iOrdinal = 30;
            this.VerificationPermission = IVerification.Permission.OnlineOfflinePermitted;
            this.VerificationState = IVerification.VerificationState.OnlineOfflineState;
        }

        public override void OnStartInspection(bool bOnline)
        {
            throw new NotImplementedException();
        }

        public override void OnEndInspection()
        {
            throw new NotImplementedException();
        }

        public override void Execute(StorableObject storableObject)
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

        public override string GetMessageText()
        {
            return "Количество свойств базового и текущего проекта отличаются %1!s!";
        }

        public override void DoHelp()
        {
        }
    }
}
