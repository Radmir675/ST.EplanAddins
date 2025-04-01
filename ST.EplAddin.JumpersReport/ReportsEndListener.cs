using Eplan.EplApi.ApplicationFramework;

namespace ST.EplAddin.JumpersReport;

public class ReportsEndListener
{
    public EventHandler anyReportUpdateEnd_EventHandler;
    public EventHandler allReportsGenerateEnd_EventHandler;

    public ReportsEndListener()
    {
        anyReportUpdateEnd_EventHandler = new EventHandler("onActionEnd.String.XFgUpdateEvaluationAction");
        allReportsGenerateEnd_EventHandler = new EventHandler("onActionEnd.String.XFgEvaluateProjectAction");

        allReportsGenerateEnd_EventHandler.EplanEvent += AllReportsGenerateEvent;
        anyReportUpdateEnd_EventHandler.EplanEvent += AnyReportUpdateEvent;
    }

    private void AnyReportUpdateEvent(IEventParameter eventParameter)
    {
        CreateReport();
    }
    private void AllReportsGenerateEvent(IEventParameter eventParameter)
    {
        CreateReport();
    }

    private void CreateReport()
    {
        ReportProvider reportProvider = new ReportProvider();
        reportProvider.Create();
    }
}