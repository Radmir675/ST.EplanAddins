using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST.EplAddin.ReportSorting
{

    using Eplan.EplApi.ApplicationFramework;
    using Eplan.EplApi.Base;
    using Eplan.EplApi.DataModel;
    using Eplan.EplApi.HEServices;
    using ST.EplAddin.ReportSorting.Comparators;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using static Eplan.EplApi.DataModel.PropertyDefinition;
    using Action = Eplan.EplApi.ApplicationFramework.Action;

    namespace EplAddin.ReportSorting
    {

        class ReportSortAction : IEplAction
        {

            public bool Execute(ActionCallingContext oActionCallingContext)
            {

                int count = oActionCallingContext.GetParameterCount();
                string[] contextParams = oActionCallingContext.GetParameters();
                string[] contextStrings = oActionCallingContext.GetStrings();

                string project = "";
                string mode = "";
                string objects = "";
                string pages = "";
                string order = "";
                string filter = "";

                oActionCallingContext.GetParameter("project", ref project);
                oActionCallingContext.GetParameter("mode", ref mode);
                oActionCallingContext.GetParameter("objects", ref objects);
                oActionCallingContext.GetParameter("pages", ref pages);
                oActionCallingContext.GetParameter("order", ref order);
                oActionCallingContext.GetParameter("filter", ref filter);

                if (mode == "ModifyObjectList")
                {
                    try
                    {
                        //obj ids
                        List<string> objectsList = objects.Split(';').ToList();
                        List<StorableObject> soList = new List<StorableObject>();

                        //resort
                        var rnd = new Random();
                        objectsList = objectsList.OrderBy(item => rnd.Next()).ToList();

                        //get objects
                        foreach (string objstring in objectsList)
                        {
                            StorableObject so = StorableObject.FromStringIdentifier(objstring);
                            soList.Add(so);
                        }

                        //sort
                        List<string> orderList = order.Split(';').ToList();
                        SortAscendingHelper comp = new SortAscendingHelper();
                        comp.SetOrder(orderList);
                        soList.Sort(comp);

                        //trace
                        //List<string> resultLis = soList.Select(s => s.Properties[31002].ToString()).ToList();
                        //foreach(string s in resultLis)
                        //System.Diagnostics.Trace.WriteLine($"CompareBy {s}");

                        //result
                        List<string> resultList = soList.Where(s => s != null).Select(s => s.ToStringIdentifier()).ToList();
                        string result = String.Join(";", resultList);
                        oActionCallingContext.AddParameter("objects", result);

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
                Name = "sort";
                Ordinal = 20;
                return true;
            }
        }
    }
}
