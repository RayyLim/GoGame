namespace GoGameTests
{
    class Rules
    {
        
    }

    public class Board
    {
        private PositionStatus[,] positionStatusMatrix = new PositionStatus[BOARDSIZE,BOARDSIZE];
        private StoneColor[,] stoneColorMatrix = new StoneColor[BOARDSIZE, BOARDSIZE];
        private StoneColor currentWinner = StoneColor.White;


        private const int BOARDSIZE = 19;

        public PositionStatus GetPositionStatus(int x, int y)
        {
            return positionStatusMatrix[x, y];
        }

        public void AddStone(StoneColor stoneColor, int x, int y)
        {
            inKomiMode = false;
            currentWinner = stoneColor;
            positionStatusMatrix[x, y] = PositionStatus.FilledPosition;
            stoneColorMatrix[x, y] = stoneColor;

            CheckStonesAroundPositionAndRemoveIfNeeded(x, y);
        }

        private void CheckStonesAroundPositionAndRemoveIfNeeded(int x, int y)
        {
            RemoveSurroundedStone(x, y - 1);
            RemoveSurroundedStone(x, y + 1);
            RemoveSurroundedStone(x - 1, y);
            RemoveSurroundedStone(x + 1, y);
        }

        private void RemoveSurroundedStone(int x, int y)
        {
            var fullySurrounded = IsFullySurroundedBy(x, y, StoneColor.White);
            if ( fullySurrounded)
            {
                positionStatusMatrix[x, y] = PositionStatus.EmptyPosition;
            }
        }

        private bool IsFullySurroundedBy(int x, int y, StoneColor surroundingStoneColor)
        {
            bool surroundedOnLeft = (x == 0 || stoneColorMatrix[x - 1, y] == surroundingStoneColor);
            bool surroundedOnRight = stoneColorMatrix[x + 1, y] == surroundingStoneColor;
            bool surroundedOnBottom = stoneColorMatrix[x, y + 1] == surroundingStoneColor;
            bool surroundedOnTop = (y == 0 || stoneColorMatrix[x, y - 1] == surroundingStoneColor);


            bool fullySurrounded = surroundedOnLeft && surroundedOnRight && surroundedOnTop && surroundedOnBottom;
            return fullySurrounded;
        }

        private bool inKomiMode = true;
        public StoneColor GetWinner()
        {
            if (inKomiMode)
            {
                return StoneColor.White;
            }

            bool surrounded = IsFullySurroundedBy(1, 1, StoneColor.Black);
            var color1 = stoneColorMatrix[1, 2];
            if (surrounded)
            {
                return color1;
            }
            return currentWinner;
        }
    }
}