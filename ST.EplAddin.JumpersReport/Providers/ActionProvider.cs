using Eplan.EplApi.Base;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.HEServices;
using ST.EplAddin.JumpersReport.Actions.ok;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace ST.EplAddin.JumpersReport.Providers;

internal class ActionProvider
{
    private const string EPLAN_ACTION_NAME = "PROJECT.FormGeneratorGui.Templates.PxfForm_TERMINALDIAGRAM";
    private readonly Project project;

    public ActionProvider()
    {
        project = GetCurrentProject();
    }
    public void Execute()
    {
        SettingNode subNode = GetNodeCollection();
        ReportBlockCreatorProvider reportBlockCreatorProvider = new(project, subNode);
        var reportBlock = reportBlockCreatorProvider.Create();
        GetAndCreateData();
        CreateReport(subNode, reportBlock);
        Check();
        //удаление клемм
        //TerminalsRepository.GetInstance().GetAllSavedTerminals().ForEach(z => z.Remove());
    }

    private void Check()
    {
        DMObjectsFinder DMObjectsFinder = new DMObjectsFinder(project);
        List<ReportBlock> reports1 = DMObjectsFinder.GetAll<ReportBlock>(true).Cast<ReportBlock>().Where(a => a.Action == ShowReportAction.ACTION_NAME).ToList();
    }

    private void CreateReport(SettingNode subNode, ReportBlock reportBlock)
    {
        var startPage = subNode.GetStringSetting("StartPage", 0);
        Reports reports = new Reports();
        reports.CreateReport(reportBlock, startPage);

    }


    private void GetAndCreateData()
    {
        ReportProvider reportProvider = new ReportProvider();
        reportProvider.CreateTerminals(project);
    }

    private SettingNode GetNodeCollection()
    {
        ProjectSettingNode prjNode3 = new ProjectSettingNode(project, EPLAN_ACTION_NAME);

        StringCollection colOfSettings3 = new StringCollection();
        prjNode3.GetListOfAllSettings(ref colOfSettings3, true);

        prjNode3.Write(System.IO.Path.GetTempPath() + "ProjectSettingNode3.xml");
        var subNode = GetActionSubNode(prjNode3);
        return subNode;
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
    private Project GetCurrentProject()
    {
        SelectionSet selection = new SelectionSet();
        Project project = selection.GetCurrentProject(false);
        return project;
    }
}