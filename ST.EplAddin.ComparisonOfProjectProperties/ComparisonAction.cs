using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.Base;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.HEServices;
using ST.EplAddin.ComparisonOfProjectProperties.Models;
using ST.EplAddin.ComparisonOfProjectProperties.ViewModels;
using ST.EplAddin.ComparisonOfProjectProperties.Views;
using System.Collections.Generic;

namespace ST.EplAddin.ComparisonOfProjectProperties
{
    //TODO:Посмотреть что будет если максимум 99
    public class ComparisonAction : IEplAction
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
            if (selectionSet.SelectedProjects.Length != 2) return false;

            var project1 = selectionSet.SelectedProjects[0];
            var project2 = selectionSet.SelectedProjects[1];
            propertiesValue1 = project1.Properties;
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
            return true;
        }
        private Dictionary<PropertyKey, Property> GetProjectValues(ProjectPropertyList projectPropertyList)
        {
            var existingValues = projectPropertyList.ExistingValues;
            var dictionary = new Dictionary<PropertyKey, Property>(existingValues.Length);

            foreach (var value in existingValues)
            {
                var propertyValue = value.GetDisplayString().GetStringToDisplay(ISOCode.Language.L_ru_RU);
                var id = value.Id.AsInt;

                if (value.Indexes.Length > 0)
                {
                    foreach (var ind in value.Indexes)
                    {
                        var idxVal = value[ind];

                        if (!idxVal.IsEmpty)
                        {
                            var propertyValue1 = idxVal.GetDisplayString().GetStringToDisplay(ISOCode.Language.L_ru_RU);
                            if (!string.IsNullOrEmpty(propertyValue1))
                            {
                                var definitionName = idxVal.Definition.Name;

                                var index = int.Parse(ind + 1.ToString());
                                dictionary.Add(new PropertyKey(id, index), new Property()
                                {
                                    Name = definitionName,
                                    Id = id,
                                    Value = propertyValue1,
                                    Index = index
                                });
                            }
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
