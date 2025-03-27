using Eplan.EplApi.DataModel;
using Eplan.EplApi.HEServices;
using System.Collections.Generic;

namespace ST.EplAddin.JumpersReport
{
    internal class ReportProvider
    {
        const string FORM_NAME = "F35_002.f35";
        public void Create()
        {
            //TerminalStripOverview
            //SelectionContext
            Action.IsReady = true;
            SelectionSet selectionSet = new SelectionSet();
            Project project = selectionSet.GetCurrentProject(false);

            Reports reports = new Reports();
            reports.CreateReportsFromTemplates(project,
                new List<DocumentTypeManager.DocumentType>(1) { DocumentTypeManager.DocumentType.TerminalDiagram });









            //Page page = selectionSet.OpenedPages.First();
            //StorableObject obj = selectionSet.GetSelectedObject(true);

            //StringCollection oProjectNewEntries = new StringCollection();
            //oProjectNewEntries.Add(FORM_NAME);
            //new Masterdata().AddToProjectEx(project, oProjectNewEntries);

            //PointD oPoint = new PointD(100.0, 30.0);
            //ReportBlock oReport = GetReportBlock(project);

            //List<FunctionBasePropertyList> termsList = new List<FunctionBasePropertyList>();
            ////termsList.Add();

            //oReport.DeviceTagNameParts = termsList.ToArray();


            //ReportBlockReference oReportBlockReference = new Reports().CreateEmbeddedReport(oReport, page, oPoint);

        }

        private ReportBlock GetReportBlock(Project project)
        {
            ReportBlock oReport = new ReportBlock();
            oReport.Create(project);
            oReport.FormName = System.IO.Path.GetFileNameWithoutExtension(FORM_NAME);
            oReport.Type = DocumentTypeManager.DocumentType.TopologyRoutingPathList; // check
            return oReport;
        }
    }
}
