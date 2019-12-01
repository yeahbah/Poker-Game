using Poker.Game.interfaces;
using System;

namespace Poker.Game
{
    public class HoldemDealer : IHoldemDealer
    {
        private readonly IDeck _deck;

        public HoldemDealer()
        {
            _deck = new Deck();
        }

        public Card[] DealHand()
        {
            return _deck.TakeCards(2);
        }

        public Card[] DealFlop()
        {
            return _deck.TakeCards(3);
        }

        public Card DealRiver()
        {
            return _deck.TakeCard();
        }

        public Card DealTurn()
        {
            return _deck.TakeCard();
        }

        public Card ShowRabbitCard()
        {
            return _deck.TakeCard();
        }

        public void Shuffle()
        {
            _deck.ResetDeck();
        }
    }
}
