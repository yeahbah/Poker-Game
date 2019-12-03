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
    public class PairEvalRuleTest
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

            var result = new PairEvalRule().Evaluate(hand);
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

            var result = new PairEvalRule().Evaluate(hand);
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

            var result = new PairEvalRule().Evaluate(hand);
            result.HasValue.ShouldBeFalse();            
        }
    }
}
