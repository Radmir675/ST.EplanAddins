using Eplan.EplApi.Base;
using System;
using System.Collections.Specialized;
using System.IO;

namespace ST.EplAddin.Tests
{
    internal class ExportPDFSchema
    {
        private string _exportPDFPathSetting = "USER.PDFExportGUI.SCHEMAS.Data.Name.1C";
        private string _pagesPathSetting = "USER.PageFilter.PDF";
        private string importedschemepage = "USER.PageBrowserGui.PageFilterScheme.PDF";

        public void SetPDFExportScheme()
        {
            var fileName = @"\\asusrv.spb.scantronic.lan\EPLANDATA\Схемы\PDs.1C.xml";
            SetScheme(fileName, _exportPDFPathSetting);
        }

        public void SetPagesScheme()
        {
            var fileName = @"\\asusrv.spb.scantronic.lan\EPLANDATA\Схемы\PBfiN.PDF.xml";
            SetScheme(fileName, _pagesPathSetting);
        }

        private void SetScheme(string fileName, string settingName)
        {
            try
            {
                var collection = new StringCollection();
                SchemeSetting oSchemeSetting = new SchemeSetting();
                SettingNode settingNode = new SettingNode("USER.PDFExportGUI.SCHEMAS");
                settingNode.GetListOfNodes(ref collection, false);
                var exist = oSchemeSetting.CheckIfSchemeExists("USER.PDFExportGUI.SCHEMAS.1С");
                if (!exist) return;

                FileInfo file = new FileInfo(fileName);
                var res = file.Exists;
                if (res == false)
                {
                    return;
                }
                oSchemeSetting.ImportScheme(_exportPDFPathSetting, fileName, true);
                oSchemeSetting.Init(f);
                oSchemeSetting.SetLastUsed(settingName);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }
        }
    }
}
