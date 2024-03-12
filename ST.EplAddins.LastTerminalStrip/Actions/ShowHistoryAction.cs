using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.HEServices;
using ST.EplAddin.LastTerminalStrip;


namespace ST.EplAddins.LastTerminalStrip
{
    public class ShowHistoryAction : IEplAction
    {

        public static string ActionName { get; set; } = "ShowHistory";
        public bool OnRegister(ref string Name, ref int Ordinal)
        {
            Name = ActionName;
            Ordinal = 32;
            return true;
        }

        public bool Execute(ActionCallingContext oActionCallingContext)
        {
            using (SafetyPoint safetyPoint = SafetyPoint.Create())
            {
                SelectionSet selectionSet = new SelectionSet();
                Project currentProject = selectionSet.GetCurrentProject(true);
                var projectName = currentProject.ProjectName;
                LoggerForm loggerForm = new LoggerForm(projectName);
                loggerForm.Show();
                loggerForm.PressShowHistory();
                safetyPoint.Commit();
            }
            return true;
        }

        public void GetActionProperties(ref ActionProperties actionProperties)
        {

        }
    }
}
