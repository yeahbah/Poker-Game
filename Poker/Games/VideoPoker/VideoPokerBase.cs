using Poker.HandEvaluator;
using Poker.HandEvaluator.PokerHands;
using System;
using System.Collections.Generic;
using System.Linq;
using Stateless;

namespace Poker.Games.VideoPoker
{
    public class VideoPokerBase : IVideoPoker
    {
        private GameVars _gameVars;        

        public VideoPokerBase()
        {
            _gameVars = new GameVars();
        }

        // private void OnDrawCards(int[] heldCardIndeces)
        // {
        //     var newHand = new Card[5];
        //     if (heldCardIndeces.Length == 0)
        //     {
        //         // redraw all 5 cards
        //         var newCards = Deck.TakeCards(5);
        //         Array.Copy(newCards, newHand, 5);
        //     }
        //     else
        //     {
        //         foreach (var i in heldCardIndeces)
        //         {
        //             newHand[i] = _hand[i];
        //         }

        //         var numTake = 5 - heldCardIndeces.Length;
        //         var newCards = Deck.TakeCards(numTake);
        //         var j = 0;
        //         for (var i = 0; i < 5; i++)
        //         {
        //             if (newHand[i] == null)
        //             {
        //                 newHand[i] = newCards[j];
        //                 j++;
        //             }
        //         }
        //     }
        //     _drawResult = newHand;
        // }

        //public GameState Status => _stateMachine.State;

        public void NewGame()
        {

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

        public void SelectBetSize(short betSize) 
        {
            if (betSize <= 0)
                return;

            _gameVars.BetSize = betSize;
        }

        private Card[] _hand;
        public Card[] Hand => _hand;

        public IList<PayShedule> PaySchedule { get; set; }


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
            _hand = Deck.TakeCards(5);

            var result = new DefaultHandEvaluator().Evaluate(_hand);         
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
                    newHand[i] = _hand[i];
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

            _hand = newHand.ToArray();
            var handEvaluator = new DefaultHandEvaluator();
            var handResult = handEvaluator.Evaluate(_hand);
            var payoutInUnits = 0m;

            var pay = PaySchedule.SingleOrDefault(p => p.HandType == handResult.HandType && p.BetSize == _gameVars.BetSize);
            if (pay != null)
            {
                payoutInUnits = pay.PaySizeInUnits;
                if (handResult.HandType == HandType.Pair)
                {
                    if (handResult.Cards[0].CardValue < CardValue.Jack)
                    {
                        payoutInUnits = 0;
                    }
                }
            }            
            
            var payoutMoney = payoutInUnits * _gameVars.UnitSize;    
            
            var result = new VideoPokerResult(new HandEvaluationResult(handResult.HandWeight, handResult.HandType, _hand, handResult.Description), payoutInUnits, payoutMoney);
            _gameVars.Money += payoutInUnits * _gameVars.BetSizeMoney;
            _hand = null;

            return result;
        }        
    }
}
