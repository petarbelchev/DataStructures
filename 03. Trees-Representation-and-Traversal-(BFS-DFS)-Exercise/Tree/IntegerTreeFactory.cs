namespace Tree
{
	using System.Collections.Generic;
	using System.Linq;

	public class IntegerTreeFactory
	{
		private Dictionary<int, IntegerTree> nodesByKey;

		public IntegerTreeFactory()
		{
			nodesByKey = new Dictionary<int, IntegerTree>();
		}

		public IntegerTree CreateTreeFromStrings(string[] input)
		{
			foreach (var kvp in input)
			{
				int[] parentChildKeys = kvp.Split(' ').Select(int.Parse).ToArray();
				int parentKey = parentChildKeys[0];
				int childKey = parentChildKeys[1];

				AddEdge(parentKey, childKey);
			}

			return GetRoot();
		}

		public IntegerTree CreateNodeByKey(int key)
		{
			if (!nodesByKey.ContainsKey(key))
				nodesByKey[key] = new IntegerTree(key);

			return nodesByKey[key];
		}

		public void AddEdge(int parent, int child)
		{
			IntegerTree parentNode = CreateNodeByKey(parent);
			IntegerTree childNode = CreateNodeByKey(child);
			parentNode.AddChild(childNode);
			childNode.AddParent(parentNode);
		}

		public IntegerTree GetRoot()
		{
			foreach (var kvp in nodesByKey)
			{
				if (kvp.Value.Parent == null)
					return kvp.Value;
			}

			return null;
		}
	}
}
