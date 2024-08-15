// Decompiled with JetBrains decompiler
// Type: ST.EplAddin.ConnectionNumeration.EsSwitchConnectionUpdateAction
// Assembly: ST.EplAddin.ConnectionNumeration, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 16E8408A-E298-4C32-9D31-7775C7701E17
// Assembly location: C:\Users\tembr\Desktop\AddIns\ST.EplAddin.ConnectionNumeration.dll

using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.Base;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.HEServices;
using System;

namespace ST.EplAddin.ConnectionNumeration
{
    public class EsSwitchConnectionUpdateAction : IEplAction
    {
        public bool OnRegister(ref string Name, ref int Ordinal)
        {
            Name = "XWnActionRunDesignate";
            Ordinal = 32;
            return true;
        }

        public bool Execute(ActionCallingContext oActionCallingContext)
        {
            string str1 = "";
            string str2 = "";
            ((Context)oActionCallingContext).GetParameter("function", ref str1);
            ((Context)oActionCallingContext).GetParameter("_cmdline", ref str2);
            ((Context)oActionCallingContext).GetParameterCount();
            ((Context)oActionCallingContext).GetParameters();
            ((Context)oActionCallingContext).GetStrings();
            SelectionSet selectionSet = new SelectionSet();
            selectionSet.LockProjectByDefault = false;
            selectionSet.LockSelectionByDefault = false;
            Project currentProject = selectionSet.GetCurrentProject(true);
            string projectName = currentProject.ProjectName;
            Connection[] connectionsWithCf = new DMObjectsFinder(currentProject).GetConnectionsWithCF((ICustomFilter)new ConnectionFilter());
            using (SafetyPoint safetyPoint = SafetyPoint.Create())
            {
                foreach (Connection connection in connectionsWithCf)
                {
                    string str3 = "";
                    if (!connection.Properties.POTENTIAL_NAME.IsEmpty)
                        str3 = connection.Properties.POTENTIAL_NAME.ToString();
                    connection.Properties.CONNECTION_GROUPING.Set(str3);
                }
                safetyPoint.Commit();
            }
            return new ActionManager().FindBaseAction((IEplAction)this, true).Execute(oActionCallingContext);
        }

        public void GetActionProperties(ref ActionProperties actionProperties) => throw new NotImplementedException();
    }
}
