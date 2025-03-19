using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.Base;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.HEServices;
using ST.EplAddin.ComparisonOfProjectProperties.Models;
using ST.EplAddin.ComparisonOfProjectProperties.ViewModels;
using ST.EplAddin.ComparisonOfProjectProperties.Views;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

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
            if (selectionSet.SelectedProjects.Length != 2)
            {
                MessageBox.Show("Выберите 2 проекта для сравнения!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            var project1 = selectionSet.SelectedProjects[0];
            var project2 = selectionSet.SelectedProjects[1];
            propertiesValue1 = project1.Properties;
            propertiesValue2 = project2?.Properties;


            var projectName1 = project1.ProjectName;
            var projectName2 = project2?.ProjectName;

            progress = new Progress("Progress");

            progress.SetTitle("Свойства проекта");
            progress.SetActionText($"Чтение свойств проекта {projectName1}");
            progress.SetAllowCancel(true);
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
                var id = value.Id.AsInt;

                if (value.Indexes.Length > 0)
                {
                    foreach (var indx in value.Indexes)
                    {
                        var indxVal = value[indx];

                        if (!indxVal.IsEmpty)
                        {
                            var propertyIndxValue = indxVal.GetDisplayString().GetStringToDisplay(ISOCode.Language.L_ru_RU);
                            if (!string.IsNullOrEmpty(propertyIndxValue))
                            {
                                var definitionName = indxVal.Definition.Name;

                                var index = int.Parse((indx + 1).ToString());
                                dictionary.Add(new PropertyKey(id, index), new Property()
                                {
                                    Id = id,
                                    Name = definitionName,
                                    Value = propertyIndxValue,
                                    Index = index
                                });
                            }
                        }
                    }
                }
                else
                {
                    var propertyValue = value.GetDisplayString().GetStringToDisplay(ISOCode.Language.L_ru_RU);

                    if (!string.IsNullOrEmpty(propertyValue))
                    {
                        var definitionName = value.Definition.Name;
                        var index = 0;
                        dictionary.Add(new PropertyKey(id, index), new Property()
                        {
                            Id = id,
                            Name = definitionName,
                            Value = propertyValue,
                        });
                    }
                }
                progress.Step(1);
                if (progress.Canceled()) break;

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
