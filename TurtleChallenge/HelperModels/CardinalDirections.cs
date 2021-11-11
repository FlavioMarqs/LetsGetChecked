namespace TurtleChallenge
{
    /// <summary>
    /// Enumeration of the four (cardinal) directions.
    /// </summary>
    /// <remarks>
    /// https://en.wikipedia.org/wiki/Cardinal_direction
    /// </remarks>
    public enum CardinalDirections
    {
        /// <summary>
        /// The north, or UP.
        /// </summary>
        /// <remarks>
        /// The integer value is required for rotation purposes (see <see cref="TurtleService.Rotate"/>).
        /// </remarks>
        North = 1,

        /// <summary>
        /// The east, or RIGHT.
        /// </summary>
        /// <remarks>
        /// The integer value is required for rotation purposes (see <see cref="TurtleService.Rotate"/>).
        /// </remarks>
        East = 2,

        /// <summary>
        /// The south, or DOWN.
        /// </summary>
        /// <remarks>
        /// The integer value is required for rotation purposes (see <see cref="TurtleService.Rotate"/>).
        /// </remarks>
        South = 3,

        /// <summary>
        /// The west, or LEFT.
        /// </summary>
        /// <remarks>
        /// The integer value is required for rotation purposes (see <see cref="TurtleService.Rotate"/>).
        /// </remarks>
        West = 4
    }
}
