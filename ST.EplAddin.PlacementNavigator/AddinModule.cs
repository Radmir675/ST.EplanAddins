using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.Gui;
using Eplan.EplSDK.WPF;

namespace ST.EplAddin.PlacementNavigator
{
    public class PlacementNavigator : IEplAddIn
    {
        EventHandler m_EventHandler;
        public bool OnExit()
        {
            return true;
        }

        public bool OnInit()
        {
            return true;
        }

        public bool OnInitGui()
        {
            Menu menu = new Menu();
            var menuId = menu.GetCustomMenuId("ST", null);
            if (menuId == 0)
                menuId = menu.AddMainMenu("ST", Menu.MainMenuName.eMainMenuUtilities, "None", "None", "Статус", 1);
            uint subMenuID = menu.AddMenuItem(
                "Навигатор символов", "GfDialogManagerShow /DialogName:SymbolsNavigator", "", menuId, 0, false, false);

            //Регистрация навигатора
            var className = "SymbolsNavigator"; //MethodBase.GetCurrentMethod().DeclaringType.Name;
            DialogBarFactory dialogBarFactory = new DialogBarFactory(className, typeof(Navigator), DialogDockingOptions.Any, 0);
            return true;
        }

        public bool OnRegister(ref bool bLoadOnStart)
        {
            bLoadOnStart = true;
            return true;
        }

        public bool OnUnregister()
        {
            return true;
        }
    }
}
