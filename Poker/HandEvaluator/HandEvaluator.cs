using Poker.HandEvaluator.HandEvalRules;
using System;
using System.Collections.Generic;

namespace Poker.HandEvaluator
{
    public class HandEvaluator : IHandEvaluator
    {
        public IEnumerable<IPokerHand> HandEvaluators { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IDictionary<Card, int> CardWeight { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public HandEvaluationResult Evaluate(Card[] cards)
        {
            throw new NotImplementedException();
        }
    }
}
