namespace TurtleChallenge.Interfaces
{
    public interface ITurtleFactory
    {
        ITurtle Create(ITurtleSettings turtleSettings);
    }
}
