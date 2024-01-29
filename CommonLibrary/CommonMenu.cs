using Eplan.EplApi.Gui;
using System;

namespace ST.EplAddin.CommonLibrary
{

    public class CommonMenu
    {
        Menu menu;
        public DataStorageJson<MenuIdentifier> dataStorageJson;

        public CommonMenu()
        {
            string userName = Environment.UserName;
            dataStorageJson = new DataStorageJson<MenuIdentifier>(@"C:\Users\Public\path.json");
        }

        public void AddMenu(string actionName, string actionMenuName)
        {
            MenuIdentifier menuIdentifier;
            string menuName = "Scantronic Systems";
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

            //Check that all json files is deleted except current processes
        }


    }
}
