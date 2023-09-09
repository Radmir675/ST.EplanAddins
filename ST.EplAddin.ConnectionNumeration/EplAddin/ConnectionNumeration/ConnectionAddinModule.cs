// Decompiled with JetBrains decompiler
// Type: EplAddin.ConnectionNumeration.AddinModule
// Assembly: ST.EplAddin.ConnectionNumeration, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 16E8408A-E298-4C32-9D31-7775C7701E17
// Assembly location: C:\Users\tembr\Desktop\AddIns\ST.EplAddin.ConnectionNumeration.dll

using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.Base;
using ST.EplAddin.Base;
using System.Diagnostics;
using Eplan.EplApi.Gui;
using ST.EplAddin.ConnectionNumeration;
using ST.EplAddin.CommonLibrary;

namespace EplAddin.ConnectionNumeration
{
    public class ConnectionAddinModule : IEplAddIn
    {
        CommonMenu commonMenu;
    public static uint numberOfPosition;

        
        public virtual string Description => "DEscriptoiion";

        public virtual string MenuPath => nameof(MenuPath);

        public virtual string MenuIcon => "DEscriptoiion.ico";


        public bool OnExit() 
        {
            commonMenu.OnExitAddin();
            return true;
        }

        public bool OnInit()
        {

            return true;
        }

        public bool OnInitGui()
        {
            string actionName = ConnectionPlacementSchemaAction.actionName;
             commonMenu = new CommonMenu();
            commonMenu.AddMenu(actionName, "Выравнивание соединений");
            return true;
        }

        public bool OnRegister(ref bool bLoadOnStart)
        {
            bLoadOnStart = true;
            System.Windows.Forms.MessageBox.Show("AutoPosition Of Connections addin registred.");
            return true;
        }

        public bool OnUnregister() => true;
    }
}
