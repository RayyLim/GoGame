namespace GoGameTests
{
    public class WinnerByCountStrategy : WinnerFindingStrategy
    {
        public WinnerByCountStrategy(Board b) : base(b)
        {
        }

        public bool IsFullySurroundedBy(int x, int y, StoneColor surroundingStoneColor)
        {
            bool surroundedOnLeft = (x < Rules.EDGE || _b.GetStoneColor(x - 1, y) == surroundingStoneColor);
            bool surroundedOnRight = (x > Board.BOARDSIZE - Rules.EDGE || _b.GetStoneColor(x + 1, y) == surroundingStoneColor);
            bool surroundedOnBottom = (y > Board.BOARDSIZE - Rules.EDGE || _b.GetStoneColor(x, y + 1) == surroundingStoneColor);
            bool surroundedOnTop = (y < Rules.EDGE || _b.GetStoneColor(x, y - 1) == surroundingStoneColor);


            bool fullySurrounded = surroundedOnLeft && surroundedOnRight && surroundedOnTop && surroundedOnBottom;
            return fullySurrounded;
        }

        public override StoneColor GetWinner()
        {
            int whiteCells = 0;
            int blackCells = 0;
            StoneColor color = StoneColor.Empty;
            for (int i = 1; i < Board.BOARDSIZE; i++)
            {
                for (int j = 1; j < Board.BOARDSIZE; j++)
                {
                    if (IsFullySurroundedBy(i, j, StoneColor.White))
                    {
                        whiteCells++;
                    }

                    if (IsFullySurroundedBy(i, j, StoneColor.Black))
                    {
                        blackCells++;
                    }
                }
            }
            if (whiteCells>blackCells)
            {
                return StoneColor.White;
            }
            
            if (blackCells>whiteCells)
            {
                return StoneColor.Black;
            }
            return StoneColor.Empty;
            
        }
    }
}