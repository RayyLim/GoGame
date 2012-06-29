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

            /*BLACK TRRITORRY HERE*/            board.AddStone(StoneColor.Black, 2,1);
            board.AddStone(StoneColor.Black, 1,2);

            StoneColor result = board.GetWinner();

            Assert.AreEqual(StoneColor.Black, result);
        }
        
        [Test]
        public void DetermineWinner_AtLeast1CellWonForBlackAndWhitePlaysLast_BlackWins()
        {
            Board board = MakeBoard();

            /*BLACK TRRITORRY HERE*/            board.AddStone(StoneColor.Black, 2,1);
            board.AddStone(StoneColor.Black, 1,2);
            
            board.AddStone(StoneColor.White, 3,1);
            
            StoneColor result = board.GetWinner();

            Assert.AreEqual(StoneColor.Black, result);
        }

        [Test]
        public void DetermineWinner_NotCornerCellWonForBlack_BlackWins()
        {
            Board board = MakeBoard();
            
                                                   board.AddStone(StoneColor.Black, 3, 1);
            board.AddStone(StoneColor.Black, 2, 2); /* BLACK TERRITORY*/                   board.AddStone(StoneColor.Black, 4, 2);
                                                   board.AddStone(StoneColor.Black, 3, 3);

                                                   board.AddStone(StoneColor.White, 3, 4);

            StoneColor result = board.GetWinner();

            Assert.AreEqual(StoneColor.Black, result);
        }
        
        
        [Test]
        public void DetermineWinner_NotCornerCellWonForWhite_WhiteWins()
        {
            Board board = MakeBoard();
            
                                                   board.AddStone(StoneColor.White, 3, 1);
            board.AddStone(StoneColor.White, 2, 2); /* WHITE TERRITORY*/                   board.AddStone(StoneColor.White, 4, 2);
                                                   board.AddStone(StoneColor.White, 3, 3);

                                                   board.AddStone(StoneColor.Black, 3, 4);

            StoneColor result = board.GetWinner();

            Assert.AreEqual(StoneColor.White, result);
        }

        //TODO: remove assumption that removes stones only if they are surrounded in white

        
        [Test]
        public void DetermineWinner_BlackDoesntOwnAnyCellButPlayedOnce_WhiteShouldWinByKomi()
        {
            Board board = MakeBoard();
            
             board.AddStone(StoneColor.Black, 3, 1);
             board.AddStone(StoneColor.White, 3, 4);

            StoneColor result = board.GetWinner();

            Assert.AreEqual(StoneColor.White, result);
        }

        // TODO: remove assumption that when no one own any territories, that the last player wins. The player that wins is White because of Komi.
        // TODO: remove assumption that first surrounded territory found by the for loop is the winner.

    }

    public enum StoneColor
    {
        Black=1,
        White=2,
        Empty = 0
    }

    public enum PositionStatus
    {
        EmptyPosition,
        FilledPosition
    }
}
