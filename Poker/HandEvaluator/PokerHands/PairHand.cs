using Poker.HandEvaluator.PokerHands;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Poker.HandEvaluator.HandEvalRules
{
    public class PairHand : IPokerHand
    {
        public HandEvaluationResult? Evaluate(Card[] cards)
        {
            Array.Sort(cards);
            var found = new List<Card>();
            Array.ForEach(cards, card =>            
            {                
                var pair = cards.Where(c => c.CardValue == card.CardValue
                                        && !found.Any(c => c.CardValue == card.CardValue));
                if (pair.Count() == 2)
                {
                    found.Add(card);
                }
            });

            var handWeight = cards.Sum(c => c.DefaultCardWeight);
            if (found.Count() == 1)
            {
                return new HandEvaluationResult(handWeight, HandType.Pair);
            }

            if (found.Count() == 2)
            {
                return new HandEvaluationResult(handWeight, HandType.TwoPair);
            }

            return null;
        }
    }
}
