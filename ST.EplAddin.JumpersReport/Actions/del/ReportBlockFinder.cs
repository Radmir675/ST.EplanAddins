using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.HEServices;
using System.Collections.Generic;
using System.Linq;
using Project = Eplan.EplApi.DataModel.Project;

namespace ST.EplAddin.JumpersReport.Actions.del
{
    internal class ReportBlockFinder : IEplAction
    {
        public bool Execute(ActionCallingContext oActionCallingContext)
        {
            SelectionSet selectionSet = new SelectionSet();
            selectionSet.LockSelectionByDefault = false;
            var project = selectionSet.GetCurrentProject(true);
            Check(project);
            return true;
        }

        public void GetActionProperties(ref ActionProperties actionProperties)
        {
        }

        public bool OnRegister(ref string Name, ref int Ordinal)
        {
            Name = "ReportBlockFinder";
            Ordinal = 98;
            return true;
        }
        //public FunctionBasePropertyList[] Check(Project project)
        //{
        //    //.Where(a => a.Action == ShowReportAction.ACTION_NAME)
        //    DMObjectsFinder DMObjectsFinder = new DMObjectsFinder(project);
        //    List<ReportBlock> reports1 = DMObjectsFinder.GetAll<ReportBlock>(true).Cast<ReportBlock>().ToList();
        //    var devices = reports1.LastOrDefault()?.DeviceTagNameParts;
        //    func = devices.ToArray();
        //    return devices;
        //}


        private void Check(Project project)
        {
            DMObjectsFinder DMObjectsFinder = new DMObjectsFinder(project);
            List<ReportBlock> reports1 = DMObjectsFinder.GetAll<ReportBlock>(true).Cast<ReportBlock>().ToList();


            var device = reports1.Where(a => a.Type == DocumentTypeManager.DocumentType.TerminalDiagram).FirstOrDefault();
            var fpl = device.DeviceTagNameParts.LastOrDefault();


            var allfunc = DMObjectsFinder.GetFunctions(null);
            List<Function> func = new List<Function>(2);

            foreach (var obj in allfunc)
            {
                var objprop = obj.Properties;

                if (objprop != null)
                {
                    if (fpl.DESIGNATION_LOCATION == objprop.DESIGNATION_LOCATION)
                    {
                        var tag = obj.Properties[20013];
                        var counter = obj.Properties[20014];

                        var tag1 = fpl.FUNC_CODE;
                        var counter1 = fpl.FUNC_COUNTER;
                        if (tag == tag1 && counter == counter1)
                        {
                            func.Add(obj);
                        }
                    }
                }

            }
            int f = 9;
        }
    }
}
