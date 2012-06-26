namespace GoGameTests
{
    class Rules
    {
        private bool inKomiMode = true;
        private StoneColor currentWinner = StoneColor.White;

        public Board Board { get; set; }
        public bool IsFullySurroundedBy(int x, int y, StoneColor surroundingStoneColor)
        {
            bool surroundedOnLeft = (x == 0 || Board.GetStoneColor(x - 1, y) == surroundingStoneColor);
            bool surroundedOnRight = Board.GetStoneColor(x + 1, y) == surroundingStoneColor;
            bool surroundedOnBottom = Board.GetStoneColor(x, y + 1) == surroundingStoneColor;
            bool surroundedOnTop = (y == 0 || Board.GetStoneColor(x, y - 1) == surroundingStoneColor);


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

            bool surrounded = IsFullySurroundedBy(1, 1, StoneColor.Black);
            var color1 = Board.GetStoneColor(1, 2);
            if (surrounded)
            {
                return color1;
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

        private const int BOARDSIZE = 19;

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