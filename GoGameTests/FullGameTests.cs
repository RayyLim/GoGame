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

            FillBoard(board, @"
                                 123456789
                                1B
                                2 
                                3
                                4
                                5
                                6
                                7
                                8
                                9
                                ");

            PositionStatus result = board.GetPositionStatus(1, 1);

            Assert.AreEqual(PositionStatus.FilledPosition, result);
        }

        [Test]
        public void AddStone_ToEmptyPosition_OtherEmptyPositionIsUnaffected()
        {
            Board board = MakeBoard();

            FillBoard(board, @"
                                 123456789
                                1B
                                2 
                                3
                                4
                                5
                                6
                                7
                                8
                                9
                                ");

            PositionStatus status = board.GetPositionStatus(1, 2);

            Assert.AreEqual(PositionStatus.EmptyPosition, status);
        }

        //what happens when you're out of board position? 

        [Test]
        public void AddStone_SurroundOppositeColorStone_RemoveOppositeColorStone()
        {
            Board board = MakeBoard();

            FillBoard(board, @"
                                 123456789
                                1 W
                                2WBW 
                                3 W
                                4
                                5
                                6
                                7
                                8
                                9
                                ");


            PositionStatus status = board.GetPositionStatus(2, 2);

            Assert.AreEqual(PositionStatus.EmptyPosition, status);
        }
        
        [Test]
        public void AddStone_SurroundOppositeColorStone_RemoveOppositeColorStone2()
        {
            Board board = MakeBoard();

            FillBoard(board, @"
                                 123456789
                                1
                                2 W
                                3WBW
                                4 W
                                5
                                6
                                7
                                8
                                9
                                ");

            PositionStatus status = board.GetPositionStatus(2, 3);

            Assert.AreEqual(PositionStatus.EmptyPosition, status);
        }
        
        [Test]
        public void AddStone_AboveAndSurroundOppositeColorStone_RemoveOppositeColorStone()
        {
            Board board = MakeBoard();

            FillBoard(board, @"
                                 123456789
                                1
                                2 
                                3WBW
                                4 W
                                5
                                6
                                7
                                8
                                9
                                ");

            board.AddStone(StoneColor.White, 2, 2);
            PositionStatus status = board.GetPositionStatus(2, 3);

            Assert.AreEqual(PositionStatus.EmptyPosition, status);
        }

        [Test]
        public void AddStone_LeftAndSurroundOppositeColorStone_RemoveOppositeColorStone()
        {
            Board board = MakeBoard();

            FillBoard(board, @"
                                 12345
                                1
                                2 W
                                3 BW
                                4 W
                                5
                                ");

            board.AddStone(StoneColor.White, 1, 3); 
            PositionStatus status = board.GetPositionStatus(2, 3);

            Assert.AreEqual(PositionStatus.EmptyPosition, status);
        }
        
        [Test]
        public void AddStone_RightAndSurroundOppositeColorStone_RemoveOppositeColorStone()
        {
            Board board = MakeBoard();

            FillBoard(board, @"
                                 12345
                                1
                                2 W
                                3WB
                                4 W
                                5
                                ");

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
            FillBoard(board, @"
                                 123456789
                                1 B
                                2B 
                                3
                                4
                                5
                                6
                                7
                                8
                                9
                                ");

         

            StoneColor result = board.GetWinner();

            Assert.AreEqual(StoneColor.Black, result);
        }
        
        [Test]
        public void DetermineWinner_AtLeast1CellWonForBlackAndWhitePlaysLast_BlackWins()
        {
            Board board = MakeBoard();

            FillBoard(board, @"
                                 12345
                                1 B
                                2B
                                3W
                                4 
                                5
                                ");
            
            board.AddStone(StoneColor.White, 3,1);
            
            StoneColor result = board.GetWinner();

            Assert.AreEqual(StoneColor.Black, result);
        }

        [Test]
        public void DetermineWinner_NotCornerCellWonForBlack_BlackWins()
        {
            Board board = MakeBoard();

            FillBoard(board, @"
                                 12345
                                1  B
                                2 B B
                                3  B
                                4  W
                                5
                                ");

            StoneColor result = board.GetWinner();

            Assert.AreEqual(StoneColor.Black, result);
        }
        
        
        [Test]
        public void DetermineWinner_NotCornerCellWonForWhite_WhiteWins()
        {
            Board board = MakeBoard();

            FillBoard(board, @"
                                 12345
                                1  W
                                2 W W
                                3  W
                                4  B
                                5
                                ");


            StoneColor result = board.GetWinner();

            Assert.AreEqual(StoneColor.White, result);
        }

        //TODO: remove assumption that removes stones only if they are surrounded in white

        
        [Test]
        public void DetermineWinner_BlackDoesntOwnAnyCellButPlayedOnce_WhiteShouldWinByKomi()
        {
            Board board = MakeBoard();

            FillBoard(board, @"
                                 12345
                                1  B
                                2 
                                3 
                                4  W
                                5
                                ");

            StoneColor result = board.GetWinner();

            Assert.AreEqual(StoneColor.White, result);
        }

        // TODO: remove assumption that when no one own any territories, that the last player wins. The player that wins is White because of Komi.
        // TODO: remove assumption that first surrounded territory found by the for loop is the winner.

        [Test]
        public void DetermineWinner_BlackOwns2CellsWhiteOwns1Cell_BlackWins()
        {
            Board board = MakeBoard();

            FillBoard(board, @"
 123456789
1 W  B  B
2W WB BB B
3 W  B  B
4
5
6
7
8
9
");
            StoneColor result = board.GetWinner();

            Assert.AreEqual(StoneColor.Black, result);
        }

        [Test]
        public void DetermineWinner_BlackOwns1CellWhiteOwns2Cells_WhiteWins()
        {
            Board board = MakeBoard();


            FillBoard(board, @"
 123456789
1 B  W  W
2B BW WW W
3 B  W  W
4
5
6
7
8
9
");


            StoneColor result = board.GetWinner();

            Assert.AreEqual(StoneColor.White, result);
        }

        private void FillBoard(Board board, string stoneMap)
        {
            stoneMap = stoneMap.Trim();

            var lines = stoneMap.Split('\r');
            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i].Trim();
                for (int j = 1; j < line.Length; j++)
                {
                    char cell = line[j];
                    if(cell != ' ')
                    {
                        StoneColor color = StoneColor.Black;
                        
                        if(cell == 'W')
                        {
                            color = StoneColor.White;
                        }

                        board.AddStone(color, i, j);
                    }
                }
            }
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
