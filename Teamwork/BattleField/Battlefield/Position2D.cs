namespace BattleField
{
    /// <summary>
    /// A position in 2D space holding coordinates for X and Y
    /// </summary>
    public class Position2D
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Position2D(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
