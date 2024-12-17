using Eplan.EplApi.EServices;
using Eplan.EplApi.MasterData;
using System.Linq;
using Cable = Eplan.EplApi.DataModel.EObjects.Cable;
using StorableObject = Eplan.EplApi.DataModel.StorableObject;

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
            if (oObject1 == null) return;
            if (oObject1 is not Cable cable || !cable.IsMainFunction) return;
            if (cable.ArticleReferences.Length <= 0 || cable.ArticleReferences[0].Properties[22041].ToInt() !=
                (int)MDPartsDatabaseItem.Enums.ProductGroup.ElectricalCableConnection) return;

            if (cable.CableConnections.Any(x => !x.IsTemplate && x.Properties[20470].ToBool()))
            {
                DoErrorMessage(oObject1, oObject1.Project, $"{cable.Name}");
                return;
            }
            foreach (var item in cable.AllSubFunctions)
            {
                if (item.Connections.Any(z => !z.IsTemplate))
                {
                    DoErrorMessage(oObject1, oObject1.Project, $"{cable.Name}");
                    return;
                }

            }
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
            return "В кабеле %1!s! не присвоены жилы шаблону функции";
        }

        public override void DoHelp() { }
    }
}
