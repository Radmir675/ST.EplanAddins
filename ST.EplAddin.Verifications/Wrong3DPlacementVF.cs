using Eplan.EplApi.DataModel;
using Eplan.EplApi.DataModel.E3D;
using Eplan.EplApi.EServices;
using System.Linq;
using static Eplan.EplApi.MasterData.MDPartsDatabaseItem.Enums;

namespace ST.EplAddin.Verifications
{
    internal class Wrong3DPlacementVF : Verification
    {
        private int m_iMessageId = 33;
        public override void DoHelp()
        {
        }

        public override void Execute(StorableObject storableObject)
        {
            if (storableObject is Function3D)
            {
                var objProps = (MergedArticleReferencePropertyList)storableObject.Properties;

                //Если только в 3Д проверить раздел изделия исключить электрику
                int functionType = objProps.FUNC_TYPE.ToInt(); //3D: -8
                if (functionType == -8)
                {
                    bool isElectro = objProps.ARTICLE_PRODUCTTOPGROUP.ToInt() == (int)ProductTopGroup.Electric;
                    if (isElectro)
                    {
                        Placement3D placement3D = (storableObject as MergedArticleReference).GetRelatedObjects().OfType<Placement3D>().FirstOrDefault();
                        Connection3D connection3D = (storableObject as MergedArticleReference).GetRelatedObjects().OfType<Connection3D>().FirstOrDefault();

                        bool isTerminalDefinition = false;
                        bool is3DConnection = false;
                        if (placement3D != null)
                        {
                            isTerminalDefinition = placement3D.Properties.FUNC_CATEGORY_GROUP_ID == "8/1/2"; //Размещение изделия, клеммник
                        }

                        if (connection3D != null) //3D Соединение
                        {
                            is3DConnection = true;
                        }

                        if (!isTerminalDefinition && !is3DConnection)
                        {
                            PropertyValue PARTNO = objProps.ARTICLEREF_PARTNO;
                            var name = objProps.FUNC_DEVICETAG_MAINNAME.ToString();
                            DoErrorMessage(storableObject, storableObject.Project, $"{PARTNO + name}");
                        }
                    }
                }
            }
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
