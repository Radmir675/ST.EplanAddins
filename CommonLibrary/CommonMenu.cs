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
        Menu menu;
        DataStorageJson<MenuIdentifier> dataStorageJson;

        public CommonMenu()
        {
            string userName = Environment.UserName;
            dataStorageJson = new DataStorageJson<MenuIdentifier>(@"C:\Users\Public\path.json");
        }

        public void AddMenu(string actionName, string menuName, string actionMenuName)
        {
            MenuIdentifier menuIdentifier;
            menuName = "Scantronic Systems";
            uint menuId;
            menu = new Menu();
            if (GetAddinnsInjectedCount() == 0)
            {
                menuId = menu.AddMainMenu(menuName, Menu.MainMenuName.eMainMenuUtilities, actionMenuName, actionName, "Статус", 1);
                dataStorageJson.SaveItemToStorage(new MenuIdentifier(menuId));
            }
            else
            {
                uint currentMenuId = GetMenuId();
                menuId = menu.AddMenuItem(actionMenuName, actionName, "Статус", currentMenuId, 0, false, false);
                var current = dataStorageJson.ReadAllFromStorage();
                current.AddinsInjectedQuantity++;
                dataStorageJson.SaveItemToStorage(current);

            }
        }

        public uint GetMenuId()
        {
            uint menuId = dataStorageJson.ReadAllFromStorage().MenuId;
            return menuId;
        }
        public int GetAddinnsInjectedCount()
        {
            int totalAddinsInjected;
            try
            {
                totalAddinsInjected = dataStorageJson.ReadAllFromStorage().AddinsInjectedQuantity;
                //ловим ошибку при обращении к quantity
            }
            catch (Exception)
            {

                return 0;
            }
            return totalAddinsInjected;

        }
        public void RemoveMenu(uint currentId)
        {
            menu.RemoveMenuItem(32);
        }
        public void OnExitAddin()
        {
            var current = dataStorageJson.ReadAllFromStorage();
            var total = current.AddinsInjectedQuantity;
            var count = current.AddinsInjectedQuantity - 1;
            dataStorageJson.SaveItemToStorage(new MenuIdentifier(current.MenuId, count));

            if (GetAddinnsInjectedCount() == 0)
            {
                dataStorageJson.RemoveJsonFile();
            }
        }

        public class MenuIdentifier
        {

            public MenuIdentifier(uint menuId)
            {
                MenuId = menuId;
                AddinsInjectedQuantity++;
            }
            public MenuIdentifier(uint menuId, int AddinsInjectedQuantity)
            {
                MenuId = menuId;
                this.AddinsInjectedQuantity = AddinsInjectedQuantity;
            }
            public MenuIdentifier()
            {

            }
            public uint MenuId { get; set; }
            public int AddinsInjectedQuantity { get; set; }
        }


    }
}
