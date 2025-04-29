using Eplan.EplApi.Base;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.EServices;

namespace ST.EplAddin.Verifications
{
    internal class ConnectionDesignationVF : Verification
    {
        private Connection _connection;

        private const int m_iMessageId = 35;
        public override void DoHelp()
        {

        }

        public override void Execute(StorableObject storableObject)
        {

            var isProperly = IsMatching(storableObject);
            if (isProperly)
            {
                var potentialName = _connection?.Properties.POTENTIAL_NAME.ToString(ISOCode.Language.L_ru_RU);
                foreach (var connectionDefPoint in _connection.ConnectionDefPoints)
                {
                    var connectionDesignation = connectionDefPoint?.Properties.CONNECTION_DESIGNATION.ToString(ISOCode.Language.L_ru_RU);

                    if (string.IsNullOrEmpty(connectionDesignation)) return;
                    if (string.IsNullOrEmpty(potentialName))
                    {
                        if (!int.TryParse(connectionDesignation, out int result))
                        {
                            DoErrorMessage(storableObject, storableObject.Project, $"{connectionDesignation}");
                        }
                    }
                    else if (!CheckPotential(connectionDesignation, potentialName))
                    {
                        DoErrorMessage(storableObject, storableObject.Project, $"{connectionDesignation}");
                    }
                }

            }
        }
        public bool CheckPotential(string connectionDesignation, string potentialName)
        {
            if (connectionDesignation.Contains(potentialName))
            {
                var number = connectionDesignation.Remove(0, potentialName.Length);
                if (string.IsNullOrEmpty(number))
                {
                    return true;
                }
                if (int.TryParse(number, out int result))
                {
                    return true;
                }
            }
            return false;
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
            return "Неверная маркировка провода %1!s!";
        }

        public override void OnEndInspection()
        {

        }

        public override void OnRegister(ref string strName, ref int iOrdinal)
        {
            strName = "ConnectionDesignationVF";
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
