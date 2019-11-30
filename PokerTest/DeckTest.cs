using Poker;
using Shouldly;
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
            var cardRow = 13;
            for (var col = 1; col <= suitColumn; col++)
            {
                for (var row = 1; row <= cardRow; row++)
                {
                    var card = new Card((CardValue)row, (Suit)col);

                    deck.Cards
                        .Where(c => c == card)
                        .Count()
                        .ShouldBe(numDecks);
                }
            }

        }
    }
}
