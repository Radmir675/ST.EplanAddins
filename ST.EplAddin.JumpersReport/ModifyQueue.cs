using System;
using System.Collections.Generic;

namespace ST.EplAddin.JumpersReport
{
    internal class ModifyQueue<T>
    {
        private readonly List<T> allTerminals = new();
        private readonly Queue<T> queue = new Queue<T>();
        public event EventHandler Changed;
        protected virtual void OnChanged()
        {
            if (Changed != null)
                Changed(this, EventArgs.Empty);
        }
        public virtual void Enqueue(T item)
        {
            queue.Enqueue(item);
            allTerminals.Add(item);
        }
        public int Count { get { return queue.Count; } }

        public virtual T Dequeue()
        {
            T item = queue.Dequeue();
            return item;
        }
        public virtual List<T> GetAll()
        {
            return allTerminals;
        }
    }
}
