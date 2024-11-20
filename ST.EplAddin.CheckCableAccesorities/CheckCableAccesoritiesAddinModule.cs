using Eplan.EplApi.ApplicationFramework;
using System.Windows.Forms;
using Menu = Eplan.EplApi.Gui.Menu;
namespace ST.EplAddin.CheckCableAccesorities
{
    internal class CheckCableAccesoritiesAddinModule : IEplAddIn
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
            uint subMenuID = menu.AddMenuItem(
             "Проверка изделий кабеля", CheckCableAccesoritiesAction.actionName, "", menuId, 0, false, false);
            return true;
        }

        public bool OnRegister(ref bool bLoadOnStart)
        {
            bLoadOnStart = true;
            MessageBox.Show("Check cable accesorities addin is implemented");
            return true;
        }

        public bool OnUnregister()
        {
            return true;
        }
    }
}
