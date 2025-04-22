using Eplan.EplApi.DataModel;
using Eplan.EplApi.DataModel.EObjects;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ST.EplAddin.JumpersReport
{
    internal class JumpersDataProvider(Project project)
    {
        private Terminal CreateTransientTerminals(JumperConnection jumperConnection, bool isLast = false)
        {
            using (SafetyPoint safetyPoint = SafetyPoint.Create())
            {
                if (!isLast)
                {
                    FunctionDefinition funcDefinition = new FunctionDefinition(project, Function.Enums.Category.Terminal, 6, 1);
                    Terminal terminal = new Terminal();
                    terminal.Create(project, funcDefinition);
                    terminal.Name = $"+{jumperConnection.StartLocation}-{jumperConnection.StartLiteralDT}:{jumperConnection.StartDTCounter}";
                    //terminal.Properties[20038] = jumperConnection.StartPinDesignation;
                    terminal.Properties[20030] = jumperConnection.StartLiteralDT + jumperConnection.StartDTCounter + ":" + jumperConnection.StartPinDesignation; //pin
                    safetyPoint.Commit();
                    return terminal;

                }
                else
                {
                    FunctionDefinition funcDefinition1 = new FunctionDefinition(project, Function.Enums.Category.Terminal, 6, 1);
                    Terminal terminal1 = new Terminal();
                    terminal1.Create(project, funcDefinition1);
                    //terminal1.Name = $"+{jumperConnection.EndLocation}-{jumperConnection.EndLiteralDT}";
                    terminal1.Name = $"+{jumperConnection.EndLocation}-{jumperConnection.EndLiteralDT}:{jumperConnection.EndDTCounter}";
                    //terminal1.Properties[20038] = jumperConnection.EndPinDesignation;
                    //terminal1.Properties[20030] = jumperConnection.EndDTCounter; //pin
                    //terminal1.Properties[20030] = jumperConnection.EndPinDesignation; //pin
                    terminal1.Properties[20030] = jumperConnection.EndLiteralDT +  jumperConnection.EndDTCounter + ":" + jumperConnection.EndPinDesignation; //pin
                    safetyPoint.Commit();
                    return terminal1;
                }

            }
        }

        private Connection[] FindInsertableJumperConnections()
        {
            DMObjectsFinder finder = new DMObjectsFinder(project);
            var connections = finder.GetConnectionsWithCF(new ConnectionFilter());
            return connections;
        }

        private IEnumerable<JumperConnection> GetSymbolsData(Connection[] connections)
        {
            foreach (var connection in connections)
            {

                var connectionResult = new JumperConnection()
                {
                    StartPinDesignation = connection.StartPin.Name,
                    EndPinDesignation = connection.EndPin.Name,
                    StartLocation = ((Function)connection.StartSymbolReference).Properties[1220],
                    EndLocation = ((Function)connection.EndSymbolReference).Properties[1220],
                    StartLiteralDT = ((Function)connection.StartSymbolReference).Properties[20013],
                    EndLiteralDT = ((Function)connection.EndSymbolReference).Properties[20013],
                    StartDTCounter = ((Function)connection.StartSymbolReference).Properties[20014],
                    EndDTCounter = ((Function)connection.EndSymbolReference).Properties[20014],
                    StartFullDeviceName = ((Function)connection.StartSymbolReference).Properties[20006],
                    EndFullDeviceName = ((Function)connection.EndSymbolReference).Properties[20006],

                };
                yield return connectionResult;
            }
        }

        private IEnumerable<IEnumerable<JumperConnection>> SortDeviceJumpers(Connection[] connections)
        {
            var result = GetSymbolsData(connections).ToList();

            //var sortedList = result.OrderBy(x => x?.StartLiteralDT ?? x.EndLiteralDT).ThenBy(z => z?.StartDTCounter ?? z.EndDTCounter);
            var sortedList = result.OrderBy(x => x?.StartLiteralDT ?? x.EndLiteralDT).ThenBy(z => z?.StartDTCounter ?? z.EndDTCounter).ThenBy(y => y?.StartPinDesignation ?? y.EndPinDesignation);
            var items = LinqExtension.GroupBy(sortedList);
            return items;
        }
        public void InsertJumperInTerminals(IEnumerable<Terminal> terminals)
        {
            if (!terminals.Any()) return;
            using (SafetyPoint safetyPoint = SafetyPoint.Create())
            {
                terminals.First().Properties.FUNC_TERMINAL_JUMPER_INTERN[1] = GetJumperConfig(terminals.Count());
                safetyPoint.Commit();
            }
        }

        private string GetJumperConfig(int count)
        {
            const string jumperCode = "1/0;";
            int capacity = jumperCode.Length * count;
            StringBuilder builder = new StringBuilder(capacity);
            for (int i = 0; i < count - 1; i++)
            {
                builder.Append(jumperCode);
            }
            var cutEndSymbol = builder.Remove(builder.Length - 1, 1);
            return cutEndSymbol.ToString();
        }

        public void FindAndCreateTerminals()
        {
            var connections = FindInsertableJumperConnections();
            var sortedList = SortDeviceJumpers(connections);
            var terminals = GetTermnals(sortedList).ToList();
            foreach (var terminal in terminals)
            {
                InsertJumperInTerminals(terminal);
            }
            TerminalsRepository.GetInstance().Save(terminals.SelectMany(z => z).ToList());
        }

        private IEnumerable<IEnumerable<Terminal>> GetTermnals(IEnumerable<IEnumerable<JumperConnection>> sortedList)
        {
            foreach (var terminals in sortedList)
            {
                var terminalList = new List<Terminal>();
                foreach (var terminal in terminals)
                {
                    var transientTerminal = CreateTransientTerminals(terminal);
                    terminalList.Add(transientTerminal);
                }
                if (terminals.Count() > 0)
                { 
                    var transientTerminal2 = CreateTransientTerminals(terminals.Last(), true);
                    terminalList.Add(transientTerminal2);
                }
                yield return terminalList;

            }
        }
    }

    internal class LinqExtension
    {
        public static IEnumerable<IEnumerable<JumperConnection>> GroupBy(IEnumerable<JumperConnection> connections)
        {
            var groups = new List<List<JumperConnection>>();

            var enumerator = connections.GetEnumerator();
            var list = new List<JumperConnection>();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                if (!list.Any())
                {
                    list.Add(current);
                    continue;
                }

                if (list.Contains(current, new Comparator()))
                {
                    list.Add(current);
                    continue;
                }

                else
                {
                    groups.Add(list);
                    list = new();
                    list.Add(current);
                    continue;
                }

            }

            groups.Add(list);
            return groups;
        }

    }
}