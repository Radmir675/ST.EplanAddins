using Eplan.EplApi.Base;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.EServices;

namespace ST.EplAddin.Verifications
{
    internal class ConnectionColorVF : Verification
    {
        private Connection _connection;

        private const int m_iMessageId = 43;
        public override void DoHelp()
        {

        }
        public override void Execute(StorableObject storableObject)
        {

            var isProperly = IsMatching(storableObject);
            if (!isProperly) return;
            foreach (var connectionDefPoint in _connection.ConnectionDefPoints)
            {
                if (connectionDefPoint.Properties[31004].IsEmpty) continue;
                var connectionDesignation = connectionDefPoint.Properties.CONNECTION_DESIGNATION.ToString(ISOCode.Language.L_ru_RU);
                DoErrorMessage(connectionDefPoint, storableObject.Project, $"{connectionDesignation}");
            }
        }

        private bool IsMatching(StorableObject objectToCheck)
        {
            if (objectToCheck is not Connection connection) return false;
            _connection = connection;
            return (connection.KindOfWire == Connection.Enums.KindOfWire.IndividualConnection &&
                connection.Properties.CONNECTION_HAS_CDP.ToBool() &&
                connection.Page != null);
        }


        public override string GetMessageText()
        {
            return "На точке определения соединения содержится вручную заданный цвет %1!s!";
        }

        public override void OnEndInspection()
        {

        }

        public override void OnRegister(ref string strName, ref int iOrdinal)
        {
            strName = "ConnectionColorVF";
            iOrdinal = 30;
            this.VerificationPermission = IVerification.Permission.OnlineOfflinePermitted;
            this.VerificationState = IVerification.VerificationState.OnlineOfflineState;
        }

        public override void OnRegister(ref string strCreator, ref IMessage.Region eRegion, ref int iMessageId, ref IMessage.Classification eClassification, ref int iOrdinal)
        {
            strCreator = "Scantronic";
            eRegion = IMessage.Region.Externals;
            iMessageId = m_iMessageId;
            eClassification = IMessage.Classification.Warning;
            iOrdinal = 20;
        }

        public override void OnStartInspection(bool bOnline)
        {

        }
    }
}
