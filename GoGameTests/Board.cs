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
            var result = new FirstCellWinnerStrategy(Board).GetWinner();
            if (result!= StoneColor.Empty)
            {
                return result;
            }
            return new KomiWinnerStrategy(Board).GetWinner();
        }


        public void NotifyStoneAdded(StoneColor stoneColor, int x, int y)
        {
        }
    }

    public class Board
    {
        private PositionStatus[,] positionStatusMatrix = new PositionStatus[BOARDSIZE,BOARDSIZE];
        private StoneColor[,] stoneColorMatrix = new StoneColor[BOARDSIZE, BOARDSIZE];

        public Board()
        {
            rules = new Rules(this);

        }

        public const int BOARDSIZE = 19;

        public PositionStatus GetPositionStatus(int x, int y)
        {
            return positionStatusMatrix[x, y];
        }

        public void AddStone(StoneColor stoneColor, int x, int y)
        {
            rules.NotifyStoneAdded(stoneColor, x, y);

            positionStatusMatrix[x, y] = PositionStatus.FilledPosition;
            stoneColorMatrix[x, y] = stoneColor;

            rules.CheckStonesAroundPositionAndRemoveIfNeeded(x, y);
        }

        public void RemoveStoneIfSurrounded(int x, int y)
        {
            var fullySurrounded = rules.IsFullySurroundedBy(x, y, StoneColor.White);
            if ( fullySurrounded)
            {
                positionStatusMatrix[x, y] = PositionStatus.EmptyPosition;
                
            }
        }

        public StoneColor GetStoneColor(int x, int y)
        {
            return stoneColorMatrix[x, y];
        }

        private Rules rules;

        public StoneColor GetWinner()
        {
            return rules.GetWinner();
        }
    }
}