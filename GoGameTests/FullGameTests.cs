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
        public void GetWhitePoints_ByDefaultBecauseOfKomi_HalfPoint()
        {
            Board board = MakeBoard();

            var result = board.GetWhitePoints();

            Assert.AreEqual(0.5, result);
        }

        [Test]
        public void GetBlackPoints_ByDefault_0Points()
        {
            Board board = MakeBoard();

            var result = board.GetBlackPoints();

            Assert.AreEqual(0, result);
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
        public void GetBlackPoints_1CellWonForBlack_1()
        {
            Board board = MakeBoard();

            /*BLACK TRRITORRY HERE*/            board.AddStone(StoneColor.Black, 2,1);
            board.AddStone(StoneColor.Black, 1,2);

            var result = board.GetBlackPoints();

            Assert.AreEqual(1, result);
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
        public void GetBlackPoints_NotCornerCellWonForBlack_1()
        {
            Board board = MakeBoard();
            
                                                   board.AddStone(StoneColor.Black, 3, 1);
            board.AddStone(StoneColor.Black, 2, 2); /* BLACK TERRITORY*/                   board.AddStone(StoneColor.Black, 4, 2);
                                                   board.AddStone(StoneColor.Black, 3, 3);

            var result = board.GetBlackPoints();

            Assert.AreEqual(1, result);
        }

        [Test]
        public void GetBlackPoints_BlackOwn2Cells_2()
        {
            Board board = MakeBoard();

                                                   board.AddStone(StoneColor.Black, 3, 1);
            board.AddStone(StoneColor.Black, 2, 2); /* BLACK TERRITORY*/                   board.AddStone(StoneColor.Black, 4, 2);
                                                   board.AddStone(StoneColor.Black, 3, 3);
                                                   board.AddStone(StoneColor.Black, 3, 4);
            board.AddStone(StoneColor.Black, 2, 5); /* BLACK TERRITORY*/                   board.AddStone(StoneColor.Black, 4, 5);
                                                   board.AddStone(StoneColor.Black, 3, 6);

            var result = board.GetBlackPoints();

            Assert.AreEqual(2, result);
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

        [Test]
        public void GetWhitePoints_NotCornerCellWonForWhite_1andHalf()
        {
            Board board = MakeBoard();
            
                                                   board.AddStone(StoneColor.White, 3, 1);
            board.AddStone(StoneColor.White, 2, 2); /* WHITE TERRITORY*/                   board.AddStone(StoneColor.White, 4, 2);
                                                   board.AddStone(StoneColor.White, 3, 3);

                                                   board.AddStone(StoneColor.Black, 3, 4);

            var result = board.GetWhitePoints();

            Assert.AreEqual(1.5, result);
        }


        
        [Test]
        public void DetermineWinner_BlackDoesntOwnAnyCellButPlayedOnce_WhiteShouldWinByKomi()
        {
            Board board = MakeBoard();
            
             board.AddStone(StoneColor.Black, 3, 1);
             board.AddStone(StoneColor.White, 3, 4);

            StoneColor result = board.GetWinner();

            Assert.AreEqual(StoneColor.White, result);
        }

        [Test]
        public void DetermineWinner_BlackDoesntOwnAnyCellButPlayedLast_WhiteShouldWinByKomi()
        {
            Board board = MakeBoard();

            board.AddStone(StoneColor.White, 3, 4);
            board.AddStone(StoneColor.Black, 3, 1);

            StoneColor result = board.GetWinner();

            Assert.AreEqual(StoneColor.White, result);
        }

        [Test]
        public void DetermineWinner_WhiteOwn2CellsWhileBlackOwns1_WhiteWins()
        {
            Board board = MakeBoard();

                                                    board.AddStone(StoneColor.Black, 3, 1);
            board.AddStone(StoneColor.Black, 2, 2); /* BLACK TERRITORY*/                   board.AddStone(StoneColor.Black, 4, 2);
                                                    board.AddStone(StoneColor.Black, 3, 3);
                                                    board.AddStone(StoneColor.White, 3, 4);
            board.AddStone(StoneColor.White, 2, 5); /* WHITE TERRITORY*/                   board.AddStone(StoneColor.White, 4, 5);
                                                    board.AddStone(StoneColor.White, 3, 6);
                                                    board.AddStone(StoneColor.White, 3, 7);
            board.AddStone(StoneColor.White, 2, 8); /* WHITE TERRITORY*/                   board.AddStone(StoneColor.White, 4, 8);
                                                    board.AddStone(StoneColor.White, 3, 9);

            StoneColor result = board.GetWinner();

            Assert.AreEqual(StoneColor.White, result);
        }

        
        // FORK: Either start fixing todos
        // TODO: remove assumption that removes stones only if they are surrounded in white
        // TODO: Recognize group with 2 empty positions next to each other
        // TODO: remove assumption in IsFullySurroundedBy that all the positions it checks are filled
        // TODO: remove assumption that white does not lose a point when it loses a stone

        // Test inheritly talks about points... so should write points test first
        [Test]
        public void DetermineWinner_BlackOwns2CellsButLoses1StoneToWhiteAndWhiteOwns1Cell_WhiteWinsByKomi()
        {
            Board board = MakeBoard();

                                                    board.AddStone(StoneColor.Black, 3, 1);
            board.AddStone(StoneColor.Black, 2, 2); /* BLACK TERRITORY*/                   board.AddStone(StoneColor.Black, 4, 2);
                                                    board.AddStone(StoneColor.Black, 3, 3);
                                                    board.AddStone(StoneColor.Black, 3, 4);
            board.AddStone(StoneColor.Black, 2, 5); /* BLACK TERRITORY*/                   board.AddStone(StoneColor.Black, 4, 5);
                                                    board.AddStone(StoneColor.Black, 3, 6);
                                                    board.AddStone(StoneColor.Black, 3, 7);
            board.AddStone(StoneColor.Black, 2, 8); board.AddStone(StoneColor.White, 3, 8); board.AddStone(StoneColor.Black, 4, 8);
            board.AddStone(StoneColor.White, 2, 9); board.AddStone(StoneColor.Black, 3, 9); board.AddStone(StoneColor.White, 4, 9);
                                                    board.AddStone(StoneColor.White, 3, 10);

            StoneColor result = board.GetWinner();

            Assert.AreEqual(StoneColor.White, result);
        }

        // What would happen if we make GetWinner, GetBlackPoints, and GetWhitePoints
        // The responsibility of another class? i.e. EndGame endGame = new EndGame(board)
        // where GetWinner will be EndGame(board).GetWinner();

        [Test]
        public void GetBlackPoints_BlackOwns2CellsButLoses1StoneToWhite_1()
        {
            Board board = MakeBoard();
        
                                                    board.AddStone(StoneColor.Black, 3, 1);
            board.AddStone(StoneColor.Black, 2, 2); /* BLACK TERRITORY*/                   board.AddStone(StoneColor.Black, 4, 2);
                                                    board.AddStone(StoneColor.Black, 3, 3);
                                                    board.AddStone(StoneColor.Black, 3, 4);
            board.AddStone(StoneColor.Black, 2, 5); /* BLACK TERRITORY*/                   board.AddStone(StoneColor.Black, 4, 5);
                                                    board.AddStone(StoneColor.Black, 3, 6);
                                                    board.AddStone(StoneColor.Black, 3, 7);
            board.AddStone(StoneColor.Black, 2, 8); /* TO BE FILLED BY WHITE */             board.AddStone(StoneColor.Black, 4, 8);
            board.AddStone(StoneColor.White, 2, 9); board.AddStone(StoneColor.Black, 3, 9); board.AddStone(StoneColor.White, 4, 9);
                                                    board.AddStone(StoneColor.White, 3, 10);
            
            board.AddStone(StoneColor.White, 3, 8);

            var result = board.GetBlackPoints();
        
            Assert.AreEqual(1, result);
        }

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
