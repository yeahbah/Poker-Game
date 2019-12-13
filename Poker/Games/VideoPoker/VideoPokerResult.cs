using Poker.HandEvaluator;

namespace Poker.Games.VideoPoker
{
    public struct VideoPokerResult
    {
        public VideoPokerResult(HandEvaluationResult handEvaluationResult, decimal payout)
        {
            HandEvaluationResult = handEvaluationResult;
            Payout = payout;
        }

        public decimal Payout { get; }

        public HandEvaluationResult HandEvaluationResult { get; }
    }
}
