using Eplan.EplApi.Base;
using Eplan.EplApi.DataModel;
using System.Collections.Generic;
using System.Linq;

namespace ST.EplAddin.JumpersReport.Providers;

internal class ReportBlockCreatorProvider
{
    private readonly Project _project;
    private readonly SettingNode _subNode;

    public ReportBlockCreatorProvider(Project project, SettingNode subNode)
    {
        _project = project;
        _subNode = subNode;
    }
    public ReportBlock Create()
    {
        ReportBlock reportBlock = new ReportBlock();
        reportBlock.Create(_project);
        SetReportProperties(reportBlock, _subNode);
        return reportBlock;

    }

    public List<FunctionBasePropertyList> GetTerminalProperties()
    {

        var terminalsRepository = TerminalsRepository.GetInstance();
        var data = terminalsRepository.GetAllWithoutRemoving();
        var list = new List<FunctionBasePropertyList>(data.Count);
        list.AddRange(data.Select(x => x.Properties));

        return list;

    }
    private void SetReportProperties(ReportBlock reportBlock, SettingNode subNode)
    {
        reportBlock.Type = DocumentTypeManager.DocumentType.TerminalDiagram;
        reportBlock.FormName = subNode.GetStringSetting("FormName", 0);
        reportBlock.IsAutomaticPageDescription = true;
        reportBlock.DeviceTagNameParts = GetTerminalProperties().ToArray();
        var DT = reportBlock.DeviceTag;
        var date = reportBlock.CreateDate;

    }
}