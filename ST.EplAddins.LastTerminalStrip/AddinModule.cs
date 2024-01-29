using Eplan.EplApi.ApplicationFramework;
using ST.EplAddin.CommonLibrary;

namespace ST.EplAddins.LastTerminalStrip
{
    public class AddinModule : IEplAddIn
    {

        CommonMenu commonMenu;
        string actionName = FindLastTerminalAction.actionName;
        public bool OnExit()
        {
            //commonMenu.OnExitAddin();
            return true;
        }

        public bool OnInit()
        {
            return true;
        }

        public bool OnInitGui()
        {

            StaticMenu staticMenu = new StaticMenu();
            staticMenu.AddMenuItem1("LastTerminal", actionName);
            //commonMenu = new CommonMenu();

            //commonMenu.AddMenu(actionName, "Найти клемму");
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
