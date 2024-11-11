using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.Base;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.HEServices;
using System;
using System.Collections.Generic;

namespace ST.EplAddin.ComparisonOfProjectProperties
{
    public class ComparisonAction : IEplAction

    {
        public static string actionName = "ComparisonOfProjectProperties";
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
            //TODO:здесь можно прописать чтобы указали путь до базового проекта
            if (selectionSet.SelectedProjects.Length != 2)
            {
                return false;
            }
            var project1 = selectionSet.SelectedProjects[0];
            var project2 = selectionSet.SelectedProjects[1];


            var result1 = GetProjectValues(project1);
            var result2 = GetProjectValues(project2);
            if (result1.Count == result2.Count)
            {

            }


            return true;
        }

        private List<PropertyValue> GetProjectValues(Project project1)
        {
            var project1Properties = project1.Properties;
            var existingValues = project1Properties.ExistingValues;
            var result = new List<PropertyValue>(20);
            foreach (var value in existingValues)
            {
                try
                {
                    if (!value.IsEmpty)
                    {
                        result.Add(value.ToString(ISOCode.Language.L_ru_RU));
                    }
                }
                catch (Exception e)
                {
                    result.Add(string.Empty);
                }

            }

            return result;
        }
        private void CopyTo(PropertyValue propertyValueFrom, PropertyValue propertyValueTo)
        {
            propertyValueFrom.CopyTo(propertyValueTo);
        }

        public void GetActionProperties(ref ActionProperties actionProperties)
        {

        }
    }
}
