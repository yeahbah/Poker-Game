using Poker.HandEvaluator.HandEvalRules;
using System.Collections.Generic;

namespace Poker.HandEvaluator
{
    public enum HandType 
    {
        HighCard,
        Pair, 
        TwoPair,
        ThreeOfAKind,
        Straight,
        Flush,
        Fullhouse,
        FourOfAKind,
        StraightFlush,
        RoyalFlush
    }

    public struct HandEvaluationResult
    {       
        public HandEvaluationResult(int handWeight, HandType handType)
        {
            HandType = handType;
            HandWeight = handWeight;
        }

        // on a multiplayer game, you can use this value to evaluate which hand is better.
        public int HandWeight { get; }
        public HandType HandType { get; }
    }
    
    public interface IHandEvaluator
    {
        IEnumerable<IPokerHand> HandEvaluators { get; set; }

        // card point system, e.g. As = 100, Ah = 99, Ac = 98, Ad = 97        
        // in games where a royal vs royal is possible, which hand is best? 
        // we have to give weight to the suit of the hand, spades > hearts > clubs > diamonds        
        IDictionary<Card, int> CardWeight { get; set; }

        // this method should only take in five cards
        HandEvaluationResult Evaluate(Card[] cards);

    }
}
