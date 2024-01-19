namespace _03.MaxHeap
{
    using System;
    using System.Collections.Generic;

    public class MaxHeap<T> : IAbstractHeap<T> where T : IComparable<T>
    {
        private List<T> elements;

        public MaxHeap() => elements = new List<T>();

        public int Size => elements.Count;

        public void Add(T element)
        {
            elements.Add(element);
            HeapifyUp(Size - 1);
        }

        public T ExtractMax()
        {
            ValidateIsNotEmpty();

            T element = elements[0];
            elements[0] = elements[Size - 1];
            elements.RemoveAt(Size - 1);
            HeapifyDown(0);

            return element;
        }

        public T Peek()
        {
            ValidateIsNotEmpty();

            return elements[0];
        }

        private void HeapifyUp(int index)
        {
            if (index == 0)
                return;

            int parentIndex = (index - 1) / 2;

            if (IsGreater(index, parentIndex))
            {
                Swap(parentIndex, index);
                HeapifyUp(parentIndex);
            }
        }

        private void ValidateIsNotEmpty()
        {
            if (Size == 0)
                throw new InvalidOperationException();
        }

        private void HeapifyDown(int parentIndex)
        {
            int biggerChildIndex = GetBiggerChildIndex(parentIndex);

            if (IsValidIndex(biggerChildIndex) && IsGreater(biggerChildIndex, parentIndex))
            {
                Swap(parentIndex, biggerChildIndex);
                HeapifyDown(biggerChildIndex);
            }
        }

        private bool IsGreater(int index, int compareToIndex)
            => elements[index].CompareTo(elements[compareToIndex]) > 0;

        private void Swap(int parentIndex, int childIndex)
        {
            T element = elements[parentIndex];
            elements[parentIndex] = elements[childIndex];
            elements[childIndex] = element;
        }

        private int GetBiggerChildIndex(int parentIndex)
        {
            int leftChildIndex = 2 * parentIndex + 1;
            int rightChildIndex = 2 * parentIndex + 2;

            if (IsValidIndex(rightChildIndex) && IsGreater(rightChildIndex, leftChildIndex))
                return rightChildIndex;
            else if (IsValidIndex(leftChildIndex))
                return leftChildIndex;
            else
                return -1;
        }

        private bool IsValidIndex(int index)
            => index >= 0 && index < Size;
    }
}
