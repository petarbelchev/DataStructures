namespace Tree
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Tree<T> : IAbstractTree<T>
    {
        private List<Tree<T>> children;

        private Tree()
            => children = new List<Tree<T>>();

        public Tree(T key, params Tree<T>[] children) : this()
        {
            Key = key;
            this.children.AddRange(children);
        }

        public T Key { get; private set; }

        public Tree<T> Parent { get; private set; }

        public IReadOnlyCollection<Tree<T>> Children
            => children.AsReadOnly();

        public void AddChild(Tree<T> child)
            => children.Add(child);

        public void AddParent(Tree<T> parent)
            => Parent = parent;

        public string AsString()
        {
            var sb = new StringBuilder();
            BuildTreeStringWithDfs(this, sb, 0);

            return sb.ToString().TrimEnd();
        }

        private void BuildTreeStringWithDfs(Tree<T> root, StringBuilder sb, int indentCount)
        {
            sb.Append(' ', indentCount);
            indentCount += 2;
            sb.AppendLine(root.Key.ToString());

            foreach (var child in root.Children)
                BuildTreeStringWithDfs(child, sb, indentCount);
        }

        public IEnumerable<T> GetInternalKeys()
        {
            var findedKeys = new List<T>();

            foreach (var child in children)
                FindInternalKeysWithDfs(child, findedKeys);

            return findedKeys;
        }

        private void FindInternalKeysWithDfs(Tree<T> node, ICollection<T> findedKeys)
        {
            if (node.Children.Any())
            {
                findedKeys.Add(node.Key);

                foreach (var child in node.Children)
                    FindInternalKeysWithDfs(child, findedKeys);
            }
        }

        public IEnumerable<T> GetLeafKeys()
        {
            var leafs = new List<T>();
            FindLeafsWithDfs(this, leafs);

            return leafs;
        }

        private void FindLeafsWithDfs(Tree<T> root, ICollection<T> findedLeafs)
        {
            if (root.Children.Any())
            {
                foreach (var child in root.children)
                    FindLeafsWithDfs(child, findedLeafs);
            }
            else
            {
                findedLeafs.Add(root.Key);
            }
        }

        public T GetDeepestKey()
        {
            var tracker = new DeepestNodeTracker();
            FindDeepestNode(this, tracker);

            return tracker.DeepestNode.Key;
        }

        public IEnumerable<T> GetLongestPath()
        {
            var tracker = new DeepestNodeTracker();
            FindDeepestNode(this, tracker);

            return GetPathFromTheRoot(tracker.DeepestNode).ToArray();
        }

        private void FindDeepestNode(Tree<T> node, DeepestNodeTracker tracker)
        {
            if (node.children.Any())
            {
                tracker.CurrentEdgesCount++;

                foreach (var subchild in node.children)
                    FindDeepestNode(subchild, tracker);
            }
            else
            {
                if (tracker.CurrentEdgesCount > tracker.BiggestEdgesCount)
                {
                    tracker.BiggestEdgesCount = tracker.CurrentEdgesCount;
                    tracker.DeepestNode = node;
                }

                tracker.CurrentEdgesCount--;
            }
        }

        private class DeepestNodeTracker
        {
            public Tree<T> DeepestNode { get; set; }

            public int CurrentEdgesCount { get; set; }

            public int BiggestEdgesCount { get; set; }
        }

        protected Stack<T> GetPathFromTheRoot(Tree<T> deepestNode)
        {
            var path = new Stack<T>();

            while (deepestNode != null)
            {
                path.Push(deepestNode.Key);
                deepestNode = deepestNode.Parent;
            }

            return path;
        }
    }
}
