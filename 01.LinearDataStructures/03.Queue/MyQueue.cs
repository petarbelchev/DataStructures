namespace Problem03.Queue
{
	using System;
	using System.Collections;
	using System.Collections.Generic;

	public class MyQueue<T> : IAbstractQueue<T>
	{
		private class Node
		{
			public T Element { get; set; }

			public Node Next { get; set; }
		}

		private Node head;

		public int Count { get; private set; }

		public void Enqueue(T item)
		{
			var newNode = new Node { Element = item };

			if (this.head == null)
				this.head = newNode;
			else
			{
				var node = this.head;

				while (node.Next != null)
					node = node.Next;

				node.Next = newNode;
			}

			this.Count++;
		}

		public T Dequeue()
		{
			this.EnsureNotEmpty();

			var oldHead = this.head;

			if (this.Count == 1)
				this.head = null;
			else
				this.head = oldHead.Next;

			this.Count--;

			return oldHead.Element;
		}

		public T Peek()
		{
			this.EnsureNotEmpty();

			return this.head.Element;
		}

		public bool Contains(T item)
		{
			var node = this.head;

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
			var node = this.head;

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
			if (this.head == null)
				throw new InvalidOperationException("Queue is empty.");
		}
	}
}