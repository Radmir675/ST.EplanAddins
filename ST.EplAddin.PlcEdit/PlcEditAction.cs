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
        public static string actionName = "PlcGuiIGfWindRackConfiguration";
        public Terminal[] PlcTerminals { get; set; }
        public List<PlcDataModelView> mappedPlcData { get; set; }
        public Project CurrentProject { get; set; }

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
            CurrentProject = selectionSet.GetCurrentProject(true);

            var selectedPlcdata = selectionSet.Selection;//отфильтровать надо именно selection
            PlcTerminals = selectedPlcdata.OfType<Terminal>().Where(x => x.Properties.FUNC_CATEGORY.ToString(ISOCode.Language.L_ru_RU) == "Вывод устройства ПЛК").ToArray();

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
            var functionsInProgram = PlcTerminals.Cast<Function>();
            var newDataPlc = e.PlcDataModelView;//по итогу должны получить две разные таблицы
            var oldDataPlc = mappedPlcData;//тут нужно получить новые данные
            var correlationTable = GetСorrelationTable(oldDataPlc, newDataPlc);
            foreach (var item in correlationTable)
            {
                var sourceFunction = functionsInProgram.FirstOrDefault(x => x.Properties.FUNC_FULLNAME == item.FunctionOldName);//найдем его
                var targetFunction = functionsInProgram.FirstOrDefault(x => x.Properties.FUNC_FULLNAME == item.FunctionNewName);//найдем его
                bool IsAssigned = AssignFinction(sourceFunction, targetFunction);
            }
            mappedPlcData = newDataPlc;//тут я хочу обновить данные
        }

        //сделать через tuple без создания нового класса
        private List<Intermediate> GetСorrelationTable(List<PlcDataModelView> oldData, List<PlcDataModelView> newDataPlc)
        {
            var result = oldData.Join(newDataPlc,
                data1 => data1.FunctionText,//проверяем и формируем группу по функциональному тексту
                data2 => data2.FunctionText,
                (data1, data2) =>
                {
                    if (data1.FunctionText != string.Empty)
                    {
                        return new Intermediate(data1.DT, data2.DT);
                    }
                    return null;
                }).Where(x => x != null).ToList();

            return FindDifferences(result);
        }
        private List<Intermediate> FindDifferences(List<Intermediate> table)
        {
            return table.Where(x => x.FunctionNewName != x.FunctionOldName).ToList();
        }

        private bool AssignFinction(Function sourceFunction, Function targetFunction)
        {
            targetFunction.Assign(sourceFunction);
            return true;
        }
    }
}
