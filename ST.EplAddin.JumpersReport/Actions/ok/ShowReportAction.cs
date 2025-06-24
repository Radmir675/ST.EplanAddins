using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.DataModel.EObjects;
using Eplan.EplApi.HEServices;
using ST.EplAddin.JumpersReport.Providers;
using System.Collections.Generic;
using System.Linq;

//https://www.eplan.help/en-us/Infoportal/Content/api/2.9/API_REPORTS_MODIFICATION.html
namespace ST.EplAddin.JumpersReport.Actions.ok
{
    public class ShowReportAction : IEplAction
    {
        public const string ACTION_NAME = "JumpersReport1";
        private string resultData;
        bool isLoop;

        public void GetActionProperties(ref ActionProperties actionProperties)
        {
        }

        public bool OnRegister(ref string Name, ref int Ordinal)
        {
            Name = ACTION_NAME;
            Ordinal = 98;
            return true;

        }

        public bool Execute(ActionCallingContext oActionCallingContext)
        {
            int count = oActionCallingContext.GetParameterCount();
            string[] contextParams = oActionCallingContext.GetParameters();
            string[] contextStrings = oActionCallingContext.GetStrings();

            string mode = "";
            string objects = "";

            oActionCallingContext.GetParameter("mode", ref mode);
            oActionCallingContext.GetParameter("objects", ref objects);






            if (mode == "Start") //TODO:тут происходят действия при обновлении отчетов
            {
                var SS = new SelectionSet();
                SS.LockProjectByDefault = false;
                var currentProject = SS.GetCurrentProject(true);
                var createAndSaveTerminalStrips = new CreateAndSaveTerminalStrips(currentProject);
                createAndSaveTerminalStrips.FindAndCreateTerminals();
                new ReportProvider().UpdateConnections();



            }

            if (mode == "ModifyObjectList")
            {
                //obj ids
                List<string> objectsList = objects.Split(';').ToList();

                //get objects

                StorableObject so = StorableObject.FromStringIdentifier(objectsList.FirstOrDefault());
                using (SafetyPoint safetyPoint = SafetyPoint.Create())
                {
                    if (so != null && so is Terminal terminal)
                    {
                        if (terminal.Properties[20013].ToString() == "KL" || terminal.Properties[20013].ToString() == "D")
                        {
                            return true;
                        }

                    }

                }

                objects = ";";
                oActionCallingContext.AddParameter("objects", objects);
                return true;
            }




            //    //TODO:надо найти имя клемммника KL b и только после этого выводить отчет
            //    objects = ";";

            //    var terminals = TerminalsRepository.GetInstance().Get();

            //    if (terminals == null || !terminals.Any())
            //    {
            //        oActionCallingContext.AddParameter("objects", objects);
            //        return true;
            //    }
            //    List<string> resultList =
            //        terminals.Where(s => s != null).Select(s => s.ToStringIdentifier()).ToList();
            //    resultData = String.Join(";", resultList);

            //    oActionCallingContext.AddParameter("objects", resultData);
            //    return true;
            //}
            if (mode == "Finish")
            {
                using (SafetyPoint safetyPoint = SafetyPoint.Create())
                {
                    TerminalsRepository.GetInstance().GetAll().ForEach(z => z.Remove());
                }

                return true;
            }
            return true;
        }

    }
}
