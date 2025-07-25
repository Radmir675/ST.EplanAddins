﻿using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.Gui;
using System;

namespace ST.EplAddins.LastTerminalStrip
{
    public class AddinModule : IEplAddIn
    {
        private string FindLastTerminalsName { get; } = FindLastTerminalAction.ActionName;
        private string ShowHistoryName { get; } = ShowHistoryAction.ActionName;
        private string ShowEmptyTerminalStrips { get; } = ShowEmptyTerminalStripsAction.ActionName;
        private string ShowUnnecessaryBackPlates { get; } = ShowUnnecessaryBackPlatesAction.ActionName;
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
            var menuId = menu.GetCustomMenuId("ST", null);
            if (menuId == 0)
                menuId = menu.AddMainMenu("ST", Menu.MainMenuName.eMainMenuUtilities, "None", "None", "Статус", 1);
            uint subMenuID = menu.AddPopupMenuItem(
             "Клеммы", "Показать журнал", ShowHistoryName, "", menuId, 0, false, false);
            menu.AddMenuItem("Поиск последних клемм в клеммниках", FindLastTerminalsName, "", subMenuID, 0, false, false);
            menu.AddMenuItem("Показать пустые определения клеммников", ShowEmptyTerminalStrips, "", subMenuID, 0, false, false);
            menu.AddMenuItem("Показать лишние торцевые пластины в клеммниках", ShowUnnecessaryBackPlates, "", subMenuID, 0, false, false);
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
