using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.DataModel;
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
            var userSelection = selectionSet.Selection;

            if (userSelection.Count() != 1)
            {
                MessageBox.Show("Вы выбрали более одного объекта!");
                return false;
            }

            StorableObject storableObject = userSelection.Single();
            
            return true;
        }

        public void GetActionProperties(ref ActionProperties actionProperties)
        {

        }


    }
}
