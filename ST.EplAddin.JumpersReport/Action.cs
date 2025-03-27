using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.DataModel;
using Eplan.EplApi.DataModel.EObjects;
using Eplan.EplApi.HEServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ST.EplAddin.JumpersReport
{
    public class Action : IEplAction
    {
        public static string actionName = "JumpersReport";
        private Project currentProject;
        string resultData;
        public static bool IsReady = false;
        public void GetActionProperties(ref ActionProperties actionProperties)
        {
        }
        public bool OnRegister(ref string Name, ref int Ordinal)
        {
            Name = actionName;
            Ordinal = 98;
            return true;

        }

        public bool Execute(ActionCallingContext oActionCallingContext)
        {
            if (!IsReady)
            {
                return false;
            }

            int count = oActionCallingContext.GetParameterCount();
            string[] contextParams = oActionCallingContext.GetParameters();
            string[] contextStrings = oActionCallingContext.GetStrings();

            string project = "";
            string mode = "";
            string objects = "";
            string pages = "";
            string order = "";
            string filter = "";
            string mainfunction = "";
            oActionCallingContext.GetParameter("project", ref project);
            oActionCallingContext.GetParameter("mode", ref mode);
            oActionCallingContext.GetParameter("objects", ref objects);
            oActionCallingContext.GetParameter("pages", ref pages);
            oActionCallingContext.GetParameter("order", ref order);
            oActionCallingContext.GetParameter("filter", ref filter);
            oActionCallingContext.GetParameter("mainfunction", ref mainfunction);

            if (mode == "Start")
            {
                using (SafetyPoint safetyPoint = SafetyPoint.Create())
                {
                    currentProject = StorableObject.FromStringIdentifier(project).Project;
                    objects = "";
                    var terminal = CreateTransientTerminal();
                    List<Terminal> terminals = new List<Terminal>();
                    terminals.Add(terminal);
                    terminals.Add(terminal);
                    terminals.Add(terminal);
                    List<string> resultList =
                        terminals.Where(s => s != null).Select(s => s.ToStringIdentifier()).ToList();

                    resultData = String.Join(";", resultList);
                    RemoveTerminals(terminals);

                    safetyPoint.Commit();
                }
            }

            if (mode == "ModifyObjectList")
            {

                oActionCallingContext.AddParameter("objects", resultData);
                return true;
            }
            if (mode == "Finish")
            {
                IsReady = false;
            }



            return false;
        }

        private void RemoveTerminals(List<Terminal> terminals)
        {
            //  terminals.ForEach(x=>x.Remove());
        }

        private bool B()
        {
            SelectionSet selection = new SelectionSet();
            var selectionJSelectedObject = selection.GetSelectedObject(true) as Terminal;
            var functionDefinition = selectionJSelectedObject.FunctionDefinition;
            var id = functionDefinition.Id;
            var group = functionDefinition.Group;
            return true;
        }



        private Terminal CreateTransientTerminal()
        {

            FunctionDefinition funcDefinition = new FunctionDefinition(currentProject, Function.Enums.Category.Terminal, 6, 1);
            Terminal terminal = new Terminal();
            terminal.Create(currentProject, funcDefinition);
            terminal.Name = "K1-X1";
            return terminal;
        }

        public Connection[] FindInsertableJumpers()
        {
            DMObjectsFinder finder = new DMObjectsFinder(currentProject);
            var connections = finder.GetConnectionsWithCF(new ConnectionFilter());
            return connections;
        }
        public void InsertJumper(List<Terminal> terminals)
        {
            terminals.First().Properties.FUNC_TERMINAL_JUMPER_INTERN[1] = "1/0;1/0";
            //Свойство сохраняется на клемме, представляющей начало созданной вручную мостовой перемычки между внутренними выводами мостовой перемычки.
            //В этом свойстве определяется "Гребенка перемычки". Для этого от начала перемычки указывается величина шага до следующих клемм с перемычками,
            //а также величина шага до относящегося к ней уровня.
            // Пример: Значение "2/0;1/-1" означает, что есть мостовая перемычка к клемме через одну на том же уровне и от нее к следующей клемме уровнем ниже.
        }

        ////get objects
        //foreach (string objstring in objectsList)
        //{
        //    StorableObject so = StorableObject.FromStringIdentifier(objstring);

        //}
    }
}
//connection.EndPin.Type==Pin.ConnPointType.Jumper