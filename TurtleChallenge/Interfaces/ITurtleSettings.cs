using System.Collections.Generic;

namespace TurtleChallenge
{
    public interface ITurtleSettings
    {
        Position Size { get; }

        Position ExitPosition { get; }

        Position StartPosition { get; }

        IEnumerable<Position> Mines { get; }

        CardinalDirections StartDirection { get; }
    }
}