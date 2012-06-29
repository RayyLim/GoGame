namespace GoGameTests
{
    internal class KomiWinnerStrategy : WinnerFindingStrategy
    {
        public KomiWinnerStrategy(Board board) : base(board)
        {
                
        }

        public override StoneColor GetWinner()
        {
            return StoneColor.White;
        }
    }
}