using Eplan.EplApi.DataModel;
using Eplan.EplApi.EServices;
using System.Linq;

namespace ST.EplAddin.Verifications
{
    internal class CableConductorsCheckPartsVF : Verification
    {
        private int m_iMessageId = 32;
        private Connection connection;
        public override void DoHelp()
        {

        }

        public override void Execute(StorableObject storableObject)
        {
            var isProperly = IsMatching(storableObject);
            if (isProperly)
            {
                connection = storableObject as Connection;
                foreach (var cdp in connection.ConnectionDefPoints)
                {
                    if (cdp.Articles.Any())
                    {
                        DoErrorMessage(storableObject, storableObject.Project, $"{connection.ConnectionDefPointProperties.CDP_CON_DT}");
                    }
                }
            }
        }
        private bool IsMatching(StorableObject objectToCheck)
        {
            if (objectToCheck is not Connection connection) return false;

            return (connection.KindOfWire == Connection.Enums.KindOfWire.Cable &&
                connection.Properties.CONNECTION_HAS_CDP.ToBool() &&
                connection.Page != null);
        }
        public override string GetMessageText()
        {
            return "Изделие на жиле кабеля %1!s!";
        }

        public override void OnEndInspection()
        {

        }

        public override void OnRegister(ref string strName, ref int iOrdinal)
        {
            strName = "CableConductorsCheckPartsVF";
            iOrdinal = 30;
            this.VerificationPermission = IVerification.Permission.OnlineOfflinePermitted;
            this.VerificationState = IVerification.VerificationState.OnlineOfflineState;
        }

        public override void OnRegister(ref string strCreator, ref IMessage.Region eRegion, ref int iMessageId, ref IMessage.Classification eClassification, ref int iOrdinal)
        {
            strCreator = "Scantronic";
            eRegion = IMessage.Region.Externals;
            iMessageId = m_iMessageId;
            eClassification = IMessage.Classification.Error;
            iOrdinal = 20;
        }

        public override void OnStartInspection(bool bOnline)
        {

        }
    }
}
