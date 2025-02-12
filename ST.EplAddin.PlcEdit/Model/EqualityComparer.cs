using System.Collections.Generic;

namespace ST.EplAddin.PlcEdit
{
    public class EqualityComparer : IEqualityComparer<NameCorrelation>
    {
        public bool Equals(NameCorrelation x, NameCorrelation y)
        {
            var result = (x.FunctionNewName == y.FunctionOldName
                          && x.FunctionOldName == y.FunctionNewName
                          && x.NewDevicePointDesignation == y.OldDevicePointDesignation
                          && x.OldDevicePointDesignation == y.NewDevicePointDesignation) ? true : false;
            return result;
        }

        public int GetHashCode(NameCorrelation obj)
        {
            return 0;
        }
    }
}
