using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Poker;
using Poker.HandEvaluator.PokerHands;
using Xunit;
using Shouldly;

namespace PokerTest
{
    public class FullhouseTest
    {
        [Fact]
        public void FullhouseBasicTest()
        {
            var hand = new[]
            {
                new Card(CardValue.Deuce, Suit.Clubs),
                new Card(CardValue.Deuce, Suit.Diamonds),
                new Card(CardValue.Four, Suit.Diamonds),
                new Card(CardValue.Four, Suit.Spades),
                new Card(CardValue.Deuce, Suit.Hearts)
            };

            var result = new FullhouseHand().Evaluate(hand);

            result.HasValue.ShouldBeTrue();
            result.Value.HandType.ShouldBe(HandType.Fullhouse);
        }

        [Fact]
        public void ThreeOfAKindNotFullTest()
        {
            var hand = new[]
            {
                new Card(CardValue.Deuce, Suit.Clubs),
                new Card(CardValue.Deuce, Suit.Diamonds),
                new Card(CardValue.Jack, Suit.Diamonds),
                new Card(CardValue.Four, Suit.Spades),
                new Card(CardValue.Deuce, Suit.Hearts)
            };

            var result = new FullhouseHand().Evaluate(hand);

            result.HasValue.ShouldBeFalse();
        }

        [Fact]
        public void QuadsNotFullhouseTest()
        {
            var hand = new[]
            {
                new Card(CardValue.Deuce, Suit.Clubs),
                new Card(CardValue.Deuce, Suit.Diamonds),
                new Card(CardValue.Deuce, Suit.Spades),
                new Card(CardValue.Four, Suit.Spades),
                new Card(CardValue.Deuce, Suit.Hearts)
            };

            var result = new FullhouseHand().Evaluate(hand);

            result.ShouldBeNull();
        }
    }
}
