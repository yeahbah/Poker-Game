using Poker;
using Shouldly;
using System;
using System.Linq;
using Xunit;

namespace PokerTest
{
    public class CardTest
    {
        [Fact]
        public void EqualCards_SameSuitTest()
        {
            var card1 = new Card(CardValue.Ace, Suit.Clubs);
            var card2 = new Card(CardValue.Ace, Suit.Clubs);

            (card1 == card2).ShouldBeTrue();
            (card1.Equals(card2)).ShouldBeTrue();
            (card1.GetHashCode() == card2.GetHashCode()).ShouldBeTrue();
        }

        [Fact]        
        public void EqualCards_NotSameSuitTest()
        {
            var card1 = new Card(CardValue.Ace, Suit.Clubs);
            var card2 = new Card(CardValue.Ace, Suit.Diamonds);

            (card1 != card2).ShouldBeTrue();
            (card1.Equals(card2)).ShouldBeFalse();
        }

        [Fact]
        public void NotEqualCards_Test()
        {
            var card1 = new Card(CardValue.Deuce, Suit.Diamonds);
            var card2 = new Card(CardValue.Trey, Suit.Hearts);

            (card1 == card2).ShouldBeFalse();
            (card1.Equals(card2)).ShouldBeFalse();
            (card1.GetHashCode() == card2.GetHashCode()).ShouldBeFalse();
        }

        [Theory]
        [InlineData(59, 4, "As")]
        [InlineData(59, 3, "Ah")]
        [InlineData(59, 2, "Ac")]
        [InlineData(59, 1, "Ad")]
        [InlineData(41, 4, "Ts")]
        [InlineData(41, 3, "Th")]
        [InlineData(41, 2, "Tc")]
        [InlineData(41, 1, "Td")]
        [InlineData(19, 4, "5s")]
        [InlineData(19, 3, "5h")]
        [InlineData(19, 2, "5c")]
        [InlineData(19, 1, "5d")]
        [InlineData(31, 4, "8s")]
        [InlineData(31, 3, "8h")]
        [InlineData(31, 2, "8c")]
        [InlineData(31, 1, "8d")]
        [InlineData(53, 4, "Ks")]
        [InlineData(53, 3, "Kh")]
        [InlineData(53, 2, "Kc")]
        [InlineData(53, 1, "Kd")]
        public void ShortCodeTest(int cardValueIndex, int suitIndex, string expected)
        {
            var cardValue = (CardValue)cardValueIndex;
            var suit = (Suit)suitIndex;
            var card = new Card(cardValue, suit);
            card.ShortCode.ShouldBe(expected);
        }

        [Fact]
        public void SortingTest()
        {
            var cards = new[] { 
                new Card(CardValue.Jack, Suit.Clubs),
                new Card(CardValue.Deuce, Suit.Diamonds),
                new Card(CardValue.Trey, Suit.Diamonds),
                new Card(CardValue.Ace, Suit.Diamonds),
                new Card(CardValue.Ace, Suit.Spades),
                new Card(CardValue.Six, Suit.Diamonds),
                new Card(CardValue.Queen, Suit.Clubs),
                new Card(CardValue.King, Suit.Hearts),
                new Card(CardValue.Ten, Suit.Spades),
            };
           
            Array.Sort(cards);
            cards[0].ShouldBe( cards.Single(c => c.CardValue == CardValue.Deuce) );
            cards[1].ShouldBe(cards.Single(c => c.CardValue == CardValue.Trey));
            cards[2].ShouldBe(cards.Single(c => c.CardValue == CardValue.Six));
            cards[3].ShouldBe(cards.Single(c => c.CardValue == CardValue.Ten));
            cards[4].ShouldBe(cards.Single(c => c.CardValue == CardValue.Jack));
            cards[5].ShouldBe(cards.Single(c => c.CardValue == CardValue.Queen));
            cards[6].ShouldBe(cards.Single(c => c.CardValue == CardValue.King));
            cards[7].ShouldBe(cards.Single(c => c.CardValue == CardValue.Ace && c.Suit == Suit.Diamonds));
            cards[8].ShouldBe(cards.Single(c => c.CardValue == CardValue.Ace && c.Suit == Suit.Spades));
        }
    }
}
