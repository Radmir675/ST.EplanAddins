using Eplan.EplApi.DataModel;
using Eplan.EplApi.HEServices;
using System;
using System.IO;
using System.Linq;

namespace ST.EplAddin.JumpersReport
{
    internal class ReportProvider
    {
        public void Create()
        {
            Action.IsOtherReportsUpdated = true;

            SelectionSet selectionSet = new SelectionSet();
            Project project = selectionSet.GetCurrentProject(false);
            if (project == null)
            {
                throw new NullReferenceException("Отсутствует ссылка на проект");
            }

            selectionSet.LockProjectByDefault = false;
            CreateTerminals(project);
            Reports reports = new Reports();
            ReportBlock reportBlock = new ReportBlock();
            reportBlock.Create(project);
            var path = "G:\\Мой диск\\EplanData\\Формы\\ST_ТБ_Клеммы_01_.f13";
            reportBlock.FormName = System.IO.Path.GetFileNameWithoutExtension(path);
            FileInfo file = new FileInfo(path);
            var result = file.Exists;
            reportBlock.Type = DocumentTypeManager.DocumentType.TerminalDiagram;
            reportBlock.Action = "JumpersReport";

            FunctionBasePropertyList[] functionBasePropertyLists = new FunctionBasePropertyList[]
            {
                TerminalsRepository.GetInstance().GetData().FirstOrDefault().Properties,
                TerminalsRepository.GetInstance().GetData().FirstOrDefault().Properties
            };
            reportBlock.DeviceTagNameParts = functionBasePropertyLists;

            reports.CreateReport(reportBlock, "-S1-K1");

            //reports.CreateReportsFromTemplates(project,
            //    new List<DocumentTypeManager.DocumentType>(1) { DocumentTypeManager.DocumentType.TerminalDiagram });
        }
        private void CreateTerminals(Project project)
        {
            var jumperDataProvider = new JumpersDataProvider(project);
            jumperDataProvider.FindAndCreateTerminals();
        }
    }
}
