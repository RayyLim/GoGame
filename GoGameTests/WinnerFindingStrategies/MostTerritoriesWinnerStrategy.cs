namespace GoGameTests
{
    public class MostTerritoriesWinnerStrategy : WinnerFindingStrategy
    {
        public MostTerritoriesWinnerStrategy(Board b) : base(b)
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
            int whiteTerritories = 0;
            int blackTerritories = 0;

            for (int i = 1; i < Board.BOARDSIZE; i++)
            {
                for (int j = 1; j < Board.BOARDSIZE; j++)
                {
                    if (IsFullySurroundedBy(i, j, StoneColor.White))
                    {
                        whiteTerritories++;
                    }

                    if (IsFullySurroundedBy(i, j, StoneColor.Black))
                    {
                        blackTerritories++;
                    }
                }
            }

            if(whiteTerritories == blackTerritories)
                return StoneColor.Empty;
            if (whiteTerritories > blackTerritories)
                return StoneColor.White;
            return StoneColor.Black;
        }
    }
}