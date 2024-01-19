namespace _01.BinaryTree
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class BinaryTree<T> : IAbstractBinaryTree<T>
    {
        public BinaryTree(T element, IAbstractBinaryTree<T> left = null, IAbstractBinaryTree<T> right = null)
        {
            this.Value = element;
            this.LeftChild = left;
            this.RightChild = right;
        }

        public T Value { get; set; }

        public IAbstractBinaryTree<T> LeftChild { get; set; }

        public IAbstractBinaryTree<T> RightChild { get; set; }

        public string AsIndentedPreOrder(int indent)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{new string(' ', indent)}{this.Value}");
            indent += 2;

            if (LeftChild != null)
                sb.AppendLine(this.LeftChild.AsIndentedPreOrder(indent));

            if (RightChild != null)
                sb.AppendLine(this.RightChild.AsIndentedPreOrder(indent));

            return sb.ToString().TrimEnd();
        }

        public void ForEachInOrder(Action<T> action)
        {
            if (this.LeftChild != null)
                this.LeftChild.ForEachInOrder(action);

            action(this.Value);

            if (this.RightChild != null)
                this.RightChild.ForEachInOrder(action);
        }

        public IEnumerable<IAbstractBinaryTree<T>> InOrder()
        {
            var trees = new List<IAbstractBinaryTree<T>>();

            if (this.LeftChild != null)
                trees.AddRange(this.LeftChild.InOrder());

            trees.Add(this);

            if (this.RightChild != null)
                trees.AddRange(this.RightChild.InOrder());

            return trees;
        }

        public IEnumerable<IAbstractBinaryTree<T>> PostOrder()
        {
            var trees = new List<IAbstractBinaryTree<T>>();

            if (this.LeftChild != null)
                trees.AddRange(this.LeftChild.PostOrder());

            if (this.RightChild != null)
                trees.AddRange(this.RightChild.PostOrder());

            trees.Add(this);

            return trees;
        }

        public IEnumerable<IAbstractBinaryTree<T>> PreOrder()
        {
            var trees = new List<IAbstractBinaryTree<T>>() { this };

            if (this.LeftChild != null)
                trees.AddRange(this.LeftChild.PreOrder());

            if (this.RightChild != null)
                trees.AddRange(this.RightChild.PreOrder());

            return trees;
        }
    }
}
