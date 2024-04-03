// Decompiled with JetBrains decompiler
// Type: ST.EplAddin.ConnectionNumeration.ConnectionPlacementSchemaAction
// Assembly: ST.EplAddin.ConnectionNumeration, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 16E8408A-E298-4C32-9D31-7775C7701E17
// Assembly location: C:\Users\tembr\Desktop\AddIns\ST.EplAddin.ConnectionNumeration.dll

using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.Base;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.HEServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ST.EplAddin.ConnectionNumeration
{
    public class ConnectionPlacementSchemaAction : IEplAction
    {
        public static string actionName = "conschema";
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
                Project currentProject = new SelectionSet()
                {
                    LockProjectByDefault = false,
                    LockSelectionByDefault = false
                }.GetCurrentProject(false);

                string projectName = currentProject.ProjectName;

                Progress progress = new Progress("SimpleProgress");
                progress.SetTitle("Connection alignment");
                progress.ShowImmediately();
                Connection[] connectionsWithCf = new DMObjectsFinder(currentProject).GetConnectionsWithCF((ICustomFilter)new ConnectionFilter());
                using (SafetyPoint safetyPoint = SafetyPoint.Create())
                {
                    progress.SetNeededSteps(connectionsWithCf.Count());
                    var step = 0;
                    foreach (Connection connection in connectionsWithCf)
                    {
                        foreach (ConnectionDefinitionPoint connectionDefPoint in connection.ConnectionDefPoints)
                        {


                            PinBase[] connectionPoints = ((SymbolReference)connectionDefPoint).GraphicalConnectionPoints;
                            SymbolReference.GraphicalConnection graphicalConnection = ((IEnumerable<SymbolReference.GraphicalConnection>)((SymbolReference)connectionDefPoint).GraphicalConnections).FirstOrDefault<SymbolReference.GraphicalConnection>();
                            if (graphicalConnection?.StartConnectionPoint.Direction == Pin.Directions.Left | graphicalConnection?.StartConnectionPoint.Direction == Pin.Directions.Right)
                            {
                                SymbolReference.PropertyPlacementsSchema placementsSchema = ((IEnumerable<SymbolReference.PropertyPlacementsSchema>)((SymbolReference)connectionDefPoint).
                                    PropertyPlacementsSchemas.All).Where<SymbolReference.PropertyPlacementsSchema>((Func<SymbolReference.PropertyPlacementsSchema, bool>)
                                    (p => p.Name == "Соединение.Обозначение.Горизонтальное")).FirstOrDefault<SymbolReference.PropertyPlacementsSchema>();
                                if (placementsSchema != null)
                                    ((SymbolReference)connectionDefPoint).PropertyPlacementsSchemas.Selected = placementsSchema;
                            }
                            if (graphicalConnection?.StartConnectionPoint.Direction == Pin.Directions.Up | graphicalConnection?.StartConnectionPoint.Direction == Pin.Directions.Down)
                            {
                                SymbolReference.PropertyPlacementsSchema placementsSchema = ((IEnumerable<SymbolReference.PropertyPlacementsSchema>)((SymbolReference)connectionDefPoint).PropertyPlacementsSchemas.All).Where<SymbolReference.PropertyPlacementsSchema>((Func<SymbolReference.PropertyPlacementsSchema, bool>)(p => p.Name == "Соединение.Обозначение.Вертикальное")).FirstOrDefault<SymbolReference.PropertyPlacementsSchema>();
                                if (placementsSchema != null)
                                    ((SymbolReference)connectionDefPoint).PropertyPlacementsSchemas.Selected = placementsSchema;
                            }
                            progress.Step(step++);
                        }
                    }
                    progress.EndPart(true);
                    safetyPoint.Commit();
                }
            }
            return true;
        }

        public void GetActionProperties(ref ActionProperties actionProperties) => throw new NotImplementedException();
    }
}
