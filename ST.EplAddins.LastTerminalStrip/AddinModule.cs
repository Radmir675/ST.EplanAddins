using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.Gui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ST.EplAddin.ConnectionNumeration;
namespace ST.EplAddins.LastTerminalStrip
{
    public class AddinModule : IEplAddIn
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
            string actionName = FindLastTerminalAction.actionName;
            Menu menu = new Menu();
            menu.AddMainMenu("ST ", Menu.MainMenuName.eMainMenuUtilities, "Last terminal", actionName, "Статус", 1);
            // uint ID= menu.AddMenuItem("SRV",actionName,"Статус",37265,0,false,false);

            return true;
            
        }

        public bool OnRegister(ref bool bLoadOnStart)
        {
            bLoadOnStart = true;
            System.Windows.Forms.MessageBox.Show("Last terminal addin is implemented");
            return true;
        }

        public bool OnUnregister()
        {
            return true;
        }
    }
}
