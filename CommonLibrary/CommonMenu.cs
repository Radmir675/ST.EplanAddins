using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.Base;
using Eplan.EplApi.Gui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST.EplAddin.CommonLibrary
{
    public class CommonMenu
    {
        public void OnInitGuiST(string ActionName) 
        {
            CreateStaticMenu(ActionName);
           
            //TODO: при вызове метода создавалось меню или добавлялс новый Action
           // если оно создано то 
        }
        public void OnOutGuiST() 
        {
           
            //если есть еще модули в меню, то его просто не удаляем
        }

        private void AddSMenuItem() 
        {

          // new Menu().AddStaticMenuItem();
        }
        private void CreateStaticMenu(string actionName)
        {
            Menu staticMenu = new Menu();
            staticMenu.AddStaticMainMenu("STS", NewmultiLanfString("Scantronic"), Menu.MainMenuName.eMainMenuUtilities, NewmultiLanfString("Последние клеммы"), actionName, NewmultiLanfString("ms"), 1);
        }

        private MultiLangString NewmultiLanfString(string input)
        {
            MultiLangString multyLangString = new MultiLangString();
            multyLangString.AddString(ISOCode.Language.L_ru_RU, input);
            return multyLangString;
        }
    }
}
