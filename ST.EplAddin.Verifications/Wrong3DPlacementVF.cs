using Eplan.EplApi.Base;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.DataModel.E3D;
using Eplan.EplApi.EServices;
using static Eplan.EplApi.MasterData.MDPartsDatabaseItem.Enums;

namespace ST.EplAddin.Verifications
{
    internal class Wrong3DPlacementVF : Verification
    {
        private const int m_iMessageId = 33;
        public override void DoHelp()
        {
        }

        public override void Execute(StorableObject storableObject)
        {
            if (storableObject is Function3D function3D)
            {
                if (!function3D.Properties.FUNC_ISPLACEDIN_CIRCUIT && !function3D.Properties.FUNC_ISPLACEDIN_OVERVIEW && !function3D.Properties.FUNC_ISPLACEDIN_SINGLELINE)
                {
                    if (function3D.ArticleReferences.Length == 0) return;
                    foreach (var article in function3D.ArticleReferences)
                    {
                        bool isElectro = article?.Properties.ARTICLE_PRODUCTTOPGROUP?.ToInt() == (int)ProductTopGroup.Electric;
                        if (isElectro)
                        {
                            if (storableObject is Placement3D placement3D)
                            {
                                var productGroup = article?.Properties.ARTICLE_PRODUCTSUBGROUP.ToInt();//подгруппа продуктов

                                if (!IsOk(productGroup.Value)) continue;
                                var name = placement3D.Properties[20002].ToString(ISOCode.Language.L_ru_RU);
                                var partNumber = article.Properties[20481];
                                DoErrorMessage(storableObject, storableObject.Project, $"{partNumber + "|" + name}");
                            }
                        }
                    }
                }
            }
        }

        private bool IsOk(int productGroup)
        {
            return productGroup != (int)ProductSubGroup.ElectricalEndangle;
        }

        public override string GetMessageText()
        {
            return "Изделие из раздела Электрика расположено только в 3Д %1!s!";
        }

        public override void OnEndInspection()
        {

        }

        public override void OnRegister(ref string strName, ref int iOrdinal)
        {
            strName = "Wrong3DPlacementVF";
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
