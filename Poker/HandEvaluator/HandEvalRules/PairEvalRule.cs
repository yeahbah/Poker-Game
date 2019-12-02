using System.Collections.Generic;
using System.Linq;

namespace Poker.HandEvaluator.HandEvalRules
{
    public class PairEvalRule : IHandEvalRule
    {
        public HandType? Evaluate(Card[] cards)
        {
            //Array.Sort(cards);
            var found = new List<Card>();
            for (var i = 0; i < cards.Length; i++)
            {
                for (var j = i + 1; j < cards.Length; j++)
                {
                    if (cards[i] == cards[j])
                    {
                        found.Add(cards[i]);
                    }                        
                }
            }

            // there can only be one pair
            if (found.Count() == 1)
            {
                return HandType.Pair;
            }

            return null;
        }
    }
}
