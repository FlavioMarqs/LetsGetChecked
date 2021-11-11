using Moq;
using NUnit.Framework;
using System;
using TurtleChallenge.Interfaces;

namespace TurtleChallenge.Tests
{
    [TestFixture]
    public class TurtleFactoryTests
    {
        private ITurtleFactory _turtleFactory;

        [SetUp]
        public void SetUp()
        {
            _turtleFactory = new TurtleFactory();
        }

        [TearDown]
        public void TearDown()
        {
            _turtleFactory = null;
        }

        [TestCase]
        public void Create_ShouldThrowArgumentNullException_WhenGivenNullTurtleSettings()
        {
            Assert.That(() => _turtleFactory.Create(null), Throws.ArgumentNullException.And.Message.Contains("turtleSettings"));
        }

        [TestCase(10, 10, 11, -2)]
        [TestCase(10, 10, 0, 10)]
        [TestCase(3, 3, 3, 0)]
        [TestCase(10, 10, -2, 3)]
        [TestCase(2, 2, 11, -2)]
        public void Create_ShouldThrowArgumentOutOfRangeException_WhenStartPositionIsOutsideMap(int width, int height, int initialX, int initialY)
        {
            Mock<ITurtleSettings> mockTurtleSettings = new Mock<ITurtleSettings>();
            mockTurtleSettings.Setup(x => x.Size).Returns(new Position(width, height));
            mockTurtleSettings.Setup(x => x.StartPosition).Returns(new Position(initialX, initialY));
            mockTurtleSettings.Setup(x => x.StartDirection).Returns(CardinalDirections.East);

            Assert.That(() => _turtleFactory.Create(mockTurtleSettings.Object),
                Throws.TypeOf<ArgumentOutOfRangeException>().And.Message.Contains(nameof(mockTurtleSettings.Object.StartPosition)));
        }


        [TestCase(5, 5, 4, 4, CardinalDirections.North)]
        [TestCase(7, 5, 6, 4, CardinalDirections.West)]
        [TestCase(10, 10, 6, 4, CardinalDirections.East)]
        [TestCase(int.MaxValue, int.MaxValue, 0, 0, CardinalDirections.South)]
        public void Create_ShouldReturnTurtle_WithCorrectInitialPositionAndDirection(int width, int height, int initialX, int initialY, CardinalDirections initialDirection)
        {
            Mock<ITurtleSettings> mockTurtleSettings = new Mock<ITurtleSettings>();
            mockTurtleSettings.Setup(x => x.Size).Returns(new Position(width, height));
            mockTurtleSettings.Setup(x => x.StartPosition).Returns(new Position(initialX, initialY));
            mockTurtleSettings.Setup(x => x.StartDirection).Returns(initialDirection);

            ITurtle turtle = _turtleFactory.Create(mockTurtleSettings.Object);
            Assert.That(turtle, Is.Not.Null);
            Assert.That(turtle.CurrentDirection == initialDirection);
            Assert.That(turtle.CurrentPosition.X, Is.EqualTo(initialX));
            Assert.That(turtle.CurrentPosition.Y, Is.EqualTo(initialY));
        }

    }
}
