using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.Gui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Menu menu = new Menu();
            menu.AddMainMenu("Scan ", Menu.MainMenuName.eMainMenuUtilities, "Change symbol", actionName, "Статус", 1);
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
