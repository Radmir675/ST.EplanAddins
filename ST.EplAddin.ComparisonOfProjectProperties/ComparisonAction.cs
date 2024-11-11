using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.Base;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.HEServices;
using ST.EplAddin.ComparisonOfProjectProperties.Models;
using ST.EplAddin.ComparisonOfProjectProperties.ViewModels;
using ST.EplAddin.ComparisonOfProjectProperties.Views;
using System;
using System.Collections.Generic;

namespace ST.EplAddin.ComparisonOfProjectProperties
{
    public class ComparisonAction : IEplAction

    {
        private ProjectPropertyList propertiesValue1 { get; set; } = null;
        private ProjectPropertyList propertiesValue2 { get; set; } = null;

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


            propertiesValue1 = project1.Properties;
            propertiesValue2 = project2.Properties;

            var result1 = GetProjectValues(propertiesValue1);
            var result2 = GetProjectValues(propertiesValue2);

            var mainWindowVm = new MainWindowVM(result1, result2);
            var mainWindow = new MainWindow();
            mainWindow.DataContext = mainWindowVm;
            mainWindow.ShowDialog();

            return true;
        }

        private Dictionary<int, PropertyData> GetProjectValues(ProjectPropertyList projectPropertyList)
        {
            var existingValues = projectPropertyList.ExistingValues;
            var dictionary = new Dictionary<int, PropertyData>();
            foreach (var value in existingValues)
            {
                try
                {
                    if (!value.IsEmpty)
                    {
                        var propertyValue = value.ToString(ISOCode.Language.L_ru_RU);
                        var id = value.Id.AsInt;

                        dictionary.Add(id, new PropertyData(id, propertyValue));
                    }
                }
                catch (Exception e)
                {
                    dictionary.Add(value.Id.AsInt, new PropertyData(value.Id.AsInt, string.Empty));
                }

            }

            return dictionary;
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
