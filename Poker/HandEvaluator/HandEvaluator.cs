using Poker.HandEvaluator.HandEvalRules;
using System.Collections.Generic;
using System.Linq;

namespace Poker.HandEvaluator
{
    public class HandEvaluator : IHandEvaluator
    {

        public HandEvaluator()
        {
            HandEvaluators = new List<IPokerHand>
            {
                new PairHand(),
                new ThreeOfAKindHand(),
                new StraightHand(),
                new FlushHand(),
                new FourOfAKindHand()
            };
        }

        public IEnumerable<IPokerHand> HandEvaluators
        {
            get; set;
        }
        public IDictionary<Card, int> CardWeight { get; set; }

        public HandEvaluationResult Evaluate(Card[] cards)
        {
            var hand = cards.Take(5).ToArray();            
            foreach(var handEvaluator in HandEvaluators)
            {
                var result = handEvaluator.Evaluate(hand);
                if (result.HasValue)
                {
                    return result.Value;
                }
            }
            var weight = cards.Sum(c => c.DefaultCardWeight);
            return new HandEvaluationResult(weight, PokerHands.HandType.HighCard);
        }
    }
}
