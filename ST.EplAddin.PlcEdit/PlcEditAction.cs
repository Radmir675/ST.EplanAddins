using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.Base;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.DataModel.EObjects;
using Eplan.EplApi.HEServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace ST.EplAddin.PlcEdit
{
    class PlcEditAction : IEplAction
    {
        //https://www.eplan.help/en-us/Infoportal/Content/api/2024/Actions.html
        //https://www.eplan.help/en-us/Infoportal/Content/api/2024/Events.html
        public static string actionName = "PlcGuiIGfWindRackConfiguration";
        private static List<PlcDataModelView> InitialPlcData { get; set; }
        public ManagePlcForm ManagePlcForm { get; private set; }
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
            using (SafetyPoint safetyPoint = SafetyPoint.Create())
            {
                var PlcTerminals = GetPlcTerminals();
                InitialPlcData = Mapper.GetPlcData(PlcTerminals);
                ShowTableForm(InitialPlcData.Select(x => x.Clone() as PlcDataModelView).ToList());
                safetyPoint.Commit();
            }
            return true;
        }
        public Terminal[] GetPlcTerminals()
        {
            SelectionSet selectionSet = new SelectionSet();
            selectionSet.LockProjectByDefault = false;
            selectionSet.LockSelectionByDefault = false;
            CurrentProject = selectionSet.GetCurrentProject(true);

            var selectedPlcdata = selectionSet.Selection;//отфильтровать надо именно selection
            FunctionsFilter functionsFilter = new FunctionsFilter();
            functionsFilter.Category = Function.Enums.Category.PLCTerminal;
            if (selectedPlcdata.Length == 1)
            {
                var terminal = selectedPlcdata[0] as Terminal;
                selectedPlcdata = new DMObjectsFinder(CurrentProject)
                    .GetTerminals(functionsFilter)
                    .Where(x => x.Properties.FUNC_FULLDEVICETAG.ToString() == terminal?.Properties.FUNC_FULLDEVICETAG.ToString())
                    //.OrderBy(x => int.Parse(x.Properties.FUNC_GEDNAMEWITHCONNECTIONDESIGNATION.ToString().Split(':').Last()))
                    .ToArray();
            }
            var result = selectedPlcdata.OfType<Terminal>().Where(x => x.Properties.FUNC_CATEGORY.ToString(ISOCode.Language.L_ru_RU) == "Вывод устройства ПЛК").ToArray();
            return result;
        }
        public void ShowTableForm(List<PlcDataModelView> plcDataModelView)
        {
            Process oCurrent = Process.GetCurrentProcess();
            var eplanOwner = new WindowWrapper(oCurrent.MainWindowHandle);

            ManagePlcForm = new ManagePlcForm(plcDataModelView);
            ManagePlcForm.Show(eplanOwner);
            ManagePlcForm.ApplyEvent += ManagePlcForm_ApplyEvent;
        }

        private void ManagePlcForm_ApplyEvent(object sender, CustomEventArgs e)
        {
            var plcTerminals = GetPlcTerminals();//тут получаем данные, которые могли быть изменены за время нашего изменения в форме
            var functionsInProgram = plcTerminals.Cast<Function>();
            var newDataPlc = e.PlcDataModelView; //тут получаем данные из формы
            InitialPlcData = Mapper.GetPlcData(plcTerminals);
            var correlationTable = GetСorrelationTable(InitialPlcData, newDataPlc);
            foreach (var item in correlationTable.tableWithoutReverse)
            {
                var sourceFunction = functionsInProgram.FirstOrDefault(x => x.Properties.FUNC_FULLNAME == item.FunctionOldName);//найдем его
                var targetFunction = functionsInProgram.FirstOrDefault(x => x.Properties.FUNC_FULLNAME == item.FunctionNewName);//найдем его
                AssignFunction(sourceFunction, targetFunction);
            }

            foreach (var item in correlationTable.tableWithReverse)
            {
                var sourceFunction = functionsInProgram.FirstOrDefault(x => x.Properties.FUNC_FULLNAME == item.FunctionOldName);//найдем его
                var targetFunction = functionsInProgram.FirstOrDefault(x => x.Properties.FUNC_FULLNAME == item.FunctionNewName);//найдем его
                AssignFunction(sourceFunction, targetFunction, true);
            }
            var s = GetPlcTerminals();
            var name = s.Select(x => x.ToStringIdentifier());
            bool isUnique = name.Distinct().Count() == name.Count();
            if (isUnique == false)
            {
                MessageBox.Show("Found ununique values");
            }
            var ss = Mapper.GetPlcData(s);
            ManagePlcForm.UpdateTable(ss);//туть передать данные после присовения для обновления формы
        }

        private (List<NameCorrelation> tableWithoutReverse, List<NameCorrelation> tableWithReverse) GetСorrelationTable(List<PlcDataModelView> oldData, List<PlcDataModelView> newDataPlc)
        {
            var result = oldData.Join(newDataPlc,
                data1 => data1.TerminalId,//проверяем и формируем группу по клеммы
                data2 => data2.TerminalId,
                (data1, data2) =>
                {
                    if (data1.FunctionText != string.Empty || data1.SymbolicAdressDefined != string.Empty)
                    {
                        return new NameCorrelation(data1.DT, data2.DT);
                    }
                    return null;
                }).Where(x => x != null).ToList();

            return FindDifferences(result);
        }
        private (List<NameCorrelation> tableWithoutReverse, List<NameCorrelation> tableWithReverse) FindDifferences(List<NameCorrelation> table)
        {
            table = table.Where(x => x.FunctionNewName != x.FunctionOldName).ToList();
            var oldNames = table.Select(x => x.FunctionOldName);
            var newNames = table.Select(x => x.FunctionNewName);
            var namesInFirstAndSecondSequence = oldNames.Intersect(newNames);

            var tableWithReverse = table.Where(x => namesInFirstAndSecondSequence.Contains(x.FunctionOldName)).Distinct(new EqualityComparer()).ToList();
            var tableWithoutReverse = table.Where(x => !namesInFirstAndSecondSequence.Contains(x.FunctionOldName)).ToList();
            return (tableWithoutReverse, tableWithReverse);
        }

        private void AssignFunction(Function sourceFunction, Function targetFunction, bool reverse = false)
        {
            try
            {

                using (SafetyPoint safetyPoint = SafetyPoint.Create())
                {
                    if (reverse == false)
                    {
                        targetFunction.Assign(sourceFunction);//сначала пишется "куда"----- "откуда" 
                    }
                    else//если есть реверс то применяем вот эту схему "с реверсом"
                    {



                    }
                    safetyPoint.Commit();
                }
            }
            catch (Exception)
            {
                ManagePlcForm.Exit();
            }
        }
    }
}
