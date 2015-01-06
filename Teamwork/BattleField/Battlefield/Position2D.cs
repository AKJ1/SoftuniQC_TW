namespace BattleField
{
    /// <summary>
    /// 2D location within array used to denote the presence of a mine.
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
