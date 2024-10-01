using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.MasterData;
using System;
using System.Linq;

namespace ST.EplAddin.CheckCableAccesorities
{
    public class CheckCheckCableAccesoritiesAction : IEplAction
    {
        public static string actionName = "CheckCableAccesorities";
        public bool Execute(ActionCallingContext oActionCallingContext)
        {
            var types = Enum.GetValues(typeof(MDPartsDatabaseItem.Enums.ProductGroup)).Cast<Enum>().ToList();
            var name = MDPartsDatabaseItem.Enums.ProductGroup.Common;

            WPF_Form form = new WPF_Form();
            form.Show();
            return true;
        }

        public void GetActionProperties(ref ActionProperties actionProperties)
        {

        }

        public bool OnRegister(ref string Name, ref int Ordinal)
        {
            Name = actionName;
            Ordinal = 99;
            return true;
        }
    }
}
