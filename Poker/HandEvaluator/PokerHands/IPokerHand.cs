namespace Poker.HandEvaluator.PokerHands
{
    public interface IPokerHand
    {
        HandEvaluationResult? Evaluate(Card[] cards);
    }
}
