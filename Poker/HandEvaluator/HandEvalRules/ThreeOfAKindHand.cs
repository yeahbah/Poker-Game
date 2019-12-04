using System;
using System.Collections.Generic;
using System.Linq;

namespace Poker.HandEvaluator.HandEvalRules
{
    public class ThreeOfAKindHand : IPokerHand
    {
        public HandEvaluationResult? Evaluate(Card[] cards)
        {
            Array.Sort(cards);
            var found = new List<Card>();
            Array.ForEach(cards, card =>
            {                
                var trips = cards.Where(c => c.CardValue == card.CardValue
                                        && !found.Any(c => c.CardValue == card.CardValue));
                if (trips.Count() == 3)
                {
                    found.Add(card);                    
                }
            });         
            
            if (found.Any())
            {
                var notAFullHouse = cards
                    .Where(c => c.CardValue != found[0].CardValue)
                    .ToArray();
                if (notAFullHouse[0].CardValue != notAFullHouse[1].CardValue)
                {
                    var handWeight = found.Sum(c => c.DefaultCardWeight);
                    return new HandEvaluationResult(handWeight, HandType.ThreeOfAKind);
                }
            }

            return null;
        }
    }
}
