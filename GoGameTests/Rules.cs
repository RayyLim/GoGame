namespace GoGameTests
{
    public class Rules
    {
        public StoneColor GetOppositeColor(int x, int y)
        {
            return Board.GetStoneColor(x, y) == StoneColor.White ? StoneColor.Black : StoneColor.White;
        }
        public const int EDGE = 2;

        public Board Board { get; set; }



        public void CheckStonesAroundPositionAndRemoveIfNeeded(int x, int y)
        {
            RemoveStoneIfSurrounded(x - 1, y - 1);
            RemoveStoneIfSurrounded(x, y - 1);
            RemoveStoneIfSurrounded(x, y + 1);
            RemoveStoneIfSurrounded(x - 1, y);
            RemoveStoneIfSurrounded(x + 1, y);
        }

        public void RemoveStoneIfSurrounded(int x, int y)
        {
            StoneRemover remover = new StoneRemover(new RemovalArgs()
                                                        {
                                                            X = x,
                                                            Y = y,
                                                            Rules = this,
                                                            Board = Board,
                                                        });
            remover.Remove();

        }

        public Rules(Board board)
        {
            Board = board;
        }

        public StoneColor GetWinner()
        {
            var result = new WinnerByCountStrategy(Board).GetWinner();
            if (result != StoneColor.Empty)
            {
                return result;
            }
            return new KomiWinnerStrategy(Board).GetWinner();
        }


        public void NotifyStoneAdded(StoneColor stoneColor, int x, int y)
        {
        }

        
    }
}