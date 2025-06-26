using Eplan.EplApi.DataModel;
using Eplan.EplApi.DataModel.EObjects;
using Eplan.EplApi.DataModel.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ST.EplAddin.JumpersReport.Providers;

internal class CreateAndSaveTerminalStrips(Project project)
{
    private readonly JumpersDataProvider jumpersData = new JumpersDataProvider(project);
    public void FindAndCreateTerminals()
    {
        var connections = jumpersData.FindInsertableJumperConnections();
        var sortedList = jumpersData.SortDeviceJumpers(connections);
        var terminals = GetTerminals(sortedList).ToList();
        terminals.ForEach(terminals => jumpersData.InsertJumperInTerminals(terminals));
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
    private void CreateAndSaveTerminalStrip(IEnumerable<Terminal> terminals)
    {
        var firstTerminal = terminals.FirstOrDefault();
        if (firstTerminal == null)
        {
            throw new NullReferenceException("Отсутствуют клеммы в клеммнике");
        }
        if (firstTerminal.TerminalStrip != null) return;

        string strSymbolLibName = "SPECIAL";
        string strSymbolName = "TDEF";
        int nVariant = 0;

        SymbolLibrary symbolLibriary = new SymbolLibrary(project, strSymbolLibName);
        Symbol symbol = new Symbol(symbolLibriary, strSymbolName);
        SymbolVariant symbolVariant = new SymbolVariant();
        symbolVariant.Initialize(symbol, nVariant);

        Function function = new Function();
        function.Create(project, symbolVariant);
        function.Name = firstTerminal.Properties.FUNC_FULLDEVICETAG;

        TerminalsRepository.GetInstance().AddTerminalStrips(firstTerminal.TerminalStrip);
    }
}