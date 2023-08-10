using Eplan.EplApi.DataModel;
using Eplan.EplApi.DataModel.EObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST.EplAddins.LastTerminalStrip
{
    class TerminalFilter :FunctionsFilter
    {
        public bool IsMatching(StorableObject objectToCheck)
        {
            TerminalStrip terminalStrip = objectToCheck as TerminalStrip;
            return terminalStrip.Category == TerminalStrip.Enums.Category.Terminal;

        }
    }
}
