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
        private IEnumerable<Function> FunctionsInProgram { get; set; }
        private Terminal[] PlcTerminals { get; set; }
        public bool OnRegister(ref string Name, ref int Ordinal)
        {
            Name = actionName;
            Ordinal = 99;
            return true;
        }
        public PlcAddinEditAction()
        {
            ManagePlcForm.ApplyEvent += ManagePlcForm_ApplyEvent;
            ManagePlcForm.ShowSearch += (sender, args) => ShowSearch(args);
        }
        public void GetActionProperties(ref ActionProperties actionProperties) { }


        public bool Execute(ActionCallingContext oActionCallingContext)
        {
            using (SafetyPoint safetyPoint = SafetyPoint.Create())
            {
                GetPLCData();
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

            ManagePlcForm = new ManagePlcForm(plcDataModelView, GetPathToSaveTemplate(CurrentProject), FunctionsInProgram);
            ManagePlcForm.Show(eplanOwner);

        }



        private void ManagePlcForm_ApplyEvent(object sender, CustomEventArgs e)
        {
            GetPLCData();
            var newDataPlc = e.PlcDataModelView; //тут получаем данные из формы
            var correlationTable = GetСorrelationTable(InitialPlcData, newDataPlc);
            while (correlationTable.Any())
            {
                try
                {
                    AsssignNewFunctions(FunctionsInProgram, correlationTable.FirstOrDefault());
                    GetPLCData();
                    correlationTable = GetСorrelationTable(InitialPlcData, newDataPlc);

                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                    break;

                }
            }
            RewritePlcProperties(PlcTerminals, newDataPlc);
            UpdateFormData();
        }

        private void GetPLCData()
        {
            PlcTerminals = GetPlcTerminals();
            FunctionsInProgram = PlcTerminals.Cast<Function>();
            InitialPlcData = Mapper.GetPlcData(PlcTerminals);
        }

        private void AsssignNewFunctions(IEnumerable<Function> functionsInProgram, NameCorrelation tableWithReverse)
        {
            var sourceFunction = functionsInProgram.Where(x => x.Properties.FUNC_FULLNAME == tableWithReverse.FunctionOldName).ToList();//найдем его
            var targetFunction = functionsInProgram.Where(x => x.Properties.FUNC_FULLNAME == tableWithReverse.FunctionNewName).ToList();//найдем его
            AssignFunction(sourceFunction, targetFunction, true);
        }
        private void RewritePlcProperties(Terminal[] plcTerminals, List<PlcDataModelView> newDataPlc)
        {
            foreach (var item in newDataPlc)
            {
                var multyLineTerminal = plcTerminals.FirstOrDefault(x => x.Properties.FUNC_FULLNAME == item.DT && x.Properties.FUNC_TYPE.ToInt() == 1);
                var overviewTerminal = plcTerminals.FirstOrDefault(x => x.Properties.FUNC_FULLNAME == item.DT && x.Properties.FUNC_TYPE.ToInt() == 3);

                if (multyLineTerminal != null)
                {
                    multyLineTerminal.Properties.FUNC_TEXT = item.FunctionText;
                    multyLineTerminal.Properties.FUNC_PLCADDRESS = item.PLCAdress;
                    multyLineTerminal.Properties.FUNC_PLCSYMBOLICADDRESS_MANUAL = item.SymbolicAdress;
                }
                if (overviewTerminal != null)//тут я разрешил перезапись обзора
                {
                    overviewTerminal.Properties.FUNC_PLCADDRESS = item.PLCAdress;
                    overviewTerminal.Properties.FUNC_PLCSYMBOLICADDRESS_MANUAL = item.SymbolicAdress;
                }
            }
        }

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
            ManagePlcForm.UpdateTable(mappedPlcTerminals);//тут передать данные после присвоения для обновления формы
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
            try
            {
                var allCrossreferencedFunctions = sourceFunction.FirstOrDefault()?.CrossReferencedObjectsAll.OfType<Terminal>() ?? targetFunction.FirstOrDefault()?.CrossReferencedObjectsAll.OfType<Terminal>();

                var sourceOverviewFunction = allCrossreferencedFunctions.FirstOrDefault(z => z.Properties.FUNC_TYPE == 3
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
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
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
        private void ShowSearch(IEnumerable<StorableObject> enumerable)
        {
            Search search = new Search();
            search.ClearSearchDB(CurrentProject);
            search.AddToSearchDB(enumerable.ToArray());
            ShowSearchNavigator();
        }
        private void ShowSearchNavigator()
        {
            ActionManager oMng = new ActionManager();
            Eplan.EplApi.ApplicationFramework.Action baseAction = oMng.FindAction("XSeShowSearchResultsAction");
            ActionCallingContext ctx = new ActionCallingContext();
            bool result = baseAction.Execute(ctx);
        }
    }
}
