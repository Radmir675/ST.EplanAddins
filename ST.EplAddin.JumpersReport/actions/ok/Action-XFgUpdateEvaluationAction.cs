using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.Base;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.HEServices;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

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

        public void GetActionProperties(ref ActionProperties actionProperties) { }
        public bool OnRegister(ref string Name, ref int Ordinal)
        {
            Name = ACTION_NAME;
            Ordinal = 98;
            return true;

        }
        public bool Execute(ActionCallingContext oActionCallingContext)
        {
            // получение шаблонов отчетов из настроек проекта
            using (SafetyPoint safetyPoint = SafetyPoint.Create())
            {
                SelectionSet selection = new SelectionSet();
                Project prj = selection.GetCurrentProject(false);

                ProjectSettingNode prjNode3 = new ProjectSettingNode(prj, "PROJECT.FormGeneratorGui.Templates.PxfForm_TERMINALDIAGRAM");

                StringCollection colOfSettings3 = new StringCollection();
                prjNode3.GetListOfAllSettings(ref colOfSettings3, true);

                prjNode3.Write(System.IO.Path.GetTempPath() + "ProjectSettingNode3.xml");
                var subNode = GetActionSubNode(prjNode3);
                var nodePath = subNode.GetNodePath();

                ReportBlock reportBlock = new ReportBlock();
                reportBlock.Create(prj);
                reportBlock.Type = DocumentTypeManager.DocumentType.TerminalDiagram;
                reportBlock.FormName = subNode.GetStringSetting("FormName", 0);
                reportBlock.IsAutomaticPageDescription = true;
                ReportProvider reportProvider = new ReportProvider();
                reportProvider.CreateTerminals(prj);


                var terminalsRepository = TerminalsRepository.GetInstance();
                var data = terminalsRepository.GetAllSavedTerminals();
                var list = new List<FunctionBasePropertyList>();
                list.AddRange(data.Select(x => new FunctionPropertyList(x)));
                reportBlock.DeviceTagNameParts = list.ToArray();

                var StartPage = subNode.GetStringSetting("StartPage", 0);

                Reports reports = new Reports();
                reports.CreateReport(reportBlock, StartPage);
                DMObjectsFinder DMObjectsFinder = new DMObjectsFinder(prj);
                List<ReportBlock> reports1 = DMObjectsFinder.GetAll<ReportBlock>(true).Cast<ReportBlock>().Where(a => a.Action == Action.ACTION_NAME).ToList();
                safetyPoint.Commit();
            }

            ActionManager oMng = new ActionManager();
            Eplan.EplApi.ApplicationFramework.Action oBaseAction = oMng.FindBaseAction(this, true);
            string props = oBaseAction.ActionProperties.ToString();
            return oBaseAction.Execute(oActionCallingContext);

        }

        private SettingNode GetActionSubNode(ProjectSettingNode prjNode3)
        {
            StringCollection sunNodes = new StringCollection();
            prjNode3.GetListOfNodes(ref sunNodes, false);
            foreach (var node in sunNodes)
            {
                SettingNode settingNode = prjNode3.GetSubNode(node);
                var actionName = settingNode.GetStringSetting("Action", 0);
                if (actionName == "JumpersReport")
                {
                    return settingNode;
                }
            }
            return null;
        }
    }
}
