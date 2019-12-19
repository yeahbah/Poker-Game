﻿using Poker.HandEvaluator;
using Poker.HandEvaluator.PokerHands;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Poker.Games.VideoPoker
{
    public class VideoPokerBase : IVideoPoker
    {

        private Card[] _hand;
        public Card[] Hand => _hand;

        public IList<PayShedule> PaySchedule { get; set; }
      

        public IDeck Deck { get; set; }

        public void Deal()
        {
            Deck.ResetDeck();
            _hand = Deck.TakeCards(5);
        }

        public VideoPokerResult Play(int[] indexOfCards, decimal bet)
        {
            if (_hand == null)
            {
                throw new InvalidOperationException("Deal the cards first.");
            }

            var newHand = new Card[5];
            if (indexOfCards.Length == 0)
            {
                // redraw all 5 cards
                var newCards = Deck.TakeCards(5);
                Array.Copy(newCards, newHand, 5);
            }
            else
            {
                foreach (var i in indexOfCards)
                {
                    newHand[i] = _hand[i];
                }

                var numTake = 5 - indexOfCards.Length;
                var newCards = Deck.TakeCards(numTake);
                var j = 0;
                for (var i = 0; i < 5; i++)
                {
                    if (newHand[i] == null)
                    {
                        newHand[i] = newCards[j];
                        j++;
                    }
                }
            }

            _hand = newHand.ToArray();
            var handEvaluator = new DefaultHandEvaluator();
            var handResult = handEvaluator.Evaluate(_hand);
            var payAmount = 0m;

            var pay = PaySchedule.SingleOrDefault(p => p.HandType == handResult.HandType && p.NumUnits == bet);
            if (pay != null)
            {
                payAmount = pay.NumUnitPay;
                if (handResult.HandType == HandType.Pair)
                {
                    if (handResult.Cards[0].CardValue < CardValue.Jack)
                    {
                        payAmount = 0;
                    }
                }
            }

            var result = new VideoPokerResult(new HandEvaluationResult(handResult.HandWeight, handResult.HandType, _hand, handResult.Description), payAmount);
            _hand = null;
            return result;
        }
    }
}
