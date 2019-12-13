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
        [InlineData(14, 4, "As")]
        [InlineData(14, 3, "Ah")]
        [InlineData(14, 2, "Ac")]
        [InlineData(14, 1, "Ad")]
        [InlineData(10, 4, "Ts")]
        [InlineData(10, 3, "Th")]
        [InlineData(10, 2, "Tc")]
        [InlineData(10, 1, "Td")]
        [InlineData(5, 4, "5s")]
        [InlineData(5, 3, "5h")]
        [InlineData(5, 2, "5c")]
        [InlineData(5, 1, "5d")]
        [InlineData(8, 4, "8s")]
        [InlineData(8, 3, "8h")]
        [InlineData(8, 2, "8c")]
        [InlineData(8, 1, "8d")]
        [InlineData(13, 4, "Ks")]
        [InlineData(13, 3, "Kh")]
        [InlineData(13, 2, "Kc")]
        [InlineData(13, 1, "Kd")]
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
            cards[8].ShouldBe( cards.Single(c => c.CardValue == CardValue.Deuce) );
            cards[7].ShouldBe(cards.Single(c => c.CardValue == CardValue.Trey));
            cards[6].ShouldBe(cards.Single(c => c.CardValue == CardValue.Six));
            cards[5].ShouldBe(cards.Single(c => c.CardValue == CardValue.Ten));
            cards[4].ShouldBe(cards.Single(c => c.CardValue == CardValue.Jack));
            cards[3].ShouldBe(cards.Single(c => c.CardValue == CardValue.Queen));
            cards[2].ShouldBe(cards.Single(c => c.CardValue == CardValue.King));
            cards[1].ShouldBe(cards.Single(c => c.CardValue == CardValue.Ace && c.Suit == Suit.Diamonds));
            cards[0].ShouldBe(cards.Single(c => c.CardValue == CardValue.Ace && c.Suit == Suit.Spades));
        }

        //[Fact]
        //public void SortingTest2()
        //{
        //    var cards = new[] {
        //        new Card(CardValue.Jack, Suit.Clubs),
        //        new Card(CardValue.Jack, Suit.Diamonds),
        //        new Card(CardValue.Ace, Suit.Diamonds),
        //        new Card(CardValue.Six, Suit.Diamonds),
        //        new Card(CardValue.Queen, Suit.Clubs)
        //    };

        //    Array.Sort(cards);
        //    cards[4].ShouldBe(cards.Single(c => c.CardValue == CardValue.Six));
        //    cards[3].ShouldBe(cards.Single(c => c.CardValue == CardValue.Queen));
        //    cards[2].ShouldBe(cards.Single(c => c.CardValue == CardValue.Ace));
        //    cards[1].ShouldBe(cards.Single(c => c.CardValue == CardValue.Jack));
        //    cards[0].ShouldBe(cards.Single(c => c.CardValue == CardValue.Jack));
        //}

        [Fact]
        public void CreateFromShortCodeExceptionTest()
        {
            Should.Throw<Exception>(() => Card.InstanceFromShortCode("Xs"));
            Should.Throw<Exception>(() => Card.InstanceFromShortCode("Y"));
            Should.Throw<Exception>(() => Card.InstanceFromShortCode("10"));
            Should.Throw<Exception>(() => Card.InstanceFromShortCode("11c"));
            Should.Throw<Exception>(() => Card.InstanceFromShortCode(""));

        }

        [Theory]
        [InlineData("Ts", CardValue.Ten, Suit.Spades)]
        [InlineData("Jc", CardValue.Jack, Suit.Clubs)]
        [InlineData("Qd", CardValue.Queen, Suit.Diamonds)]
        [InlineData("Kh", CardValue.King, Suit.Hearts)]
        [InlineData("As", CardValue.Ace, Suit.Spades)]
        [InlineData("2s", CardValue.Deuce, Suit.Spades)]
        [InlineData("3c", CardValue.Trey, Suit.Clubs)]
        [InlineData("4d", CardValue.Four, Suit.Diamonds)]
        [InlineData("5h", CardValue.Five, Suit.Hearts)]
        [InlineData("6s", CardValue.Six, Suit.Spades)]
        [InlineData("7c", CardValue.Seven, Suit.Clubs)]
        [InlineData("8h", CardValue.Eight, Suit.Hearts)]
        [InlineData("9d", CardValue.Nine, Suit.Diamonds)]
        public void CreateFromShortCodeTest(string shortCode, CardValue expectedCardValue, Suit expectedSuit)
        {
            var card = Card.InstanceFromShortCode(shortCode);
            card.CardValue.ShouldBe(expectedCardValue);
            card.Suit.ShouldBe(expectedSuit);
        }
    }
}
