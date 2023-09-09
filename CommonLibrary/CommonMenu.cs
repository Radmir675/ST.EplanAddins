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

    public partial class CommonMenu
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
            int totalAddinsInjected = dataStorageJson.ReadAllFromStorage()?.AddinsInjectedQuantity ?? 0;
            return totalAddinsInjected;

        }
        public void RemoveMenu(uint currentMenuId)
        {
            menu.RemoveMenuItem(currentMenuId);
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
       

    }
}
