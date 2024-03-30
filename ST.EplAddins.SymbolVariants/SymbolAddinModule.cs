using Eplan.EplApi.ApplicationFramework;
using System;

namespace ST.EplAddins.SymbolVariants
{
    public class SymbolAddinModule : IEplAddIn
    {
        public bool OnExit()
        {
            throw new NotImplementedException();
        }

        public bool OnInit()
        {
            return true;
        }

        public bool OnInitGui()
        {

            //Menu menu = new Menu();
            //  menu.AddMainMenu("Scan ", Menu.MainMenuName.eMainMenuUtilities, "Change symbol", ChangeSymbolAction.actionName, "Статус", 1);
            return true;
        }

        public bool OnRegister(ref bool bLoadOnStart)
        {
            bLoadOnStart = true;
            System.Windows.Forms.MessageBox.Show("Symbol addin is implemented");
            return true;
        }

        public bool OnUnregister()
        {
            return true;
        }
    }
}
