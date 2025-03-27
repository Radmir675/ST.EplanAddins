using Eplan.EplApi.DataModel;
using Eplan.EplApi.HEServices;
using System.Collections.Generic;

namespace ST.EplAddin.JumpersReport
{
    internal class ReportProvider
    {
        public void Create()
        {
            Action.IsOtherReportsUpdated = true;
            SelectionSet selectionSet = new SelectionSet();
            Project project = selectionSet.GetCurrentProject(false);

            Reports reports = new Reports();
            reports.CreateReportsFromTemplates(project,
                new List<DocumentTypeManager.DocumentType>(1) { DocumentTypeManager.DocumentType.TerminalDiagram });
        }
    }
}
