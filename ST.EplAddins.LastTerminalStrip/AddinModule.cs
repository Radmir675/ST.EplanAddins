using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.Base;
using Eplan.EplApi.Gui;
using System;

namespace ST.EplAddins.LastTerminalStrip
{
    public class AddinModule : IEplAddIn
    {
        string actionName = FindLastTerminalAction.actionName;
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
            //menu.AddMainMenu("ST ", Menu.MainMenuName.eMainMenuUtilities, "Terminal", actionName, "Статус", 1);
            //var menuId = menu.GetCustomMenuId("Terminal", actionName);



            var menuId = menu.GetCustomMenuId("ST", actionName);
            if (menuId == 0)
                menuId = menu.AddMainMenu("ST", Eplan.EplApi.Gui.Menu.MainMenuName.eMainMenuHelp, ".", ".", "", 1);
            uint subMenuID = menu.AddPopupMenuItem(
               "Terminals strips", "Show history", "gstatistic", "", menuId, 0, false, false);
            menu.AddMenuItem("Find last terminals", actionName, "", subMenuID, 0, false, false);
            menu.AddMenuItem("Show history", "gfixmergearcs", null, subMenuID, 0, false, false);
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

        private MultiLangString NewmultiLanfString(string input)
        {
            MultiLangString multyLangString = new MultiLangString();
            multyLangString.AddString(ISOCode.Language.L_ru_RU, input);
            return multyLangString;
        }
    }
}
