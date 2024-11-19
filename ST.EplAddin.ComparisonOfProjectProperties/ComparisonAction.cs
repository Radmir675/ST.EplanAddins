using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.Base;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.HEServices;
using ST.EplAddin.ComparisonOfProjectProperties.Models;
using ST.EplAddin.ComparisonOfProjectProperties.ViewModels;
using ST.EplAddin.ComparisonOfProjectProperties.Views;
using System;
using System.Collections.Generic;
using Project = Eplan.EplApi.DataModel.Project;

namespace ST.EplAddin.ComparisonOfProjectProperties
{
    public partial class ComparisonAction : IEplAction
    {
        private ProjectPropertyList propertiesValue1 { get; set; } = null;
        private ProjectPropertyList propertiesValue2 { get; set; } = null;
        Progress progress;

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
            propertiesValue2 = project2?.Properties;


            var projectName1 = project1.ProjectName;
            var projectName2 = project2?.ProjectName;

            progress = new Progress("Progress");
            progress.SetTitle("Свойства проекта");
            progress.SetActionText($"Чтение свойств проекта {projectName1}");
            progress.SetAllowCancel(false);
            progress.SetAskOnCancel(true);
            progress.ShowImmediately();
            progress.SetNeededSteps(project1.Properties.ExistingValues.Length + project2.Properties.ExistingValues.Length);

            progress.SetNeededParts(2);
            progress.ShowLevel(2);
            var result1 = GetProjectValues(propertiesValue1);

            progress.EndPart();
            progress.SetActionText($"Чтение свойств проекта {projectName2}");
            var result2 = GetProjectValues(propertiesValue2);
            progress.EndPart(true);


            var dataStorage = new PropertiesDataStorage(result1, result2, projectName1, projectName2);
            var mainWindow = new MainWindow
            {
                DataContext = new MainWindowVM(dataStorage)
            };
            var dialogResult = mainWindow.ShowDialog() ?? false;


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
        private Dictionary<PropertyKey, Property> GetProjectValues(ProjectPropertyList projectPropertyList)
        {
            var existingValues = projectPropertyList.ExistingValues;
            var dictionary = new Dictionary<PropertyKey, Property>();
            foreach (var value in existingValues)
            {
                var propertyValue = value.GetDisplayString().GetStringToDisplay(ISOCode.Language.L_ru_RU);
                var id = value.Id.AsInt;

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
                                var index = int.Parse(value.Indexes[i].ToString());
                                dictionary.Add(new PropertyKey(id, index), new Property()
                                {
                                    Name = definitionName,
                                    Id = id,
                                    Value = propertyValue1,
                                    Index = index
                                });
                            }
                        }
                        catch (Exception)
                        {

                        }
                    }
                }
                else if (!string.IsNullOrEmpty(propertyValue))
                {
                    var definitionName = value.Definition.Name;
                    var index = 0;
                    dictionary.Add(new PropertyKey(id, index), new Property()
                    {
                        Name = definitionName,
                        Id = id,
                        Value = propertyValue,
                        Index = index
                    });
                    progress.Step(1);
                }
                progress.Step(1);
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
