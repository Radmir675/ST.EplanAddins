using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.DataModel.EObjects;
using Eplan.EplApi.HEServices;
using ST.EplAddin.JumpersReport.Providers;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Project = Eplan.EplApi.DataModel.Project;
using SelectionSet = Eplan.EplApi.HEServices.SelectionSet;
using StorableObject = Eplan.EplApi.DataModel.StorableObject;
//16:25:32	onActionEnd.String.JumpersReport1	JumpersReport1
//https://www.eplan.help/en-us/Infoportal/Content/api/2.9/API_REPORTS_MODIFICATION.html
namespace ST.EplAddin.JumpersReport.Actions.ok
{
    public class ShowReportAction : IEplAction
    {
        public const string ACTION_NAME = "JumpersReport1";
        private bool reportBegin = true;
        private bool ModifyObjectListEntarnce = false;

        public void GetActionProperties(ref ActionProperties actionProperties)
        {

        }

        public bool OnRegister(ref string Name, ref int Ordinal)
        {
            Name = ACTION_NAME;
            Ordinal = 20;
            return true;
        }

        public bool Execute(ActionCallingContext oActionCallingContext)
        {
            int count = oActionCallingContext.GetParameterCount();
            string[] contextParams = oActionCallingContext.GetParameters();
            string[] contextStrings = oActionCallingContext.GetStrings();

            string mode = "";
            string objects = "";
            string pages = "";

            oActionCallingContext.GetParameter("mode", ref mode);
            oActionCallingContext.GetParameter("objects", ref objects);
            oActionCallingContext.GetParameter("pages", ref pages);


            if (reportBegin)
            {
                ModifyObjectListEntarnce = false;
                var selectionSet = new SelectionSet
                {
                    LockProjectByDefault = false
                };
                var currentProject = selectionSet.GetCurrentProject(false);

                var selectedPages = selectionSet.GetSelectedPages();
                if (selectedPages.Any())
                {

                    MessageBox.Show(
                        "Ваш отчет по вставным перемычкам благополучно удален. Пожалуйста сгенерируйте его заново.");
                    reportBegin = false;
                    return true;
                }
                var createAndSaveTerminalStrips = new CreateAndSaveTerminalStrips(currentProject);
                createAndSaveTerminalStrips.FindAndCreateTerminals();
                new ReportProvider().UpdateConnections(currentProject);
                reportBegin = false;
            }

            if (mode == "ModifyObjectList")
            {
                ModifyObjectListEntarnce = true;
                //obj ids
                List<string> objectsList = objects.Split(';').ToList();

                //get objects

                StorableObject so = StorableObject.FromStringIdentifier(objectsList.FirstOrDefault());
                using (SafetyPoint safetyPoint = SafetyPoint.Create())
                {
                    if (so != null && so is Terminal terminal)
                    {
                        var names = TerminalsRepository.GetInstance().GetAllWithoutRemoving()
                            .Select(x => x.Properties[20013].ToString()).Distinct();
                        if (names.Contains(terminal.Properties[20013].ToString()))
                        {
                            return true;
                        }
                    }
                }

                objects = ";";
                oActionCallingContext.AddParameter("objects", objects);
                return false;
            }

            if (mode == "Finish")
            {
                using SafetyPoint safetyPoint = SafetyPoint.Create();
                TerminalsRepository.GetInstance().GetAll().ForEach(z => z.Remove());
                safetyPoint.Commit();
                reportBegin = true;
                return true;

            }
            return false;
        }

        private bool SetInitObjectsToUpdate(ActionCallingContext oActionCallingContext, ref string objects,
            Project currentProject)
        {
            var selectionSet = new SelectionSet
            {
                LockProjectByDefault = false
            };
            var selectedPages = selectionSet.GetSelectedPages();
            if (selectedPages == null || !selectedPages.Any())
            {
                Reports reports = new Reports();
                using (SafetyPoint safetyPoint = SafetyPoint.Create())
                {

                    reports.CreateReportsFromTemplates(currentProject,
                        new List<DocumentTypeManager.DocumentType>(1) { DocumentTypeManager.DocumentType.TerminalDiagram });//TerminalDiagram   //TerminalLineupDiagram
                    safetyPoint.Commit();
                }  //TerminalLineupDiagram
                return false;

            }
            //получаем страницу обновления
            var currentLocation = selectedPages.FirstOrDefault().Properties.DESIGNATION_LOCATION;//"S1"

            List<string> resultList = TerminalsRepository.GetInstance().GetAllWithoutRemoving()
                .Where(x => x.Properties[1200] == currentLocation)
                .Select(x => x.ToStringIdentifier())
                .ToList();
            string result = string.Join(";", resultList);
            objects = result;
            oActionCallingContext.AddParameter("objects", result);
            return true;
        }
        //if (isNeedToCreateNew)
        //{
        //    var selectionSet = new SelectionSet
        //    {
        //        LockProjectByDefault = false
        //    };
        //    var selectedPages = selectionSet.GetSelectedPages();
        //    selectedPages = null;
        //    Reports reports = new Reports();

        //    reports.CreateReportsFromTemplates(selectionSet.GetCurrentProject(true),
        //        new List<DocumentTypeManager.DocumentType>(1) { DocumentTypeManager.DocumentType.TerminalConnectiondiagram
        //        });//TerminalDiagram   //TerminalLineupDiagram
        //    safetyPoint.Commit();
        //    reportBegin = false;

        //}
    }
}





//if (mode == "Start") //TODO:тут происходят действия при обновлении отчетов
//{
//    var selectionSet = new SelectionSet
//    {
//        LockProjectByDefault = false
//    };
//    var currentProject = selectionSet.GetCurrentProject(true);

//    var selectedPages = selectionSet.GetSelectedPages();
//    if (selectedPages.Any())
//    {


//        var currentLocation = selectedPages.FirstOrDefault().Properties.DESIGNATION_LOCATION;//"S1"

//        List<string> resultList = TerminalsRepository.GetInstance().GetAllWithoutRemoving()
//            .Where(x => x.Properties[1200] == currentLocation)
//            .Select(x => x.ToStringIdentifier())
//            .ToList();
//        string result = string.Join(";", resultList);
//        objects = result + ";";
//        oActionCallingContext.AddParameter("objects", objects);
//        return true;
//    }
//}
