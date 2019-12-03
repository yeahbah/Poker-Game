using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.HandEvaluator.HandEvalRules
{
    public class ThreeOfAKindEvalRule : IHandEvalRule
    {
        public HandEvaluationResult? Evaluate(Card[] cards)
        {
            Array.Sort(cards);
            var found = new List<Card>();
            for (var i = 0; i < cards.Length; i++)
            {
                var currentCard = cards[i];
                var trips = cards.Where(c => c.CardValue == currentCard.CardValue
                                        && !found.Any(c => c.CardValue == currentCard.CardValue));
                if (trips.Count() == 3)
                {                    
                    found.Add(currentCard);
                    break;
                }
            }            
            
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
