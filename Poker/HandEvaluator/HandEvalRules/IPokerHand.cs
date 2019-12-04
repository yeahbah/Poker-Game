using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.HandEvaluator.HandEvalRules
{
    public interface IPokerHand
    {
        HandEvaluationResult? Evaluate(Card[] cards);
    }
}
