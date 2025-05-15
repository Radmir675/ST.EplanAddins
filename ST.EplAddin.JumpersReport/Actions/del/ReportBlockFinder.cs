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
        private void Check(Project project)
        {
            //.Where(a => a.Action == ShowReportAction.ACTION_NAME)
            DMObjectsFinder DMObjectsFinder = new DMObjectsFinder(project);
            List<ReportBlock> reports1 = DMObjectsFinder.GetAll<ReportBlock>(true).Cast<ReportBlock>().ToList();
            var devices = reports1.FirstOrDefault()?.DeviceTagNameParts;
        }
    }
}
