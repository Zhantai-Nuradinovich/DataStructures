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

        //Insert at the front
        public void Add(T e)
        {
            var node = new Node<T>(e);
            var lastNode = Head;
            node.Next = null;
            _length++;

            if (Head == null)
            {
                node.Prev = null;
                Head = node;
                return;
            }

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
            {
                nodeAtIndexPrev.Next = node;
                node.Prev = nodeAtIndexPrev;
            }
            else
                Head = node;

            nodeAtIndex.Prev = node;
            node.Next = nodeAtIndex;

            _length++;
        }

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

            if (node == null)
                return;

            if (node.Prev == null)
            {
                Head = node.Next;
                Head.Prev = null;
            }
            else
                node.Prev.Next = node.Next;

            if (node.Next != null)
                node.Next.Prev = node.Prev;

            _length--;
        }

        public T RemoveAt(int index)
        {
            if (index >= _length || index < 0)
                throw new IndexOutOfRangeException();

            var nodeAtIndex = Head;
            for (int i = 0; i < index; i++)
                nodeAtIndex = nodeAtIndex.Next;

            if (nodeAtIndex.Prev != null)
                nodeAtIndex.Prev.Next = nodeAtIndex.Next;
            else
                Head = nodeAtIndex.Next;

            if (nodeAtIndex.Next != null)
                nodeAtIndex.Next.Prev = nodeAtIndex.Prev;

            _length--;
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
            _startingNode = new Node<T>(head.Data);
            _startingNode.Next = head;
            _head = _startingNode;
        }

        public Node<T> CurrentNode => _head;
        public T Current => _head.Data;

        object IEnumerator.Current => Current;

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            var canMoveNext = _head.Next != null;
            _head = _head.Next;

            return canMoveNext;
        }

        public void Reset()
        {
            _head = _startingNode;
        }
    }
    public class Node<T>
    {
        public T Data { get; set; }
        public Node<T> Prev { get; set; }
        public Node<T> Next { get; set; }
        public Node(T d) 
        { 
            Data = d; 
        }
    }
}
