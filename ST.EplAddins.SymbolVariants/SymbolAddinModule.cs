using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.Gui;

namespace ST.EplAddins.SymbolVariants
{
    public class SymbolAddinModule : IEplAddIn
    {
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
            uint subMenuID = menu.AddMenuItem("Изменить вариант символа", ChangeSymbolAction.actionName, "", menuId, 0, false, false);
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
