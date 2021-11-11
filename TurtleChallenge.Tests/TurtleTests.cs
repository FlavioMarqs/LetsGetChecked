using Moq;
using NUnit.Framework;
using TurtleChallenge.Interfaces;

namespace TurtleChallenge.Tests
{
    [TestFixture]
    public class TurtleTests
    {
        [TestCase]
        public void Constructor_ShouldThrowArgumentNullException_WhenGivenNullTurtleSettings()
        {
            Assert.That(() => new Turtle(null), Throws.ArgumentNullException.And.Message.Contains("settings"));
        }

        [TestCase(0, 0, CardinalDirections.North)]
        [TestCase(1, 11, CardinalDirections.East)]
        [TestCase(2, 10, CardinalDirections.South)]
        [TestCase(3, 9, CardinalDirections.West)]
        [TestCase(4, 8, CardinalDirections.North)]
        [TestCase(5, 7, CardinalDirections.East)]
        public void Constructor_ShouldSetProperties_FromTurtleSettings(int initialX, int initialY, CardinalDirections direction)
        {
            Mock<ITurtleSettings> mockTurtleSettings = new Mock<ITurtleSettings>();
            mockTurtleSettings.Setup(x => x.StartPosition).Returns(new Position(initialX, initialY));
            mockTurtleSettings.Setup(x => x.StartDirection).Returns(direction);

            ITurtle turtle = new Turtle(mockTurtleSettings.Object);

            Assert.That(turtle, Is.Not.Null);
            Assert.That(turtle.CurrentDirection, Is.EqualTo(direction));
            Assert.That(turtle.CurrentPosition, Is.EqualTo(new Position(initialX, initialY)));
        }

        [TestCase]
        public void Constructor_ShouldNotThrow_WhenGivenValidTurtleFactory()
        {
            Mock<ITurtleSettings> mockTurtleSettings = new Mock<ITurtleSettings>();
            mockTurtleSettings.Setup(x => x.StartPosition).Returns(new Position(0, 0));
            mockTurtleSettings.Setup(x => x.StartDirection).Returns(CardinalDirections.North);

            ITurtle turtle = null;
            Assert.DoesNotThrow(() => turtle = new Turtle(mockTurtleSettings.Object));
            Assert.That(turtle, Is.Not.Null);

        }

        [TestCase(CardinalDirections.North, CardinalDirections.East)]
        [TestCase(CardinalDirections.East, CardinalDirections.South)]
        [TestCase(CardinalDirections.South, CardinalDirections.West)]
        [TestCase(CardinalDirections.West, CardinalDirections.North)]
        public void Rotate_ShouldIncrementDirection(CardinalDirections originalDirection, CardinalDirections expectedDirection)
        {
            Mock<ITurtleSettings> mockTurtleSettings = new Mock<ITurtleSettings>();
            mockTurtleSettings.Setup(x => x.StartPosition).Returns(new Position(0, 0));
            mockTurtleSettings.Setup(x => x.StartDirection).Returns(originalDirection);

            ITurtle turtle = new Turtle(mockTurtleSettings.Object);

            Assert.Multiple(() =>
            {
                Assert.That(turtle, Is.Not.Null);
                Assert.That(turtle.CurrentDirection, Is.EqualTo(originalDirection));
                Assert.DoesNotThrow(() => turtle.Rotate());
                Assert.That(turtle.CurrentDirection, Is.EqualTo(expectedDirection));
            });
        }

        [TestCase(CardinalDirections.North, 10, 10, 10, 9)]
        [TestCase(CardinalDirections.East, 10, 10, 11, 10)]
        [TestCase(CardinalDirections.South, 10, 10, 10, 11)]
        [TestCase(CardinalDirections.West, 10, 10, 9, 10)]

        public void Move_ShouldUpdateCurrentPosition_AcordingToDirection(CardinalDirections direction, int initialX, int initialY, int expectedX, int expectedY)
        {
            Mock<ITurtleSettings> mockTurtleSettings = new Mock<ITurtleSettings>();
            mockTurtleSettings.Setup(x => x.StartPosition).Returns(new Position(initialX, initialY));
            mockTurtleSettings.Setup(x => x.StartDirection).Returns(direction);

            ITurtle turtle = new Turtle(mockTurtleSettings.Object);

            Position expectedPosition = new Position(expectedX, expectedY);

            Assert.That(turtle, Is.Not.Null);
            Assert.That(turtle.CurrentDirection, Is.EqualTo(direction));
            Assert.DoesNotThrow(() => turtle.Move());
            Assert.That(turtle.CurrentPosition.Equals(expectedPosition));
        }
    }
}
