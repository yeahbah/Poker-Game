using System;
using System.Linq;

namespace Poker.HandEvaluator.HandEvalRules
{
    public class StraightHand : IPokerHand
    {
        public HandEvaluationResult? Evaluate(Card[] cards)
        {
            Array.Sort(cards);
            var ok = false;
            var sameSuit = false;
            for(var i = 0; i < cards.Length; i++)
            {                
                if (i < cards.Length - 1)
                {
                    ok = (cards[i].CardValue - cards[i + 1].CardValue == 1);
                    if (!ok)
                    {
                        break;
                    }
                    sameSuit = cards[i].Suit == cards[i + 1].Suit;
                }                
            };

            if (ok)
            {
                var handWeight = cards.Sum(card => card.DefaultCardWeight);                
                if (sameSuit)
                {
                    if (cards[0].CardValue == CardValue.Ace && cards[4].CardValue == CardValue.Ten)
                    {
                        return new HandEvaluationResult(handWeight, HandType.RoyalFlush);
                    }

                    return new HandEvaluationResult(handWeight, HandType.StraightFlush);
                }

                return new HandEvaluationResult(handWeight, HandType.Straight);
            }

            return null;
        }
    }
}
