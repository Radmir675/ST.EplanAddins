using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EplAddin.ReportSorting
{
    
    class RandomSortAction : IEplAction
    {

        public bool Execute(ActionCallingContext oActionCallingContext)
        {
            string mode = "";
            string objects = "";

            oActionCallingContext.GetParameter("mode", ref mode);
            oActionCallingContext.GetParameter("objects", ref objects);

            int count = oActionCallingContext.GetParameterCount();
            string[] contextParams = oActionCallingContext.GetParameters();
            string[] contextStrings = oActionCallingContext.GetStrings();

            ContextParameterBlock bp = oActionCallingContext.GetContextParameter();

            if (mode == "ModifyObjectList")
            {
                try
                {
                    List<string> objectsList = objects.Split(';').ToList();
                    var rnd = new Random();

                    objectsList = objectsList.OrderBy(item => rnd.Next()).ToList();

                    oActionCallingContext.AddParameter("objects", String.Join(";", objectsList));

                    return true;
                }
                catch (BaseException e)
                {
                    String strMessage = e.Message;
                    int linenumber = (new StackTrace(e, true)).GetFrame(0).GetFileLineNumber();
                    string srcFileName = (new StackTrace(e, true)).GetFrame(0).GetFileName();
                    Trace.WriteLine($"Exception [{srcFileName}:{linenumber}]: " + strMessage);
                }
            }

            return false;
        }

        public void GetActionProperties(ref ActionProperties actionProperties)
        {
            throw new NotImplementedException();
        }

        public bool OnRegister(ref string Name, ref int Ordinal)
        {
            Name = "rnd";
            Ordinal = 20;
            return true;
        }
    }

}
