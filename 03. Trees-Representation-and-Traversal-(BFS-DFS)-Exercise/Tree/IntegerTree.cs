namespace Tree
{
	using System.Collections.Generic;
	using System.Linq;

	public class IntegerTree : Tree<int>, IIntegerTree
	{
		public IntegerTree(int key, params Tree<int>[] children)
			: base(key, children)
		{
		}

		public IEnumerable<IEnumerable<int>> GetPathsWithGivenSum(int sum)
		{
			var paths = new List<IEnumerable<int>>();
			FindPathsWithGivenSum(this, sum, paths);

			return paths;
		}

		private void FindPathsWithGivenSum(Tree<int> node, int sum, List<IEnumerable<int>> paths)
		{
			sum -= node.Key;

			foreach (var child in node.Children)
			{
				if (child.Key == sum && !child.Children.Any())
					paths.Add(GetPathFromTheRoot(child));
				else if (child.Key <= sum)
					FindPathsWithGivenSum(child, sum, paths);
			}
		}

		public IEnumerable<Tree<int>> GetSubtreesWithGivenSum(int sum)
		{
			var subtrees = new List<Tree<int>>();
			var queue = new Queue<Tree<int>>();

			foreach (var child in this.Children)
				queue.Enqueue(child);

			while (queue.Count > 0)
			{
				Tree<int> node = queue.Dequeue();

				foreach (var child in node.Children)
					queue.Enqueue(child);

				int treeKeysSum = GetTreeKeysSum(node);

				if (treeKeysSum == sum)
					subtrees.Add(node);
			}

			return subtrees;
		}

		private int GetTreeKeysSum(Tree<int> root)
		{
			var queue = new Queue<Tree<int>>();
			queue.Enqueue(root);
			int keysSum = 0;

			while (queue.Count > 0)
			{
				Tree<int> node = queue.Dequeue();
				keysSum += node.Key;

				foreach (var child in node.Children)
					queue.Enqueue(child);
			}

			return keysSum;
		}
	}
}
