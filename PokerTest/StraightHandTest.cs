using Poker;
using Poker.HandEvaluator;
using Poker.HandEvaluator.HandEvalRules;
using Poker.HandEvaluator.PokerHands;
using Shouldly;
using Xunit;

namespace PokerTest
{
    public class StraightHandTest
    {
        [Fact]
        public void StraightTest()
        {
           var hand = new[]
           {
                new Card(CardValue.Deuce, Suit.Clubs),
                new Card(CardValue.Trey, Suit.Hearts),
                new Card(CardValue.Four, Suit.Diamonds),
                new Card(CardValue.Five, Suit.Spades),
                new Card(CardValue.Six, Suit.Hearts)
            };

            var result = new StraightHand().Evaluate(hand);
            result.HasValue.ShouldBeTrue();
            result.Value.HandType.ShouldBe(HandType.Straight);
        }

        [Fact]
        public void StraightFlushTest()
        {
           var hand = new[]
           {
                new Card(CardValue.Deuce, Suit.Clubs),
                new Card(CardValue.Trey, Suit.Clubs),
                new Card(CardValue.Four, Suit.Clubs),
                new Card(CardValue.Five, Suit.Clubs),
                new Card(CardValue.Six, Suit.Clubs)
            };

            var result = new StraightHand().Evaluate(hand);
            result.HasValue.ShouldBeTrue();
            result.Value.HandType.ShouldBe(HandType.StraightFlush);
        }

        [Fact]
        public void RoyalFlushTest()
        {
           var hand = new[]
           {
                new Card(CardValue.Ten, Suit.Clubs),
                new Card(CardValue.Queen, Suit.Clubs),
                new Card(CardValue.Ace, Suit.Clubs),
                new Card(CardValue.Jack, Suit.Clubs),
                new Card(CardValue.King, Suit.Clubs)
            };

            var result = new StraightHand().Evaluate(hand);
            result.HasValue.ShouldBeTrue();
            result.Value.HandType.ShouldBe(HandType.RoyalFlush);
        }

        [Fact]
        public void NotAStraight()
        {
           var hand = new[]
           {
                new Card(CardValue.Ten, Suit.Clubs),
                new Card(CardValue.Nine, Suit.Clubs),
                new Card(CardValue.Eight, Suit.Clubs),
                new Card(CardValue.Deuce, Suit.Diamonds),
                new Card(CardValue.Seven, Suit.Clubs)
            };

            var result = new StraightHand().Evaluate(hand);
            result.HasValue.ShouldBeFalse();         
        }

        [Fact]
        public void WheelTest()
        {
            var hand = new[]
           {
                new Card(CardValue.Deuce, Suit.Clubs),
                new Card(CardValue.Trey, Suit.Clubs),
                new Card(CardValue.Four, Suit.Clubs),
                new Card(CardValue.Five, Suit.Clubs),
                new Card(CardValue.Ace, Suit.Diamonds)
            };

            var result = new StraightHand().Evaluate(hand);
            result.HasValue.ShouldBeTrue();
            result.Value.HandType.ShouldBe(HandType.Straight);
        }
    }
}
