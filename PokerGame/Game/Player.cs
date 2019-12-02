using PokerGame.Game.interfaces;
using System.Collections.Generic;

namespace PokerGame.Game
{
    public class Player : IPlayer
    {
        private decimal _stack = 0;
        public Player(decimal buyInAmount)
        {
            BuyIn(buyInAmount);
            _games = new List<IHoldemGame>();
        }

        public IHand Hand { get; set; }
        public decimal Stack => _stack;

        private readonly List<IHoldemGame> _games;
        public IEnumerable<IHoldemGame> Games => _games;

        public void BuyIn(decimal amount)
        {
            _stack += amount;
        }

        public void JoinGame(IHoldemGame game)
        {
            if (!_games.Contains(game))
            {
                // update stack size of player in the game
                _games.Add(game);
            }
        }

        public void ExitGame(IHoldemGame game)
        {
            if (_games.Contains(game))
            {
                _games.Remove(game);
            }
        }
    }
}
