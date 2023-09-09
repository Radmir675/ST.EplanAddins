using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.Gui;
using System;
using Eplan.EplApi.Base;
using ST.EplAddin.CommonLibrary;
namespace ST.EplAddins.LastTerminalStrip
{
    public class AddinModule : IEplAddIn
    {
        CommonMenu commonMenu;
        string actionName = FindLastTerminalAction.actionName;
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

             commonMenu = new CommonMenu();
            commonMenu.AddMenu(actionName,"Найти клемму");
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
