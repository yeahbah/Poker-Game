using Poker;
using Poker.HandEvaluator;
using Poker.HandEvaluator.PokerHands;
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

        [Fact]
        public void SetOverSetTest()
        {
            var hand1 = new[]
            {
                new Card(CardValue.Deuce, Suit.Clubs),
                new Card(CardValue.Deuce, Suit.Diamonds),
                new Card(CardValue.Six, Suit.Diamonds),
                new Card(CardValue.Four, Suit.Spades),
                new Card(CardValue.Deuce, Suit.Hearts)
            };
            var result1 = new ThreeOfAKindHand().Evaluate(hand1);

            var hand2 = new[]
            {
                new Card(CardValue.Ace, Suit.Clubs),
                new Card(CardValue.Ace, Suit.Diamonds),
                new Card(CardValue.Six, Suit.Diamonds),
                new Card(CardValue.Four, Suit.Spades),
                new Card(CardValue.Ace, Suit.Hearts)
            };
            var result2 = new ThreeOfAKindHand().Evaluate(hand2);

            result1.ShouldNotBeNull();
            result1.Value.HandWeight.ShouldBeLessThan(result2.Value.HandWeight);
        }
    }
}
