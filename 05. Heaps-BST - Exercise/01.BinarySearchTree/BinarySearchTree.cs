namespace _02.BinarySearchTree
{
    using System;
    using System.Collections.Generic;

    public class BinarySearchTree<T> : IBinarySearchTree<T> where T : IComparable
    {
        private class Node
        {
            public Node(T value)
                => Value = value;

            public T Value { get; }
            public Node Left { get; set; }
            public Node Right { get; set; }
        }

        private Node root;

        private int count;

        private BinarySearchTree(Node node)
            => PreOrderCopy(node);

        public BinarySearchTree()
        { }

        public void Insert(T element)
        {
            root = Insert(element, root);

            count++;
        }

        public bool Contains(T element)
        {
            Node current = FindElement(element);

            return current != null;
        }

        public void EachInOrder(Action<T> action)
            => EachInOrder(root, action);

        public IBinarySearchTree<T> Search(T element)
        {
            Node current = FindElement(element);

            return new BinarySearchTree<T>(current);
        }

        public void Delete(T element)
        {
            if (root == null)
                throw new InvalidOperationException();

            root = DeleteNode(root, element);

            count--;
        }

        private Node DeleteNode(Node parent, T nodeValue)
        {
            int compareResult = nodeValue.CompareTo(parent.Value);

            if (compareResult < 0)
                parent.Left = DeleteNode(parent.Left, nodeValue);
            else if (compareResult > 0)
                parent.Right = DeleteNode(parent.Right, nodeValue);
            else
            {
                if (parent.Right == null)
                    return parent.Left;
                else if (parent.Left == null)
                    return parent.Right;

                Node oldParent = parent;
                parent = FindLeftmost(oldParent.Right);
                parent.Right = DeleteLeftmost(oldParent.Right);
                parent.Left = oldParent.Left;
            }

            return parent;
        }

        private Node FindLeftmost(Node node)
        {
            if (node.Left == null)
                return node;

            return FindLeftmost(node.Left);
        }

        public void DeleteMax()
        {
            if (root == null)
                throw new InvalidOperationException();

            count--;

            root = DeleteRightmost(root);
        }

        private Node DeleteRightmost(Node node)
        {
            if (node.Right == null)
                return node.Left;

            node.Right = DeleteRightmost(node.Right);

            return node;
        }

        public void DeleteMin()
        {
            if (root == null)
                throw new InvalidOperationException();

            count--;

            root = DeleteLeftmost(root);
        }

        private Node DeleteLeftmost(Node node)
        {
            if (node.Left == null)
                return node.Right;

            node.Left = DeleteLeftmost(node.Left);

            return node;
        }

        public int Count() => count;

        public int Rank(T element)
            => CountSmaller(root, element);

        private int CountSmaller(Node node, T element)
        {
            int count = 0;

            if (node != null)
            {
                count += CountSmaller(node.Left, element);

                if (element.CompareTo(node.Value) > 0)
                {
                    count++;
                    count += CountSmaller(node.Right, element);
                }
            }

            return count;
        }

        public T Select(int rank)
        {
            Node node = FindFirstWithRankPreOrder(rank, root);

            if (node == null)
                throw new InvalidOperationException();

            return node.Value;
        }

        private Node FindFirstWithRankPreOrder(int rank, Node node)
        {
            if (node == null)
                return null;

            int nodeRank = Rank(node.Value);
            if (nodeRank == rank)
                return node;

            Node childNode;

            if (nodeRank > rank)
                childNode = FindFirstWithRankPreOrder(rank, node.Left);
            else
                childNode = FindFirstWithRankPreOrder(rank, node.Right);

            return childNode;
        }

        public T Ceiling(T element)
        {
            if (root == null)
                throw new InvalidOperationException();

            return FindNearestLarger(root, element);
        }

        private T FindNearestLarger(Node node, T element)
        {
            int compare = element.CompareTo(node.Value);

            if (compare >= 0)
            {
                if (node.Right == null)
                    throw new InvalidOperationException();

                if (element.CompareTo(node.Right.Value) < 0)
                    return node.Right.Value;

                return FindNearestLarger(node.Right, element);
            }
            else
            {
                if (node.Left == null || element.CompareTo(node.Left.Value) > 0)
                    return node.Value;

                return FindNearestLarger(node.Left, element);
            }
        }

        public T Floor(T element)
        {
            Node node = FindElement(element);

            if (node == null)
                throw new InvalidOperationException();

            return FindMaxValue(node.Left);
        }

        private Node FindElement(T element)
        {
            Node current = root;

            while (current != null)
            {
                if (current.Value.CompareTo(element) > 0)
                    current = current.Left;
                else if (current.Value.CompareTo(element) < 0)
                    current = current.Right;
                else
                    break;
            }

            return current;
        }

        private T FindMaxValue(Node node)
        {
            if (node.Right == null)
                return node.Value;

            return FindMaxValue(node.Right);
        }

        public IEnumerable<T> Range(T startRange, T endRange)
        {
            var nodes = new List<T>();
            FindNodesInRange(root, startRange, endRange, nodes);

            return nodes;
        }

        private void FindNodesInRange(Node node, T startRange, T endRange, List<T> nodes)
        {
            if (node == null)
                return;

            bool isNodeInStartRange = startRange.CompareTo(node.Value) <= 0;
            bool isNodeInEndRange = endRange.CompareTo(node.Value) >= 0;

            if (isNodeInStartRange)
                FindNodesInRange(node.Left, startRange, endRange, nodes);

            if (isNodeInStartRange && isNodeInEndRange)
                nodes.Add(node.Value);

            if (isNodeInEndRange)
                FindNodesInRange(node.Right, startRange, endRange, nodes);
        }

        private void PreOrderCopy(Node node)
        {
            if (node == null)
                return;

            Insert(node.Value);
            PreOrderCopy(node.Left);
            PreOrderCopy(node.Right);
        }

        private Node Insert(T element, Node node)
        {
            if (node == null)
                node = new Node(element);
            else if (element.CompareTo(node.Value) < 0)
                node.Left = Insert(element, node.Left);
            else if (element.CompareTo(node.Value) > 0)
                node.Right = Insert(element, node.Right);

            return node;
        }

        private void EachInOrder(Node node, Action<T> action)
        {
            if (node == null)
                return;

            EachInOrder(node.Left, action);
            action(node.Value);
            EachInOrder(node.Right, action);
        }
    }
}
