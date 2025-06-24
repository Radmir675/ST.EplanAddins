using Eplan.EplApi.Base;
using Eplan.EplApi.DataModel;
using System.Collections.Generic;
using System.Linq;
using Project = Eplan.EplApi.DataModel.Project;

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

    //public List<FunctionBasePropertyList> GetTerminalProperties()
    //{

    //    var terminalsRepository = TerminalsRepository.GetInstance();
    //    var data = terminalsRepository.GetAllWithoutRemoving();
    //    var list = new List<FunctionBasePropertyList>(data.Count);
    //    list.AddRange(data.Select(x => x.Properties));

    //    return list;
    //}
    //public List<FunctionBasePropertyList> GetTerminalProperties()
    //{

    //    var terminalsRepository = TerminalsRepository.GetInstance();
    //    var data = terminalsRepository.GetFirstTerminalsFromTerminalStrip().ToList();
    //    var list = new List<FunctionBasePropertyList>(data.Count);
    //    list.AddRange(data.Select(x => x.Properties));

    //    return list;
    //}
    private void SetReportProperties(ReportBlock reportBlock, SettingNode subNode)
    {
        reportBlock.Type = DocumentTypeManager.DocumentType.TerminalLineupDiagram;
        reportBlock.FormName = subNode.GetStringSetting("FormName", 0);
        reportBlock.IsAutomaticPageDescription = true;


        var functionBasePropertyLists = GetTerminalStripProperties()?.ToArray();
        reportBlock.DeviceTagNameParts = functionBasePropertyLists;
        var DT = reportBlock.DeviceTag;

    }

    private IEnumerable<FunctionBasePropertyList> GetTerminalStripProperties()
    {
        var terminalsRepository = TerminalsRepository.GetInstance();
        var terminalStrips = terminalsRepository.GetTerminalStrips();

        foreach (var terminalStrip in terminalStrips)
        {
            yield return terminalStrip.Properties;
        }
    }
}