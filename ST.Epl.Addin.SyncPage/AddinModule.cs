using Eplan.EplApi.ApplicationFramework;

namespace ST.Epl.Addin.SyncPage
{
    public class AddinModule : IEplAddIn
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
            //Menu menu = new Menu();
            //menu.AddMainMenu("ST1 ", Menu.MainMenuName.eMainMenuUtilities, "Sync", FindCurrentPageAction.actionName, "Статус", 1);
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
