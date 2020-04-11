using Poker.HandEvaluator;

namespace Poker.Games.VideoPoker
{
    public struct VideoPokerResult
    {
        public VideoPokerResult(HandEvaluationResult handEvaluationResult, decimal payoutInUnits, decimal payoutValue)
        {
            Hand = handEvaluationResult;
            PayoutInUnits = payoutInUnits;
            PayoutValue = payoutValue;
        }

        public decimal PayoutInUnits { get; }
        public decimal PayoutValue { get; }
        public HandEvaluationResult Hand { get; }
        
    }
}
