namespace Problem01.CircularQueue
{
	using System;
	using System.Collections;
	using System.Collections.Generic;

	public class CircularQueue<T> : IAbstractQueue<T>
	{
		private const int InitialCapacity = 4;
		private T[] items;
		private int startIndex;
		private int endIndex;

		public CircularQueue(int capacity = InitialCapacity)
			=> items = new T[capacity];

		public int Count { get; private set; }

		public int Capacity => items.Length;

		public T Dequeue()
		{
			EnsureNotEmpty();
			var result = items[startIndex];
			startIndex = NextIndex(startIndex);
			Count--;

			// NOTE: May be added check for shrink

			return result;
		}

		public void Enqueue(T item)
		{
			if (Count == items.Length)
				StretchArray();

			items[endIndex] = item;
			endIndex = NextIndex(endIndex);
			Count++;
		}

		public T Peek()
		{
			EnsureNotEmpty();

			return items[startIndex];
		}

		public T[] ToArray()
		{
			var result = new T[Count];
			CopyElements(result);

			return result;
		}

		public IEnumerator<T> GetEnumerator()
		{
			for (int i = 0; i < Count; i++)
				yield return items[(startIndex + i) % items.Length];
		}

		IEnumerator IEnumerable.GetEnumerator()
			=> GetEnumerator();

		private void StretchArray()
		{
			var newArray = new T[items.Length * 2];
			CopyElements(newArray);
			items = newArray;
			startIndex = 0;
			endIndex = Count;
		}

		private void CopyElements(T[] array)
		{
			for (int i = 0; i < Count; i++)
				array[i] = items[(startIndex + i) % items.Length];
		}

		private void EnsureNotEmpty()
		{
			if (Count == 0)
				throw new InvalidOperationException("Queue is empty.");
		}

		private int NextIndex(int currentIndex)
			=> (currentIndex + 1) % items.Length;
	}
}
