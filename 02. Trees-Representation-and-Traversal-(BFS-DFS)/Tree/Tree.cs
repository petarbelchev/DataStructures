namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Tree<T> : IAbstractTree<T>
    {
        private T value;
        private Tree<T> parent;
        private List<Tree<T>> children;

        public Tree(T value)
        {
            this.value = value;
            this.children = new List<Tree<T>>();
        }

        public Tree(T value, params Tree<T>[] children)
            : this(value)
        {
            foreach (var child in children)
            {
                child.parent = this;
                this.children.Add(child);
            }
        }

        public void AddChild(T parentKey, Tree<T> child)
        {
            if (this.value.Equals(parentKey))
            {
                this.children.Add(child);
                child.parent = this;

                return;
            }

            if (!this.children.Any())
                throw new ArgumentNullException();

            Tree<T> parentNode = FindNodeByBfs(parentKey);

            if (parentNode == null)
                throw new ArgumentNullException();

            parentNode.children.Add(child);
            child.parent = parentNode;
        }

        public IEnumerable<T> OrderBfs()
        {
            List<T> result = new List<T>();
            Queue<Tree<T>> queue = new Queue<Tree<T>>();
            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                Tree<T> tree = queue.Dequeue();
                result.Add(tree.value);

                foreach (var child in tree.children)
                    queue.Enqueue(child);
            }

            return result;
        }

        public IEnumerable<T> OrderDfs()
        {
            List<T> result = new List<T>();
            Dfs(this, result);

            return result;
        }

        public void Swap(T firstKey, T secondKey)
        {
            if (this.value.Equals(firstKey) || this.value.Equals(secondKey))
                throw new ArgumentException();

            //Tree<T> firstNode = FindNodeByDfs(this, firstKey);
            Tree<T> firstNode = FindNodeByBfs(firstKey);

            if (firstNode == null)
                throw new ArgumentNullException();

            //Tree<T> secondNode = FindNodeByDfs(this, secondKey);
            Tree<T> secondNode = FindNodeByBfs(secondKey);

            if (secondNode == null)
                throw new ArgumentNullException();

            Tree<T> firstNodeParent = firstNode.parent;
            Tree<T> secondNodeParent = secondNode.parent;
            int secondNodeIndex = secondNodeParent.children.IndexOf(secondNode);
            int firstNodeIndex = firstNodeParent.children.IndexOf(firstNode);

            if (IsNodeChild(child: firstNode, parent: secondNode))
            {
                secondNodeParent.children[secondNodeIndex] = firstNode;
            }
            else if (IsNodeChild(child: secondNode, parent: firstNode))
            {
                firstNodeParent.children[firstNodeIndex] = secondNode;
            }
            else
            {
                firstNodeParent.children[firstNodeIndex] = secondNode;
                secondNode.parent = firstNodeParent;

                secondNodeParent.children[secondNodeIndex] = firstNode;
                firstNode.parent = secondNodeParent;
            }
        }

        public void RemoveNode(T nodeKey)
        {
            //RemoveByBfs(nodeKey);
            RemoveByDfs(nodeKey);
        }

        private void RemoveByBfs(T nodeKey)
        {
            if (this.value.Equals(nodeKey))
                throw new ArgumentException();

            Tree<T> node = FindNodeByBfs(nodeKey);

            if (node == null)
                throw new ArgumentNullException();

            Tree<T> parentNode = node.parent;
            parentNode.children.Remove(node);
            node.parent = null;
        }

        private void RemoveByDfs(T nodeKey)
        {
            if (this.value.Equals(nodeKey))
                throw new ArgumentException();

            Tree<T> node = FindNodeByDfs(this, nodeKey);

            if (node != null)
            {
                Tree<T> parent = node.parent;
                parent.children.Remove(node);
                node.parent = null;
            }
            else
                throw new ArgumentNullException();
        }

        private void Dfs(Tree<T> tree, ICollection<T> result)
        {
            foreach (var child in tree.children)
                Dfs(child, result);

            result.Add(tree.value);
        }

        private Tree<T> FindNodeByBfs(T parentKey)
        {
            Queue<Tree<T>> queue = new Queue<Tree<T>>();
            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                Tree<T> subtree = queue.Dequeue();

                foreach (var child in subtree.children)
                {
                    if (child.value.Equals(parentKey))
                        return child;

                    queue.Enqueue(child);
                }
            }

            return null;
        }

        private Tree<T> FindNodeByDfs(Tree<T> root, T nodeKey)
        {
            foreach (Tree<T> child in root.children)
            {
                if (child.value.Equals(nodeKey))
                {
                    return child;
                }
                else
                {
                    Tree<T> node = FindNodeByDfs(child, nodeKey);

                    if (node != null)
                        return node;
                }
            }

            return null;
        }

        private bool IsNodeChild(Tree<T> child, Tree<T> parent)
        {
            while (child.parent != null)
            {
                if (child.parent.Equals(parent))
                    return true;

                child = child.parent;
            }

            return false;
        }
    }
}
