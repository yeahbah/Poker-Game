using Poker;
using Poker.HandEvaluator;
using Poker.HandEvaluator.HandEvalRules;
using Shouldly;
using Xunit;

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

            var result = new ThreeOfAKindHand().Evaluate(hand);

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

            var result = new ThreeOfAKindHand().Evaluate(hand);

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

            var result = new ThreeOfAKindHand().Evaluate(hand);

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

            var result = new ThreeOfAKindHand().Evaluate(hand);

            result.ShouldBeNull();
        }
    }
}
