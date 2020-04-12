using Poker.HandEvaluator;

namespace Poker.Games.VideoPoker
{
    public struct VideoPokerResult
    {
        public VideoPokerResult(HandEvaluationResult handEvaluationResult, decimal payoutInUnits, decimal payoutMoney)
        {
            HandEvaluationResult = handEvaluationResult;
            PayoutInUnits = payoutInUnits;
            PayoutMoney = payoutMoney;
        }

        public decimal PayoutInUnits { get; }
        public decimal PayoutMoney { get; }
        public HandEvaluationResult HandEvaluationResult { get; }
        
    }
}
