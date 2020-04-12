using System;
using System.Collections.Generic;
using System.Linq;

namespace Poker.HandEvaluator.PokerHands
{
    public class FourOfAKindHand : IPokerHand
    {
        public HandEvaluationResult? Evaluate(Card[] hand)
        {
            var cards = hand.ToArray();
            Array.Sort(cards.ToArray());
            var found = new List<Card>();
            Array.ForEach(hand, card =>
            {
                var quad = cards.Where(c => c.CardValue == card.CardValue
                                        && !found.Any(c => c.CardValue == card.CardValue));
                if (quad.Count() == 4)
                {
                    found.AddRange(quad.ToArray());
                }
            });

            if (found.Any())
            {
                //found.AddRange(hand
                //    .Where(c => !found.Contains(c)));
                var handWeight = found.Sum(c => c.DefaultCardWeight);
                return new HandEvaluationResult(handWeight, HandType.FourOfAKind, hand, found.ToArray(), $"Four of Kind, {found[0].CardValue}s.");                
            }

            return null;
        }
    }
}
