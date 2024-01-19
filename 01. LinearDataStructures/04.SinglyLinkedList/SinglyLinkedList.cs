namespace Problem04.SinglyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class SinglyLinkedList<T> : IAbstractLinkedList<T>
    {
        private class Node
        {
            public T Item { get; set; }

            public Node Next { get; set; }
        }

        private Node head;

        public int Count { get; private set; }

        public void AddFirst(T item)
        {
            var newNode = new Node { Item = item };

            if (this.head == null)
                this.head = newNode;
            else
            {
                newNode.Next = this.head;
                this.head = newNode;
            }

            this.Count++;
        }

        public void AddLast(T item)
        {
            var newNode = new Node { Item = item };

            if (this.head == null)
                this.head = newNode;
            else
            {
                Node lastNode = this.FindLast();
                lastNode.Next = newNode;
            }

            this.Count++;
        }

        public T GetFirst()
        {
            this.EnsureNotEmpty();

            return this.head.Item;
        }

        public T GetLast()
        {
            this.EnsureNotEmpty();

            Node lastNode = this.FindLast();

            return lastNode.Item;
        }

        public T RemoveFirst()
        {
            this.EnsureNotEmpty();

            Node oldHead = this.head;
            this.head = oldHead.Next;
            this.Count--;

            return oldHead.Item;
        }

        public T RemoveLast()
        {
            this.EnsureNotEmpty();

            Node node = this.head;

            if (this.Count == 1)
                this.head = null;
            else
            {
                while (node.Next.Next != null)
                    node = node.Next;

                node.Next = null;
            }

            this.Count--;

            return node.Item;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var node = this.head;

            while (node != null)
            {
                yield return node.Item;
                node = node.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();

        private void EnsureNotEmpty()
        {
            if (this.head == null)
                throw new InvalidOperationException("List is empty.");
        }

        private Node FindLast()
        {
            var node = this.head;

            while (node.Next != null)
                node = node.Next;

            return node;
        }
    }
}