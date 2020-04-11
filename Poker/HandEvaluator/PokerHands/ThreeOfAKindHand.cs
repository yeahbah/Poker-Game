using System;
using System.Collections.Generic;
using System.Linq;

namespace Poker.HandEvaluator.PokerHands
{
    public class ThreeOfAKindHand : IPokerHand
    {
        public HandEvaluationResult? Evaluate(Card[] cards)
        {
            Array.Sort(cards.ToArray());
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
                    found.AddRange(cards.Where(c => !found.Contains(c)));
                    var handWeight = found.Sum(c => c.DefaultCardWeight);
                    return new HandEvaluationResult(handWeight, HandType.ThreeOfAKind, cards, $"Three of a Kind, {found[0].CardValue}s.");
                }
            }

            return null;
        }
    }
}
