using NUnit.Framework;

namespace _05.CalculateSequenceWithQueue.Tests
{
	[TestFixture]
	public class CalculateSequenceWithQueueTests
	{
		private CalculateSequenceWithQueue instance;
		private const int NumbersToCalculate = 50;

		[SetUp]
		public void Setup()
			=> instance = new CalculateSequenceWithQueue(NumbersToCalculate);

		[Test]
		public void Test1()
		{
			// Arrange
			int[] expected = new int[] { 2, 3, 5, 4, 4, 7, 5, 6, 11, 7, 5, 9, 6 };

			// Act
			int[] actual = instance.Calculate(2);

			// Assert
			Assert.That(actual.Length, Is.EqualTo(NumbersToCalculate));
			for (int i = 0; i < expected.Length; i++)
			{
				Assert.That(actual[i], Is.EqualTo(expected[i]));
			}
		}

		[Test]
		public void Test2()
		{
			// Arrange
			int[] expected = new int[] { -1, 0, -1, 1, 1, 1, 2 };

			// Act
			int[] actual = instance.Calculate(-1);

			// Assert
			Assert.That(actual.Length, Is.EqualTo(NumbersToCalculate));
			for (int i = 0; i < expected.Length; i++)
			{
				Assert.That(actual[i], Is.EqualTo(expected[i]));
			}
		}

		[Test]
		public void Test3()
		{
			// Arrange
			int[] expected = new int[] { 1000, 1001, 2001, 1002, 1002, 2003, 1003 };

			// Act
			int[] actual = instance.Calculate(1000);

			// Assert
			Assert.That(actual.Length, Is.EqualTo(NumbersToCalculate));
			for (int i = 0; i < expected.Length; i++)
			{
				Assert.That(actual[i], Is.EqualTo(expected[i]));
			}
		}
	}
}