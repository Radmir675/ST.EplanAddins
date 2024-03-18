using Eplan.EplApi.ApplicationFramework;
using System;
using System.Diagnostics;

namespace ST.EplAddin.Footnote
{
    class SettingsDialog_Action : IEplAction
    {
        public bool Execute(ActionCallingContext oActionCallingContext)
        {
            Process oCurrent = Process.GetCurrentProcess();
            var eplanHandle = new WindowWrapper(oCurrent.MainWindowHandle);

            SettingsDialogForm dialog = new SettingsDialogForm();

            dialog.ShowDialog(eplanHandle);

            return true;
        }
        public void GetActionProperties(ref ActionProperties actionProperties)
        {
            throw new NotImplementedException();
        }

        public bool OnRegister(ref string Name, ref int Ordinal)
        {
            Name = "FootnoteSettings";
            Ordinal = 20;
            return true;
        }
    }
}
