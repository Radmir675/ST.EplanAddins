using Eplan.EplApi.DataModel.EObjects;
using System.Collections.Generic;
using System.Linq;

namespace ST.EplAddin.JumpersReport
{
    internal class TerminalsRepository
    {
        private static TerminalsRepository instance;
        private ModifyQueue<List<Terminal>> queue = new();
        private List<TerminalStrip> terminalStrips = new();
        private TerminalsRepository() { }

        public static TerminalsRepository GetInstance()
        {
            return instance ??= new TerminalsRepository();
        }

        public List<Terminal> Get()
        {
            return queue.Dequeue();
        }
        public void Set(List<Terminal> list)
        {
            queue.Enqueue(list);
        }
        public List<Terminal> GetAll()
        {
            var result = queue.DequeueAll().SelectMany(x => x).ToList();
            return result;
        }
        public List<Terminal> GetAllWithoutRemoving()
        {
            var result = queue.GetAllWithoutRemoving().SelectMany(x => x).ToList();
            return result;
        }
        public IEnumerable<Terminal> GetFirstTerminalsFromTerminalStrip()
        {
            Terminal terminal = null;

            while ((terminal = queue.Dequeue().FirstOrDefault()) != null)
            {
                yield return terminal;
            }

        }

        internal List<TerminalStrip> GetTerminalStrips()
        {
            return terminalStrips;
        }
        public void AddTerminalStrips(TerminalStrip terminalStrip)
        {

            terminalStrips.Add(terminalStrip);
        }
        public void RemoveTerminalStrips()
        {
            terminalStrips.Clear();
        }
    }
}
