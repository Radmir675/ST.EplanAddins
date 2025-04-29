using Eplan.EplApi.DataModel;
using Eplan.EplApi.DataModel.E3D;
using Eplan.EplApi.EServices;
using System;
using System.Linq;

namespace ST.EplAddin.Verifications
{
    public class Function3DArticlesCoutVF : Verification
    {
        private int m_iMessageId = 30;
        public override void Execute(StorableObject storableObject)
        {
            if (storableObject is Function3D f && storableObject is Component c)
            {
                var ArticlesCount = f?.ArticleReferences.FirstOrDefault()?.Properties[20482].ToInt() ?? 0;
                if (ArticlesCount > 1)
                {
                    DoErrorMessage(storableObject, storableObject.Project, $"{c.VisibleName}");
                }
            }
        }

        public override void OnEndInspection()
        {
            // TODO:  Add NewVerification.OnEndInspection implementation
        }

        public override void OnRegister(ref string strName, ref int iOrdinal)
        {
            strName = "Function3DArticlesCoutVF";
            iOrdinal = 30;
            this.VerificationPermission = IVerification.Permission.OnlineOfflinePermitted;
            this.VerificationState = IVerification.VerificationState.OnlineOfflineState;
        }

        public override void OnStartInspection(bool bOnline)
        {

        }

        public override string GetMessageText()
        {
            return "Изделие расположенное на 3D представлено в количестве больше 1: %1!s!"; ;
        }

        public override void DoHelp()
        {
            // TODO:  NewVerification.DoHelp implementation
        }

        public override void OnRegister(ref String strCreator, ref IMessage.Region eRegion, ref int iMessageId, ref IMessage.Classification eClassification, ref int iOrdinal)
        {
            strCreator = "Scantronic";
            eRegion = IMessage.Region.Externals;
            iMessageId = m_iMessageId;
            eClassification = IMessage.Classification.Error;
            iOrdinal = 30;
        }
    }
}



