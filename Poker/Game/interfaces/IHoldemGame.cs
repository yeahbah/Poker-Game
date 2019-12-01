using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Game.interfaces
{
    public interface IHoldemGame
    {
        void NewGame(int numPlayers, decimal smallBlindAmount, decimal bigBlindAmount);

        // position index and player
        IEnumerable<IPlayer> Players { get; }

        // add a new player into the game
        void AddPlayer(IPlayer player);

        short DealerPosition { get; }

        // set dealer and blinds, deal cards
        void Start();

        decimal MaxBuyInAmount { get;  }

        short MaxPlayerCount { get; }

        IDealer Dealer { get; }

    }
}
