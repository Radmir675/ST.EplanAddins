using Eplan.EplApi.DataModel;
using Eplan.EplApi.DataModel.EObjects;
using ST.EplAddin.JumpersReport.LINQ;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ST.EplAddin.JumpersReport.Providers
{
    internal class JumpersDataProvider(Project project)
    {
        public Connection[] FindInsertableJumperConnections()
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

        public IEnumerable<IEnumerable<JumperConnection>> SortDeviceJumpers(Connection[] connections)
        {
            var result = GetSymbolsData(connections).ToList();
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
    }
}