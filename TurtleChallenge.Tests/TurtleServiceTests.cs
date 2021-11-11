using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using TurtleChallenge.Interfaces;

namespace TurtleChallenge.Tests
{
    [TestFixture]
    public class TurtleServiceTests
    {
        [TestCase]
        public void Constructor_ShouldThrowArgumentNullException_WhenGivenNullTurtleFactory()
        {
            Assert.That(() => new TurtleService(null), Throws.ArgumentNullException.And.Message.Contains("turtleFactory"));
        }

        [TestCase]
        public void Constructor_ShouldNotThrow_WhenGivenValidTurtleFactory()
        {
            Mock<ITurtleFactory> mockTurtleFactory = new Mock<ITurtleFactory>();
            Assert.DoesNotThrow(() => new TurtleService(mockTurtleFactory.Object));
        }

        [TestCase]
        public void MoveTurtleAcrossMap_ShouldThrowArgumentNullException_WhenGivenNullActionsList()
        {
            Mock<ITurtleFactory> mockTurtleFactory = new Mock<ITurtleFactory>();
            TurtleService turtleService = new TurtleService(mockTurtleFactory.Object);
            Mock<ITurtleSettings> mockTurtleSettings = new Mock<ITurtleSettings>();

            Assert.That(() => turtleService.MoveTurtleAcrossMap(mockTurtleSettings.Object, null),
                Throws.ArgumentNullException.And.Message.Contains("actions"));
        }

        [TestCase]
        public void MoveTurtleAcrossMap_ShouldThrowArgumentNullException_WhenGivenEmptyActionsList()
        {
            Mock<ITurtleFactory> mockTurtleFactory = new Mock<ITurtleFactory>();
            TurtleService turtleService = new TurtleService(mockTurtleFactory.Object);
            Mock<ITurtleSettings> mockTurtleSettings = new Mock<ITurtleSettings>();

            Assert.That(() => turtleService.MoveTurtleAcrossMap(mockTurtleSettings.Object, new List<TurtleAction>()),
                Throws.ArgumentNullException.And.Message.Contains("actions"));
        }

        [TestCase]
        public void MoveTurtleAcrossMap_ShouldThrowArgumentNullException_WhenGivenNullTurtleSettings()
        {
            Mock<ITurtleFactory> mockTurtleFactory = new Mock<ITurtleFactory>();
            TurtleService turtleService = new TurtleService(mockTurtleFactory.Object);

            Assert.That(() => turtleService.MoveTurtleAcrossMap(null, new List<TurtleAction>() { TurtleAction.Rotate }),
                Throws.ArgumentNullException.And.Message.Contains("settings"));
        }

        [TestCase]
        public void MoveTurtleAcrossMap_ShouldMoveTurtleAcrossMap()
        {
            IEnumerable<TurtleAction> actions = new List<TurtleAction>
            {
                TurtleAction.Rotate,
                TurtleAction.Move,
                TurtleAction.Move,
                TurtleAction.Move,
                TurtleAction.Move,
                TurtleAction.Move,
                TurtleAction.Move,
                TurtleAction.Move,
                TurtleAction.Move,
                TurtleAction.Move,
                TurtleAction.Move,
                TurtleAction.Move
            };
            Mock<ITurtleSettings> mockTurtleSettings = new Mock<ITurtleSettings>();
            mockTurtleSettings.Setup(x => x.StartPosition).Returns(new Position(0, 0));
            mockTurtleSettings.Setup(x => x.StartDirection).Returns(CardinalDirections.North);
            mockTurtleSettings.Setup(x => x.Mines).Returns(new List<Position>());
            mockTurtleSettings.Setup(x => x.ExitPosition).Returns(new Position(2, 0));
            mockTurtleSettings.Setup(x => x.Size).Returns(new Position(5, 5));

            Mock<ITurtleFactory> mockTurtleFactory = new Mock<ITurtleFactory>();
            Turtle turtle = new Turtle(mockTurtleSettings.Object);
            mockTurtleFactory.Setup(x => x.Create(It.IsAny<ITurtleSettings>())).Returns(turtle);

            TurtleService turtleService = new TurtleService(mockTurtleFactory.Object);

            Assert.DoesNotThrow(() => turtleService.MoveTurtleAcrossMap(mockTurtleSettings.Object, actions));
            Assert.That(turtle.CurrentPosition.Equals(mockTurtleSettings.Object.ExitPosition));
        }

        [TestCase(1)]
        [TestCase(11)]
        [TestCase(111)]
        public void MoveTurtleAcrossMap_Should_InvokeTurtleMoveMethod(int moveCount)
        {
            Mock<ITurtleFactory> mockTurtleFactory = new Mock<ITurtleFactory>();
            Mock<ITurtle> mockTurtle = new Mock<ITurtle>();
            mockTurtle.Setup(x => x.Move()).Verifiable();
            mockTurtle.Setup(x => x.CurrentDirection).Returns(CardinalDirections.South);
            mockTurtle.Setup(x => x.CurrentPosition).Returns(new Position(0, 0));
            mockTurtleFactory.Setup(x => x.Create(It.IsAny<ITurtleSettings>())).Returns(mockTurtle.Object);
            TurtleService turtleService = new TurtleService(mockTurtleFactory.Object);
            Mock<ITurtleSettings> mockTurtleSettings = new Mock<ITurtleSettings>();
            mockTurtleSettings.Setup(x => x.StartPosition).Returns(new Position(0, 0));
            mockTurtleSettings.Setup(x => x.StartDirection).Returns(CardinalDirections.South);
            mockTurtleSettings.Setup(x => x.Mines).Returns(new List<Position>());
            mockTurtleSettings.Setup(x => x.ExitPosition).Returns(new Position(2, 0));
            mockTurtleSettings.Setup(x => x.Size).Returns(new Position(5, 5));

            IList<TurtleAction> moves = new List<TurtleAction>();
            for (int i = 0; i < moveCount; i++)
            {
                moves.Add(TurtleAction.Move);
            }

            turtleService.MoveTurtleAcrossMap(mockTurtleSettings.Object, moves);

            mockTurtle.Verify(x => x.Move(), Times.Exactly(moveCount));
        }
    }
}
