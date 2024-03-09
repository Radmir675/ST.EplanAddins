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
            menu.AddMainMenu("ST ", Menu.MainMenuName.eMainMenuUtilities, "Terminal", actionName, "Статус", 1);
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
