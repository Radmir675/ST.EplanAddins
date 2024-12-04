using System.Collections.Generic;

namespace ST.EplAddin.ComparisonOfProjectProperties.Models
{
    internal class ChangesRecord
    {
        private static List<int> changesRecord;

        public List<int> GetChangesList()
        {
            return changesRecord ?? new List<int>();
        }

        public ChangesRecord()
        {
            changesRecord ??= new List<int>();

        }
        public void RemoveAll()
        {
            changesRecord.Clear();
        }
    }
}
