using System;
using TurtleChallenge.Interfaces;

namespace TurtleChallenge
{
    public class Turtle : ITurtle
    {
        public Position CurrentPosition { get; private set; }

        public CardinalDirections CurrentDirection { get; private set; }

        public Turtle(ITurtleSettings settings)
        {
            if (settings is null)
                throw new ArgumentNullException(nameof(settings));

            CurrentDirection = settings.StartDirection;
            CurrentPosition = settings.StartPosition;
        }

        /// <summary>
        /// Moves the hero one position towards the <see cref="CardinalDirections"/> it is currently facing.
        /// </summary>
        public void Move()
        {
            switch (CurrentDirection)
            {
                case CardinalDirections.North:
                    CurrentPosition.Y -= 1;
                    break;
                case CardinalDirections.East:
                    CurrentPosition.X += 1;
                    break;
                case CardinalDirections.South:
                    CurrentPosition.Y += 1;
                    break;
                case CardinalDirections.West:
                    CurrentPosition.X -= 1;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Rotates the turtle by 90 degrees (clock-wise).
        /// </summary>
        public void Rotate()
        {
            if ((int)CurrentDirection % 4 == 0)
            {
                //if we're facing the 'last' direction, reset it to the 'first' direction
                CurrentDirection = (CardinalDirections)1;
            }
            else
            {
                // rotate to the 'next' direction
                CurrentDirection = (CardinalDirections)((int)CurrentDirection + 1);
            }
        }
    }
}
