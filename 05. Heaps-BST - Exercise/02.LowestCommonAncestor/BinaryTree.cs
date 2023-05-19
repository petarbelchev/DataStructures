namespace _02.LowestCommonAncestor
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;

	public class BinaryTree<T> : IAbstractBinaryTree<T>
		where T : IComparable<T>
	{
		public BinaryTree(
			T value,
			BinaryTree<T> leftChild,
			BinaryTree<T> rightChild)
		{
			this.Value = value;
			this.LeftChild = leftChild;
			this.RightChild = rightChild;

			if (leftChild != null)
				this.LeftChild.Parent = this;

			if (rightChild != null)
				this.RightChild.Parent = this;
		}

		public T Value { get; set; }

		public BinaryTree<T> LeftChild { get; set; }

		public BinaryTree<T> RightChild { get; set; }

		public BinaryTree<T> Parent { get; set; }

		public T FindLowestCommonAncestor(T first, T second)
		{
			T smaller = first;
			T bigger = second;

			if (first.CompareTo(second) > 0)
			{
				smaller = second;
				bigger = first;
			}

			if (!IsDescendantExist(this, smaller))
				throw new InvalidOperationException();

			if (!IsDescendantExist(this, bigger))
				throw new InvalidOperationException();

			return FindLowestCommonAncestor(this, smaller, bigger);
		}

		private bool IsDescendantExist(BinaryTree<T> tree, T value)
		{
			if (tree == null)
				return false;

			int compare = value.CompareTo(tree.Value);

			if (compare > 0)
				return IsDescendantExist(tree.RightChild, value);
			else if (compare < 0)
				return IsDescendantExist(tree.LeftChild, value);
			else
				return true;
		}

		private T FindLowestCommonAncestor(BinaryTree<T> tree, T smaller, T bigger)
		{
			int biggerCompare = bigger.CompareTo(tree.Value);

			bool isTreeBetween = smaller.CompareTo(tree.Value) < 0 && biggerCompare > 0;

			if (isTreeBetween)
				return tree.Value;

			if (biggerCompare < 0)
				return GoTo(tree.LeftChild, smaller, bigger);
			else
				return GoTo(tree.RightChild, smaller, bigger);
		}

		private T GoTo(BinaryTree<T> tree, T smaller, T bigger) 
		{
			if (AreEqual(tree.Value, smaller) || AreEqual(tree.Value, bigger))
				return tree.Value;

			return FindLowestCommonAncestor(tree, smaller, bigger);
		}

		private bool AreEqual(T first, T second)
			=> first.CompareTo(second) == 0;
	}
}
