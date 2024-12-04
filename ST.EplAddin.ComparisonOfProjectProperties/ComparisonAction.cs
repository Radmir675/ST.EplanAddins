using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.Base;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.HEServices;
using ST.EplAddin.ComparisonOfProjectProperties.Models;
using ST.EplAddin.ComparisonOfProjectProperties.ViewModels;
using ST.EplAddin.ComparisonOfProjectProperties.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

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


            if (projectName1 == "dc")
            {
                using (SafetyPoint safetyPoint = SafetyPoint.Create())
                {
                    using (UndoStep undo = new UndoManager().CreateUndoStep())
                    {
                        ChangesRecord changesRecord = new ChangesRecord();
                        var recordChangesList = changesRecord.GetChangesList();
                        foreach (var key in recordChangesList)
                        {
                            //TODO: проверить существует ли такой индекс
                            var initialPropertyValue = propertiesValue1[key];
                            var targetPropertyValue = propertiesValue2[key];
                            try
                            {
                                CopyTo(initialPropertyValue, targetPropertyValue);
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show($"Не удалось присвоить значение свойству {propertiesValue1[key].Definition.Name} | {key} ");
                            }
                        }
                        undo.SetUndoDescription($"Обновление свойств проекта {projectName2}");
                    }
                    safetyPoint.Commit();
                }
            }
            return true;
        }
        private Dictionary<PropertyKey, Property> GetProjectValues(ProjectPropertyList projectPropertyList)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();


            var existingValues = projectPropertyList.ExistingValues;
            var dictionary = new Dictionary<PropertyKey, Property>(existingValues.Length);

            // Console.WriteLine("GetProjectValues " + sw.Elapsed + "ms");



            foreach (var value in existingValues)
            {

                //Stopwatch sw2 = new Stopwatch();
                //sw2.Start();
                var propertyValue = value.GetDisplayString().GetStringToDisplay(ISOCode.Language.L_ru_RU);
                var id = value.Id.AsInt;

                // Console.WriteLine("GetProjectValues2 " + sw.Elapsed + "ms");
                //sw2.Reset();

                if (value.Indexes.Length > 0)
                {



                    int lastindex = value.LastUsedIndex;


                    foreach (var ind in value.Indexes)
                    {
                        var idxVal = value[ind];
                        //  }

                        //   (int i = 0; i < lastindex; i++)

                        //       for (int i = 0; i < lastindex; i++)
                        //  {

                        // if (i + 1 != value.Indexes[i])
                        // {
                        //
                        //    int f = 0;
                        //}

                        //var va1 = value.Indexes[i];
                        // var va2 = value[value.Indexes[i]];
                        // var va3 = value[i+1];


                        //  var idxVal = value[i + 1];//value[value.Indexes[i]];

                        if (!idxVal.IsEmpty)
                        {
                            var propertyValue1 = idxVal.GetDisplayString().GetStringToDisplay(ISOCode.Language.L_ru_RU);
                            if (!string.IsNullOrEmpty(propertyValue1))
                            {
                                var definitionName = idxVal.Definition.Name;
                                //var index = int.Parse(value.Indexes[i].ToString());

                                var index = int.Parse(i + 1.ToString());
                                Console.WriteLine("index " + index + " i" + i);
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
                    //r propertyValue1 = value.GetDisplayString().GetStringToDisplay(ISOCode.Language.L_ru_RU);
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

            // MessageBox.Show("GetProjectValues " + sw.Elapsed +" "+ existingValues.Length + "/" + dictionary.Count);

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
