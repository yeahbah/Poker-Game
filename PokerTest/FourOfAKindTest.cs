using Poker;
using Poker.HandEvaluator.HandEvalRules;
using Poker.HandEvaluator.PokerHands;
using Shouldly;
using Xunit;

namespace PokerTest
{
    public class FourOfAKindTest
    {
        [Fact]
        public void WeHaveQuadsTest()
        {
            var hand = new[]
            {
                new Card(CardValue.Deuce, Suit.Clubs),
                new Card(CardValue.Deuce, Suit.Diamonds),
                new Card(CardValue.Deuce, Suit.Spades),
                new Card(CardValue.Four, Suit.Spades),
                new Card(CardValue.Deuce, Suit.Hearts)
            };

            var result = new FourOfAKindHand().Evaluate(hand);

            result.HasValue.ShouldBeTrue();
            result.Value.HandType.ShouldBe(HandType.FourOfAKind);
        }

        [Fact]
        public void NotQuadsTest()
        {
            var hand = new[]
            {
                new Card(CardValue.Deuce, Suit.Clubs),
                new Card(CardValue.Deuce, Suit.Diamonds),
                new Card(CardValue.Four, Suit.Spades),
                new Card(CardValue.Four, Suit.Spades),
                new Card(CardValue.Deuce, Suit.Hearts)
            };

            var result = new FourOfAKindHand().Evaluate(hand);

            result.ShouldBeNull();            
        }

        [Fact]
        public void QuadsVsQuadsTest()
        {
            var hand1 = new[]
            {
                new Card(CardValue.Deuce, Suit.Clubs),
                new Card(CardValue.Deuce, Suit.Diamonds),
                new Card(CardValue.Deuce, Suit.Spades),
                new Card(CardValue.Four, Suit.Spades),
                new Card(CardValue.Deuce, Suit.Hearts)
            };
            var result1 = new FourOfAKindHand().Evaluate(hand1);

            var hand2 = new[]
            {
                new Card(CardValue.Ace, Suit.Clubs),
                new Card(CardValue.Ace, Suit.Diamonds),
                new Card(CardValue.Ace, Suit.Spades),
                new Card(CardValue.King, Suit.Spades),
                new Card(CardValue.Ace, Suit.Hearts)
            };
            var result2 = new FourOfAKindHand().Evaluate(hand2);

            result1.HasValue.ShouldBeTrue();
            result2.HasValue.ShouldBeTrue();

            result1.Value.HandWeight.ShouldBeLessThan(result2.Value.HandWeight);
        }
    }
}
