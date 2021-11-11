using System;
using System.Collections.Generic;
using System.Linq;
using TurtleChallenge.Interfaces;

namespace TurtleChallenge
{
    /// <summary>
    /// Service that will move an <see cref="ITurtle"/> across the 'map'.
    /// </summary>
    public class TurtleService
    {
        private readonly ITurtleFactory _turtleFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="TurtleService"/> class.
        /// </summary>
        /// <param name="ITurtleFactory">The factory for <see cref="ITurtle"/>s.</param>
        /// <exception cref="ArgumentNullException">turtleFactory</exception>
        public TurtleService(ITurtleFactory turtleFactory)
        {
            _turtleFactory = turtleFactory ?? throw new ArgumentNullException(nameof(turtleFactory));
        }

        /// <summary>
        /// Does the work.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns><c>true</c> if it finds the exit; <c>false</c> otherwise.</returns>
        public bool MoveTurtleAcrossMap(ITurtleSettings settings, IEnumerable<TurtleAction> actions)
        {
            if (settings is null)
                throw new ArgumentNullException(nameof(settings));

            if (actions is null || actions.Count() == 0)
                throw new ArgumentNullException(nameof(actions));

            bool reachedExit = false;
            ITurtle turtle = _turtleFactory.Create(settings);

            foreach (var action in actions)
            {
                switch (action)
                {
                    case TurtleAction.Move:
                        MoveTurtle(turtle, settings);
                        break;
                    case TurtleAction.Rotate:
                        turtle.Rotate();
                        break;
                    default:
                        Console.WriteLine("ERROR: Unexpected TurtleAction found; skipping.");
                        continue;
                }

                // check for MINES
                if (settings.Mines.Any(x => x.Equals(turtle.CurrentPosition)))
                {
                    Console.WriteLine($"Turtle hit a mine at [{turtle.CurrentPosition.X}][{turtle.CurrentPosition.Y}]");
                }
                else
                {
                    Console.WriteLine($"The turtle has arrived at [{turtle.CurrentPosition.X}][{turtle.CurrentPosition.Y}], facing [{turtle.CurrentDirection}]!");
                }

                // check for EXIT
                if (turtle.CurrentPosition.Equals(settings.ExitPosition))
                {
                    Console.WriteLine($"Our hero has found the exit at [{turtle.CurrentPosition.X}][{turtle.CurrentPosition.Y}]!");
                    reachedExit = true;
                    break;
                }
            }

            return reachedExit;
        }

        /// <summary>
        /// Moves the turtle (if possible).
        /// </summary>
        /// <param name="turtle">The <see cref="ITurtle"/>.</param>
        /// <param name="settings">The <see cref="ITurtleSettings"/>.</param>
        private void MoveTurtle(ITurtle turtle, ITurtleSettings settings)
        {
            bool canMove = true;
            switch (turtle.CurrentDirection)
            {
                case CardinalDirections.North:
                    // prevent from going out of bounds
                    if (turtle.CurrentPosition.Y == 0)
                    {
                        canMove = false;
                        Console.WriteLine("Hitting the (upper) wall, can't go out of bounds!");
                    }
                    break;
                case CardinalDirections.East:
                    // prevent from going out of bounds
                    if (turtle.CurrentPosition.X == settings.Size.X - 1)
                    {
                        canMove = false;
                        Console.WriteLine("Hitting the (right) wall, can't go out of bounds!");
                    }
                    break;
                case CardinalDirections.South:
                    // prevent from going out of bounds
                    if (turtle.CurrentPosition.Y == settings.Size.Y - 1)
                    {
                        canMove = false;
                        Console.WriteLine("Hitting the (lower) wall, can't go out of bounds!");
                    }
                    break;
                case CardinalDirections.West:
                    // prevent from going out of bounds
                    if (turtle.CurrentPosition.X == 0)
                    {
                        canMove = false;
                        Console.WriteLine("Hitting the (left) wall, can't go out of bounds!");
                    }
                    break;
                default:
                    Console.WriteLine($"ERROR: This should never happen; an incorrect turtle.CurrentDirection [{turtle.CurrentDirection}] was found.");
                    canMove = false;
                    break;
            }

            if (canMove)
            {
                turtle.Move();
            }
        }
    }
}
