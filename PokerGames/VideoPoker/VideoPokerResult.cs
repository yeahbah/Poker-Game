using Yeahbah.Poker.HandEvaluator;

namespace PokerGames.VideoPoker
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
