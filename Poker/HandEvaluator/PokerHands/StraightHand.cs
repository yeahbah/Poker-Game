using Poker.HandEvaluator.PokerHands;
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
            var sameSuit = 1;
            for(var i = 0; i < cards.Length; i++)
            {                
                if (i < cards.Length - 1)
                {
                    var diff = cards[i].CardValue - cards[i + 1].CardValue;
                    ok = (diff == 1 || diff == 9); // 9 is when you have a wheel : A 5 4 3 2 
                    if (!ok)
                    {
                        break;
                    }
                    if (cards[i].Suit == cards[i + 1].Suit)
                    {
                        sameSuit++;
                    }
                }                
            };

            if (ok)
            {
                var handWeight = cards.Sum(card => card.DefaultCardWeight);                
                if (sameSuit == 5)
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
