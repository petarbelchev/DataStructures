namespace Problem04.BalancedParentheses
{
	using System.Collections.Generic;

	public class BalancedParenthesesSolve : ISolvable
	{
		public bool AreBalanced(string parentheses)
		{
			if (parentheses.Length % 2 != 0)
				return false;

			var stack = new Stack<char>();

			foreach (char p in parentheses)
			{
				char lookFor = default;

				if (p == ']') lookFor = '[';
				else if (p == ')') lookFor = '(';
				else if (p == '}') lookFor = '{';
				else stack.Push(p);

				if (lookFor == default)
					continue;

				if (stack.Pop() != lookFor)
					return false;
			}

			return true;
		}
	}
}
