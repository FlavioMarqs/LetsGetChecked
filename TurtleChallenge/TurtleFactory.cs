using System;
using TurtleChallenge.Interfaces;

namespace TurtleChallenge
{
    public class TurtleFactory : ITurtleFactory
    {
        /// <summary>
        /// Creates an <see cref="ITurtle"/>, with initial values from <paramref name="turtleSettings"/>.
        /// </summary>
        /// <param name="turtleSettings">The <see cref="ITurtleSettings"/> for initializing the <see cref="ITurtle"/>.</param>
        /// <returns>A turtle (<see cref="ITurtle"/>).</returns>
        /// <exception cref="ArgumentOutOfRangeException">StartPosition</exception>
        /// <exception cref="ArgumentNullException">turtleSettings</exception>
        /// <remarks>
        /// Will throw <see cref="ArgumentOutOfRangeException"/> if the <see cref="ITurtleSettings.StartPosition"/> is outside the 'map'.
        /// </remarks>
        public ITurtle Create(ITurtleSettings turtleSettings)
        {
            if (turtleSettings is null)
                throw new ArgumentNullException(nameof(turtleSettings));

            if (turtleSettings.Size.X <= turtleSettings.StartPosition.X || turtleSettings.Size.Y <= turtleSettings.StartPosition.Y || turtleSettings.StartPosition.X < 0 || turtleSettings.StartPosition.Y < 0)
                throw new ArgumentOutOfRangeException(nameof(turtleSettings.StartPosition));

            return new Turtle(turtleSettings);
        }
    }
}
