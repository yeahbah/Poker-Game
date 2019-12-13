using System;
using System.Linq;

namespace Poker.HandEvaluator.PokerHands
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
                var firstCardIsAnAce = cards[0].CardValue == CardValue.Ace;                
                if (sameSuit == 5 && firstCardIsAnAce && cards[4].CardValue == CardValue.Ten)
                {                    
                    return new HandEvaluationResult(handWeight, HandType.RoyalFlush, cards, $"Royal Straight Flush.");
                }

                if (firstCardIsAnAce)
                {
                    handWeight -= 64; // ace weight = 71, on wheel ace, ace weight should be lower than deuce
                }

                if (sameSuit == 5)
                {
                    return new HandEvaluationResult(handWeight, HandType.StraightFlush, cards, $"Straight Flush to {cards[0].CardValue}");
                }
                return new HandEvaluationResult(handWeight, HandType.Straight, cards, $"Straight to {cards[0].CardValue}.");
            }

            return null;
        }
    }
}
