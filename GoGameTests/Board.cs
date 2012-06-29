namespace GoGameTests
{
    class Rules
    {
        public const int EDGE = 2;

        public Board Board { get; set; }
        public bool IsFullySurroundedBy(int x, int y, StoneColor surroundingStoneColor)
        {
            bool surroundedOnLeft = (x < EDGE || Board.GetStoneColor(x - 1, y) == surroundingStoneColor);
            bool surroundedOnRight = (x > Board.BOARDSIZE - EDGE || Board.GetStoneColor(x + 1, y) == surroundingStoneColor);
            bool surroundedOnBottom = (y > Board.BOARDSIZE - EDGE || Board.GetStoneColor(x, y + 1) == surroundingStoneColor);
            bool surroundedOnTop = (y < EDGE || Board.GetStoneColor(x, y - 1) == surroundingStoneColor);


            bool fullySurrounded = surroundedOnLeft && surroundedOnRight && surroundedOnTop && surroundedOnBottom;
            return fullySurrounded;
        }
        
       
        public void CheckStonesAroundPositionAndRemoveIfNeeded(int x, int y)
        {
            Board.RemoveStoneIfSurrounded(x, y - 1);
            Board.RemoveStoneIfSurrounded(x, y + 1);
            Board.RemoveStoneIfSurrounded(x - 1, y);
            Board.RemoveStoneIfSurrounded(x + 1, y);
        }

        public Rules(Board board)
        {
            Board = board;
        }

        public StoneColor GetWinner()
        {
            double whitePoints = Board.GetWhitePoints();
            double blackPoints = Board.GetBlackPoints();

            if (whitePoints == blackPoints)
                return StoneColor.Empty;

            if (whitePoints > blackPoints)
                return StoneColor.White;

            return StoneColor.Black;
        }


        public void NotifyStoneAdded(StoneColor stoneColor, int x, int y)
        {
        }
    }

    public class Board
    {
        private StoneColor[,] stoneColorMatrix = new StoneColor[BOARDSIZE, BOARDSIZE];

        public Board()
        {
            rules = new Rules(this);

        }

        public const int BOARDSIZE = 19;
        private const double KOMI = 0.5;

        public PositionStatus GetPositionStatus(int x, int y)
        {
            return stoneColorMatrix[x, y] == StoneColor.Empty ? PositionStatus.EmptyPosition : PositionStatus.FilledPosition;
        }

        public void AddStone(StoneColor stoneColor, int x, int y)
        {
            rules.NotifyStoneAdded(stoneColor, x, y);

            stoneColorMatrix[x, y] = stoneColor;

            rules.CheckStonesAroundPositionAndRemoveIfNeeded(x, y);
        }

        

        public void RemoveStoneIfSurrounded(int x, int y)
        {
            var fullySurrounded = rules.IsFullySurroundedBy(x, y, StoneColor.White);
            if ( fullySurrounded)
            {
                stoneColorMatrix[x, y] = StoneColor.Empty;
                blackStonesRemoved++;
            }
        }

        public StoneColor GetStoneColor(int x, int y)
        {
            return stoneColorMatrix[x, y];
        }

        private Rules rules;
        private int blackStonesRemoved = 0;

        public StoneColor GetWinner()
        {
            return rules.GetWinner();
        }

        public double GetWhitePoints()
        {
            var whiteTerritories = GetTerritoriesForStoneColor(StoneColor.White);
            return whiteTerritories + KOMI;
        }

        private double GetTerritoriesForStoneColor(StoneColor color)
        {
            double whiteTerritories = 0;
            for (int i = 1; i < Board.BOARDSIZE; i++)
            {
                for (int j = 1; j < Board.BOARDSIZE; j++)
                {
                    if (rules.IsFullySurroundedBy(i, j, color))
                    {
                        whiteTerritories++;
                    }
                }
            }
            return whiteTerritories;
        }

        public double GetBlackPoints()
        {
            var blackTerritories = GetTerritoriesForStoneColor(StoneColor.Black);
            return blackTerritories - blackStonesRemoved;
        }
    }
}