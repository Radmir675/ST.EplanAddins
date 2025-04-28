using System.Collections.Generic;

namespace ST.EplAddin.JumpersReport
{
    internal class Comparator : IEqualityComparer<JumperConnection>
    {
        public bool Equals(JumperConnection x, JumperConnection y)
        {
            if (x.EndFullDeviceName == y.StartFullDeviceName || x.StartFullDeviceName == y.EndFullDeviceName)
            {
                if (x.EndPinDesignation == y.StartPinDesignation || x.StartPinDesignation == y.EndPinDesignation)
                {
                    return true;
                }
            }
            return false;

        }

        public int GetHashCode(JumperConnection obj)
        {
            return 0;
        }
    }
}