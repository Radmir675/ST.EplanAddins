using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.Base;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.DataModel.E3D;
using Eplan.EplApi.DataModel.EObjects;
using Eplan.EplApi.DataModel.MasterData;
using Eplan.EplApi.HEServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;


namespace ST.EplAddin.PlcEdit
{
    class PlcAddinEditAction : IEplAction
    {
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
                string fullDeviceTag = GetPlcFullName(selectedPlcdata);
                if (fullDeviceTag != string.Empty)
                {
                    selectedPlcdata = new DMObjectsFinder(CurrentProject)
                        .GetTerminals(functionsFilter)
                        .Where(x => x.Properties.FUNC_FULLDEVICETAG.ToString() == fullDeviceTag)
                        .ToArray();
                }
            }
            var result = selectedPlcdata.OfType<Terminal>().Where(x => x.Properties.FUNC_CATEGORY.ToString(ISOCode.Language.L_ru_RU) == "Вывод устройства ПЛК").ToArray();
            return result;
        }

        private string GetPlcFullName(StorableObject[] selectedPlcdata)
        {
            var fullDeviceTag = string.Empty;

            var plcTerminal = selectedPlcdata[0] as Function;
            if (plcTerminal == null)
            {
                var placement = selectedPlcdata[0] as Placement3D;
                fullDeviceTag = placement?.Properties.FUNC_FULLDEVICETAG.ToString();
            }
            else
            {
                fullDeviceTag = plcTerminal?.Properties.FUNC_FULLDEVICETAG.ToString();
            }
            return fullDeviceTag;
        }

        public void ShowTableForm(List<PlcDataModelView> plcDataModelView)
        {
            Process oCurrent = Process.GetCurrentProcess();
            var eplanOwner = new WindowWrapper(oCurrent.MainWindowHandle);

            ManagePlcForm = new ManagePlcForm(plcDataModelView, GetPathToSaveTemplate(CurrentProject));
            ManagePlcForm.Show(eplanOwner);
            ManagePlcForm.ApplyEvent += ManagePlcForm_ApplyEvent;
        }

        private void ManagePlcForm_ApplyEvent(object sender, CustomEventArgs e)
        {
            Terminal[] plcTerminals;
            IEnumerable<Function> functionsInProgram;
            Get(out plcTerminals, out functionsInProgram);

            var newDataPlc = e.PlcDataModelView; //тут получаем данные из формы
            var correlationTable = GetСorrelationTable(InitialPlcData, newDataPlc);
            //correlationTable.Clear();
            while (correlationTable.Any())
            {
                AsssignNewFunctions(functionsInProgram, correlationTable.FirstOrDefault());
                Get(out plcTerminals, out functionsInProgram);
                correlationTable = GetСorrelationTable(InitialPlcData, newDataPlc);
                //  correlationTable.RemoveAt(0);
            }

            RewritePlcProperties(plcTerminals, newDataPlc);
            UpdateFormData();
        }

        private void Get(out Terminal[] plcTerminals, out IEnumerable<Function> functionsInProgram)
        {
            plcTerminals = GetPlcTerminals();
            functionsInProgram = plcTerminals.Cast<Function>();
            InitialPlcData = Mapper.GetPlcData(plcTerminals);
        }

        private void AsssignNewFunctions(IEnumerable<Function> functionsInProgram, NameCorrelation tableWithReverse)
        {
            //тут надо скать главные функции
            var sourceFunction = functionsInProgram.Where(x => x.Properties.FUNC_FULLNAME == tableWithReverse.FunctionOldName).ToList();//найдем его
            var targetFunction = functionsInProgram.Where(x => x.Properties.FUNC_FULLNAME == tableWithReverse.FunctionNewName).ToList();//найдем его
            AssignFunction(sourceFunction, targetFunction, true);
        }
        private void RewritePlcProperties(Terminal[] plcTerminals, List<PlcDataModelView> newDataPlc)
        {
            foreach (var item in newDataPlc)    //тут будет применение всех измененных текстов для всех типов представлений
            {
                var terminals = plcTerminals.Where(x => x.Properties.FUNC_FULLNAME == item.DT && x.Properties.FUNC_TYPE.ToInt() == 1); //.GetDisplayString().GetString(ISOCode.Language.L_ru_RU) );

                if (terminals != null)
                {
                    foreach (var terminal in terminals)
                    {
                        terminal.Properties.FUNC_TEXT = item.FunctionText;
                        terminal.Properties.FUNC_PLCADDRESS = item.PLCAdress;
                        terminal.Properties.FUNC_PLCSYMBOLICADDRESS_MANUAL = item.SymbolicAdress;
                        //terminal.Properties.FUNC_ALLCONNECTIONDESCRIPTIONS = item.DevicePointDescription;//DI3
                        //terminal.Properties.FUNC_ALLCONNECTIONDESIGNATIONS = item.DevicePinNumber;//6
                    }
                }
            }
        }

        //TODO: на будущее надо сделать

        //private void RewritePlcProperties(Terminal[] plcTerminals, List<PlcDataModelView> newDataPlc)
        //{
        //    foreach (var item in newDataPlc)    //тут будет применение всех измененных текстов для всех типов представлений
        //    {
        //        var terminals = plcTerminals.Where(x => x.Properties.FUNC_FULLNAME == item.DT); //.GetDisplayString().GetString(ISOCode.Language.L_ru_RU) );
        //        var multyLineTerminal = terminals.FirstOrDefault(z => z.Properties.FUNC_TYPE.ToInt() == 1);
        //        var overviewTerminal = terminals.FirstOrDefault(z => z.Properties.FUNC_TYPE.ToInt() == 3);
        //        var overviewTerminalFuncText = overviewTerminal?.Properties?.FUNC_TEXT;
        //        if (overviewTerminalFuncText != string.Empty && overviewTerminalFuncText != null)
        //        {
        //            DialogResult result = MessageBox.Show("Обзор выводов ПЛК содержит ненужный функциональный текст. Вы хотите его удалить?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        //            if (result == DialogResult.Yes)
        //            {
        //                overviewTerminalFuncText = string.Empty;
        //            }
        //        }
        //        if (multyLineTerminal != null)
        //        {
        //            multyLineTerminal.Properties.FUNC_TEXT = item.FunctionText;
        //            multyLineTerminal.Properties.FUNC_PLCADDRESS = item.PLCAdress;
        //            multyLineTerminal.Properties.FUNC_PLCSYMBOLICADDRESS_MANUAL = item.SymbolicAdress;
        //            //terminal.Properties.FUNC_ALLCONNECTIONDESCRIPTIONS = item.DevicePointDescription;//DI3
        //            //terminal.Properties.FUNC_ALLCONNECTIONDESIGNATIONS = item.DevicePinNumber;//6
        //        }
        //    }
        //}


        private string GetPath(Project project)
        {
            using (LockingStep lockingStep = new LockingStep())
            {
                string path = project.ProjectDirectoryPath;
                //  string fullPath = System.IO.Path.Combine(path, $"{project.ProjectName}.txt");
                return path;
            }
        }
        private string GetPathToSaveTemplate(Project project)
        {
            using (LockingStep lockingStep = new LockingStep())
            {
                string projectPath = project.ProjectDirectoryPath;
                var fullPath = Path.Combine(projectPath, "CSV_files_template");
                Directory.CreateDirectory(fullPath);
                return fullPath;
            }
        }
        private void CheckToIdenticalTerminal(Terminal[] terminal)
        {
            var name = terminal.Select(x => x.ToStringIdentifier());
            bool isUnique = name.Distinct().Count() == name.Count();
            if (isUnique == false)
            {
                MessageBox.Show("An ID match was found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ManagePlcForm.Exit();
            }
        }
        private void UpdateFormData()
        {
            var terminals = GetPlcTerminals();
            CheckToIdenticalTerminal(terminals);
            var mappedPlcTerminals = Mapper.GetPlcData(terminals);
            ManagePlcForm.UpdateTable(mappedPlcTerminals);//туть передать данные после присвоения для обновления формы
        }

        private List<NameCorrelation> GetСorrelationTable(List<PlcDataModelView> oldData, List<PlcDataModelView> newDataPlc)
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
        private List<NameCorrelation> FindDifferences(List<NameCorrelation> table)
        {
            table = table.Where(x => x.FunctionNewName != x.FunctionOldName).ToList();
            var oldNames = table.Select(x => x.FunctionOldName);
            var newNames = table.Select(x => x.FunctionNewName);
            //var namesInFirstAndSecondSequence = oldNames.Intersect(newNames);

            var tableWithReverse = table.Distinct(new EqualityComparer()).ToList();

            return tableWithReverse;
        }

        private void AssignFunction(List<Function> sourceFunction, List<Function> targetFunction, bool reverse = false)
        {
            try
            {
                using (UndoStep undo = new UndoManager().CreateUndoStep())
                {
                    using (SafetyPoint safetyPoint = SafetyPoint.Create())
                    {
                        if (reverse == false)
                        {
                            // targetFunction.Assign(sourceFunction);//сначала пишется "куда"----- "откуда" 
                        }
                        else//если есть реверс то применяем вот эту схему "с реверсом"
                        {
                            ReverseOutputPins(sourceFunction, targetFunction);
                        }
                        safetyPoint.Commit();
                        undo.SetUndoDescription($"Удалить присвоение");
                    }
                }


            }
            catch (Exception)
            {
                ManagePlcForm.Exit();
            }
        }

        private void ReverseOutputPins(List<Function> sourceFunction, List<Function> targetFunction)
        {
            var allCrossreferencedFunctions = sourceFunction.FirstOrDefault()?.CrossReferencedObjectsAll.OfType<Terminal>() ?? targetFunction.FirstOrDefault()?.CrossReferencedObjectsAll.OfType<Terminal>();

            var sourceOverviewFunction = sourceFunction.FirstOrDefault(z => z.Properties.FUNC_TYPE == 3
            && z.Properties.FUNC_ALLCONNECTIONDESIGNATIONS == sourceFunction.FirstOrDefault()?.Properties.FUNC_ALLCONNECTIONDESIGNATIONS);

            var targetOverviewFunction = allCrossreferencedFunctions.FirstOrDefault(z => z.Properties.FUNC_TYPE == 3
            && z.Properties.FUNC_ALLCONNECTIONDESIGNATIONS == targetFunction.FirstOrDefault()?.Properties.FUNC_ALLCONNECTIONDESIGNATIONS);

            var targetMainFunction = targetFunction.FirstOrDefault(x => x.Properties.FUNC_TYPE == 1);
            var sourceMainFunction = sourceFunction.FirstOrDefault(x => x.Properties.FUNC_TYPE == 1);


            if (targetMainFunction?.Properties.FUNC_TYPE == 1 && sourceMainFunction?.Properties.FUNC_TYPE == 1)
            {
                sourceOverviewFunction.Assign(targetMainFunction);
                targetOverviewFunction.Assign(sourceMainFunction);
            }
            else if (sourceMainFunction?.Properties.FUNC_TYPE == 1)
            {
                targetOverviewFunction?.Assign(sourceMainFunction);
            }
            else if (targetMainFunction?.Properties.FUNC_TYPE == 1)
            {
                sourceOverviewFunction.Assign(targetMainFunction);
            }
            else
            {
                MessageBox.Show("Упс... Что-то пошло не так.");
                return;
            }
        }

        private Function CreateTransientFunction()
        {
            Function function = new Function();
            SymbolVariant symbolVariant = new SymbolVariant();
            Symbol symbol = new Symbol();
            symbol.Initialize(new SymbolLibrary(CurrentProject, "IEC_symbol"), 350);
            symbolVariant.Initialize(symbol, 1);
            function.CreateTransient(CurrentProject, symbolVariant);
            return function;
        }
    }
}
