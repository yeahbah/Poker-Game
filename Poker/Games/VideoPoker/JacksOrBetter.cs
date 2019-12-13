using Poker.HandEvaluator;
using Poker.HandEvaluator.PokerHands;
using System;
using System.Collections.Generic;

namespace Poker.Games.VideoPoker
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
            _deck = deck;
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
            _deck.ResetDeck();
            _hand = _deck.TakeCards(5);
        }

        public decimal Play(int[] indexOfCards, decimal bet)
        {
            if (_hand == null)
            {
                throw new InvalidOperationException("Deal the cards first.");
            }

            var newHand = new List<Card>();
            if (indexOfCards.Length == 5)
            {
                // redraw all 5 cards
                newHand.AddRange(_deck.TakeCards(5));
            }
            else
            {
                foreach(var i in indexOfCards)
                {
                    newHand.Add(_hand[i]);
                }
                var numTake = 5 - indexOfCards.Length;
                newHand.AddRange(_deck.TakeCards(numTake));
            }

            _hand = newHand.ToArray();
            var handEvaluator = new DefaultHandEvaluator();
            var result = handEvaluator.Evaluate(_hand);
            var payAmount = 0m;

            if (_paySchedule.TryGetValue(result.HandType, out var numUnits))
            {
                payAmount = numUnits * bet;
                if (result.HandType == HandType.Pair)
                {
                    if (result.Cards[0].CardValue < CardValue.Jack)
                    {
                        payAmount = 0;
                    }
                }                
            }

            _hand = null;
            return payAmount;
        }
    }
}
