using Eplan.EplApi.DataModel;
using Eplan.EplApi.DataModel.E3D;
using Eplan.EplApi.EServices;
using System;

namespace ST.EplAddin.Verifications
{
    internal class LengthPartVF : Verification
    {
        private const int m_iMessageId = 44;
        private const int TOLERANCE = 1;
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
            if (oObject1 is not BusBar busBar) return;
            var name = busBar.Name;
            var length3D = Get3DLength(busBar);
            var length2D = Get2DLength(busBar);
            if (Math.Abs(length2D - length3D) > TOLERANCE)
            {
                DoErrorMessage(oObject1, name);
            }
        }

        private double Get2DLength(BusBar busBar)
        {
            var length = busBar.ArticleReferences[0]?.Properties[20496].ToDouble();
            return length ?? 0;
        }

        private double Get3DLength(BusBar busBar)
        {
            var length = Math.Round(busBar.Length, 0);
            return length;
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
            return "Длина шины %1!s! не соответствует функции в 2D представлении";
        }

        public override void DoHelp()
        {
        }
    }
}
