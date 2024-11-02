using Eplan.EplApi.DataModel;
using Eplan.EplApi.DataModel.E3D;
using Eplan.EplApi.EServices;
using System;
using System.Linq;

namespace ST.EplAddin.Verifications
{
    public class Function3DArticlesCoutVerification : Eplan.EplApi.EServices.Verification
    {
        private int m_iMessageId = 30;
        public override void Execute(StorableObject storableObject)
        {
            if (storableObject is not Function3D f) return;

            if (storableObject is not Component c) return;

            var ArticlesCount = f?.ArticleReferences.FirstOrDefault()?.Properties[20482].ToInt() ?? 0;

            if (ArticlesCount > 1)
            {
                String msg = $"Изделие расположенное на 3D представлено в количестве {ArticlesCount} штук";
                DoErrorMessage(storableObject, storableObject.Project, msg);
            }
        }

        public override void OnEndInspection()
        {
            // TODO:  Add NewVerification.OnEndInspection implementation
        }

        public override void OnRegister(ref string strName, ref int iOrdinal)
        {
            strName = "Проверка числа изделий на 3Д объектах (partCount=1)";
            iOrdinal = 30;
            this.VerificationPermission = IVerification.Permission.OnlineOfflinePermitted;
            this.VerificationState = IVerification.VerificationState.OnlineOfflineState;
        }

        public override void OnStartInspection(bool bOnline)
        {

        }

        public override string GetMessageText()
        {
            return "Function3DArticlesCoutVerification static text . %1!s!";
        }

        public override void DoHelp()
        {
            // TODO:  NewVerification.DoHelp implementation
        }

        public override void OnRegister(ref String strCreator, ref Eplan.EplApi.EServices.IMessage.Region eRegion, ref int iMessageId, ref Eplan.EplApi.EServices.IMessage.Classification eClassification, ref int iOrdinal)
        {
            strCreator = "Scantronic";
            eRegion = IMessage.Region.Externals;
            iMessageId = m_iMessageId;
            eClassification = IMessage.Classification.Error;
            iOrdinal = 20;
        }
    }
}



