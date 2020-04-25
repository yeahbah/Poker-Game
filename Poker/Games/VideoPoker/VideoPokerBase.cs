using Poker.HandEvaluator;
using Poker.HandEvaluator.PokerHands;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Poker.Games.VideoPoker
{
    public class VideoPokerBase : IVideoPoker
    {
        private GameVars _gameVars;        

        public VideoPokerBase()
        {
            _gameVars = new GameVars();
        }

        public void NewGame()
        {
            Hand = Array.Empty<Card>();
        }

        public void DepositMoney(decimal depositAmount)
        {
            if (depositAmount <= 0)
                return;

            _gameVars.Money += depositAmount;
        }

        public void SelectUnitSize(decimal unitSize) 
        {
            _gameVars.UnitSize = unitSize;
        }

        public void SelectBetSize(int betSize) 
        {
            if (betSize <= 0)
                return;

            _gameVars.BetSize = betSize;
        }

        public Card[] Hand { get; private set; }

        public IList<PaySchedule> PaySchedule { get; set; }


        public VideoPokerBase(IDeck deck)
        {
            this.Deck = deck;

        }
        public IDeck Deck { get; set; }
        public GameVars GameVars 
        { 
            get => _gameVars; 
            set { _gameVars = value; } 
        }        

        public HandEvaluationResult Deal()
        {
            if (_gameVars.Money <= 0)
            {
                throw new VideoPokerException("Please deposit money to play.");
            }

            _gameVars.Money -= _gameVars.BetSizeMoney;

            Deck.ResetDeck();
            Hand = Deck.TakeCards(5);

            var result = new DefaultHandEvaluator().Evaluate(Hand);         
            return result;
        }

        public VideoPokerResult Draw(int[] heldCardIndeces)
        {
            var newHand = new Card[5];
            if (heldCardIndeces.Length == 0)
            {
                // redraw all 5 cards
                var newCards = Deck.TakeCards(5);
                Array.Copy(newCards, newHand, 5);
            }
            else
            {
                foreach (var i in heldCardIndeces)
                {
                    newHand[i] = Hand[i];
                }

                var numTake = 5 - heldCardIndeces.Length;
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

            Hand = newHand.ToArray();
            var handEvaluator = new DefaultHandEvaluator();
            var handResult = handEvaluator.Evaluate(Hand);
            var payoutInUnits = 0m;

            // I think this section should be in descendant classes
            var pay = PaySchedule.SingleOrDefault(p => p.HandType == handResult.HandType && p.BetSize == _gameVars.BetSize);
            if (pay != null)
            {
                payoutInUnits = pay.PaySizeInUnits;
                if (handResult.HandType == HandType.Pair)
                {
                    if (handResult.Cards.First().CardValue < CardValue.Jack)
                    {
                        payoutInUnits = 0;
                    }
                }
            }            
            //TODO move payout routine in descendant classes
            
            var payoutMoney = payoutInUnits * _gameVars.UnitSize;
            _gameVars.Money += payoutMoney;
            var result = new VideoPokerResult(handResult, payoutInUnits, payoutMoney);

            Hand = Array.Empty<Card>();

            return result;
        }

        public VideoPokerResult Draw(Card[] cards, int[] heldCardsIndeces)
        {
            Hand = cards.ToArray();
            return Draw(heldCardsIndeces);
        }
    }
}
