using System;
using System.Linq;

namespace GoGameTests
{
    [Serializable]
    public class Board
    {
        private PositionStatus[,] positionStatusMatrix = new PositionStatus[BOARDSIZE+1, BOARDSIZE+1];
        private StoneColor[,] stoneColorMatrix = new StoneColor[BOARDSIZE+1, BOARDSIZE+1];

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



        public void RemoveStone(int x, int y)
        {
            positionStatusMatrix[x, y] = PositionStatus.EmptyPosition;
        }


        public StoneColor GetStoneColor(int x, int y)
        {
            if (y < 1) return StoneColor.Empty;
            // TODO: check for x boundary
            return stoneColorMatrix[x, y];
        }

        private Rules rules;

        public StoneColor GetWinner()
        {
            return rules.GetWinner();
        }
    }


}