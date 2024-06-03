using Eplan.EplApi.DataModel;
using System.Linq;

namespace ST.EplAddin.ConnectionNumeration
{
    class CableConnectionFilter : ICustomFilter
    {
        public bool IsMatching(StorableObject objectToCheck)
        {
            Connection c = objectToCheck as Connection;
            return (c.KindOfWire == Connection.Enums.KindOfWire.Cable &&
                c.Properties.CONNECTION_HAS_CDP.ToBool() &&
                c.Page != null &&
                c.Page.PageType == DocumentTypeManager.DocumentType.Circuit &&
                (c as Connection).ConnectionDefPoints.FirstOrDefault().SymbolVariant.SymbolName == "CDPNG");
        }
    }
}
