using Eplan.EplApi.Gui;

namespace ST.EplAddin.CommonLibrary
{
    public class StaticMenu
    {
        private static uint menuStaticId { get; set; }
        private static string actionName { get; set; }
        public void AddMenuItem1(string showingActionName, string actionName1)
        {
            Menu menu = new Menu();
            if (menu.GetCustomMenuId(actionName ?? "", null) == 0)
            {
                menuStaticId = menu.AddMainMenu("ST", Menu.MainMenuName.eMainMenuUtilities, showingActionName, actionName1, "status", 1);

            }
            else
            {
                var id = menu.AddMenuItem(showingActionName, actionName, "status1", menuStaticId, 0, true, true);
            }
            actionName = showingActionName;
        }
    }
}
