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

        public HandEvaluationResult Evaluate(Card[] hand)
        {            
            var cards = hand.Take(5).ToArray();
            foreach(var handEvaluator in HandEvaluators)
            {
                var result = handEvaluator.Evaluate(cards);
                if (result.HasValue)
                {
                    return result.Value;
                }
            }

            Array.Sort(cards);
            Array.Resize(ref cards, 1);
            var weight = hand.Sum(c => c.DefaultCardWeight);
            return new HandEvaluationResult(weight, HandType.HighCard, hand, cards, $"High Card, {cards[0].CardValue}.");                    
        }
    }
}
