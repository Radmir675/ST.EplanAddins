using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.Gui;
using System;
using Eplan.EplApi.Base;
using ST.EplAddin.CommonLibrary;
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
            CommonMenu commonMenu = new CommonMenu();
            commonMenu.OnInitGuiST(actionName);

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
        public void CreateStaticMenu()
        {
            Menu staticMenu = new Menu();
            staticMenu.AddStaticMainMenu("STS", NewmultiLanfString("Rlc"), Menu.MainMenuName.eMainMenuUtilities, NewmultiLanfString("Последние клеммы"), actionName, NewmultiLanfString("ms"), 1);
        }

        private MultiLangString NewmultiLanfString(string input)
        {
            MultiLangString multyLangString = new MultiLangString();
            multyLangString.AddString(ISOCode.Language.L_ru_RU, input);
            return multyLangString;
        }
    }

}
