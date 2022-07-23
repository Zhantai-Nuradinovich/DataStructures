using System;
using System.Collections;
using System.Collections.Generic;
using Tasks.DoNotChange;

namespace Tasks
{
    public class DoublyLinkedList<T> : IDoublyLinkedList<T>
    {
        public Node<T> Head { get; set; }
        private int _length;
        public int Length => _length;

        public void Add(T e)
        {
            var node = new Node<T>(e);
            _length++;

            if (Head == null)
            {
                Head = node;
                return;
            }

            var lastNode = Head;
            while (lastNode.Next != null)
                lastNode = lastNode.Next;

            lastNode.Next = node;
            node.Prev = lastNode;
        }

        public void AddAt(int index, T e)
        {
            var node = new Node<T>(e);
            var nodeAtIndex = Head;
            for (int i = 0; i < index; i++)
                nodeAtIndex = nodeAtIndex.Next;

            if(nodeAtIndex == null) // index is greater than Length by 1
            {
                Add(e);
                return;
            }
            
            var nodeAtIndexPrev = nodeAtIndex.Prev;
            if (nodeAtIndexPrev != null)
                AddAfter(nodeAtIndexPrev, node);
            else
                Head = node;

            AddBefore(nodeAtIndex, node);
            _length++;
        }
        #region Aux Methods
        private void AddAfter(Node<T> existingNode, Node<T> node)
        {
            existingNode.Next = node;
            node.Prev = existingNode;
        }
        private void AddBefore(Node<T> existingNode, Node<T> node)
        {
            existingNode.Prev = node;
            node.Next = existingNode;
        }
        private void RemoveNode(Node<T> node)
        {
            if (node == null)
                return;

            var nodeNext = node.Next;
            var nodePrev = node.Prev;

            if (nodePrev == null)
            {
                Head = nodeNext;
                Head.Prev = null;
            }
            else
                nodePrev.Next = nodeNext;

            if (nodeNext != null)
                nodeNext.Prev = nodePrev;
            _length--;
        }
        #endregion

        public T ElementAt(int index)
        {
            if (index >= _length || index < 0)
                throw new IndexOutOfRangeException();

            var nodeAtIndex = Head;
            for (int i = 0; i < index; i++)
                nodeAtIndex = nodeAtIndex.Next;

            return nodeAtIndex.Data;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new DoublyLinkedListEnumerator<T>(Head);
        }

        public void Remove(T item)
        {
            Node<T> node = null;
            var enumerator = new DoublyLinkedListEnumerator<T>(Head);
            while (enumerator.MoveNext())
            {
                if (enumerator.CurrentNode.Data.Equals(item))
                {
                    node = enumerator.CurrentNode;
                    break;
                }
            }
            RemoveNode(node);
        }

        public T RemoveAt(int index)
        {
            if (index >= _length || index < 0)
                throw new IndexOutOfRangeException();

            var nodeAtIndex = Head;
            for (int i = 0; i < index; i++)
                nodeAtIndex = nodeAtIndex.Next;

            RemoveNode(nodeAtIndex);
            return nodeAtIndex.Data;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    public class DoublyLinkedListEnumerator<T> : IEnumerator<T>
    {
        private Node<T> _startingNode;//указатель на -1 элемент
        private Node<T> _head;
        public DoublyLinkedListEnumerator(Node<T> head)  
        {
            _startingNode = new Node<T>() { Next = head };
            _head = _startingNode;
        }
        public Node<T> CurrentNode => _head;
        public T Current => _head.Data;
        object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            bool canMoveNext = _head.Next != null;
            _head = _head.Next;
            return canMoveNext;
        }

        public void Reset()
        {
            _head = _startingNode;
        }
        public void Dispose() { }
    }
    public class Node<T>
    {
        public T Data { get; set; }
        public Node<T> Prev { get; set; }
        public Node<T> Next { get; set; }
        public Node() { }
        public Node(T d) : this() 
        { 
            Data = d; 
        }
    }
}
