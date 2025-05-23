using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.Base;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.DataModel.EObjects;
using Eplan.EplApi.HEServices;
using ST.EplAddin.PlcEdit.Helpers;
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
        public const string _actionName = "PlcGuiIGfWindRackConfiguration";
        private static List<PlcDataModelView> InitialPlcData { get; set; }
        public ManagePlcForm ManagePlcForm { get; private set; }
        public Project CurrentProject { get; set; }
        private IEnumerable<Function> FunctionsInProgram { get; set; }
        private Terminal[] PlcTerminals { get; set; }
        public string DeviceTag { get; set; }

        private StorableObject[] selectedPlcData;
        public bool OnRegister(ref string Name, ref int Ordinal)
        {
            Name = _actionName;
            Ordinal = 99;
            return true;
        }
        public PlcAddinEditAction()
        {
            ManagePlcForm.ApplyEvent += ManagePlcForm_ApplyEvent;
        }
        public void GetActionProperties(ref ActionProperties actionProperties) { }

        public bool Execute(ActionCallingContext oActionCallingContext)
        {
            using (SafetyPoint safetyPoint = SafetyPoint.Create())
            {
                selectedPlcData = null;
                GetPLCData();
                ShowTableForm(InitialPlcData.Select(x => x.Clone() as PlcDataModelView).ToList());
                safetyPoint.Commit();
            }
            return true;
        }
        public Terminal[] GetPlcTerminals()
        {
            selectedPlcData ??= GetSelectedObjects();

            if (selectedPlcData.OfType<PLC>().Count() == selectedPlcData.Count())
            {
                List<List<Terminal>> plcTerminals = new List<List<Terminal>>();
                foreach (var plc in selectedPlcData)
                {
                    var s = plc.CrossReferencedObjectsAll.OfType<Terminal>().ToList();
                    plcTerminals.Add(s);
                }
                var result = plcTerminals.SelectMany(x => x).ToArray();
                return result;
            }
            else
            {
                var result = selectedPlcData?
                    .OfType<Terminal>()
                    .Where(x => x.Properties.FUNC_CATEGORY.ToString(ISOCode.Language.L_ru_RU) == "Вывод устройства ПЛК")
                    .ToArray();
                return result;
            }
        }

        private StorableObject[] GetSelectedObjects()
        {
            SelectionSet selectionSet = new SelectionSet();
            selectionSet.LockProjectByDefault = false;
            selectionSet.LockSelectionByDefault = false;
            CurrentProject = selectionSet.GetCurrentProject(true);

            var selectedPlcData = selectionSet.Selection;//отфильтровать надо именно selection
            return selectedPlcData;
        }

        public void ShowTableForm(List<PlcDataModelView> plcDataModelView)
        {
            Process oCurrent = Process.GetCurrentProcess();
            var eplanOwner = new WindowWrapper(oCurrent.MainWindowHandle);

            ManagePlcForm = new ManagePlcForm(plcDataModelView, GetPathToSaveTemplate(CurrentProject), FunctionsInProgram);
            ManagePlcForm.Show(eplanOwner);
        }
        private void ManagePlcForm_ApplyEvent(object sender, CustomEventArgs e)
        {
            GetPLCData();
            var newDataPlc = e.PlcDataModelView; //тут получаем данные из формы
            var correlationTable = GetСorrelationTable(InitialPlcData, newDataPlc);

            using (UndoStep undo = new UndoManager().CreateUndoStep())
            {
                try
                {
                    while (correlationTable.Any())
                    {
                        AsssignNewFunctions(FunctionsInProgram, correlationTable.FirstOrDefault());
                        GetPLCData();
                        correlationTable = GetСorrelationTable(InitialPlcData, newDataPlc);
                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                    undo.CloseOpenUndo();
                    undo.DoUndo(true);
                    Application.Exit();
                    return;
                }
                RewritePlcProperties(PlcTerminals, newDataPlc);
                UpdateFormData();
                undo.SetUndoDescription($"Обновление плк модуля {DeviceTag} через API");
            }
        }

        private void GetPLCData()
        {
            PlcTerminals = GetPlcTerminals();
            FunctionsInProgram = PlcTerminals.Cast<Function>();
            Mapper mapper = new();
            InitialPlcData = mapper.GetPlcData(PlcTerminals);
        }

        private void AsssignNewFunctions(IEnumerable<Function> functionsInProgram, NameCorrelation tableWithReverse)
        {
            var sourceFunction = functionsInProgram.Where(x => x.Properties.FUNC_FULLNAME == tableWithReverse.FunctionOldName
                                                               && x.Properties[20183].ToString() == tableWithReverse.OldDevicePointDesignation
                                                               ).ToList();//найдем его
            var targetFunction = functionsInProgram.Where(x => x.Properties.FUNC_FULLNAME == tableWithReverse.FunctionNewName
                && x.Properties[20183].ToString() == tableWithReverse.NewDevicePointDesignation).ToList();//найдем его
            AssignFunction(sourceFunction, targetFunction);
        }
        private void RewritePlcProperties(Terminal[] plcTerminals, List<PlcDataModelView> newDataPlc)
        {
            foreach (var item in newDataPlc)
            {
                var multyLineTerminal = plcTerminals.FirstOrDefault(x => x.Properties.FUNC_FULLNAME == item.DT
                                                                         && x.Properties.FUNC_TYPE.ToInt() == 1
                                                                         && item.DevicePointDesignation == x.Properties.FUNC_PLCAUTOPLUG_AND_CONNPTDESIGNATION.ToString(ISOCode.Language.L_ru_RU));
                var overviewTerminal = plcTerminals.FirstOrDefault(x => x.Properties.FUNC_FULLNAME == item.DT
                                                                        && x.Properties.FUNC_TYPE.ToInt() == 3
                    && item.DevicePointDesignation == x.Properties.FUNC_PLCAUTOPLUG_AND_CONNPTDESIGNATION.ToString(ISOCode.Language.L_ru_RU));

                if (multyLineTerminal != null)
                {
                    multyLineTerminal.Properties.FUNC_TEXT = item.FunctionText;
                    multyLineTerminal.Properties.FUNC_PLCADDRESS = item.PLCAdress;
                    multyLineTerminal.Properties.FUNC_PLCSYMBOLICADDRESS_MANUAL = item.SymbolicAdress;
                }
                if (overviewTerminal != null)//тут я разрешил перезапись обзора
                {
                    if (Properties.Settings.Default.IsRewriteSymbolicAdress)
                    {
                        overviewTerminal.Properties.FUNC_PLCSYMBOLICADDRESS_MANUAL = item.SymbolicAdress;
                    }
                    if (Properties.Settings.Default.IsRewritePLCAdress)
                    {
                        overviewTerminal.Properties.FUNC_PLCADDRESS = item.PLCAdress;
                    }
                    if (Properties.Settings.Default.IsDeleteOverviewFunctionText)
                    {
                        overviewTerminal.Properties.FUNC_TEXT = string.Empty;
                    }
                    else
                    {
                        overviewTerminal.Properties.FUNC_TEXT = item.FunctionText;
                    }

                }
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
            if (isUnique != false) return;
            MessageBox.Show("An ID match was found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            ManagePlcForm.Exit();
        }
        private void UpdateFormData()
        {
            var terminals = GetPlcTerminals();
            CheckToIdenticalTerminal(terminals);
            Mapper mapper = new();
            var mappedPlcTerminals = mapper.GetPlcData(terminals);
            ManagePlcForm?.UpdateTable(mappedPlcTerminals);//тут передать данные после присвоения для обновления формы
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
                        return new NameCorrelation(data1.DT, data2.DT, data1.DevicePointDesignation, data2.DevicePointDesignation);
                    }
                    return null;
                }).Where(x => x != null).ToList();

            return FindDifferences(result);
        }
        private List<NameCorrelation> FindDifferences(List<NameCorrelation> table)
        {
            table = table.Where(x => x.FunctionNewName != x.FunctionOldName && x.NewDevicePointDesignation != x.OldDevicePointDesignation).ToList();
            var tableWithReverse = table.Distinct(new EqualityComparer()).ToList();
            return tableWithReverse;
        }

        private void AssignFunction(List<Function> sourceFunction, List<Function> targetFunction)
        {
            using (SafetyPoint safetyPoint = SafetyPoint.Create())
            {
                ReverseOutputPins(sourceFunction, targetFunction);
                safetyPoint.Commit();
            }
        }

        private void ReverseOutputPins(List<Function> sourceFunction, List<Function> targetFunction)
        {
            var allCrossreferencedFunctionsSource =
                sourceFunction.FirstOrDefault()?.CrossReferencedObjectsAll.OfType<Terminal>();
            var allCrossreferencedFunctionsTarget = targetFunction.FirstOrDefault()?.CrossReferencedObjectsAll.OfType<Terminal>();

            var sourceOverviewFunction = allCrossreferencedFunctionsSource.FirstOrDefault(z => z.Properties.FUNC_TYPE == 3
            && z.Properties[20183] == sourceFunction.FirstOrDefault()?.Properties[20183]
            && z.Properties.FUNC_FULLNAME == sourceFunction.FirstOrDefault()?.Properties.FUNC_FULLNAME);

            var targetOverviewFunction = allCrossreferencedFunctionsTarget.FirstOrDefault(z => z.Properties.FUNC_TYPE == 3
            && z.Properties[20183] == targetFunction.FirstOrDefault()?.Properties[20183]
                                         && z.Properties.FUNC_FULLNAME == targetFunction.FirstOrDefault()?.Properties.FUNC_FULLNAME);

            var targetMainFunction = targetFunction.FirstOrDefault(x => x.Properties.FUNC_TYPE == 1);
            var sourceMainFunction = sourceFunction.FirstOrDefault(x => x.Properties.FUNC_TYPE == 1);

            if (targetOverviewFunction == null)
            {
                throw new Exception("Не удалось найти обзор выхода ПЛК");
            }
            if (targetMainFunction != null && sourceMainFunction != null)
            {
                sourceOverviewFunction.Assign(targetMainFunction);
                targetOverviewFunction.Assign(sourceMainFunction);
            }
            else if (sourceMainFunction != null)
            {
                targetOverviewFunction?.Assign(sourceMainFunction);
            }
            else if (targetMainFunction != null)
            {
                sourceOverviewFunction.Assign(targetMainFunction);
            }
            else
            {
                throw new Exception("Присвоение обзора-обзору недопустимо при наличии у обзора многополюсного представления. Пожалуйста выберите верный диапазон данных!");
            }
        }
    }
}
