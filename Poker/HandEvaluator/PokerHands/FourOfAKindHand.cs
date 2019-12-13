using System;
using System.Collections.Generic;
using System.Linq;

namespace Poker.HandEvaluator.PokerHands
{
    public class FourOfAKindHand : IPokerHand
    {
        public HandEvaluationResult? Evaluate(Card[] cards)
        {
            Array.Sort(cards);
            var found = new List<Card>();
            Array.ForEach(cards, card =>
            {
                var quad = cards.Where(c => c.CardValue == card.CardValue
                                        && !found.Any(c => c.CardValue == card.CardValue));
                if (quad.Count() == 4)
                {
                    found.Add(card);
                }
            });

            if (found.Any())
            {
                found.AddRange(cards
                    .Where(c => !found.Contains(c)));
                var handWeight = found.Sum(c => c.DefaultCardWeight);
                return new HandEvaluationResult(handWeight, HandType.FourOfAKind, found.ToArray(), $"Four of Kind, {cards[0].CardValue}s.");                
            }

            return null;
        }
    }
}
