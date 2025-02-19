using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.Base;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.DataModel.EObjects;
using Eplan.EplApi.HEServices;
using System.Linq;

namespace ST.EplAddin.PlcEdit
{
    internal class TestAction : IEplAction
    {
        public const string _actionName = "CheckPArts";
        public bool OnRegister(ref string Name, ref int Ordinal)
        {
            Name = _actionName;
            Ordinal = 99;
            return true;
        }

        public bool Execute(ActionCallingContext oActionCallingContext)
        {
            SelectionSet selectionPro = new();
            var selection = selectionPro.Selection;
            selectionPro.LockProjectByDefault = false;
            selectionPro.LockSelectionByDefault = false;
            var res = (selection.FirstOrDefault() as Terminal);
            MergedFunction merged = new MergedFunction(res);
            var gg = merged.MergedObjects.Cast<Terminal>();

            foreach (var VARIABLE in gg)
            {
                var t = VARIABLE.Properties.FUNC_TYPE.GetDisplayString().GetString(ISOCode.Language.L_ru_RU);
            }
            return true;
        }

        public void GetActionProperties(ref ActionProperties actionProperties)
        {

        }
    }
}
