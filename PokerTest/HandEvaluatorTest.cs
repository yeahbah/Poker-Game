using Poker;
using Poker.HandEvaluator;
using Poker.HandEvaluator.PokerHands;
using Shouldly;
using Xunit;

namespace PokerTest
{
    public class HandEvaluatorTest
    {
        [Fact]
        public void FlushTest()
        {
           var hand = new[]
           {
                new Card(CardValue.Deuce, Suit.Clubs),
                new Card(CardValue.Ten, Suit.Clubs),
                new Card(CardValue.Trey, Suit.Clubs),
                new Card(CardValue.Four, Suit.Clubs),
                new Card(CardValue.Five, Suit.Clubs)
            };
            var handEvaluator = new DefaultHandEvaluator();
            var result = handEvaluator.Evaluate(hand);
            result.HandType.ShouldBe(HandType.Flush);
        }

        [Fact]
        public void PairTest()
        {
            var hand = new[]
            {
                new Card(CardValue.Ace, Suit.Clubs),
                new Card(CardValue.Ace, Suit.Diamonds),
                new Card(CardValue.Deuce, Suit.Diamonds),
                new Card(CardValue.King, Suit.Spades),
                new Card(CardValue.Ten, Suit.Hearts)
            };
            var handEvaluator = new DefaultHandEvaluator();
            var result = handEvaluator.Evaluate(hand);            
            result.HandType.ShouldBe(HandType.Pair);
        }

        [Fact]
        public void TwoPairTest()
        {
            var hand = new[]
            {
                new Card(CardValue.Deuce, Suit.Clubs),
                new Card(CardValue.Five, Suit.Diamonds),
                new Card(CardValue.Deuce, Suit.Diamonds),
                new Card(CardValue.King, Suit.Spades),
                new Card(CardValue.King, Suit.Hearts)
            };

            var handEvaluator = new DefaultHandEvaluator();
            var result = handEvaluator.Evaluate(hand);            
            result.HandType.ShouldBe(HandType.TwoPair);
        }

        [Fact]
        public void ThreeOfAKindTest()
        {
            var hand = new[]
            {
                new Card(CardValue.Deuce, Suit.Clubs),
                new Card(CardValue.Deuce, Suit.Diamonds),
                new Card(CardValue.Jack, Suit.Diamonds),
                new Card(CardValue.Four, Suit.Spades),
                new Card(CardValue.Deuce, Suit.Hearts)
            };
            var result = new DefaultHandEvaluator().Evaluate(hand);            
            result.HandType.ShouldBe(HandType.ThreeOfAKind);
        }

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

            var result = new DefaultHandEvaluator().Evaluate(hand);
            result.HandType.ShouldBe(HandType.Straight);
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

            var result = new DefaultHandEvaluator().Evaluate(hand);
            result.HandType.ShouldBe(HandType.StraightFlush);
        }

        [Fact]
        public void Nothing()
        {
            var hand = new[]
            {
                new Card(CardValue.Ace, Suit.Clubs),
                new Card(CardValue.Deuce, Suit.Diamonds),
                new Card(CardValue.Jack, Suit.Diamonds),
                new Card(CardValue.Four, Suit.Spades),
                new Card(CardValue.Eight, Suit.Hearts)
            };
            var result = new DefaultHandEvaluator().Evaluate(hand);
            result.HandType.ShouldBe(HandType.HighCard);
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

            var result = new DefaultHandEvaluator().Evaluate(hand);
            result.HandType.ShouldBe(HandType.RoyalFlush);
        }
    }
}
