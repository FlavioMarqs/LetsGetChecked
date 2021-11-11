namespace TurtleChallenge.Interfaces
{
    public interface ITurtle
    {
        void Move();

        void Rotate();

        Position CurrentPosition { get; }

        CardinalDirections CurrentDirection { get; }
    }
}
