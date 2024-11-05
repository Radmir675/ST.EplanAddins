using Eplan.EplApi.Base;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.DataModel.EObjects;
using Eplan.EplApi.EServices;
using Eplan.EplApi.MasterData;
using System.Linq;
using System.Text.RegularExpressions;

namespace ST.EplAddin.Verifications
{
    internal class ArticleReferenceUnitCheckVF : Verification
    {
        private int m_iMessageId = 30;
        public override void DoHelp()
        {

        }

        public override void Execute(StorableObject storableObject)
        {
            if (storableObject is Cable cable)
            {
                var checkedArticle = cable?.Articles.FirstOrDefault();
                string pattern = @"^шт\.?$";
                if (checkedArticle?.Properties[22028].ToInt() == (int)MDPartsDatabaseItem.Enums.ProductSubGroup.ElectricalCablePreFabricated)
                {
                    var unit = checkedArticle.Properties[22042].GetDisplayString().GetString(ISOCode.Language.L_ru_RU);
                    if (!Regex.IsMatch(unit, pattern, RegexOptions.IgnoreCase)) //единица измерения
                    {
                        var unit4 = checkedArticle.Properties[22042].GetDisplayString().GetStringToDisplay(ISOCode.Language.L_ru_RU);
                        DoErrorMessage(storableObject, storableObject.Project, $"{cable.Name}");
                    }
                }
            }
        }

        public override string GetMessageText()
        {
            return "Неверная единица измерения кабеля %1!s!";
        }

        public override void OnEndInspection()
        {

        }

        public override void OnRegister(ref string strName, ref int iOrdinal)
        {
            strName = "ArticleReferenceUnitCheckVF";
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
