using NUnit.Framework;

namespace SequenceNToM.Tests
{
    public class SequenceNToMTests
    {
        private SequenceNToM instance;

        [SetUp]
        public void Setup()
            => instance = new SequenceNToM();

        [Test]
        public void Test1()
        {
            // Arrange
            int n = 3;
            int m = 10;
            string expected = "3 -> 5 -> 10";

            // Act
            string actual = instance.FindSequence(n, m);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Test2()
        {
            // Arrange
            int n = 5;
            int m = -5;
            string expected = "(no solution)";

            // Act
            string actual = instance.FindSequence(n, m);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Test3()
        {
            // Arrange
            int n = 10;
            int m = 30;
            string expected = "10 -> 11 -> 13 -> 15 -> 30";

            // Act
            string actual = instance.FindSequence(n, m);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}