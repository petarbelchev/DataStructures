namespace Problem02.Stack
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class MyStack<T> : IAbstractStack<T>
    {
        private class Node
        {
            public Node(T element)
            {
                this.Element = element;
            }

            public Node(T element, Node next)
               : this(element)
            {
                this.Next = next;
            }

            public T Element { get; set; }

            public Node Next { get; set; }
        }

        private Node top;

        public int Count { get; private set; }

        public void Push(T item)
        {
            if (this.top == null)
            {
                this.top = new Node(item);
            }
            else
            {
                var newTop = new Node(item, this.top);
                this.top = newTop;
            }

            this.Count++;
        }

        public T Pop()
        {
            this.EnsureNotEmpty();
            var oldTop = this.top.Element;
            this.top = this.top.Next;
            this.Count--;

            return oldTop;
        }

        public T Peek()
        {
            this.EnsureNotEmpty();
            return this.top.Element;
        }

        public bool Contains(T item)
        {
            var node = this.top;

            while (node != null)
            {
                if (node.Element.Equals(item))
                    return true;

                node = node.Next;
            }

            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var node = this.top;

            while (node != null)
            {
                yield return node.Element;
                node = node.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();

        private void EnsureNotEmpty()
        {
            if (this.top == null)
                throw new InvalidOperationException("Stack is empty.");
        }
    }
}