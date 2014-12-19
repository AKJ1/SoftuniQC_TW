namespace BattleField
{
    /// <summary>
    /// 2D location within array used to denote the presence of a mine.
    /// </summary>
    public class Mine
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Mine(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
