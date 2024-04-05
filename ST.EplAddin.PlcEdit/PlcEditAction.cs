using Eplan.EplApi.ApplicationFramework;
using System;
using System.Diagnostics;

namespace ST.EplAddin.PlcEdit
{
    class PlcEditAction : IEplAction
    {
        public static string actionName = "GfDlgMgrActionIGfWind";
        public bool OnRegister(ref string Name, ref int Ordinal)
        {
            Name = actionName;
            Ordinal = 99;
            return true;
        }
        public void GetActionProperties(ref ActionProperties actionProperties)
        {
            throw new NotImplementedException();
        }

        public bool Execute(ActionCallingContext oActionCallingContext)
        {
            Process oCurrent = Process.GetCurrentProcess();
            var eplanOwner = new WindowWrapper(oCurrent.MainWindowHandle);

            ManagePlcForm managePlcForm = new ManagePlcForm();
            managePlcForm.Show(eplanOwner);
            return true;
        }
    }
}
