using Poker.HandEvaluator.PokerHands;
using System.Collections.Generic;

namespace Poker.Games.VideoPoker
{
    public class JacksOrBetter : VideoPokerBase
    {
        // 9-6 Jacks or better
        public JacksOrBetter(IDeck deck)
        {
            PaySchedule = new Dictionary<HandType, decimal>
            {
                {HandType.Pair, 1},
                {HandType.TwoPair, 2},
                {HandType.ThreeOfAKind, 3},
                {HandType.Straight, 4},
                {HandType.Flush, 6},
                {HandType.Fullhouse, 9},
                {HandType.FourOfAKind, 25},
                {HandType.StraightFlush, 50},
                {HandType.RoyalFlush, 250}
            };
            Deck = deck;
        }

    }
}
