using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.HEServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Action = Eplan.EplApi.ApplicationFramework.Action;

namespace ST.EplAddin.Footnote
{
    
    class Footnote_PropertiesActionXGed : IEplAction
    {

        public bool Execute(ActionCallingContext oActionCallingContext)
        {
            string function = "", cmdline = "";
            oActionCallingContext.GetParameter("specialGroupHandling", ref function);
            oActionCallingContext.GetParameter("_cmdline", ref cmdline);
            
            int count = oActionCallingContext.GetParameterCount();
            string[] contextParams = oActionCallingContext.GetParameters();
            string[] contextStrings = oActionCallingContext.GetStrings();

            if (cmdline == "XGedEditPropertiesAction /specialGroupHandling:1")
            {
                SelectionSet Set = new SelectionSet();
                Set.LockProjectByDefault = false;
                Set.LockSelectionByDefault = false;
                Project CurrentProject = Set.GetCurrentProject(true);
                StorableObject so = Set.GetSelectedObject(true);

                bool isBlock = so is Block;
                if (isBlock)
                {
                    Block bl = so as Block;
                    bool isFootnoteBlock = bl.Name.Contains(FootnoteItem.FOOTNOTE_KEY);
                    if (isFootnoteBlock)
                    {
                        FootnoteItem note = new FootnoteItem();
                        note.Create(bl);
                        PropertiesDialog p = new PropertiesDialog();
                        p.setItem(note);
                        p.ShowDialog();
                        return true;
                    }
                }
            }

            ActionManager oMng = new ActionManager();
            Action baseAction = oMng.FindBaseAction(this, true);
            return baseAction.Execute(oActionCallingContext);
        }

        public void GetActionProperties(ref ActionProperties actionProperties)
        {
            throw new NotImplementedException();
        }

        public bool OnRegister(ref string Name, ref int Ordinal)
        {
            Name = "XGedEditPropertiesAction";
            Ordinal = 99;
            return true;

        }
    }
}
