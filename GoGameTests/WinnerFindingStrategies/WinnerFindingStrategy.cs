namespace GoGameTests
{
    public abstract class WinnerFindingStrategy
    {
        protected readonly Board _b;

        public WinnerFindingStrategy(Board b)
        {
            _b = b;
        }


        public abstract StoneColor GetWinner();
    }
}