using Eplan.EplApi.DataModel;
using Eplan.EplApi.DataModel.EObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST.EplAddins.LastTerminalStrip
{
    class TerminalFilter : ICustomFilter
    {
        public bool IsMatching(StorableObject objectToCheck)
        {
            Terminal terminal = objectToCheck as Terminal;
            return terminal.Category == Terminal.Enums.Category.Terminal;

            


        }
    }
}
