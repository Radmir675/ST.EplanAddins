using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.Base;
using Eplan.EplApi.Gui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace ST.EplAddin.CommonLibrary
{
   
    public class CommonMenu
    {

        private int insertCount;
        public void OnInitGuiST(string ActionName,string menuName) 
        {
      
            if (insertCount == 0)
            {
                CreateStaticMenu(ActionName, menuName);

            }
            else 
            {
                AddStaticMenuItem(ActionName, menuName);
            }
           
           
        }
        public void OnOutGuiST() 
        {
           
            //если есть еще модули в меню, то его просто не удаляем
        }

        private void AddSMenuItem() 
        {

          // new Menu().AddStaticMenuItem();
        }
        private void CreateStaticMenu(string actionName, string menuName)
        {
            Menu staticMenu = new Menu();
            staticMenu.AddStaticMainMenu("STS", NewmultiLanfString("Scantronic"), Menu.MainMenuName.eMainMenuUtilities, NewmultiLanfString(menuName), actionName, NewmultiLanfString("ms"), 1);
            insertCount++;
        }
        private void AddStaticMenuItem(string actionName, string menuName) 
        {
            Menu staticMenu = new Menu();
            staticMenu.AddStaticMenuItem("STS", NewmultiLanfString("Scantronic"), actionName, NewmultiLanfString("Status"),0,"wd",1,false,false);
            insertCount++;
        }

        private MultiLangString NewmultiLanfString(string input)
        {
            MultiLangString multyLangString = new MultiLangString();
            multyLangString.AddString(ISOCode.Language.L_ru_RU, input);
            return multyLangString;
        }
        private AddCallStack() 
        {
            JsonSerializer.Serialize(,);
        }
    }
}
