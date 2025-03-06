using Eplan.EplApi.ApplicationFramework;

namespace ST.EplAddin.Tests
{
    public class Action : IEplAction
    {
        public static string actionName = "EplanTests";
        public bool OnRegister(ref string Name, ref int Ordinal)
        {
            Name = actionName;
            Ordinal = 99;
            return true;
        }

        public bool Execute(ActionCallingContext oActionCallingContext)
        {
            ExportPDFSchema exportPdfSchema = new();
            exportPdfSchema.SetPDFExportScheme();
            exportPdfSchema.SetPagesScheme();

            return true;
        }


        public void GetActionProperties(ref ActionProperties actionProperties)
        {

        }
    }
}
