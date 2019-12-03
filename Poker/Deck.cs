using System;
using System.Collections.Generic;
using System.Linq;

namespace Poker
{
    public class Deck : IDeck
    {
        private int _numDecks = 1;
        public Deck(int numDecks = 1)
        {
            _numDecks = numDecks;
            InitializeDeck();
        }

        private Card[] _cards;
        private void InitializeDeck()
        {
            // 13 card (index 2), values 4 suits
            // 13 x 4 array
            const int cardRow = 14; // start index 2
            const int suitColumn = 4;

            var cardList = new List<Card>();
            for (var deck = 0; deck < _numDecks; deck++)
            {
                for (var col = 1; col <= suitColumn; col++)
                {
                    for (var row = 2; row <= cardRow; row++)
                    {
                        var card = new Card((CardValue)row, (Suit)col);
                        cardList.Add(card);
                    }
                }
            }

            _cards = cardList.ToArray();
        }

        public Card[] Cards => _cards;

        protected void RemoveCard(Card card)
        {
            var cardIndex = Array.IndexOf(_cards, card);

            if (cardIndex < 0) return;

            _cards = _cards
                .Where((c, index) => index != cardIndex)
                .ToArray();
        }

        public Card TakeCard()
        {
            var cardIndex = 0;
            if (_cards.Length > 1)
            {
                cardIndex = RandomNumber.Between(0, _cards.Length - 1);
            }

            var result = _cards[cardIndex];
            RemoveCard(result);
            return result;
        }

        public Card[] TakeCards(int numCards)
        {
            if (numCards > _cards.Length)
            {
                throw new InvalidOperationException("Number cards to take is more than the ");
            }

            var result = new List<Card>();
            for (var i = 0; i < numCards; i++)
            {
                var card = TakeCard();
                result.Add(card);
            }

            return result.ToArray();
        }

        public void ResetDeck()
        {
            InitializeDeck();
        }
    }
}
