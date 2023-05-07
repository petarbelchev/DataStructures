namespace Problem01.List
{
	using System;
	using System.Collections;
	using System.Collections.Generic;

	public class MyList<T> : IAbstractList<T>
	{
		private const int DEFAULT_CAPACITY = 4;
		private T[] items;

		public MyList()
			: this(DEFAULT_CAPACITY)
		{ }

		public MyList(int capacity)
		{
			if (capacity < 0)
				throw new ArgumentOutOfRangeException(nameof(capacity));

			this.items = new T[capacity];
		}

		public T this[int index]
		{
			get
			{
				this.ValidateIndex(index);
				return this.items[index];
			}
			set
			{
				this.ValidateIndex(index);
				this.items[index] = value;
			}
		}

		public int Count { get; private set; }

		public void Add(T item)
		{
			this.StretchArrayIfNessesery();
			this.items[this.Count++] = item;
		}

		public bool Contains(T item)
		{
			int result = this.IndexOf(item);

			if (result == -1)
				return false;

			return true;
		}

		public int IndexOf(T item)
		{
			for (int i = 0; i < this.Count; i++)
			{
				if (this.items[i].Equals(item))
					return i;
			}

			return -1;
		}

		public void Insert(int index, T item)
		{
			this.ValidateIndex(index);
			this.StretchArrayIfNessesery();
			this.MoveRight(index);
			this.items[index] = item;
		}

		public bool Remove(T item)
		{
			for (int i = 0; i < this.Count; i++)
			{
				if (this.items[i].Equals(item))
				{
					this.MoveLeft(i);
					return true;
				}
			}

			return false;
		}

		public void RemoveAt(int index)
		{
			this.ValidateIndex(index);
			this.MoveLeft(index);
		}

		public IEnumerator<T> GetEnumerator()
		{
			for (int i = 0; i < this.Count; i++)
			{
				yield return this.items[i];
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		private void StretchArrayIfNessesery()
		{
			if (this.items.Length == this.Count)
			{
				var newArray = new T[this.Count * 2];

				for (int i = 0; i < this.Count; i++)
					newArray[i] = this.items[i];

				this.items = newArray;
			}
		}

		private void ValidateIndex(int index)
		{
			if (index < 0 || index >= this.Count)
				throw new IndexOutOfRangeException($"Index \"{index}\" is out of range.");
		}

		private void MoveLeft(int index)
		{
			for (int i = index; i < this.Count - 1; i++)
				this.items[i] = this.items[i + 1];

			this.Count--;
		}

		private void MoveRight(int index)
		{
			for (int i = this.Count; i > index; i--)
				this.items[i] = this.items[i - 1];

			this.Count++;
		}
	}
}