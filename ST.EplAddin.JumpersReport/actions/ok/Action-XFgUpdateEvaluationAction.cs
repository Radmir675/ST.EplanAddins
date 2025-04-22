using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.HEServices;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

//https://www.eplan.help/en-us/Infoportal/Content/api/2.9/API_REPORTS_MODIFICATION.html
namespace ST.EplAddin.JumpersReport
{
    public class Action_XFgUpdateEvaluationAction : IEplAction
    {
        private const string ACTION_NAME = "XFgUpdateEvaluationAction";  //Сервисные программы > Отчеты > Обновить
        private string resultData;
        public static bool IsOtherReportsUpdated = false;

        //AVXFgCreateEvaluationProjectAction
        //VXFgUpdateEvaluationAction
        //DMEvaluationBlock
        //VXFgCompareEvaluationsTestAction
        //AVXFgCompareEvaluations
        //AVXFgExtEvaluations
        //AVXGedEvaluationTab@@ 
       // GfDlgMgrActionIGfWind /function:EvaluateTemplate

        public void GetActionProperties(ref ActionProperties actionProperties)
        {
            var PROP = new ActionParameterProperties();
            var t = new ActionParameterProperties();
            t.Set("STReport");
            actionProperties.AddParameter(t);

        }
        public bool OnRegister(ref string Name, ref int Ordinal)
        {
            Name = ACTION_NAME;
            Ordinal = 98;
            return true;

        }
        public bool Execute(ActionCallingContext oActionCallingContext)
        {
            int count = oActionCallingContext.GetParameterCount();
            string[] contextParams = oActionCallingContext.GetParameters();
            string[] contextStrings = oActionCallingContext.GetStrings();


            // получение шаблонов отчетов из настроек проекта
            using (SafetyPoint safetyPoint = SafetyPoint.Create())
            {
                SelectionSet selection = new SelectionSet();
                Project prj = selection.GetCurrentProject(false);

                ProjectSettingNode prjNode3 = new ProjectSettingNode(prj, "PROJECT.FormGeneratorGui.Templates.PxfForm_TERMINALDIAGRAM");

                StringCollection colOfSettings3 = new StringCollection();
                prjNode3.GetListOfAllSettings(ref colOfSettings3, true);

                prjNode3.Write(System.IO.Path.GetTempPath() + "ProjectSettingNode3.xml");


                ReportBlock rb = new ReportBlock();

                rb.Create(prj);


                Reports reports = new Reports();

                string StartPage = "";

                reports.CreateReport(rb, StartPage);

                DMObjectsFinder DMObjectsFinder = new DMObjectsFinder(prj);
                List<ReportBlock> reports1 = DMObjectsFinder.GetAll<ReportBlock>(true).Cast<ReportBlock>().Where(a => a.Action == "JumpersReport").ToList();

            }

            ActionManager oMng = new ActionManager();
            Eplan.EplApi.ApplicationFramework.Action oBaseAction = oMng.FindBaseAction(this, true);
            string props = oBaseAction.ActionProperties.ToString();
            return oBaseAction.Execute(oActionCallingContext);

        }
    }
}
