using Poker;
using Poker.HandEvaluator;
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

        [Fact]
        public void StraightVsStraight_DemolishedTest()
        {
           var hand1 = new[]
           {
                new Card(CardValue.Deuce, Suit.Clubs),
                new Card(CardValue.Trey, Suit.Clubs),
                new Card(CardValue.Four, Suit.Clubs),
                new Card(CardValue.Five, Suit.Clubs),
                new Card(CardValue.Ace, Suit.Diamonds)
            };
            var result1 = new StraightHand().Evaluate(hand1);

            var hand2 = new[]
           {
                new Card(CardValue.Six, Suit.Spades),
                new Card(CardValue.Trey, Suit.Spades),
                new Card(CardValue.Four, Suit.Spades),
                new Card(CardValue.Five, Suit.Hearts),
                new Card(CardValue.Deuce, Suit.Diamonds)
            };
            var result2 = new StraightHand().Evaluate(hand2);

            result1.HasValue.ShouldBeTrue();
            result1.Value.HandWeight.ShouldBeLessThan(result2.Value.HandWeight);  
        }

        [Fact]
        public void StraightFlushVsRoyal_BadBeatTest()
        {
            var hand1 = new[]
            {
                new Card(CardValue.Ace, Suit.Clubs),
                new Card(CardValue.Ten, Suit.Clubs),
                new Card(CardValue.Queen, Suit.Clubs),
                new Card(CardValue.Jack, Suit.Clubs),
                new Card(CardValue.King, Suit.Clubs)
            };
            var result1 = new StraightHand().Evaluate(hand1);

            var hand2 = new[]
           {
                new Card(CardValue.Nine, Suit.Spades),
                new Card(CardValue.Jack, Suit.Spades),
                new Card(CardValue.King, Suit.Spades),
                new Card(CardValue.Queen, Suit.Spades),
                new Card(CardValue.Ten, Suit.Spades)
            };
            var result2 = new StraightHand().Evaluate(hand2);

            result1.HasValue.ShouldBeTrue();
            result2.HasValue.ShouldBeTrue();
            result1.Value.HandType.ShouldBe(HandType.RoyalFlush);
            result2.Value.HandType.ShouldBe(HandType.StraightFlush);
            
            result1.Value.HandWeight.ShouldBeGreaterThan(result2.Value.HandWeight);
        }

        [Fact]
        public void StraightFlushVsStraightFlush_DestroyedTest()
        {
            var hand1 = new[]
            {
                new Card(CardValue.Ace, Suit.Clubs),
                new Card(CardValue.Deuce, Suit.Clubs),
                new Card(CardValue.Four, Suit.Clubs),
                new Card(CardValue.Five, Suit.Clubs),
                new Card(CardValue.Trey, Suit.Clubs)
            };
            var result1 = new StraightHand().Evaluate(hand1);

            var hand2 = new[]
           {
                new Card(CardValue.Deuce, Suit.Spades),
                new Card(CardValue.Trey, Suit.Spades),
                new Card(CardValue.Four, Suit.Spades),
                new Card(CardValue.Five, Suit.Spades),
                new Card(CardValue.Six, Suit.Spades)
            };
            var result2 = new StraightHand().Evaluate(hand2);

            result1.HasValue.ShouldBeTrue();
            result2.HasValue.ShouldBeTrue();
            result1.Value.HandType.ShouldBe(HandType.StraightFlush);
            result2.Value.HandType.ShouldBe(HandType.StraightFlush);

            result1.Value.HandWeight.ShouldBeLessThan(result2.Value.HandWeight);
        }

        [Fact]
        public void StraightVsStraight_Chop()
        {
            var hand1 = new[]
           {
                new Card(CardValue.Seven, Suit.Clubs),
                new Card(CardValue.Eight, Suit.Clubs),
                new Card(CardValue.Nine, Suit.Clubs),
                new Card(CardValue.Six, Suit.Clubs),
                new Card(CardValue.Ten, Suit.Diamonds)
            };
            var result1 = new StraightHand().Evaluate(hand1);

            var hand2 = new[]
           {
                new Card(CardValue.Six, Suit.Spades),
                new Card(CardValue.Seven, Suit.Spades),
                new Card(CardValue.Eight, Suit.Spades),
                new Card(CardValue.Nine, Suit.Hearts),
                new Card(CardValue.Ten, Suit.Diamonds)
            };
            var result2 = new StraightHand().Evaluate(hand2);

            result1.HasValue.ShouldBeTrue();
            result1.Value.HandWeight.ShouldBe(result2.Value.HandWeight);
        }
    }
}
