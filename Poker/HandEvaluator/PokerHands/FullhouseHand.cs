using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.HandEvaluator.PokerHands
{
    public class FullhouseHand : IPokerHand
    {
        public HandEvaluationResult? Evaluate(Card[] cards)
        {
            Array.Sort(cards.ToArray());
            var found = new List<Card>();
            var trips = new Card[] { };
            var pair = new Card[] { };
            Array.ForEach(cards, card =>
            {
                var x = cards.Where(c => c.CardValue == card.CardValue
                                             && !found.Any(c => c.CardValue == card.CardValue))
                    .ToArray();
                switch (x.Length)
                {
                    case 3:
                        trips = x.ToArray();
                        found.AddRange(trips);
                        break;
                    case 2:
                        pair = x;
                        found.AddRange(pair);
                        break;
                }
            });

            if (trips.Any() && pair.Any())
            {
                return new HandEvaluationResult(cards.Sum(c => c.DefaultCardWeight), 
                    HandType.Fullhouse, cards, $"Fullhouse, {trips[0].CardValue}s full of {pair[0].CardValue}s");
            }

            return null;
        }
    }
}
