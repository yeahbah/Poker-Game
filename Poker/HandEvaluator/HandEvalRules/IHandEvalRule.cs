using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.HandEvaluator.HandEvalRules
{
    public interface IHandEvalRule
    {
        HandType? Evaluate(Card[] cards);
    }
}
