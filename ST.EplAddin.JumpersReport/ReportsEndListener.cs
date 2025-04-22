using Eplan.EplApi.ApplicationFramework;
using System.Diagnostics;

namespace ST.EplAddin.JumpersReport;

public class ReportsEndListener
{
    public EventHandler anyReportUpdateEnd_EventHandler;
    public EventHandler allReportsGenerateEnd_EventHandler;
    public EventHandler onPostCreateEvaluation_EventHandler;
    public EventHandler onPostCreateEvaluationTemplate_EventHandler;

    public ReportsEndListener()
    {
        anyReportUpdateEnd_EventHandler = new EventHandler("onActionEnd.String.XFgUpdateEvaluationAction"); //Обновить
        anyReportUpdateEnd_EventHandler.EplanEvent += AnyReportUpdateEvent;

        allReportsGenerateEnd_EventHandler = new EventHandler("onActionEnd.String.XFgEvaluateProjectAction"); //Генерировать по проекту
        //allReportsGenerateEnd_EventHandler.EplanEvent += AllReportsGenerateEvent;

        onPostCreateEvaluation_EventHandler = new EventHandler("onPostCreateEvaluation");  //обновление отчера через форму Генерировать "Шестеренка" окончание
        onPostCreateEvaluation_EventHandler.EplanEvent += OnPostCreateEvaluationEvent;

        onPostCreateEvaluationTemplate_EventHandler = new EventHandler("onPostCreateEvaluationTemplate"); //создание шаблона отчета  через форму генерировать
        //onPostCreateEvaluationTemplate_EventHandler.EplanEvent += OnPostCreateEvaluationTemplateEvent;

        //AVXFgCreateEvaluationProjectAction
        //VXFgUpdateEvaluationAction
        //DMEvaluationBlock
        //VXFgCompareEvaluationsTestAction
        //AVXFgCompareEvaluations
        //AVXFgExtEvaluations
        //AVXGedEvaluationTab@@ 


    }

    private void AnyReportUpdateEvent(IEventParameter eventParameter)
    {
        CreateReport();
    }
    private void AllReportsGenerateEvent(IEventParameter eventParameter)
    {
        CreateReport();
    }

    private void OnPostCreateEvaluationEvent(IEventParameter eventParameter)
    {
        int a = 0;
        //CreateReport();
    }

    private void OnPostCreateEvaluationTemplateEvent(IEventParameter eventParameter)
    {
        //CreateReport();
    }

    private void CreateReport()
    {
        ReportProvider reportProvider = new ReportProvider();
        reportProvider.Create();
    }
}