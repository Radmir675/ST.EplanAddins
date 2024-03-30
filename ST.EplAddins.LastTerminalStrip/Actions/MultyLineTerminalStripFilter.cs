using Eplan.EplApi.DataModel;
using Eplan.EplApi.DataModel.EObjects;


namespace ST.EplAddins.LastTerminalStrip
{
    public partial class FindLastTerminalAction
    {
        public class MultyLineTerminalStripFilter : ICustomFilter
        {
            public bool IsMatching(StorableObject objectToCheck)
            {
                if (objectToCheck != null && (objectToCheck as TerminalStrip).Properties.FUNC_TYPE == 1)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
