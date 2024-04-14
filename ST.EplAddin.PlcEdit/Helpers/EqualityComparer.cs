using System;
using System.Collections.Generic;

namespace ST.EplAddin.PlcEdit
{
    public class EqualityComparer : IEqualityComparer<NameCorrelation>
    {
        public bool Equals(NameCorrelation x, NameCorrelation y)
        {
            return (x.FunctionNewName == y.FunctionOldName || x.FunctionOldName == y.FunctionNewName) ? true : false;
        }

        public int GetHashCode(NameCorrelation obj)
        {
            return HashCode.Combine(obj.FunctionNewName, obj.FunctionNewName);
        }
    }
}
