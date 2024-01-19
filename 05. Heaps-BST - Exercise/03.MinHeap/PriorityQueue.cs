using System;
using System.Collections.Generic;

namespace _03.MinHeap
{
    public class PriorityQueue<T> : MinHeap<T> where T : IComparable<T>
    {
        private Dictionary<T, int> indexesByKeys;

        public PriorityQueue()
        {
            elements = new List<T>();
            indexesByKeys = new Dictionary<T, int>();
        }

        public void Enqueue(T element)
        {
            elements.Add(element);
            indexesByKeys.Add(element, Count - 1);
            HeapifyUp(Count - 1);
        }

        private void HeapifyUp(int index)
        {
            int parentIndex = (index - 1) / 2;

            if (IsValidIndex(parentIndex) && IsGreater(parentIndex, index))
            {
                Swap(index, parentIndex);
                HeapifyUp(parentIndex);
            }
        }

        private void Swap(int index, int swapWithIndex)
        {
            T element = elements[index];
            elements[index] = elements[swapWithIndex];
            elements[swapWithIndex] = element;

            indexesByKeys[element] = swapWithIndex;
            indexesByKeys[elements[index]] = index;
        }

        public T Dequeue()
        {
            ValidateIsNotEmpty();
            T element = elements[0];
            elements[0] = elements[Count - 1];
            elements.RemoveAt(Count - 1);
            HeapifyDown(0);

            return element;
        }

        private void HeapifyDown(int index)
        {
            int left = 2 * index + 1;
            int right = 2 * index + 2;
            int smallerIndex = GetSmallerIndex(left, right);

            if (IsValidIndex(smallerIndex) && IsGreater(index, smallerIndex))
            {
                Swap(index, smallerIndex);
                HeapifyDown(index);
            }
        }

        public void DecreaseKey(T key)
            => HeapifyUp(indexesByKeys[key]);
    }
}
