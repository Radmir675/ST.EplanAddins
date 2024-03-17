using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.Gui;
using System;
using Action = Eplan.EplApi.ApplicationFramework.Action;

namespace ST.EplAddin.Footnote
{
    class Footnote_InsertAction : IEplAction, IEplActionChecked
    {
        string paramValue = "Footnote_InsertAction";
        public int Checked(string strActionName, ActionCallingContext actionContext)
        {
            if (strActionName == "TESTACTIONMIXED")
            {
                return 2;
            }
            else if (strActionName == "TESTACTION")
            {
                return 1;
            }
            else
            {
                return 1;
            }
        }

        public bool Execute(ActionCallingContext oActionCallingContext)
        {
            /*
            int count = oActionCallingContext.GetParameterCount();
            string[] contextParams = oActionCallingContext.GetParameters();
            string[] contextStrings 
            = oActionCallingContext.GetStrings();*/
            //string paramValue = null;

            //MFOnCmdMsgManager.cpp(549) : mainmenu item call: Системные сообщения
            //oActionCallingContext.SetParameter("_cmdline", ref paramValue);


            oActionCallingContext.AddParameter("Name", "MyInteraction");
            oActionCallingContext.AddParameter("_cmdline", "XGedStartInteractionAction2D /Name:MyInteraction");

            ContextMenuLocation oLocation = new ContextMenuLocation();
            oLocation.DialogName = "Editor";
            oLocation.ContextMenuName = "Ged";
            ContextMenu oTestMenu = new ContextMenu();
            oTestMenu.AddMenuItem(oLocation, "My Contextmenuname", "Footnote_InsertAction", true, false);

            string strMenuName = "";
            string strActionName = "";
            bool bIsSeparator = false;
            oTestMenu.GetMenuItem(oLocation, 1, ref strMenuName, ref strActionName, ref bIsSeparator);

            string strAction = "XGedStartInteractionAction2D";

            ActionManager oActionManager = new ActionManager();
            Action oAction = oActionManager.FindAction(strAction);
            if (oAction != null)
            {
                ActionCallingContext oContext = new ActionCallingContext();
                oContext.AddParameter("Name", "MyInteraction");
                oContext.AddParameter("_cmdline", "XGedStartInteractionAction2D /Name:MyInteraction");
                oAction.Execute(oContext);
            }
            /*
            new CommandLineInterpreter(true).Execute("XGedStartInteractionAction /Name:XGedFootnote");*/
            return true;
            /*
            string paramValue = "";
            oActionCallingContext.GetParameter("_cmdline", ref paramValue);

        
            ActionManager oMng = new ActionManager();
            Action oBaseAction = oMng.FindBaseAction(this, true);
            return oBaseAction.Execute(oActionCallingContext);*/
        }

        public void GetActionProperties(ref ActionProperties actionProperties)
        {
            throw new NotImplementedException();
        }

        public bool OnRegister(ref string Name, ref int Ordinal)
        {
            Name = "Footnote_InsertAction";
            Ordinal = 99;
            return true;
        }
    }
}
