using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.DataModel.EObjects;
using Eplan.EplApi.HEServices;
using System.Collections.Generic;
using System.Diagnostics;

namespace ST.EplAddin.PlcEdit
{
    class PlcEditAction : IEplAction
    {
        public static string actionName = "GfDlgMgrActionIGfWind";
        public PLC[] PlcTerminals { get; set; }
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

            var selectedPlcdata = selectionSet.Selection;

            FunctionsFilter PlcTerminalFilter = new FunctionsFilter();
            PlcTerminalFilter.Category = Function.Enums.Category.PLCTerminal;

            using (SafetyPoint safetyPoint = SafetyPoint.Create())
            {
                PlcTerminals = new DMObjectsFinder(currentProject).GetPLCs(PlcTerminalFilter);
                var mappedPlcData = DataMapper(PlcTerminals);
                ShowTableForm(mappedPlcData);
            }
            return true;
        }

        public List<PlcDataModelView> DataMapper(PLC[] plcTerminals)
        {
            return new List<PlcDataModelView>();
        }
        public void ShowTableForm(List<PlcDataModelView> plcDataModelView)
        {
            Process oCurrent = Process.GetCurrentProcess();
            var eplanOwner = new WindowWrapper(oCurrent.MainWindowHandle);

            ManagePlcForm managePlcForm = new ManagePlcForm(plcDataModelView);
            managePlcForm.Show(eplanOwner);
        }
    }
}
