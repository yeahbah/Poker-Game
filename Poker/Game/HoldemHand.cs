using Poker.Game.interfaces;

namespace Poker.Game
{
    public class HoldemHand : IHand
    {
        public Card[] Cards { get; set; }

        public override string ToString()
        {
            return Cards[0].ShortCode + Cards[1].ShortCode;                
        }
    }
}
