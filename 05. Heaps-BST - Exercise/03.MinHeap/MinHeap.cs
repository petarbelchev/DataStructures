using System;
using System.Collections.Generic;

namespace _03.MinHeap
{
	public class MinHeap<T> : IAbstractHeap<T>
		where T : IComparable<T>
	{
		protected List<T> elements;

		public MinHeap() => elements = new List<T>();

		public int Count => elements.Count;

		public void Add(T element)
		{
			elements.Add(element);
			HeapifyUp(Count - 1);
		}

		private void HeapifyUp(int index)
		{
			int parentIndex = (index - 1) / 2;

			if (IsGreater(parentIndex, index))
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
		}

		public T ExtractMin()
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
			int leftIndex = 2 * index + 1;
			int rightIndex = 2 * index + 2;
			int smallerIndex = GetSmallerIndex(leftIndex, rightIndex);

			if (IsValidIndex(smallerIndex) && IsGreater(index, smallerIndex))
			{
				Swap(index, smallerIndex);
				HeapifyDown(smallerIndex);
			}
		}

		protected int GetSmallerIndex(int left, int right)
		{
			if (IsValidIndex(right) && IsGreater(left, right))
				return right;
			else if (IsValidIndex(left))
				return left;
			else
				return -1;
		}

		protected bool IsGreater(int index, int compareWithIndex)
			=> elements[index].CompareTo(elements[compareWithIndex]) > 0;

		protected bool IsValidIndex(int index)
			=> index >= 0 && index < Count;

		public T Peek()
		{
			ValidateIsNotEmpty();

			return elements[0];
		}

		protected void ValidateIsNotEmpty()
		{
			if (elements.Count == 0)
				throw new InvalidOperationException();
		}
	}
}
