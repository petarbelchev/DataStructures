namespace _02.BinarySearchTree
{
    using System;

    public class BinarySearchTree<T> : IBinarySearchTree<T>
        where T : IComparable<T>
    {
        private class Node
        {
            public T Value { get; set; }

            public Node LeftChild { get; set; }

            public Node RightChild { get; set; }
        }

        private Node root;

        public bool Contains(T element)
            => FindNode(element) != null;

        public void EachInOrder(Action<T> action)
            => EachInOrder(root, action);

        public void Insert(T element)
            => root = Insert(root, element);

        private Node Insert(Node node, T element)
        {
            if (node == null)
                node = new Node() { Value = element };
            else
            {
                int compareResult = element.CompareTo(node.Value);

                if (compareResult < 0)
                    node.LeftChild = Insert(node.LeftChild, element);
                else if (compareResult > 0)
                    node.RightChild = Insert(node.RightChild, element);
            }

            return node;
        }

        public IBinarySearchTree<T> Search(T element)
        {
            Node root = FindNode(element);

            if (root == null)
                return null;

            var bst = new BinarySearchTree<T>();
            EachInOrder(root, bst.Insert);

            return bst;
        }

        private Node FindNode(T element)
        {
            Node node = root;

            while (node != null)
            {
                int compareResult = element.CompareTo(node.Value);

                if (compareResult < 0)
                    node = node.LeftChild;
                else if (compareResult > 0)
                    node = node.RightChild;
                else
                    break;
            }

            return node;
        }

        private void EachInOrder(Node root, Action<T> action)
        {
            if (root == null)
                return;

            EachInOrder(root.LeftChild, action);
            action(root.Value);
            EachInOrder(root.RightChild, action);
        }
    }
}
