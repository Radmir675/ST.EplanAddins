using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.Base;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.HEServices;
using ST.EplAddin.ComparisonOfProjectProperties.Models;
using System;
using System.Collections.Generic;
using Project = Eplan.EplApi.DataModel.Project;

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
            Ordinal = 99;
            return true;
        }

        public bool Execute(ActionCallingContext oActionCallingContext)
        {
            SelectionSet selectionSet = new SelectionSet
            {
                LockProjectByDefault = false,
                LockSelectionByDefault = false
            };
            //TODO:здесь можно прописать чтобы указали путь до базового проекта
            Project project2 = null;
            if (selectionSet.SelectedProjects.Length == 2)
            {
                project2 = selectionSet?.SelectedProjects[1];

            }
            var project1 = selectionSet.SelectedProjects[0];
            propertiesValue1 = project1.Properties;
            if (selectionSet.SelectedProjects.Length == 1)
            {
                GetProjectValues(propertiesValue1);
                return true;
            }
            propertiesValue2 = project2.Properties;


            var result1 = GetProjectValues(propertiesValue1);
            var result2 = GetProjectValues(propertiesValue2);
            var projectName1 = project1.ProjectName;
            var projectName2 = project2.ProjectName;

            //var dataStorage = new PropertiesDataStorage(result1, result2, projectName1, projectName2);
            //var mainWindow = new MainWindow
            //{
            //    DataContext = new MainWindowVM(dataStorage)
            //};
            //var dialogResult = mainWindow.ShowDialog() ?? false;
            //if (dialogResult)
            //{
            //    using (SafetyPoint safetyPoint = SafetyPoint.Create())
            //    {
            //        using (UndoStep undo = new UndoManager().CreateUndoStep())
            //        {
            //            ChangesRecord changesRecord = new ChangesRecord();
            //            var recordChangesList = changesRecord.GetChangesList();
            //            foreach (var key in recordChangesList)
            //            {
            //                //TODO: проверить существует ли такой индекс
            //                var initialPropertyValue = propertiesValue1[key];
            //                var targetPropertyValue = propertiesValue2[key];
            //                try
            //                {
            //                    CopyTo(initialPropertyValue, targetPropertyValue);
            //                }
            //                catch (Exception e)
            //                {
            //                    MessageBox.Show($"Не удалось присвоить значение свойству {propertiesValue1[key].Definition.Name} | {key} ");
            //                }
            //            }
            //            undo.SetUndoDescription($"Обновление свойств проекта {projectName2}");
            //        }
            //        safetyPoint.Commit();
            //    }
            //}
            return true;
        }

        private Dictionary<string, PropertyData> GetProjectValues(ProjectPropertyList projectPropertyList)
        {
            var existingValues = projectPropertyList.ExistingValues;
            var dictionary = new Dictionary<string, PropertyData>();
            foreach (var value in existingValues)
            {
                var propertyValue = value.GetDisplayString().GetStringToDisplay(ISOCode.Language.L_ru_RU);
                var id = value.Id.AsInt;
                if (id == 10618)
                {
                    var s = 1;
                }

                if (value.Indexes.Length > 0)
                {
                    for (int i = 0; i < value.Indexes.Length; i++)
                    {

                        try
                        {
                            var propertyValue1 = value[value.Indexes[i]].GetDisplayString().GetStringToDisplay(ISOCode.Language.L_ru_RU);
                            if (!string.IsNullOrEmpty(propertyValue1))
                            {
                                var definitionName = value[value.Indexes[i]].Definition.Name;
                                dictionary.Add($"{id}[{value.Indexes[i]}]", new PropertyData(id, propertyValue, definitionName));
                            }

                        }
                        catch (Exception e)
                        {

                        }
                    }
                }
                else if (!string.IsNullOrEmpty(propertyValue))
                {
                    var definitionName = value.Definition.Name;
                    var res = value.Definition.IsNamePart;
                    dictionary.Add($"{id}", new PropertyData(id, propertyValue, definitionName));
                }

            }

            return dictionary;
        }
        private void CopyTo(PropertyValue propertyValueFrom, PropertyValue propertyValueTo)
        {
            propertyValueFrom.CopyTo(propertyValueTo);
        }
        public void GetR(Project project)
        {
            List<(string, string, string, bool)> items = new();
            foreach (AnyPropertyId hPProp in Properties.AllProjectPropIDs)
            {
                // check if exists

                if (project.Properties[hPProp].Definition.Type == PropertyDefinition.PropertyType.String)
                {
                    try
                    {
                        //read string property
                        var oPropValue = project.Properties[hPProp];
                        var strTmp = oPropValue.ToString();
                        var definitionName = hPProp.Definition.Name;
                        var res = project.Properties[hPProp].Definition.IsNamePart;
                        items.Add((strTmp, hPProp.ToString(), definitionName, res));

                    }
                    catch (Exception e)
                    {

                    }
                }
                else
                {
                    var s = 123;
                }

            }
        }
        public void GetActionProperties(ref ActionProperties actionProperties)
        {

        }
    }
}
