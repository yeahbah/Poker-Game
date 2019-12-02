using System.Collections.Generic;

namespace PokerGame.Game.interfaces
{
    public interface IPlayer
    {
        IHand Hand { get; set; }
        decimal Stack { get; }

        void BuyIn(decimal amount);

        void JoinGame(IHoldemGame game);
        void ExitGame(IHoldemGame game);

        IEnumerable<IHoldemGame> Games { get; }
    }
}