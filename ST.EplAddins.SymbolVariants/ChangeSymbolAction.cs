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
            using (new LockingStep())
            {

                SelectionSet selectionSet = new SelectionSet()
                {
                    LockProjectByDefault = false,
                    LockSelectionByDefault = false
                };
                Project currentProject = selectionSet.GetCurrentProject(true);
                var userSelection = selectionSet.Selection;
                // TODO отсортировать объекты одного типа(символы)
                //TODO если символы одного имени то для каждого применить операцию вращения

                if (userSelection.Count() != 1)
                {
                    MessageBox.Show("Пожалуйста выберите лишь 1 объект");
                    return false;
                }

                using (SafetyPoint safetyPoint = SafetyPoint.Create())
                {

                    StorableObject storableObject = userSelection.Single();
                    SymbolLibrary symbolLibrary = new SymbolLibrary();
                    SymbolReference symbolref = storableObject as SymbolReference;

                    Symbol parent = symbolref.SymbolVariant.Parent;

                    int intSymbolCurrent = symbolref.SymbolVariant.VariantNr;//отсчет идет от 0
                    int symbolsCount = parent.Variants.Count();//общее число
                    int intSymbolNext = intSymbolCurrent < symbolsCount - 1 ? intSymbolNext = intSymbolCurrent + 1 : 0;

                    var currentVariant = symbolref.SymbolVariant;

                    string symbolLibraryName = symbolref.SymbolVariant.SymbolLibraryName;
                    string symbolName = symbolref.SymbolVariant.SymbolName;


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
                    symbolref.SymbolVariant = symbolVariantToReplace;
                    safetyPoint.Commit();
                }

            }
            return true;
        }
        //TODO: допилить множественное изменение если это один и тот же символ

        public void GetActionProperties(ref ActionProperties actionProperties)
        {

        }


    }
}
