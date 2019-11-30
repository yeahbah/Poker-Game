using System.Collections.Generic;

namespace Poker
{
    public class Deck
    {
        public Deck(int numDecks = 1)
        {
            InitializeDeck(numDecks);
        }

        private Card[] _cards;
        private void InitializeDeck(int numDecks)
        {
            // 13 card values 4 suits
            // 13 x 4 array
            const int cardRow = 13;
            const int suitColumn = 4;

            var cardList = new List<Card>();
            for (var deck=0; deck < numDecks; deck++)
            {
                for (var col = 1; col <= suitColumn; col++)
                {
                    for (var row = 1; row <= cardRow; row++)
                    {
                        var card = new Card((CardValue)row, (Suit)col);
                        cardList.Add(card);
                    }
                }
            }

            _cards = cardList.ToArray();
        }

        public Card[] Cards => _cards;
    }
}
