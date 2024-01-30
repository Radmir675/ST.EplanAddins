using Eplan.EplApi.Gui;

namespace ST.EplAddin.CommonLibrary
{
    public class CustomUserMenu
    {
        private static uint PreviousMenuId { get; set; }
        private static string LastActionName { get; set; }
        public bool AddMenuItem(string showingActionName, string actionName)
        {
            Menu menu = new Menu();
            if (menu.GetCustomMenuId(LastActionName ?? "", null) == 0)
            {
                PreviousMenuId = menu.AddMainMenu("ST", Menu.MainMenuName.eMainMenuUtilities, showingActionName, actionName, "status", 1);
            }
            else
            {
                var id = menu.AddMenuItem(showingActionName, actionName, "status", PreviousMenuId, 0, true, true);
            }
            LastActionName = showingActionName;
            return true;
        }
    }
}
