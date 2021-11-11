using NUnit.Framework;
using System;

namespace TurtleChallenge.Tests
{
    [TestFixture]
    public class CardinalDirectionsTests
    {
        /// Do not change this constant; part of the logic behind this enum is to be limited to exactly four directions. See <see cref="ITurtle.Rotate"/>.
        private const int _expectedMaximumValues = 4;

        [TestCase]
        public void CardinalDirections_ShouldHaveExactlyFourDifferentValues()
        {
            int count = Enum.GetValues(typeof(CardinalDirections)).Length;
            Assert.That(count, Is.EqualTo(_expectedMaximumValues));
        }
    }
}
