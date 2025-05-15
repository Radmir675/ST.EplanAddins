using Eplan.EplApi.DataModel;
using Eplan.EplApi.DataModel.EObjects;
using System.Collections.Generic;
using System.Linq;

namespace ST.EplAddin.JumpersReport.Providers;

internal class TerminalProvider(Project project)
{
    private readonly JumpersDataProvider jumpersData = new JumpersDataProvider(project);
    public void FindAndCreateTerminals()
    {
        var connections = jumpersData.FindInsertableJumperConnections();
        var sortedList = jumpersData.SortDeviceJumpers(connections);
        var terminals = GetTerminals(sortedList).ToList();
        foreach (var terminal in terminals)
        {
            jumpersData.InsertJumperInTerminals(terminal);
        }
        TerminalsRepository.GetInstance().Set(terminals.SelectMany(z => z).ToList());
    }
    private IEnumerable<IEnumerable<Terminal>> GetTerminals(IEnumerable<IEnumerable<JumperConnection>> sortedList)
    {
        foreach (var terminals in sortedList)
        {
            var terminalList = new List<Terminal>();
            foreach (var terminal in terminals)
            {
                var transientTerminal = CreateTransientTerminals(terminal);
                terminalList.Add(transientTerminal);
            }
            if (terminals.Any())
            {
                var transientTerminal2 = CreateTransientTerminals(terminals.Last(), true);
                terminalList.Add(transientTerminal2);
            }
            yield return terminalList;

        }
    }

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
                terminal.Properties[20030] = jumperConnection.StartLiteralDT + jumperConnection.StartDTCounter + ":" + jumperConnection.StartPinDesignation; //pin
                terminal.Properties[20810] = true;
                safetyPoint.Commit();
                return terminal;

            }
            else
            {
                FunctionDefinition funcDefinition1 = new FunctionDefinition(project, Function.Enums.Category.Terminal, 6, 1);
                Terminal terminal1 = new Terminal();
                terminal1.Create(project, funcDefinition1);
                terminal1.Name = $"+{jumperConnection.EndLocation}-{jumperConnection.EndLiteralDT}:{jumperConnection.EndDTCounter}";
                terminal1.Properties[20030] = jumperConnection.EndLiteralDT + jumperConnection.EndDTCounter + ":" + jumperConnection.EndPinDesignation; //pin
                terminal1.Properties[20810] = true;
                safetyPoint.Commit();
                return terminal1;
            }

        }
    }
}