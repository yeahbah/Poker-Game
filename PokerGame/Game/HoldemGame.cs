using Poker;
using PokerGame.Game.interfaces;
using System;
using System.Collections.Generic;

namespace PokerGame.Game
{
    public abstract class HoldemGame : IHoldemGame
    {
        protected HoldemGame()
        {
            _players = new List<IPlayer>();
        }

        private List<IPlayer> _players;
        public IEnumerable<IPlayer> Players => _players;

        private short _dealerPosition;
        public short DealerPosition => _dealerPosition;

        public decimal MaxBuyInAmount => GetMaxBuyInAmount();

        protected abstract decimal GetMaxBuyInAmount();

        public short MaxPlayerCount => GetMaxPlayerCount();

        public IDealer Dealer => GetDealer();
        public abstract IDealer GetDealer();

        protected abstract short GetMaxPlayerCount();

        public void NewGame(int numPlayers, decimal smalBlindAmount, decimal bigBlindAmount)
        {
            _dealerPosition = 0;
        }

        public abstract void Start();

        public void AddPlayer(IPlayer player)
        {
            _players.Add(player);
        }
    }
}
