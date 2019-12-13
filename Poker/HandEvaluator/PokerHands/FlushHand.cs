using Poker.HandEvaluator.PokerHands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.HandEvaluator.HandEvalRules
{
    public class FlushHand : IPokerHand
    {
        public HandEvaluationResult? Evaluate(Card[] cards)
        {            
            Array.Sort(cards);
            var sameSuit = 1;
            for(var i = 0; i < cards.Length; i++)
            {
                var card = cards[i];
                if (i < cards.Length - 1)
                {
                    if (card.Suit == cards[i + 1].Suit)
                    {
                        sameSuit++;
                    }
                }
            }

            // should we be worried a straight flushes or just leave it to the evaluator?            
            if (sameSuit == 5 && (new StraightHand().Evaluate(cards) == null)) 
            {
                var handWeight = cards.Sum(card => card.DefaultCardWeight);
                return new HandEvaluationResult(handWeight, HandType.Flush, cards);
            }

            return null;
        }
    }
}
