using System;
using System.Collections.Generic;
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

        [Test]
        public void AddStone_StoneSurroundedButHasAllyNeighbor_StoneIsNotRemoved()
        {
            Board board = MakeBoard();

                                                              board.AddStoneToPosition(StoneColor.White, 2, 2);
            board.AddStoneToPosition(StoneColor.White, 1, 3); board.AddStoneToPosition(StoneColor.Black, 2, 3); board.AddStoneToPosition(StoneColor.Black, 3, 3);
                                                              board.AddStoneToPosition(StoneColor.White, 2, 4);

            PositionStatus status = board.GetPositionStatus(2, 3);

            Assert.AreEqual(PositionStatus.FilledPosition, status);
        }

        // [Test]
        public void AddStone_SurroundOppositeColorStoneGroupOfSize2_RemoveOppositeColorStoneGroup()
        {
            Board board = MakeBoard();

                                                              board.AddStoneToPosition(StoneColor.White, 2, 2); board.AddStoneToPosition(StoneColor.White, 3, 2);
            board.AddStoneToPosition(StoneColor.White, 1, 3); board.AddStoneToPosition(StoneColor.Black, 2, 3); board.AddStoneToPosition(StoneColor.Black, 3, 3); board.AddStoneToPosition(StoneColor.White, 4, 3);
                                                              board.AddStoneToPosition(StoneColor.White, 2, 4); board.AddStoneToPosition(StoneColor.White, 3, 4);

            PositionStatus status1 = board.GetPositionStatus(2, 3);
            PositionStatus status2 = board.GetPositionStatus(3, 3);

            Assert.AreEqual(PositionStatus.EmptyPosition, board.GetPositionStatus(2, 3));
            Assert.AreEqual(PositionStatus.EmptyPosition, board.GetPositionStatus(3, 3));
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
            // Find all stones part of current position group
            //List<Tuple<int, int>> stoneGroup = new List<Tuple<int, int>>() { new Tuple<int, int>(x, y) };

            //bool emptyOnLeft = (x == 0 || positionStatusMatrix[x - 1, y] == PositionStatus.EmptyPosition);
            //bool emptyOnRight = positionStatusMatrix[x + 1, y] == PositionStatus.EmptyPosition;
            //bool emptyOnBottom = positionStatusMatrix[x, y + 1] == PositionStatus.EmptyPosition;
            //bool emptyOnTop = (y == 0 || positionStatusMatrix[x, y - 1] == PositionStatus.EmptyPosition);

            //if(x != 0 && positionStatusMatrix[x-1,y] == PositionStatus.FilledPosition && stoneColorMatrix[x-1,y] == stoneColorMatrix[x,y])
            //{
            //    stoneGroup.Add(new Tuple<int, int>(x-1,y));
            //}

            //foreach(var stone in stoneGroup)
            //{
            //    if (Surrounded(stone.Item1, stone.Item2))
            //    {
            //        positionStatusMatrix[stone.Item1, stone.Item2] = PositionStatus.EmptyPosition;
            //    }
            //}

            if (Surrounded(x, y))
            {
                positionStatusMatrix[x, y] = PositionStatus.EmptyPosition;
            }


            //StoneColor currentColor = StoneColor.Black;
            //if(!emptyOnRight && stoneColorMatrix[x-1,y] == currentColor)
            //{
            //    stoneGroup.Add(new Tuple<int, int>(x-1,y));
            //}
        }

        //private bool HasEmptyNeighbor(int x, int y)
        //{
        //    // Check if there are empty positions around current position
        //    bool emptyOnLeft = (x == 0 || positionStatusMatrix[x - 1, y] == PositionStatus.EmptyPosition);
        //    bool emptyOnRight = positionStatusMatrix[x + 1, y] == PositionStatus.EmptyPosition;
        //    bool emptyOnBottom = positionStatusMatrix[x, y + 1] == PositionStatus.EmptyPosition;
        //    bool emptyOnTop = (y == 0 || positionStatusMatrix[x, y - 1] == PositionStatus.EmptyPosition);

        //    bool hasEmptyNeighbor = emptyOnLeft || emptyOnRight || emptyOnBottom || emptyOnTop;
        //    return hasEmptyNeighbor;
        //}

        private bool Surrounded(int x, int y)
        {
            // Check if there are empty positions around current position
            bool surroundedOnLeft = (x == 0 || stoneColorMatrix[x - 1, y] == StoneColor.White);
            bool surroundedOnRight = stoneColorMatrix[x + 1, y] == StoneColor.White;
            bool surroundedOnBottom = stoneColorMatrix[x, y + 1] == StoneColor.White;
            bool surroundedOnTop = (y == 0 || stoneColorMatrix[x, y - 1] == StoneColor.White);

            bool surrounded = surroundedOnLeft && surroundedOnRight && surroundedOnBottom && surroundedOnTop;
            return surrounded;
        }
    }
}
