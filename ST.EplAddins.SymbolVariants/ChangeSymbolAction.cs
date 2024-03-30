using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.Base;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.DataModel.MasterData;
using Eplan.EplApi.HEServices;
using System;
using System.Linq;
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
            selectionSet.LockProjectByDefault = false;
            selectionSet.LockSelectionByDefault = false;

            Project currentProject = selectionSet.GetCurrentProject(true);
            var userSelection = selectionSet.Selection;
          
            if (userSelection.Any())
            {
                MessageBox.Show("Пожалуйста выберите объекты", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            using (SafetyPoint safetyPoint = SafetyPoint.Create())
            {
                StorableObject[] selectedSymbols = userSelection;
                SymbolLibrary symbolLibrary = new SymbolLibrary();
                SymbolReference symbolref = selectedSymbols.First() as SymbolReference;//будем вести просмотр по первому объекту
                string symbolName = symbolref.SymbolVariant.SymbolName;//название символа
                bool isSymbolsNameEqual = selectedSymbols.All(symbol => (symbol as SymbolReference).SymbolVariant.SymbolName == symbolName);//проверка на одинаковый тип символов
                if (isSymbolsNameEqual == false)
                {
                    MessageBox.Show("Объекты разных типов", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                Symbol parent = symbolref.SymbolVariant.Parent;

                int intSymbolCurrent = symbolref.SymbolVariant.VariantNr;//отсчет идет от 0
                int symbolsCount = parent.Variants.Count();//общее число
                int intSymbolNext = intSymbolCurrent < symbolsCount - 1 ? intSymbolNext = intSymbolCurrent + 1 : 0;

                var currentVariant = symbolref.SymbolVariant;

                string symbolLibraryName = symbolref.SymbolVariant.SymbolLibraryName;

                var currentSymbol = currentProject.SymbolLibraries
                    .Where(x => x.Name == symbolLibraryName)
                    .Select(x => x.Symbols.Where(symbolNamez => symbolNamez.Name == symbolName)).Single();

                SymbolVariant symbolVariantToReplace = null;
                try
                {
                    symbolVariantToReplace = currentSymbol.Select(c => c.Variants.Single(g => g.VariantNr == intSymbolNext)).Single();

                }
                catch (Exception)
                {
                    symbolVariantToReplace = currentSymbol.Select(c => c.Variants.Single(g => g.VariantNr == 0)).Single();
                }
                bool locked = symbolref.IsLocked;

                locked = symbolref.IsLocked;
                try
                {
                    //symbolref.SymbolVariant = symbolVariantToReplace;//запилить
                    selectedSymbols.ToList().ForEach(symbol => (symbol as SymbolReference).SymbolVariant = symbolVariantToReplace);
                }
                catch (LockingException e)
                {
                    e.GetMessageText();

                }
                safetyPoint.Commit();
            }
            return true;
        }

        public void GetActionProperties(ref ActionProperties actionProperties)
        {

        }


    }
}
