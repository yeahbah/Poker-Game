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
    }
}
