using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.HEServices;
using System;
using System.IO;
using Project = Eplan.EplApi.DataModel.Project;

namespace ST.EplAddin.Tests
{
    public class Action : IEplAction
    {
        public static string actionName = "EplanTests";
        private string _schemeVerificationPath = @"\\asusrv.spb.scantronic.lan\EPLANDATA\Схемы\ISve.EplAddin.xml";
        private string _EplanPathSetting = "PROJECT.XEsInspectionGui.Scheme_Verifications";
        private string schemeName = "EplAddin";
        ProjectSchemeSetting oScheme;
        Project tt;
        public bool OnRegister(ref string Name, ref int Ordinal)
        {
            Name = actionName;
            Ordinal = 99;
            return true;
        }

        public bool Execute(ActionCallingContext oActionCallingContext)
        {
            SelectionSet ss = new SelectionSet();

            tt = ss.GetCurrentProject(true);
            oScheme = new ProjectSchemeSetting();
            Import();
            Set();
            return true;
        }


        public void GetActionProperties(ref ActionProperties actionProperties)
        {

        }

        private void GetCatalogData()
        {
            //var collection = new StringCollection();
            //SettingNode settingNode = new SettingNode((_EplanPathSetting));
            //settingNode.GetListOfNodes(ref collection, false);

            SelectionSet ss = new SelectionSet();

            tt = ss.GetCurrentProject(true);

            oScheme = new ProjectSchemeSetting();
            oScheme.Init(_EplanPathSetting, ss.GetCurrentProject(true));
            if (oScheme.CheckIfSchemeExists(schemeName))
            {
                //  oScheme.SetScheme(schemeName);
                oScheme.SetLastUsed(schemeName);
                //var displayname = oScheme.MLangName.GetStringToDisplay(ISOCode.Language.L_ru_RU);

            }
            else
            {

                oScheme.ImportScheme(schemeName, _schemeVerificationPath, true);
            }
        }
        public bool Import()
        {
            FileInfo file = new FileInfo(_schemeVerificationPath);
            if (!file.Exists)
            {
                Console.WriteLine($"Файла по данному пути не существует {_schemeVerificationPath}");
                return false;
            }
            oScheme.Init(_EplanPathSetting, tt);
            oScheme.ImportScheme(schemeName, _schemeVerificationPath, true);
            return true;
        }
        public void Set()
        {
            // scheme.Init(_eplanPathSetting, _project);
            if (oScheme.CheckIfSchemeExists(schemeName))
            {

                oScheme.SetLastUsed(schemeName);
            }
            else
            {
                Console.WriteLine($"Не удалось установить схему {schemeName}");
            }

        }
    }
}
