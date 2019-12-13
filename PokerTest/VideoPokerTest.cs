using Xunit;
using Moq;
using Shouldly;
using Poker;
using Poker.Games.VideoPoker;

namespace PokerTest
{
    public class VideoPokerTest
    {
        [Fact]
        public void PairTest_PaidPair()
        {
            var deck = new Mock<IDeck>();
            deck.Setup(d => d.TakeCards(5))
                .Returns(new[]
                {
                    new Card(CardValue.Ace, Suit.Clubs),
                    new Card(CardValue.Ace, Suit.Diamonds),
                    new Card(CardValue.Deuce, Suit.Diamonds),
                    new Card(CardValue.King, Suit.Spades),
                    new Card(CardValue.Ten, Suit.Hearts)
                });
            deck.Setup(d => d.TakeCards(3))
                .Returns(new[]
                {
                    new Card(CardValue.Deuce, Suit.Diamonds),
                    new Card(CardValue.King, Suit.Spades),
                    new Card(CardValue.Ten, Suit.Hearts)
                });
            var game = new JacksOrBetter(deck.Object);
            game.Deal();

            var heldCards = new[] { 0, 1 };
            var result = game.Play(heldCards, 25);
            result.ShouldBe(25);
        }


        [Fact]
        public void PairTest_NotPaidPair()
        {
            var deck = new Mock<IDeck>();
            deck.Setup(d => d.TakeCards(5))
                .Returns(new[]
                {
                    new Card(CardValue.Ten, Suit.Clubs),
                    new Card(CardValue.Ten, Suit.Diamonds),
                    new Card(CardValue.Deuce, Suit.Diamonds),
                    new Card(CardValue.King, Suit.Spades),
                    new Card(CardValue.Trey, Suit.Hearts)
                });
            deck.Setup(d => d.TakeCards(3))
                .Returns(new[]
                {
                    new Card(CardValue.Deuce, Suit.Diamonds),
                    new Card(CardValue.King, Suit.Spades),
                    new Card(CardValue.Trey, Suit.Hearts)
                });
            var game = new JacksOrBetter(deck.Object);
            game.Deal();

            var heldCards = new[] { 0, 1 };
            var result = game.Play(heldCards, 25);
            result.ShouldBe(0);
        }

        [Fact]
        public void TwoPairTest()
        {
            var deck = new Mock<IDeck>();
            deck.Setup(d => d.TakeCards(5))
                .Returns(new[]
                {
                    new Card(CardValue.Ten, Suit.Clubs),
                    new Card(CardValue.Ten, Suit.Diamonds),
                    new Card(CardValue.Deuce, Suit.Diamonds),
                    new Card(CardValue.Deuce, Suit.Spades),
                    new Card(CardValue.Trey, Suit.Hearts)
                });
            deck.Setup(d => d.TakeCards(1))
                .Returns(new[]
                {
                    new Card(CardValue.Trey, Suit.Diamonds)                    
                });
            var game = new JacksOrBetter(deck.Object);
            game.Deal();

            var heldCards = new[] { 0, 1, 2, 3 };
            var result = game.Play(heldCards, 25);
            result.ShouldBe(50);
        }

        [Fact]
        public void ThreeOfAKindTest()
        {
            var deck = new Mock<IDeck>();
            deck.Setup(d => d.TakeCards(5))
                .Returns(new[]
                {
                    new Card(CardValue.Ten, Suit.Clubs),
                    new Card(CardValue.Ten, Suit.Diamonds),
                    new Card(CardValue.Deuce, Suit.Spades),
                    new Card(CardValue.Ten, Suit.Hearts),                    
                    new Card(CardValue.Trey, Suit.Hearts)
                });
            deck.Setup(d => d.TakeCards(2))
                .Returns(new[]
                {
                    new Card(CardValue.Trey, Suit.Diamonds),
                    new Card(CardValue.Five, Suit.Diamonds)
                });
            var game = new JacksOrBetter(deck.Object);
            game.Deal();

            var heldCards = new[] { 0, 1, 3 };
            var result = game.Play(heldCards, 1);
            result.ShouldBe(3);
        }
    }
}
