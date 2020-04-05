﻿using Poker.HandEvaluator.PokerHands;
using System.Collections.Generic;

namespace Poker.Games.VideoPoker
{
    public class JacksOrBetter : VideoPokerBase
    {
        // 9-6 Jacks or better
        public JacksOrBetter(IDeck deck)
        {
            PaySchedule = new List<PayShedule>();
            var payIncrement = 0;
            var royalPayout = 0;
            for (var numUnits = 1; numUnits <= 25; numUnits++)
            {
                if (numUnits >= 1 && numUnits < 5)
                {
                    royalPayout = 250 * numUnits;
                }
                else if (numUnits >= 5)
                {
                    // jumps to 4000 on a 5 unit bet
                    // jumps 800 onwards
                    royalPayout = 4000 + payIncrement;
                    payIncrement += 800;
                }

                PaySchedule.Add(new PayShedule{HandType = HandType.Pair, BetSize = numUnits, PaySizeInUnits = 1 * numUnits});
                PaySchedule.Add(new PayShedule{HandType = HandType.TwoPair, BetSize = numUnits, PaySizeInUnits = 2 * numUnits});
                PaySchedule.Add(new PayShedule{HandType = HandType.ThreeOfAKind, BetSize = numUnits, PaySizeInUnits = 3 * numUnits});
                PaySchedule.Add(new PayShedule{HandType = HandType.Straight, BetSize = numUnits, PaySizeInUnits = 4 * numUnits});
                PaySchedule.Add(new PayShedule{HandType = HandType.Flush, BetSize = numUnits, PaySizeInUnits = 6 * numUnits});
                PaySchedule.Add(new PayShedule{HandType = HandType.Fullhouse, BetSize = numUnits, PaySizeInUnits = 9 * numUnits});
                PaySchedule.Add(new PayShedule{HandType = HandType.FourOfAKind, BetSize = numUnits, PaySizeInUnits = 25 * numUnits});
                PaySchedule.Add(new PayShedule{HandType = HandType.StraightFlush, BetSize = numUnits, PaySizeInUnits = 50 * numUnits});
                PaySchedule.Add(new PayShedule{HandType = HandType.RoyalFlush, BetSize = numUnits, PaySizeInUnits = royalPayout});
            }

            Deck = deck;
        }

    }
}
