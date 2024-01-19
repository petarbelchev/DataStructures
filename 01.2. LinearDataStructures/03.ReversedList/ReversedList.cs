namespace Problem03.ReversedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class ReversedList<T> : IAbstractList<T>
    {
        private const int DefaultCapacity = 4;

        private T[] items;

        public ReversedList()
            : this(DefaultCapacity) { }

        public ReversedList(int capacity)
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
                int reversedIndex = Count - 1 - index;
                return this.items[reversedIndex];
            }
            set
            {
                this.ValidateIndex(index);
                int reversedIndex = Count - 1 - index;
                this.items[reversedIndex] = value;
            }
        }

        public int Count { get; private set; }

        public void Add(T item)
        {
            this.StretchArrayIfNessesery();
            this.items[this.Count++] = item;
        }

        public bool Contains(T item)
            => this.IndexOf(item) != -1;

        public int IndexOf(T item)
        {
            for (int i = 1; i <= Count; i++)
            {
                if (items[Count - i].Equals(item))
                    return i - 1;
            }

            return -1;
        }

        public void Insert(int index, T item)
        {
            this.ValidateIndex(index);
            int reversedIndex = Count - index;
            this.StretchArrayIfNessesery();
            this.MoveRight(reversedIndex);
            this.items[reversedIndex] = item;
        }

        public bool Remove(T item)
        {
            for (int i = Count - 1; i >= 0; i--)
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
            int reversedIndex = Count - 1 - index;
            this.MoveLeft(reversedIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = this.Count - 1; i >= 0; i--)
                yield return this.items[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

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