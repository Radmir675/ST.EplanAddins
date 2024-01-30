using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.DataModel;
using ST.EplAddin.SyncPage;
using System;
using Action = Eplan.EplApi.ApplicationFramework.Action;

namespace ST.Epl.Addin.SyncPage
{
    class FindCurrentPageAction : IEplAction
    {
        public static string actionName = "XEsSyncPDDsAction";
        public bool Execute(ActionCallingContext oActionCallingContext)
        {
            using (SafetyPoint safetyPoint = SafetyPoint.Create())
            {
                ActionManager oMng = new ActionManager();
                Action baseAction = oMng.FindBaseAction(this, true);
                bool resultSync = baseAction.Execute(oActionCallingContext);

                SyncPageAction syncPageAction = new SyncPageAction();
                var result = syncPageAction.Execute();
                safetyPoint.Commit();
                return result;
            }

        }

        public void GetActionProperties(ref ActionProperties actionProperties)
        {
            throw new NotImplementedException();
        }

        public bool OnRegister(ref string Name, ref int Ordinal)
        {
            Name = actionName;
            Ordinal = 32;
            return true;
        }
    }
}
