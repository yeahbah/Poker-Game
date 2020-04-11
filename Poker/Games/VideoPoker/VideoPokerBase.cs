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

        // StateMachine<GameState, GameTrigger> _stateMachine;
        // StateMachine<GameState, GameTrigger>.TriggerWithParameters<decimal> _depositTrigger;
        // StateMachine<GameState, GameTrigger>.TriggerWithParameters<decimal> _selectUnitSize;
        // StateMachine<GameState, GameTrigger>.TriggerWithParameters<short> _selectBetSize;
        // StateMachine<GameState, GameTrigger>.TriggerWithParameters<int[]> _drawCards;

        private GameVars _gameVars;        

        public VideoPokerBase()
        {
            _gameVars = new GameVars();

            // _stateMachine = new StateMachine<GameState, GameTrigger>(GameState.Idle);
            // _depositTrigger = _stateMachine.SetTriggerParameters<decimal>(GameTrigger.Deposit);
            // _selectUnitSize = _stateMachine.SetTriggerParameters<decimal>(GameTrigger.SelectUnitSize);
            // _selectBetSize = _stateMachine.SetTriggerParameters<short>(GameTrigger.SelectBetSize);
            // _drawCards = _stateMachine.SetTriggerParameters<int[]>(GameTrigger.Draw);
            
            
            // _stateMachine.Configure(GameState.Idle)                
            //     .Permit(GameTrigger.Deposit, GameState.MoneyDeposited)
            //     .Permit(GameTrigger.SelectUnitSize, GameState.UnitSizeSelected);

            // _stateMachine.Configure(GameState.MoneyDeposited)
            //     .OnEntryFrom(_depositTrigger, OnDeposit)            
            //     .Permit(GameTrigger.SelectUnitSize, GameState.UnitSizeSelected);                    

            // _stateMachine.Configure(GameState.UnitSizeSelected)
            //     .OnEntryFrom(_selectUnitSize, OnSelectUnitSize)
            //     .Permit(GameTrigger.SelectBetSize, GameState.BetSizeSelected);

            // _stateMachine.Configure(GameState.BetSizeSelected)
            //     .OnEntryFrom(_selectBetSize, OnSelectBetSize) 
            //     .Permit(GameTrigger.NewGame, GameState.NewGame)
            //     .Permit(GameTrigger.Deal, GameState.DealtCards); 

            // _stateMachine.Configure(GameState.NewGame)
            //     .Permit(GameTrigger.Deal, GameState.DealtCards);

            // _stateMachine.Configure(GameState.DealtCards)
            //     //.OnEntryFrom(_drawCards, OnDrawCards)
            //     .Permit(GameTrigger.Draw, GameState.CardsDrawn);

            // _stateMachine.Configure(GameState.CardsDrawn)
            //     .Permit(GameTrigger.NewGame, GameState.NewGame);
        }

        private void OnDrawCards(int[] heldCardIndeces)
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
            _drawResult = newHand;
        }

        //public GameState Status => _stateMachine.State;

        public void NewGame()
        {
            //_stateMachine.Fire(GameTrigger.NewGame);            
            _hand = null;
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

        private Card[] _drawResult;
        public Card[] DrawResult => _drawResult;

        public HandEvaluationResult Deal()
        {
            if (_gameVars.Money <= 0)
            {
                throw new VideoPokerException("Please deposit money to play.");
            }

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
            
            var payoutValue = payoutInUnits * _gameVars.BetSize;            
            var result = new VideoPokerResult(handResult, payoutInUnits, payoutValue);
            _gameVars.Money += payoutInUnits * _gameVars.AbsoluteBetSize;
            _hand = null;

            return result;
        }        
    }
}
