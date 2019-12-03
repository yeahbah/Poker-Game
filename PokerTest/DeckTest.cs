using Poker;
using Shouldly;
using System;
using System.Linq;
using Xunit;

namespace PokerTest
{
    public class DeckTest
    {
        [Fact]
        public void Deck_Contains_52_Cards_Test()
        {
            var deck = new Deck();
            deck.Cards.Length.ShouldBe(52);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        public void MultiDeck_CardCount_Test(int numDecks)
        {
            var deck = new Deck(numDecks);
            deck.Cards.Length.ShouldBe(numDecks * 52);
        }

        [Fact]
        public void Deck_Has_Unique_Cards()
        {
            var deck = new Deck();
            var distinctCount = deck.Cards.Distinct().Count();
            distinctCount.ShouldBe(52);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        public void Multi_Deck_Test(int numDecks)
        {
            var deck = new Deck(numDecks);

            var suitColumn = 4;
            var cardRow = 14;
            for (var col = 1; col <= suitColumn; col++)
            {
                for (var row = 2; row <= cardRow; row++)
                {
                    var card = new Card((CardValue)row, (Suit)col);

                    deck.Cards
                        .Where(c => c == card)
                        .Count()
                        .ShouldBe(numDecks);
                }
            }
        }

        [Fact]
        public void TakeCardTest()
        {
            var deck = new Deck();

            var random = new Random();
            while (deck.Cards.Length > 0)
            {
                var cardsCount = deck.Cards.Length;
                var cardTaken = deck.TakeCard();

                deck.Cards.Length.ShouldBe(cardsCount - 1);
                Should.Throw<InvalidOperationException>(() => deck.Cards.Single(card => card == cardTaken));
            }

            deck.ResetDeck();
            deck.Cards.Length.ShouldBe(52);

        }

        [Fact]
        public void TakeCardsTest()
        {
            var deck = new Deck();
            var numCards = 2;
            var cards = deck.TakeCards(numCards);

            cards.Length.ShouldBe(numCards);
            deck.Cards.Length.ShouldBe(50);

            deck.TakeCards(5);
            deck.Cards.Length.ShouldBe(45);

            deck.ResetDeck();
            deck.Cards.Length.ShouldBe(52);
        }
    }
}
