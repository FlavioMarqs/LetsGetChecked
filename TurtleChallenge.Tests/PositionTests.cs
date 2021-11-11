using NUnit.Framework;

namespace TurtleChallenge.Tests
{
    [TestFixture]
    public class PositionTests
    {
        [TestCase(3, 3)]
        [TestCase(1, 2)]
        [TestCase(0, 0)]
        [TestCase(int.MaxValue, int.MaxValue)]
        public void Constructor_ShouldSetProperties_FromParameters(int x, int y)
        {
            Position position = new Position(x, y);

            Assert.That(position.X, Is.EqualTo(x));
            Assert.That(position.Y, Is.EqualTo(y));
        }

    }
}
