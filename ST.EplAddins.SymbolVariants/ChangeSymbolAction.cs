using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.DataModel.MasterData;
using Eplan.EplApi.HEServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ST.EplAddins.SymbolVariants
{
    class ChangeSymbolAction : IEplAction
    {
        public static string actionName = "ChangeVariantsSymbol";
        public bool OnRegister(ref string Name, ref int Ordinal)
        {
            Name = actionName;
            Ordinal = 32;
            return true;
        }
        public bool Execute(ActionCallingContext oActionCallingContext)
        {
            SelectionSet selectionSet = new SelectionSet();
            Project currentProject = selectionSet.GetCurrentProject(true);
            var userSelection = selectionSet.Selection;

            if (userSelection.Count() != 1)
            {
                MessageBox.Show("Пожалуйста выберите лишь 1 объект");
                return false;
            }

            StorableObject storableObject = userSelection.Single();
            var s=((Symbol)storableObject).Variants;
            Symbol symbols = (Symbol)userSelection.Single() ;
            
            var selectedSymbolVariantsCount=symbols.Variants;
            

            SymbolReference symbolReference = new SymbolReference();
            var currentSymbol=symbolReference.SymbolVariant.VariantNr;
            return true;
        }

        public void GetActionProperties(ref ActionProperties actionProperties)
        {

        }


    }
}
