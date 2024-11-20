using System.Collections.Generic;

namespace ST.EplAddin.ComparisonOfProjectProperties
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
        public void Add(PropertyKey key)
        {
            //if (!changesRecord.Contains(key))
            //{
            //    changesRecord.Add(key);
            //}
        }

        public void RemoveAll()
        {
            changesRecord.Clear();
        }
    }
}
