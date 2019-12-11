using Poker;
using Poker.HandEvaluator;
using Poker.HandEvaluator.PokerHands;
using System;
using System.Collections.Generic;

namespace VideoPoker
{
    public class JacksOrBetter : IVideoPoker
    {

        // 9-6 Jacks or better
        public JacksOrBetter(IDeck deck)
        {
            _paySchedule = new Dictionary<HandType, decimal>();
            _paySchedule.Add(HandType.Pair, 1);
            _paySchedule.Add(HandType.TwoPair, 2);
            _paySchedule.Add(HandType.ThreeOfAKind, 3);
            _paySchedule.Add(HandType.Straight, 4);
            _paySchedule.Add(HandType.Flush, 6);
            _paySchedule.Add(HandType.Fullhouse, 9);
            _paySchedule.Add(HandType.FourOfAKind, 25);
            _paySchedule.Add(HandType.StraightFlush, 50);
            _paySchedule.Add(HandType.RoyalFlush, 800);
            this._deck = deck;
        }

        private Card[] _hand;
        public Card[] Hand => _hand;

        private IDictionary<HandType, decimal> _paySchedule;
        private readonly IDeck _deck;

        public IDictionary<HandType, decimal> PaySchedule 
        { 
            get => _paySchedule; 
            set { _paySchedule = value; } 
        }

        public void Deal()
        {
            _hand = _deck.TakeCards(5);
        }

        public decimal Play(int[] indexOfCards, decimal bet)
        {
            if (_hand == null)
            {
                throw new InvalidOperationException("Deal the cards first.");
            }

            var handEvaluator = new HandEvaluator();
            var result = handEvaluator.Evaluate(_hand);
            if (_paySchedule.TryGetValue(result.HandType, out var numUnits))
            {
                return numUnits * bet;
            }
            
            _hand = null;
            return 0;
        }
    }
}
