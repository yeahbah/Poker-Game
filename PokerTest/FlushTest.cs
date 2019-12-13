using Poker;
using Poker.HandEvaluator.PokerHands;
using Shouldly;
using Xunit;

namespace PokerTest
{
    public class FlushTest
    {
        [Fact]
        public void WeGotAFlushTest()
        {
           var hand = new[]
           {
                new Card(CardValue.Deuce, Suit.Clubs),
                new Card(CardValue.Queen, Suit.Clubs),
                new Card(CardValue.Trey, Suit.Clubs),
                new Card(CardValue.Seven, Suit.Clubs),
                new Card(CardValue.King, Suit.Clubs)
            };

            var result = new FlushHand().Evaluate(hand);
            result.HasValue.ShouldBeTrue();
            result.Value.HandType.ShouldBe(HandType.Flush);
        }

        [Fact]
        public void StraightFlushNotARegularFlush()
        {
           var hand = new[]
           {
                new Card(CardValue.Deuce, Suit.Clubs),
                new Card(CardValue.Ace, Suit.Clubs),
                new Card(CardValue.Trey, Suit.Clubs),
                new Card(CardValue.Four, Suit.Clubs),
                new Card(CardValue.Five, Suit.Clubs)
            };

            var result = new FlushHand().Evaluate(hand);
            result.HasValue.ShouldBeFalse();            
        }

        [Fact]
        public void NotARegularFlush()
        {
            var hand = new[]
           {
                new Card(CardValue.Deuce, Suit.Clubs),
                new Card(CardValue.King, Suit.Spades),
                new Card(CardValue.Trey, Suit.Clubs),
                new Card(CardValue.Four, Suit.Hearts),
                new Card(CardValue.Eight, Suit.Clubs)
            };

            var result = new FlushHand().Evaluate(hand);
            result.HasValue.ShouldBeFalse();
        }
    }
}
