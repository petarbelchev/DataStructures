namespace _05.TopView
{
	using System;
	using System.Collections.Generic;

	public class BinaryTree<T> : IAbstractBinaryTree<T>
		where T : IComparable<T>
	{
		public BinaryTree(T value, BinaryTree<T> left, BinaryTree<T> right)
		{
			this.Value = value;
			this.LeftChild = left;
			this.RightChild = right;
		}

		public T Value { get; set; }

		public BinaryTree<T> LeftChild { get; set; }

		public BinaryTree<T> RightChild { get; set; }

		public List<T> TopView()
		{
			if (this.Value == null)
				return null;

			var values = new List<T>() { this.Value };
			GetLeft(this.LeftChild, values);
			GetRight(this.RightChild, values);

			return values;
		}

		private void GetLeft(BinaryTree<T> tree, List<T> values)
		{
			if (tree == null)
				return;

			values.Add(tree.Value);
			GetLeft(tree.LeftChild, values);
		}

		private void GetRight(BinaryTree<T> tree, List<T> values)
		{
			if (tree == null)
				return;

			values.Add(tree.Value);
			GetLeft(tree.RightChild, values);
		}
	}
}
