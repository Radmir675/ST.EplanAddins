using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.Base;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.DataModel.EObjects;
using Eplan.EplApi.HEServices;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ST.EplAddin.PlcEdit
{
    class PlcEditAction : IEplAction
    {
        public static string actionName = "GfDlgMgrActionIGfWind";
        public Terminal[] PlcTerminals { get; set; }
        public List<PlcDataModelView> mappedPlcData { get; set; }
        public bool OnRegister(ref string Name, ref int Ordinal)
        {
            Name = actionName;
            Ordinal = 99;
            return true;
        }
        public void GetActionProperties(ref ActionProperties actionProperties) { }


        public bool Execute(ActionCallingContext oActionCallingContext)
        {
            SelectionSet selectionSet = new SelectionSet();
            selectionSet.LockProjectByDefault = false;
            selectionSet.LockSelectionByDefault = false;
            var currentProject = selectionSet.GetCurrentProject(true);

            var selectedPlcdata = selectionSet.Selection;//отфильтровать надо именно selection
            var PlcTerminals = selectedPlcdata.OfType<Terminal>().Where(x => x.Properties.FUNC_CATEGORY.ToString(ISOCode.Language.L_ru_RU) == "Вывод устройства ПЛК").ToArray();

            using (SafetyPoint safetyPoint = SafetyPoint.Create())
            {
                mappedPlcData = Mapper.GetPlcData(PlcTerminals);
                ShowTableForm(mappedPlcData.Select(x => x.Clone() as PlcDataModelView).ToList());
            }
            return true;
        }

        public void ShowTableForm(List<PlcDataModelView> plcDataModelView)
        {
            Process oCurrent = Process.GetCurrentProcess();
            var eplanOwner = new WindowWrapper(oCurrent.MainWindowHandle);

            ManagePlcForm managePlcForm = new ManagePlcForm(plcDataModelView);
            managePlcForm.Show(eplanOwner);
            managePlcForm.ApplyEvent += ManagePlcForm_ApplyEvent;
        }

        private void ManagePlcForm_ApplyEvent(object sender, CustomEventArgs e)
        {
            var newDataPlc = e.PlcDataModelView;//по итогу должны получить две разные таблицы
            var Data = mappedPlcData;

        }

        public void AssignFinction(Function sourceFunction, Function targetFunction)
        {
            Function newFunction = new Function();
            targetFunction.Assign(newFunction);
            sourceFunction.Assign(targetFunction);
            newFunction.Assign(sourceFunction);
        }
    }
}
