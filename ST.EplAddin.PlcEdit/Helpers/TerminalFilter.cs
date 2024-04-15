using Eplan.EplApi.DataModel;
using Eplan.EplApi.DataModel.EObjects;

namespace ST.EplAddin.PlcEdit
{
    public class TerminalFilter : ICustomFilter
    {
        private readonly Terminal initialTerminal;

        public TerminalFilter(Terminal initialTerminal)
        {
            this.initialTerminal = initialTerminal;
        }
        public bool IsMatching(StorableObject objectToCheck)
        {
            if (objectToCheck is Terminal terminal)
            {
                return terminal.Properties.FUNC_FULLDEVICETAG == initialTerminal.Properties.FUNC_FULLDEVICETAG ? true : false;
            }
            return false;
        }
    }
}
