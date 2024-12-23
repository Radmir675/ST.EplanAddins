using Eplan.EplApi.EServices;
using System.Collections.Generic;
using System.Linq;
using Cable = Eplan.EplApi.DataModel.EObjects.Cable;
using StorableObject = Eplan.EplApi.DataModel.StorableObject;

namespace ST.EplAddin.Verifications
{
    internal class CableMainFunctionsCheckVF : Verification
    {
        private int m_iMessageId = 41;
        List<string> result;
        public override void OnRegister(ref string strName, ref int iOrdinal)
        {
            strName = "CableMainFunctionsCheckVF";
            iOrdinal = 30;
            VerificationPermission = IVerification.Permission.OnlineOfflinePermitted;
            VerificationState = IVerification.VerificationState.OnlineOfflineState;
        }

        public override void OnStartInspection(bool bOnline)
        {
            if (bOnline) { return; }
            result = new();
        }

        public override void OnEndInspection() { }

        public override void Execute(StorableObject oObject1)
        {
            if (oObject1 == null) return;
            if (oObject1 is not Cable cable) return;
            if (!cable.IsMainFunction) return;

            result.Add(cable.Name);

            if (result.Count(x => x == cable.Name) > 1)
            {
                DoErrorMessage(oObject1, oObject1.Project, cable.Name);
            }
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
            return "Кабель %1!s! имеет несколько главных функций";
        }

        public override void DoHelp() { }
    }
}
