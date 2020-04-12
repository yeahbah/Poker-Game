namespace Poker.Games.VideoPoker
{
    public class GameVars
    {
        public GameVars()
        {
            UnitSize = 0.05m;
            Money = 0;
            BetSize = 1;
        }

        public decimal UnitSize { get; set; }
        public int BetSize { get; set; }
        public decimal BetSizeMoney => UnitSize * BetSize;
        public decimal Money { get; set; }        
    }
}