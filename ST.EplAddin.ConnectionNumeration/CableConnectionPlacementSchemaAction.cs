using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.HEServices;
using System;
using System.Linq;

namespace ST.EplAddin.ConnectionNumeration
{
    class CableConnectionPlacementSchemaAction : IEplAction
    {
        public static string actionName = "cabconschema";
        public bool OnRegister(ref string Name, ref int Ordinal)
        {
            Name = actionName;
            Ordinal = 32;
            return true;
        }

        public bool Execute(ActionCallingContext oActionCallingContext)
        {

            using (LockingStep oLS = new LockingStep())
            {
                SelectionSet set = new SelectionSet();
                set.LockProjectByDefault = false;
                set.LockSelectionByDefault = false;

                Project CurrentProject = set.GetCurrentProject(false);
                string ProjectName = CurrentProject.ProjectName;

                DMObjectsFinder oFinder = new DMObjectsFinder(CurrentProject);
                Connection[] conns = oFinder.GetConnectionsWithCF(new CableConnectionFilter());

                using (SafetyPoint safetyPoint = SafetyPoint.Create())
                {
                    foreach (Connection c in conns)
                    {
                        foreach (ConnectionDefinitionPoint cdp in c.ConnectionDefPoints)
                        {
                            PinBase[] pb = cdp.GraphicalConnectionPoints;
                            SymbolReference.GraphicalConnection[] srgc = cdp.GraphicalConnections;

                            SymbolReference.GraphicalConnection gc = srgc.FirstOrDefault();

                            SymbolReference.PropertyPlacementsSchema pps = cdp.PropertyPlacementsSchemas.All.Where(p => p.Name == "Жила кабеля").FirstOrDefault();
                            if (pps != null)
                            {
                                cdp.PropertyPlacementsSchemas.Selected = pps;
                            }
                            else
                            {
                                int a = 0;
                            }
                        }
                    }
                    safetyPoint.Commit();
                }
            }
            return true;
        }

        public void GetActionProperties(ref ActionProperties actionProperties)
        {
            throw new NotImplementedException();
        }
    }
}
