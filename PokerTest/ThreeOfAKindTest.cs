using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Shouldly;
using Poker;
using Poker.HandEvaluator.HandEvalRules;
using Poker.HandEvaluator;

namespace PokerTest
{
    public class ThreeOfAKindTest
    {
        [Fact]
        public void HoustonWeHaveThreeOfAKindTest()
        {
            var hand = new[]
            {
                new Card(CardValue.Deuce, Suit.Clubs),
                new Card(CardValue.Deuce, Suit.Diamonds),
                new Card(CardValue.Jack, Suit.Diamonds),
                new Card(CardValue.Four, Suit.Spades),
                new Card(CardValue.Deuce, Suit.Hearts)
            };

            var result = new ThreeOfAKindEvalRule().Evaluate(hand);

            result.HasValue.ShouldBeTrue();
            result.Value.HandType.ShouldBe(HandType.ThreeOfAKind);
        }

        [Fact]
        public void QuadsNotThreeOfAKindTest()
        {
            var hand = new[]
            {
                new Card(CardValue.Deuce, Suit.Clubs),
                new Card(CardValue.Deuce, Suit.Diamonds),
                new Card(CardValue.Deuce, Suit.Spades),
                new Card(CardValue.Four, Suit.Spades),
                new Card(CardValue.Deuce, Suit.Hearts)
            };

            var result = new ThreeOfAKindEvalRule().Evaluate(hand);

            result.ShouldBeNull();            
        }

        [Fact]
        public void TwoPairNotTripsTest()
        {
            var hand = new[]
            {
                new Card(CardValue.Deuce, Suit.Clubs),
                new Card(CardValue.Deuce, Suit.Diamonds),
                new Card(CardValue.Trey, Suit.Diamonds),
                new Card(CardValue.Trey, Suit.Spades),
                new Card(CardValue.Queen, Suit.Hearts)
            };

            var result = new ThreeOfAKindEvalRule().Evaluate(hand);

            result.ShouldBeNull();            
        }

        [Fact]
        public void FullhouseNotTripsTest()
        {
            var hand = new[]
            {
                new Card(CardValue.Deuce, Suit.Clubs),
                new Card(CardValue.Deuce, Suit.Diamonds),
                new Card(CardValue.Four, Suit.Diamonds),
                new Card(CardValue.Four, Suit.Spades),
                new Card(CardValue.Deuce, Suit.Hearts)
            };

            var result = new ThreeOfAKindEvalRule().Evaluate(hand);

            result.ShouldBeNull();
        }
    }
}
