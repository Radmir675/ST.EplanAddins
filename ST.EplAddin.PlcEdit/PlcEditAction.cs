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

                var mappedPlcData = Mapper.GetPlcData(PlcTerminals);
                ShowTableForm(mappedPlcData);
            }
            return true;
        }

        public void ShowTableForm(List<PlcDataModelView> plcDataModelView)
        {
            Process oCurrent = Process.GetCurrentProcess();
            var eplanOwner = new WindowWrapper(oCurrent.MainWindowHandle);

            ManagePlcForm managePlcForm = new ManagePlcForm(plcDataModelView);
            managePlcForm.Show(eplanOwner);
        }

        public void AssignFinction(Function sourceFunction, Function targetFunction)
        {
            //если у этой функция пустая то надо делать замены
            //if (sourceFunction)
            //{

            //}
            sourceFunction.Assign(targetFunction);
        }
    }
}
