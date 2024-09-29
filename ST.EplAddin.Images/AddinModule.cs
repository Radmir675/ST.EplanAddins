using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.MasterData;
using Eplan.EplSDK.WPF.DB;
using System.IO;

namespace EplAddin.Article_AddImageContextDialog
{
    /*
        XPartsManagementDialog 1178 вставит картинку
        из буфера, перетаскиванием из файла и по ссылке
        обрезать уменьшить.
        сохранить */
    //https://suplanus.de/partsmanagementextension/
    public class AddinModule : IEplAddIn
    {

        public string TabsheetName => "IMG";// nameof(PartsManagementExtensionContent);
        public string ItemType => "eplan.part";
        public string AddinName
        {
            get
            {
                string addinName = typeof(AddinModule).Assembly.CodeBase;
                addinName = Path.GetFileNameWithoutExtension(addinName);
                return addinName;
            }
        }
        public bool OnRegister(ref bool bLoadOnStart)
        {
            bLoadOnStart = true;
            return true;
        }

        public bool OnUnregister()
        {
            var partsManagement = new MDPartsManagement();
            partsManagement.UnregisterAddin(AddinName);
            partsManagement.UnregisterItem(ItemType);
            partsManagement.UnregisterTabsheet(TabsheetName);
            partsManagement.UnregisterTabsheet("IMG");
            return true;
        }

        public bool OnInit()
        {
            return true;
        }

        public bool OnInitGui()
        {
            var partsManagement = new MDPartsManagement();
            string actionName = nameof(PartsManagementExtensionExampleAction);
            partsManagement.RegisterAddin(AddinName, actionName);
            partsManagement.RegisterItem(AddinName, ItemType);
            partsManagement.RegisterTabsheet(AddinName, ItemType, TabsheetName);
            DialogFactoryDB dialogBarFactory = new DialogFactoryDB(TabsheetName,
                typeof(PartsManagementExtensionContent));
            PartsManagementExtensionExampleAction.TabsheetName = TabsheetName;
            return true;
        }

        public bool OnExit()
        {
            return true;
        }
    }
}
