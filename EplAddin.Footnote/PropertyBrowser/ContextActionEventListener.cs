using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.Gui;
using Eplan.EplApi.HEServices;
using ST.EplAddin.Footnote.ProperyBrowser;
using EventHandler = Eplan.EplApi.ApplicationFramework.EventHandler;
namespace ST.EplAddin.Footnote
{
    class ContextActionEventListener
    {
        EventHandler myHandler = new EventHandler();

        private ContextMenu oTestMenu;
        private ContextMenuLocation oLocation;

        public ContextActionEventListener()
        {
            myHandler.SetEvent("ActualiseMenuConditions");
            myHandler.EplanNameEvent += myHandler_EplanEvent;
        }

        private void myHandler_EplanEvent(IEventParameter iEventParameter, string eventName)
        {
            string s = eventName;

            SelectionSet Set = new SelectionSet();
            Set.LockProjectByDefault = false;
            Set.LockSelectionByDefault = false;
            var currentProject = Set.GetCurrentProject(false);
            var storableObject = Set.GetSelectedObject(true);
            oLocation = new ContextMenuLocation("Editor", "Ged");//изменить

            if (FootnoteVerification.IsFootnoteBlock(storableObject))
            {
                createContextMenu();
            }
            else
            {
                removeContextMenu();
            }
        }

        private void createContextMenu()
        {
            if (oTestMenu == null)
            {
                oTestMenu = new ContextMenu();
                var result = oTestMenu.AddMenuItem(oLocation, "Добавить выноску", "ChangeVariantsSymbol", true, false);
            }

        }
        private void removeContextMenu()
        {
            if (oTestMenu != null && oLocation != null)
            {
                oTestMenu.RemoveMenuItem(oLocation, "Добавить выноску", "ChangeVariantsSymbol", true, false);
                oTestMenu = null;
            }
        }
    }
}
