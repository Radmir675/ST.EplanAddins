using Eplan.EplApi.DataModel;
using Eplan.EplApi.DataModel.EObjects;
using System.Linq;


namespace ST.EplAddins.LastTerminalStrip
{
    public class EmptyArticleTerminalStripFilter : ICustomFilter
    {
        public bool IsMatching(StorableObject objectToCheck)
        {
            if (objectToCheck is TerminalStrip terminalStrip)
            {
                if (!terminalStrip.Articles.Any() && terminalStrip.Properties.FUNC_TYPE == 1)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
