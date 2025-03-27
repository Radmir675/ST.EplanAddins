using Eplan.EplApi.DataModel.EObjects;
using System.Collections.Generic;
using System.Linq;

namespace ST.EplAddin.JumpersReport
{
    internal class TerminalsRepository
    {
        private static TerminalsRepository instance;
        private Queue<List<Terminal>> queue = new();
        private List<List<Terminal>> allTerminals = new();
        private TerminalsRepository() { }

        public static TerminalsRepository GetInstance()
        {
            if (instance == null)
            {
                instance ??= new TerminalsRepository();
            }

            return instance;
        }

        public List<Terminal> GetData()
        {
            return queue.Dequeue();
        }
        public void Save(List<Terminal> list)
        {
            queue.Enqueue(list);
            allTerminals.Add(list);
        }
        public List<Terminal> GetAllSavedTerminals()
        {
            var result = allTerminals.SelectMany(x => x).ToList();
            return result;
        }
        public static void Reset()
        {
            instance = null;
        }

    }
}
