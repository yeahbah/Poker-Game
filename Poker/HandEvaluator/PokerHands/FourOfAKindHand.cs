using Poker.HandEvaluator.PokerHands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.HandEvaluator.HandEvalRules
{
    public class FourOfAKindHand : IPokerHand
    {
        public HandEvaluationResult? Evaluate(Card[] cards)
        {
            Array.Sort(cards);
            var found = new List<Card>();
            Array.ForEach(cards, card =>
            {
                var trips = cards.Where(c => c.CardValue == card.CardValue
                                        && !found.Any(c => c.CardValue == card.CardValue));
                if (trips.Count() == 4)
                {
                    found.Add(card);
                }
            });

            if (found.Any())
            {                
                var handWeight = found.Sum(c => c.DefaultCardWeight);
                return new HandEvaluationResult(handWeight, HandType.FourOfAKind);                
            }

            return null;
        }
    }
}
