using Eplan.EplApi.DataModel;
using Eplan.EplApi.DataModel.EObjects;
using System.Collections.Generic;
using System.Linq;

namespace ST.EplAddin.JumpersReport
{
    internal class JumpersDataProvider(Project project)
    {
        private Terminal CreateTransientTerminal(string name)
        {
            using (SafetyPoint safetyPoint = SafetyPoint.Create())
            {

                FunctionDefinition funcDefinition = new FunctionDefinition(project, Function.Enums.Category.Terminal, 6, 1);
                Terminal terminal = new Terminal();
                terminal.Create(project, funcDefinition);
                terminal.Name = name;
                safetyPoint.Commit();
                return terminal;
            }
        }

        private Connection[] FindInsertableJumperConnections()
        {
            DMObjectsFinder finder = new DMObjectsFinder(project);
            var connections = finder.GetConnectionsWithCF(new ConnectionFilter());
            return connections;
        }
        private IEnumerable<List<Connection>> SortDeviceJumpers(Connection[] connections)
        {
            // connections.GroupJoin()





            // connections.FirstOrDefault().StartPin.
            return null;
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
            terminals.ForEach(x => x.Remove());
        }

        public void FindAndCreateTerminals()
        {
            var connections = FindInsertableJumperConnections();
            var sortedList = SortDeviceJumpers(connections);
            var terminal1 = CreateTransientTerminal("K1-X1");
            var terminal2 = CreateTransientTerminal("K1-X1");
            var terminal3 = CreateTransientTerminal("K1-X1");
            var terminal4 = CreateTransientTerminal("K1-X2");
            var terminal5 = CreateTransientTerminal("K1-X2");
            var result = new List<Terminal>(3) { terminal1, terminal2, terminal3 };
            var resul1 = new List<Terminal>(3) { terminal4, terminal5 };
            TerminalsRepository.GetInstance().Save(result);
            TerminalsRepository.GetInstance().Save(resul1);
        }
    }
}

//connection.EndPin.Type==Pin.ConnPointType.Jumper