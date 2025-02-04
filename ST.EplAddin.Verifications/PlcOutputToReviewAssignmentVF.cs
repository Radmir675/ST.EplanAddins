using Eplan.EplApi.DataModel.EObjects;
using Eplan.EplApi.EServices;
using Function = Eplan.EplApi.DataModel.Function;
using StorableObject = Eplan.EplApi.DataModel.StorableObject;

namespace ST.EplAddin.Verifications
{
    internal class PlcOutputToReviewAssignmentVF : Verification
    {
        private const int m_iMessageId = 42;
        public override void OnRegister(ref string strName, ref int iOrdinal)
        {
            strName = "PlcOutputToReviewAssignmentVF";
            iOrdinal = 30;
            VerificationPermission = IVerification.Permission.OnlineOfflinePermitted;
            VerificationState = IVerification.VerificationState.OnlineOfflineState;
        }

        public override void OnStartInspection(bool bOnline) { }

        public override void OnEndInspection() { }

        public override void Execute(StorableObject oObject1)
        {
            if (oObject1 == null) return;
            if (oObject1 is not Terminal terminal) return;
            if (terminal.Category != Function.Enums.Category.PLCTerminal) return;

            if (terminal.Properties[20121].ToInt() == 1 && !terminal.IsTemplate)
            {
                DoErrorMessage(terminal, oObject1.Project, $"{terminal.Name}");

            }
            if (terminal.Properties[20121].ToInt() == 3)
            {
                if (!terminal.IsTemplate && terminal.Properties[20470].ToBool() == false)
                {
                    DoErrorMessage(terminal, oObject1.Project, $"{terminal.Name}");
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
            iOrdinal = 30;
        }

        public override string GetMessageText()
        {
            return "Вывод ПЛК %1!s! не присвоен шаблону функции";
        }

        public override void DoHelp()
        {
        }

    }
}
