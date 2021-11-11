namespace TurtleChallenge
{
    public class Position
    {
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }

        public int Y { get; set; }

        public override bool Equals(object obj)
        {
            return obj != null &&
                obj is Position &&
                ((Position)obj).X == X && ((Position)obj).Y == Y;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() * 17 + Y.GetHashCode();
        }
    }
}
