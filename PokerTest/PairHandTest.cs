using Poker;
using Poker.HandEvaluator;
using Poker.HandEvaluator.PokerHands;
using Shouldly;
using Xunit;

namespace PokerTest
{
    public class PairHandTest
    {
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

            var result = new PairHand().Evaluate(hand);
            result.HasValue.ShouldBeTrue();
            result.Value.HandType.ShouldBe(HandType.Pair);
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

            var result = new PairHand().Evaluate(hand);
            result.HasValue.ShouldBeTrue();
            result.Value.HandType.ShouldBe(HandType.TwoPair);
        }

        [Fact]
        public void NotAPairTest()
        {
            var hand = new[]
            {
                new Card(CardValue.Jack, Suit.Clubs),
                new Card(CardValue.Five, Suit.Diamonds),
                new Card(CardValue.Jack, Suit.Diamonds),
                new Card(CardValue.Jack, Suit.Spades),
                new Card(CardValue.King, Suit.Hearts)
            };

            var result = new PairHand().Evaluate(hand);
            result.HasValue.ShouldBeFalse();            
        }

        [Fact]
        public void NotATwoPairTest()
        {
           var hand = new[]
           {
                new Card(CardValue.Jack, Suit.Clubs),
                new Card(CardValue.Jack, Suit.Hearts),
                new Card(CardValue.Jack, Suit.Diamonds),
                new Card(CardValue.Jack, Suit.Spades),
                new Card(CardValue.King, Suit.Hearts)
            };

            var result = new PairHand().Evaluate(hand);
            result.HasValue.ShouldBeFalse();
        }

        [Fact]
        public void FullhouseNotPairTest()
        {
            var hand = new[]
            {
                new Card(CardValue.Deuce, Suit.Clubs),
                new Card(CardValue.Deuce, Suit.Diamonds),
                new Card(CardValue.Four, Suit.Diamonds),
                new Card(CardValue.Four, Suit.Spades),
                new Card(CardValue.Deuce, Suit.Hearts)
            };

            var result = new PairHand().Evaluate(hand);

            result.HasValue.ShouldBeFalse();
        }

        [Fact]
        public void PairVsPairTest_BetterKicker()
        {
           var hand1 = new[]
           {
                new Card(CardValue.Ace, Suit.Clubs),
                new Card(CardValue.Ace, Suit.Hearts),
                new Card(CardValue.Queen, Suit.Diamonds),
                new Card(CardValue.Deuce, Suit.Spades),
                new Card(CardValue.Trey, Suit.Hearts)
            };
            var result1 = new PairHand().Evaluate(hand1);

           var hand2 = new[]
           {
                new Card(CardValue.Ace, Suit.Diamonds),
                new Card(CardValue.Ace, Suit.Hearts),
                new Card(CardValue.Jack, Suit.Diamonds),
                new Card(CardValue.Deuce, Suit.Spades),
                new Card(CardValue.Trey, Suit.Hearts)
            };
            var result2 = new PairHand().Evaluate(hand2);

            result1.HasValue.ShouldBeTrue();
            result1.Value.HandWeight.ShouldBeGreaterThan(result2.Value.HandWeight);
        }

        [Fact]
        public void PairVsPairTest_Chop()
        {
           var hand1 = new[]
           {
                new Card(CardValue.Ace, Suit.Clubs),
                new Card(CardValue.Ace, Suit.Hearts),
                new Card(CardValue.Nine, Suit.Diamonds),
                new Card(CardValue.Deuce, Suit.Spades),
                new Card(CardValue.Trey, Suit.Hearts)
            };
            var result1 = new PairHand().Evaluate(hand1);

            var hand2 = new[]
            {
                new Card(CardValue.Ace, Suit.Diamonds),
                new Card(CardValue.Ace, Suit.Hearts),
                new Card(CardValue.Nine, Suit.Diamonds),
                new Card(CardValue.Deuce, Suit.Spades),
                new Card(CardValue.Trey, Suit.Hearts)
            };
            var result2 = new PairHand().Evaluate(hand2);

            result1.HasValue.ShouldBeTrue();
            result1.Value.HandWeight.ShouldBe(result2.Value.HandWeight);
        }
    }
}
