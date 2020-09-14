using Yeahbah.Poker.HandEvaluator.PokerHands;

namespace PokerGames.VideoPoker
{
    public class PaySchedule
    {
        public HandType HandType { get; set; }
        public int BetSize { get; set; }
        public int PaySizeInUnits { get; set; }
    }
}
