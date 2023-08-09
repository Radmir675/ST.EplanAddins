using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.Gui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        //"ST Add-ins",, Menu.MainMenuName.eMainMenuUtilities, 
        // "Выравнивание соединений", actionName, "Статус", 1
        //"Принадлежности последних клемм"
        public bool OnInitGui()
        {
            string actionName = FindLastTerminalAction.actionName;
            Menu menu = new Menu();
            menu.AddMenuItem("л",actionName,"dc",1,1,false,false);
           
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
