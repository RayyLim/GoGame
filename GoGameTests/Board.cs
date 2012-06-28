namespace GoGameTests
{
    class Rules
    {
        private const int EDGE = 2;
        private bool inKomiMode = true;
        private StoneColor currentWinner = StoneColor.White;

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

        public StoneColor GetWinner()
        {
            if (inKomiMode)
            {
                return StoneColor.White;
            }

            for (int i = 1; i < Board.BOARDSIZE; i++)
            {
                for (int j = 1; j < Board.BOARDSIZE; j++)
                {
                    if (IsFullySurroundedBy(i,j,StoneColor.White))
                    {
                        return StoneColor.White;
                    }
                    
                    if (IsFullySurroundedBy(i,j,StoneColor.Black))
                    {
                        return StoneColor.Black;
                    }

                }
                
            }
            return currentWinner;
        }


        public void NotifyStoneAdded(StoneColor stoneColor, int x, int y)
        {
            inKomiMode = false;
            currentWinner = stoneColor;
        }
    }

    public class Board
    {
        private PositionStatus[,] positionStatusMatrix = new PositionStatus[BOARDSIZE,BOARDSIZE];
        private StoneColor[,] stoneColorMatrix = new StoneColor[BOARDSIZE, BOARDSIZE];

        public Board()
        {
            rules = new Rules {Board = this};

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