using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.Gui;

namespace ST.EplAddin.ComparisonOfProjectProperties
{
    public class AddinModule : IEplAddIn
    {
        public bool OnUnregister()
        {
            return true;
        }

        public bool OnInit()
        {
            Menu menu = new Menu();
            var menuId = menu.GetCustomMenuId("ST", null);
            if (menuId == 0)
                menuId = menu.AddMainMenu("ST", Menu.MainMenuName.eMainMenuUtilities, "None", "None", "Статус", 1);
            uint subMenuID = menu.AddMenuItem(
                "Сравнение свойств проектов", ComparisonAction.actionName, "", menuId, 0, false, false);

            return true;
        }

        public bool OnInitGui()
        {
            return true;
        }
        public bool OnRegister(ref bool bLoadOnStart)
        {
            bLoadOnStart = true;
            return true;
        }

        public bool OnExit()
        {
            return true;
        }
    }
}
