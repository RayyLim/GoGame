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

            board.AddStone(StoneColor.Black, 1, 1);
            PositionStatus result = board.GetPositionStatus(1, 1);

            Assert.AreEqual(PositionStatus.FilledPosition, result);
        }

        [Test]
        public void AddStone_ToEmptyPosition_OtherEmptyPositionIsUnaffected()
        {
            Board board = MakeBoard();

            board.AddStone(StoneColor.Black, 1, 1);
            PositionStatus status = board.GetPositionStatus(1, 2);

            Assert.AreEqual(PositionStatus.EmptyPosition, status);
        }

        //what happens when you're out of board position? 

        [Test]
        public void AddStone_SurroundOppositeColorStone_RemoveOppositeColorStone()
        {
            Board board = MakeBoard();
                                                              board.AddStone(StoneColor.White, 2, 1);
            board.AddStone(StoneColor.White, 1, 2); board.AddStone(StoneColor.Black, 2, 2); board.AddStone(StoneColor.White, 3, 2);
                                                              board.AddStone(StoneColor.White, 2, 3);

            PositionStatus status = board.GetPositionStatus(2, 2);

            Assert.AreEqual(PositionStatus.EmptyPosition, status);
        }
        
        [Test]
        public void AddStone_SurroundOppositeColorStone_RemoveOppositeColorStone2()
        {
            Board board = MakeBoard();
                                                              board.AddStone(StoneColor.White, 2, 2);
            board.AddStone(StoneColor.White, 1, 3); board.AddStone(StoneColor.Black, 2, 3); board.AddStone(StoneColor.White, 3, 3);
                                                              board.AddStone(StoneColor.White, 2, 4);

            PositionStatus status = board.GetPositionStatus(2, 3);

            Assert.AreEqual(PositionStatus.EmptyPosition, status);
        }
        
        [Test]
        public void AddStone_AboveAndSurroundOppositeColorStone_RemoveOppositeColorStone()
        {
            Board board = MakeBoard();
            board.AddStone(StoneColor.White, 1, 3); board.AddStone(StoneColor.Black, 2, 3); board.AddStone(StoneColor.White, 3, 3);
                                                              board.AddStone(StoneColor.White, 2, 4);



            board.AddStone(StoneColor.White, 2, 2);
            PositionStatus status = board.GetPositionStatus(2, 3);

            Assert.AreEqual(PositionStatus.EmptyPosition, status);
        }

        [Test]
        public void AddStone_LeftAndSurroundOppositeColorStone_RemoveOppositeColorStone()
        {
            Board board = MakeBoard();

                                                              board.AddStone(StoneColor.White, 2, 2);
                                                              board.AddStone(StoneColor.Black, 2, 3); board.AddStone(StoneColor.White, 3, 3);
                                                              board.AddStone(StoneColor.White, 2, 4);

            board.AddStone(StoneColor.White, 1, 3); 
            PositionStatus status = board.GetPositionStatus(2, 3);

            Assert.AreEqual(PositionStatus.EmptyPosition, status);
        }
        
        [Test]
        public void AddStone_RightAndSurroundOppositeColorStone_RemoveOppositeColorStone()
        {
            Board board = MakeBoard();

                                                              board.AddStone(StoneColor.White, 2, 2);
            board.AddStone(StoneColor.White, 1, 3); board.AddStone(StoneColor.Black, 2, 3); 
                                                              board.AddStone(StoneColor.White, 2, 4);

            board.AddStone(StoneColor.White, 3, 3);
            PositionStatus status = board.GetPositionStatus(2, 3);

            Assert.AreEqual(PositionStatus.EmptyPosition, status);
        }

        [Test]
        public void DetermineWinner_ByDefaultBecauseOfKomi_White()
        {
            Board board = MakeBoard();

            StoneColor result = board.GetWinner();

            Assert.AreEqual(StoneColor.White, result);
        }

        [Test]
        public void DetermineWinner_AtLeast1CellWonForBlack_BlackWins()
        {
            Board board = MakeBoard();

            /*BLACK TRRITORRY HERE*/            board.AddStone(StoneColor.Black, 1,2);
            board.AddStone(StoneColor.Black, 2,1);

            StoneColor result = board.GetWinner();

            Assert.AreEqual(StoneColor.Black, result);
        }
        
        [Test]
        public void DetermineWinner_AtLeast1CellWonForBlackAndWhitePlaysLast_BlackWins()
        {
            Board board = MakeBoard();

            /*BLACK TRRITORRY HERE*/            board.AddStone(StoneColor.Black, 1,2);
            board.AddStone(StoneColor.Black, 2,1);
            
            board.AddStone(StoneColor.White, 3,1);
            
            StoneColor result = board.GetWinner();

            Assert.AreEqual(StoneColor.Black, result);
        }
    }

    public enum StoneColor
    {
        Black,
        White,
        Empty = 0
    }

    public enum PositionStatus
    {
        EmptyPosition,
        FilledPosition
    }
}
