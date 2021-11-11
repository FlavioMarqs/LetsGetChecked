using System;
using System.Collections.Generic;

namespace TurtleChallenge.ConsoleApp
{
    [Serializable]
    public class TurtleSettings : ITurtleSettings
    {
        public Position Size { get; set; }

        public Position ExitPosition { get; set; }

        public Position StartPosition { get; set; }

        public IEnumerable<Position> Mines { get; set; }

        public CardinalDirections StartDirection { get; set; }
    }
}
