using System;
using Tasks.DoNotChange;

namespace Tasks
{
    public class HybridFlowProcessor<T> : IHybridFlowProcessor<T>
    {
        private DoublyLinkedList<T> _linkedList;

        public HybridFlowProcessor()
        {
            _linkedList = new DoublyLinkedList<T>();
        }
        public T Dequeue()
        {
            var head = _linkedList.Head;
            if (head == null)
                throw new InvalidOperationException();

            return head.Data;
        }

        public void Enqueue(T item)
        {
            _linkedList.Add(item);
        }

        public T Pop()
        {
            return Dequeue();
        }

        public void Push(T item)
        {
            _linkedList.AddAt(0, item);
        }
    }
}
