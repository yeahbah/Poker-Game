using Poker;

namespace PokerGame.Game.NoLimitHoldem
{
    public class HoldemDealer : IDealer
    {
        private Deck _deck;
        public HoldemDealer()
        {
            _deck = new Deck();
        }

        public Card[] DealHand()
        {
            return _deck.TakeCards(2);
        }

        public void Shuffle()
        {
            _deck.ResetDeck();
        }
    }
}
