using Poker.HandEvaluator.PokerHands;

namespace Poker.Games.VideoPoker
{
    public class PayShedule
    {
        public HandType HandType { get; set; }
        public int BetSize { get; set; }
        public int PaySizeInUnits { get; set; }
    }
}
