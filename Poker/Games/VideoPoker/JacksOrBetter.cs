using Poker.HandEvaluator.PokerHands;
using System.Collections.Generic;

namespace Poker.Games.VideoPoker
{
    public class JacksOrBetter : VideoPokerBase
    {
        // 9-6 Jacks or better
        public JacksOrBetter(IDeck deck)
        {
            PaySchedule = new List<PayShedule>();
            for (var numUnits = 1; numUnits <= 25; numUnits++)
            {
                var royalPayout = 250;
                if (numUnits >= 1 && numUnits < 5)
                {
                    royalPayout *= numUnits;
                }
                else if (numUnits >= 5)
                {
                    // TODO verify the royal payout
                    royalPayout = ((numUnits - 1) * royalPayout) * 4;
                }

                PaySchedule.Add(new PayShedule{HandType = HandType.Pair, NumUnits = numUnits, NumUnitPay = 1 * numUnits});
                PaySchedule.Add(new PayShedule{HandType = HandType.TwoPair, NumUnits = numUnits, NumUnitPay = 2 * numUnits});
                PaySchedule.Add(new PayShedule{HandType = HandType.ThreeOfAKind, NumUnits = numUnits, NumUnitPay = 3 * numUnits});
                PaySchedule.Add(new PayShedule{HandType = HandType.Straight, NumUnits = numUnits, NumUnitPay = 4 * numUnits});
                PaySchedule.Add(new PayShedule{HandType = HandType.Flush, NumUnits = numUnits, NumUnitPay = 6 * numUnits});
                PaySchedule.Add(new PayShedule{HandType = HandType.Fullhouse, NumUnits = numUnits, NumUnitPay = 9 * numUnits});
                PaySchedule.Add(new PayShedule{HandType = HandType.FourOfAKind, NumUnits = numUnits, NumUnitPay = 25 * numUnits});
                PaySchedule.Add(new PayShedule{HandType = HandType.StraightFlush, NumUnits = numUnits, NumUnitPay = 50 * numUnits});
                PaySchedule.Add(new PayShedule{HandType = HandType.RoyalFlush, NumUnits = numUnits, NumUnitPay = royalPayout});
            }

            Deck = deck;
        }

    }
}
