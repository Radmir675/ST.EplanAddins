// Decompiled with JetBrains decompiler
// Type: EplAddin.ConnectionNumeration.AddinModule
// Assembly: ST.EplAddin.ConnectionNumeration, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 16E8408A-E298-4C32-9D31-7775C7701E17
// Assembly location: C:\Users\tembr\Desktop\AddIns\ST.EplAddin.ConnectionNumeration.dll

using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.Gui;
using ST.EplAddin.ConnectionNumeration;

namespace EplAddin.ConnectionNumeration
{
    public class ConnectionAddinModule : IEplAddIn
    {
        public static uint numberOfPosition;


        public virtual string Description => "DEscriptoiion";

        public virtual string MenuPath => nameof(MenuPath);

        public virtual string MenuIcon => "DEscriptoiion.ico";


        public bool OnExit() => true;

        public bool OnInit()
        {

            return true;
        }

        public bool OnInitGui()
        {
            string connectionNumeration = ConnectionPlacementSchemaAction.actionName;
            string cableNumeration = CableConnectionPlacementSchemaAction.actionName;

            Menu menu = new Menu();
            var menuId = menu.GetCustomMenuId("ST", null);
            if (menuId == 0)
                menuId = menu.AddMainMenu("ST", Menu.MainMenuName.eMainMenuUtilities, "None", "None", "Статус", 1);
            uint subMenuID = menu.AddMenuItem(
                "Жилы кабеля", cableNumeration, "", menuId, 0, false, false);
            uint subMenuID1 = menu.AddMenuItem(
                "Выравнивание соединений", cableNumeration, "", menuId, 0, false, false);
            return true;
        }

        public bool OnRegister(ref bool bLoadOnStart)
        {
            bLoadOnStart = true;
            return true;
        }

        public bool OnUnregister() => true;
    }
}
