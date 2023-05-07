namespace Problem02.DoublyLinkedList
{
	using System;
	using System.Collections;
	using System.Collections.Generic;

	public class DoublyLinkedList<T> : IAbstractLinkedList<T>
	{
		private class Node
		{
			public T Item { get; set; }

			public Node Previous { get; set; }

			public Node Next { get; set; }
		}

		private Node head;

		private Node tail;

		public int Count { get; private set; }

		public void AddFirst(T item)
		{
			var newHead = new Node { Item = item };

			if (Count == 0)
				head = tail = newHead;
			else
			{
				newHead.Previous = head;
				head.Next = newHead;
				head = newHead;
			}

			Count++;
		}

		public void AddLast(T item)
		{
			var newTail = new Node { Item = item };

			if (Count == 0)
				head = tail = newTail;
			else
			{
				newTail.Next = tail;
				tail.Previous = newTail;
				tail = newTail;
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
			EnsureNotEmpty();

			return this.tail.Item;
		}

		public T RemoveFirst()
		{
			EnsureNotEmpty();
			T result = head.Item;
			head = head.Previous;
			Count--;
			
			if (head != null)
				tail.Previous = null;

			return result;
		}

		public T RemoveLast()
		{
			EnsureNotEmpty();
			T result = tail.Item;
			tail = tail.Next;
			Count--;
			
			if (tail != null)
				tail.Previous = null;

			return result;
		}

		public IEnumerator<T> GetEnumerator()
		{
			var node = head;
			while (node != null)
			{
				yield return node.Item;
				node = node.Previous;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
			=> GetEnumerator();

		private void EnsureNotEmpty()
		{
			if (this.Count == 0)
				throw new InvalidOperationException("List is empty.");
		}
	}
}