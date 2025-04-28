using Eplan.EplApi.Base;
using Eplan.EplApi.DataModel;

namespace ST.EplAddin.JumpersReport
{
    public class ConnectionFilter : ICustomFilter
    {
        public bool IsMatching(StorableObject objectToCheck)
        {
            var res = (objectToCheck as Connection).Properties.FUNC_COMPONENTTYPE;
            if (objectToCheck is not Connection connection) return false;
            return (connection.KindOfWire == Connection.Enums.KindOfWire.IndividualConnection &&
                    connection.Properties.CONNECTION_HAS_CDP.ToBool() &&
                    connection.Page != null &&
                    connection.Properties.FUNC_COMPONENTTYPE.ToString(ISOCode.Language.L_ru_RU) == "Вставная перемычка");
        }
    }
}
