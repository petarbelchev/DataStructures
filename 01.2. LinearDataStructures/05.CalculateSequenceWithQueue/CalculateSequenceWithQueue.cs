namespace _05.CalculateSequenceWithQueue
{
	using System.Collections.Generic;

	public class CalculateSequenceWithQueue
	{
		private int NumbersToCalculate;

		public CalculateSequenceWithQueue(int numbersToCalculate)
		{
			NumbersToCalculate = numbersToCalculate;
		}

		public int[] Calculate(int n)
		{
			var queue = new Queue<int>();
			queue.Enqueue(n);
			var result = new List<int>() { n };

			while (result.Count < NumbersToCalculate)
			{
				int baseNum = queue.Dequeue();

				int num = baseNum + 1;
				result.Add(num);
				if (result.Count == NumbersToCalculate)
					break;
				queue.Enqueue(num);
				
				num = 2 * baseNum + 1;
				result.Add(num);
				if (result.Count == NumbersToCalculate)
					break;
				queue.Enqueue(num);

				num = baseNum + 2;
				result.Add(num);
				if (result.Count == NumbersToCalculate)
					break;
				queue.Enqueue(num);
			}

			return result.ToArray();
		}
	}
}
