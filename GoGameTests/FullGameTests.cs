using System;
using NUnit.Framework;

namespace GoGameTests
{
    [TestFixture]
    public class FullGameTests
    {
        [Test]
        public void GetPositionStatus_InitialStatus_EmptyPosition()
        {
            Board board = MakeBoard();
            
            PositionStatus result = board.GetPositionStatus(1, 1);
            
            Assert.AreEqual(PositionStatus.EmptyPosition, result);
        }

        private static Board MakeBoard()
        {
            return new Board();
        }

        [Test]
        public void AddStone_ToEmptyPosition_PositionIsFilled()
        {
            Board board = MakeBoard();

            board.AddStoneToPosition(StoneColor.Black, 1, 1);
            PositionStatus result = board.GetPositionStatus(1, 1);

            Assert.AreEqual(PositionStatus.FilledPosition, result);
        }

        [Test]
        public void AddStone_ToEmptyPosition_OtherEmptyPositionIsUnaffected()
        {
            Board board = MakeBoard();

            board.AddStoneToPosition(StoneColor.Black, 1, 1);
            PositionStatus status = board.GetPositionStatus(1, 2);

            Assert.AreEqual(PositionStatus.EmptyPosition, status);
        }

        //what happens when you're out of board position? 

        [Test]
        public void AddStone_SurroundOppositeColorStone_RemoveOppositeColorStone()
        {
            Board board = MakeBoard();
                                                              board.AddStoneToPosition(StoneColor.White, 2, 1);
            board.AddStoneToPosition(StoneColor.White, 1, 2); board.AddStoneToPosition(StoneColor.Black, 2, 2); board.AddStoneToPosition(StoneColor.White, 3, 2);
                                                              board.AddStoneToPosition(StoneColor.White, 2, 3);

            PositionStatus status = board.GetPositionStatus(2, 2);

            Assert.AreEqual(PositionStatus.EmptyPosition, status);
        }
        
        [Test]
        public void AddStone_SurroundOppositeColorStone_RemoveOppositeColorStone2()
        {
            Board board = MakeBoard();
                                                              board.AddStoneToPosition(StoneColor.White, 2, 2);
            board.AddStoneToPosition(StoneColor.White, 1, 3); board.AddStoneToPosition(StoneColor.Black, 2, 3); board.AddStoneToPosition(StoneColor.White, 3, 3);
                                                              board.AddStoneToPosition(StoneColor.White, 2, 4);

            PositionStatus status = board.GetPositionStatus(2, 3);

            Assert.AreEqual(PositionStatus.EmptyPosition, status);
        }
        
        [Test]
        public void AddStone_AboveAndSurroundOppositeColorStone_RemoveOppositeColorStone()
        {
            Board board = MakeBoard();
            board.AddStoneToPosition(StoneColor.White, 1, 3); board.AddStoneToPosition(StoneColor.Black, 2, 3); board.AddStoneToPosition(StoneColor.White, 3, 3);
                                                              board.AddStoneToPosition(StoneColor.White, 2, 4);



            board.AddStoneToPosition(StoneColor.White, 2, 2);
            PositionStatus status = board.GetPositionStatus(2, 3);

            Assert.AreEqual(PositionStatus.EmptyPosition, status);
        }

        [Test]
        public void AddStone_LeftAndSurroundOppositeColorStone_RemoveOppositeColorStone()
        {
            Board board = MakeBoard();

                                                              board.AddStoneToPosition(StoneColor.White, 2, 2);
                                                              board.AddStoneToPosition(StoneColor.Black, 2, 3); board.AddStoneToPosition(StoneColor.White, 3, 3);
                                                              board.AddStoneToPosition(StoneColor.White, 2, 4);

            board.AddStoneToPosition(StoneColor.White, 1, 3); 
            PositionStatus status = board.GetPositionStatus(2, 3);

            Assert.AreEqual(PositionStatus.EmptyPosition, status);
        }
        
        [Test]
        public void AddStone_RightAndSurroundOppositeColorStone_RemoveOppositeColorStone()
        {
            Board board = MakeBoard();

                                                              board.AddStoneToPosition(StoneColor.White, 2, 2);
            board.AddStoneToPosition(StoneColor.White, 1, 3); board.AddStoneToPosition(StoneColor.Black, 2, 3); 
                                                              board.AddStoneToPosition(StoneColor.White, 2, 4);

            board.AddStoneToPosition(StoneColor.White, 3, 3);
            PositionStatus status = board.GetPositionStatus(2, 3);

            Assert.AreEqual(PositionStatus.EmptyPosition, status);
        }
    }

    public enum StoneColor
    {
        Black,
        White
    }

    public enum PositionStatus
    {
        EmptyPosition,
        FilledPosition
    }

    public class Board
    {
        private PositionStatus[,] positionStatusMatrix = new PositionStatus[BOARDSIZE,BOARDSIZE];
        private StoneColor[,] stoneColorMatrix = new StoneColor[BOARDSIZE, BOARDSIZE];


        private const int BOARDSIZE = 19;

        public PositionStatus GetPositionStatus(int x, int y)
        {
            return positionStatusMatrix[x, y];
        }

        public void AddStoneToPosition(StoneColor stoneColor, int x, int y)
        {
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
            bool surroundedOnLeft = (x==0 || stoneColorMatrix[x - 1, y] == StoneColor.White); 
            bool surroundedOnRight = stoneColorMatrix[x+1, y] == StoneColor.White;
            bool surroundedOnBottom = stoneColorMatrix[x, y + 1] == StoneColor.White;
            bool surroundedOnTop = (y==0 || stoneColorMatrix[x, y-1] == StoneColor.White);

             
                if ( surroundedOnLeft &&
                    surroundedOnRight &&
                    surroundedOnTop &&
                    surroundedOnBottom)
                {
                    positionStatusMatrix[x, y] = PositionStatus.EmptyPosition;
                }
            

        }
    }
}
