using Poker.HandEvaluator;
using Poker.HandEvaluator.PokerHands;
using System;
using System.Collections.Generic;
using System.Linq;
using Stateless;

namespace Poker.Games.VideoPoker
{
    public class VideoPokerBase : IVideoPoker, IVideoPokerGameFlow
    {

        StateMachine<GameState, GameTrigger> _stateMachine;
        StateMachine<GameState, GameTrigger>.TriggerWithParameters<decimal> _depositTrigger;
        StateMachine<GameState, GameTrigger>.TriggerWithParameters<decimal> _selectUnitSize;
        StateMachine<GameState, GameTrigger>.TriggerWithParameters<short> _selectBetSize;

        private GameVars _gameVars;

        public VideoPokerBase()
        {
            _gameVars = new GameVars();

            _stateMachine = new StateMachine<GameState, GameTrigger>(GameState.Idle);
            _depositTrigger = _stateMachine.SetTriggerParameters<decimal>(GameTrigger.Deposit);
            _selectUnitSize = _stateMachine.SetTriggerParameters<decimal>(GameTrigger.SelectUnitSize);
            _selectBetSize = _stateMachine.SetTriggerParameters<short>(GameTrigger.SelectBetSize);

            _stateMachine.Configure(GameState.Idle)                
                .Permit(GameTrigger.Deposit, GameState.MoneyDeposited)
                .Permit(GameTrigger.SelectUnitSize, GameState.UnitSizeSelected);


            _stateMachine.Configure(GameState.MoneyDeposited)
                .OnEntryFrom(_depositTrigger, OnDeposit)            
                .Permit(GameTrigger.SelectUnitSize, GameState.UnitSizeSelected);                    

            _stateMachine.Configure(GameState.UnitSizeSelected)
                .OnEntryFrom(_selectUnitSize, OnSelectUnitSize)
                .Permit(GameTrigger.SelectBetSize, GameState.BetSizeSelected);

            _stateMachine.Configure(GameState.BetSizeSelected)
                .OnEntryFrom(_selectBetSize, OnSelectBetSize) 
                .Permit(GameTrigger.NewGame, GameState.NewGame)
                .Permit(GameTrigger.Deal, GameState.DealtCards);

            _stateMachine.Configure(GameState.NewGame)
                .Permit(GameTrigger.Deal, GameState.DealtCards)
                .Permit(GameTrigger.SelectBetSize, GameState.BetSizeSelected);
        }

        public GameState Status => _stateMachine.State;

        public void NewGame()
        {
            _stateMachine.Fire(GameTrigger.NewGame);
        }

        public void DepositMoney(decimal depositAmount)
        {
            if (depositAmount <= 0)
                return;

            _stateMachine.Fire(_depositTrigger, depositAmount);
        }

        public void SelectUnitSize(decimal unitSize)
        {
            _stateMachine.Fire(_selectUnitSize, unitSize);
        }

        public void SelectBetSize(short betSize)
        {
            _stateMachine.Fire(_selectBetSize, betSize);
        }

        private void OnDeposit(decimal depositAmount)
        {
            _gameVars.Money += depositAmount;
        }

        private void OnSelectUnitSize(decimal unitSize)
        {
            _gameVars.UnitSize = unitSize;
        }

        private void OnSelectBetSize(short betSize)
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

            var pay = PaySchedule.SingleOrDefault(p => p.HandType == handResult.HandType && p.BetSize == bet);
            if (pay != null)
            {
                payAmount = pay.PaySizeInUnits;
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
