using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.HEServices;
using System;
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
            if (project == null)
            {
                throw new NullReferenceException("Отсутствует ссылка на проект");
            }

            selectionSet.LockProjectByDefault = false;
            CreateTerminals(project);
            Reports reports = new Reports();
            using (SafetyPoint safetyPoint = SafetyPoint.Create())
            {

                reports.CreateReportsFromTemplates(project,
                    new List<DocumentTypeManager.DocumentType>(1) { DocumentTypeManager.DocumentType.TerminalDiagram });//TerminalDiagram   //TerminalLineupDiagram
                safetyPoint.Commit();
            }
            TerminalsRepository.GetInstance().GetAllSavedTerminals().ForEach(z => z.Remove());
        }
        private void CreateTerminals(Project project)
        {
            var jumperDataProvider = new JumpersDataProvider(project);
            jumperDataProvider.FindAndCreateTerminals();
            UpdateConnections();
        }

        private void UpdateConnections()
        {
            var action = new ActionManager().FindAction("EsGenerateConnections");

            if (action != null)
            {
                ActionCallingContext oContext = new ActionCallingContext();
                action.Execute(oContext);
            }
        }
    }
}
