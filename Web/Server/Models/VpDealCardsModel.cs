using Poker;
using Poker.HandEvaluator.PokerHands;
using System.Collections.Generic;

namespace Web.Models
{
    public class VpDealCardsModel
    {
        public HandType HandType { get; set; }

        public IEnumerable<Card> Hand { get; set; }

        public string Description { get; set; }
    }
}
