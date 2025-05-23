using Eplan.EplApi.DataModel;
using Eplan.EplApi.EServices;
using System.Linq;
using Connection = Eplan.EplApi.DataModel.Connection;
using StorableObject = Eplan.EplApi.DataModel.StorableObject;

namespace ST.EplAddin.Verifications.VF
{
    internal class ConnectionCountVF : Verification
    {
        private const int m_iMessageId = 34;
        private Connection _connection;
        public override void DoHelp() { }
        public override void Execute(StorableObject oObject1)
        {
            var isProperly = IsMatching(oObject1);
            if (!isProperly) return;
            if (_connection.ConnectionDefPoints.Length > 1)
            {
                var connections = _connection.ConnectionDefPoints;
                var invalidConnection = TryGetInvalidConnection(connections);
                if (invalidConnection != null)
                {
                    DoErrorMessage(invalidConnection);
                }
            }
        }

        private ConnectionDefinitionPoint TryGetInvalidConnection(ConnectionDefinitionPoint[] connections)
        {
            var con = connections.GroupBy(x => x.Page);
            foreach (var item in con)
            {
                if (item.Count() > 1)
                {
                    var invalidConnection = item.First();
                    return invalidConnection;
                }
            }
            return null;
        }
        public override string GetMessageText()
        {
            return "Несколько точек определения соединения на одном соединении";
        }

        public override void OnEndInspection() { }

        public override void OnRegister(ref string strName, ref int iOrdinal)
        {
            strName = "ConnectionCountVF";
            iOrdinal = 30;
            VerificationPermission = IVerification.Permission.OnlineOfflinePermitted;
            VerificationState = IVerification.VerificationState.OnlineOfflineState;
        }

        public override void OnRegister(ref string strCreator, ref IMessage.Region eRegion, ref int iMessageId, ref IMessage.Classification eClassification, ref int iOrdinal)
        {
            strCreator = "Scantronic";
            eRegion = IMessage.Region.Externals;
            iMessageId = m_iMessageId;
            eClassification = IMessage.Classification.Warning;
            iOrdinal = 20;
        }

        public override void OnStartInspection(bool bOnline) { }
        private bool IsMatching(StorableObject objectToCheck)
        {
            if (objectToCheck is not Connection connection) return false;
            _connection = connection;
            return (connection.KindOfWire == Connection.Enums.KindOfWire.IndividualConnection &&
                    connection.Properties.CONNECTION_HAS_CDP.ToBool() &&
                    connection.Page != null);
        }
    }
}
