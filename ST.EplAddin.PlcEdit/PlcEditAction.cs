using Eplan.EplApi.ApplicationFramework;
using System;
using System.Windows.Forms;

namespace ST.EplAddins.SymbolVariants
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
            MessageBox.Show("Мы здесь");
            return true;
        }
    }
}
