using System;
using System.Collections.Generic;
using System.Linq;

namespace Poker.HandEvaluator.HandEvalRules
{
    public class PairEvalRule : IHandEvalRule
    {
        public HandEvaluationResult? Evaluate(Card[] cards)
        {
            Array.Sort(cards);
            var found = new List<Card>();
            for (var i = 0; i < cards.Length; i++)
            {
                var currentCard = cards[i];
                var pair = cards.Where(c => c.CardValue == currentCard.CardValue 
                                        && !found.Any(c => c.CardValue == currentCard.CardValue));
                if (pair.Count() == 2)
                {
                    found.Add(currentCard);
                }               
            }

            var handWeight = found.Sum(c => c.DefaultCardWeight);
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
