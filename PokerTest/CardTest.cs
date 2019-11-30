using Poker;
using Shouldly;
using Xunit;

namespace PokerTest
{
    public class CardTest
    {
        [Fact]
        public void EqualCards_Test()
        {
            var card1 = new Card(CardValue.Ace, Suit.Clubs);
            var card2 = new Card(CardValue.Ace, Suit.Clubs);

            (card1 == card2).ShouldBeTrue();
            (card1.Equals(card2)).ShouldBeTrue();
            (card1.GetHashCode() == card2.GetHashCode()).ShouldBeTrue();
        }

        [Fact]
        public void NotEqualCards_Test()
        {
            var card1 = new Card(CardValue.Deuce, Suit.Diamonds);
            var card2 = new Card(CardValue.Deuce, Suit.Hearts);

            (card1 == card2).ShouldBeFalse();
            (card1.Equals(card2)).ShouldBeFalse();
            (card1.GetHashCode() == card2.GetHashCode()).ShouldBeFalse();
        }

        [Theory]
        [InlineData(1, 1, "As")]
        [InlineData(1, 2, "Ah")]
        [InlineData(1, 3, "Ac")]
        [InlineData(1, 4, "Ad")]
        [InlineData(10, 1, "Ts")]
        [InlineData(10, 2, "Th")]
        [InlineData(10, 3, "Tc")]
        [InlineData(10, 4, "Td")]
        [InlineData(5, 1, "5s")]
        [InlineData(5, 2, "5h")]
        [InlineData(5, 3, "5c")]
        [InlineData(5, 4, "5d")]
        [InlineData(8, 1, "8s")]
        [InlineData(8, 2, "8h")]
        [InlineData(8, 3, "8c")]
        [InlineData(8, 4, "8d")]
        [InlineData(13, 1, "Ks")]
        [InlineData(13, 2, "Kh")]
        [InlineData(13, 3, "Kc")]
        [InlineData(13, 4, "Kd")]
        public void ShortCodeTest(int cardValueIndex, int suitIndex, string expected)
        {
            var cardValue = (CardValue)cardValueIndex;
            var suit = (Suit)suitIndex;
            var card = new Card(cardValue, suit);
            card.ShortCode.ShouldBe(expected);
        }
    }
}
