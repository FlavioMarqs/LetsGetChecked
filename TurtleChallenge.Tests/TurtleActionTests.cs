using NUnit.Framework;
using System;

namespace TurtleChallenge.Tests
{
    [TestFixture]
    public class TurtleActionTests
    {
        /// Do not change this constant; part of the logic behind this enum is to be limited to exactly four directions. See <see cref="ITurtle"/>.
        private const int _expectedMaximumValues = 2;

        [TestCase]
        public void TurtleAction_ShouldHaveExactlyTwoDifferentValues()
        {
            int count = Enum.GetValues(typeof(TurtleAction)).Length;
            Assert.That(count, Is.EqualTo(_expectedMaximumValues));
        }
    }
}
