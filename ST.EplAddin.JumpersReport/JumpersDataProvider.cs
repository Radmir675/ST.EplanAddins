using Eplan.EplApi.DataModel;
using Eplan.EplApi.DataModel.EObjects;
using System.Collections.Generic;
using System.Linq;

namespace ST.EplAddin.JumpersReport
{
    internal class JumpersDataProvider(Project project)
    {
        private Terminal CreateTransientTerminal()
        {
            using (SafetyPoint safetyPoint = SafetyPoint.Create())
            {

                FunctionDefinition funcDefinition = new FunctionDefinition(project, Function.Enums.Category.Terminal, 6, 1);
                Terminal terminal = new Terminal();
                terminal.Create(project, funcDefinition);
                terminal.Name = "K1-X1";
                safetyPoint.Commit();
                return terminal;
            }
        }

        public List<Terminal> GetSetTerminals()
        {
            var connections = FindInsertableJumperConnections();
            var sortedList = SortDeviceJumpers(connections);
            var terminal = CreateTransientTerminal();
            InsertJumperInTerminals(terminal);
            Queue<List<Terminal>> terminals = new Queue<List<Terminal>>();
            return terminals.Dequeue();
        }
        private Connection[] FindInsertableJumperConnections()
        {
            DMObjectsFinder finder = new DMObjectsFinder(project);
            var connections = finder.GetConnectionsWithCF(new ConnectionFilter());
            return connections;
        }
        public string SortDeviceJumpers(Connection[] connections)
        {
            return "";
        }
        public void InsertJumperInTerminals(List<Terminal> terminals)
        {
            using (SafetyPoint safetyPoint = SafetyPoint.Create())
            {
                terminals.First().Properties.FUNC_TERMINAL_JUMPER_INTERN[1] = "1/0;1/0";
                //Свойство сохраняется на клемме, представляющей начало созданной вручную мостовой перемычки между внутренними выводами мостовой перемычки.
                //В этом свойстве определяется "Гребенка перемычки". Для этого от начала перемычки указывается величина шага до следующих клемм с перемычками,
                //а также величина шага до относящегося к ней уровня.
                // Пример: Значение "2/0;1/-1" означает, что есть мостовая перемычка к клемме через одну на том же уровне и от нее к следующей клемме уровнем ниже.
                safetyPoint.Commit();
            }
        }
        public void RemoveTerminals(List<Terminal> terminals)
        {
            //  terminals.ForEach(x=>x.Remove());
        }
    }
}

//connection.EndPin.Type==Pin.ConnPointType.Jumper