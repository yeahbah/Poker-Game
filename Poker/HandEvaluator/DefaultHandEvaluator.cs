using System;
using System.Collections.Generic;
using System.Linq;
using Poker.HandEvaluator.PokerHands;

namespace Poker.HandEvaluator
{
    public class DefaultHandEvaluator : IHandEvaluator
    {
        public DefaultHandEvaluator()
        {
            HandEvaluators = new List<IPokerHand>
            {
                new FlushHand(),
                new FourOfAKindHand(),
                new FullhouseHand(),
                new StraightHand(),
                new ThreeOfAKindHand(),
                new PairHand()
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
            return new HandEvaluationResult(weight, PokerHands.HandType.HighCard, cards, $"High Card, {hand.Max(c => c.CardValue)}.");                    
        }
    }
}
