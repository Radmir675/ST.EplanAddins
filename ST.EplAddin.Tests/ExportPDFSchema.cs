using Eplan.EplApi.Base;
using System.IO;

namespace ST.EplAddin.Tests
{
    internal class ExportPDFSchema
    {
        private const string _exportPDFPathSetting = "USER.PDFExportGUI.SCHEMAS";
        private const string _schemeExportPDFName = "1C";
        private const string _templatePDFExportPath = @"\\asusrv.spb.scantronic.lan\EPLANDATA\Схемы\PDs.1C.xml";

        private const string _pagesPathSetting = "USER.PageBrowserGui.PageFilterScheme";
        private const string _schemePagesFilterName = "PDF";
        private const string _templatePagesFilterPath = @"\\asusrv.spb.scantronic.lan\EPLANDATA\Схемы\PBfiN..PDF.xml";

        public void SetPDFExportScheme()
        {
            SchemeSetting oSchemeSetting = new SchemeSetting();
            oSchemeSetting.Init(_exportPDFPathSetting);
            if (oSchemeSetting.CheckIfSchemeExists(_schemeExportPDFName))
            {
                oSchemeSetting.SetLastUsed(_schemeExportPDFName);
            }
            else
            {
                FileInfo file = new FileInfo(_templatePDFExportPath);
                if (!file.Exists)
                {
                    //TODo: сюда записать ошибки
                    return;
                }
                oSchemeSetting.ImportScheme(_schemeExportPDFName, _templatePDFExportPath, true);
                SetPDFExportScheme();
            }
        }
        public void SetPagesScheme()
        {
            SchemeSetting oSchemeSetting = new SchemeSetting();
            oSchemeSetting.Init(_pagesPathSetting);
            if (oSchemeSetting.CheckIfSchemeExists(_schemePagesFilterName))
            {
                oSchemeSetting.SetLastUsed(_schemePagesFilterName);
            }
            else
            {
                FileInfo file = new FileInfo(_templatePagesFilterPath);
                if (!file.Exists)
                {
                    //TODO: сюда записать ошибки
                    return;
                }
                oSchemeSetting.ImportScheme(_schemePagesFilterName, _templatePagesFilterPath, true);
                SetPagesScheme();
            }
        }
    }
}
