using Poker;
using Poker.HandEvaluator.PokerHands;
using System.Collections.Generic;

namespace Web.Models
{
    public class VpDrawResultModel
    {
        public decimal PayoutInUnits { get; set; }
        public decimal PayoutMoney { get; set; }

        public HandType HandType { get; set;  }

        public IEnumerable<Card> Hand { get; set; }
        public string Description { get; set;  }
    }
}
