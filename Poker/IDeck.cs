namespace Poker
{
    public interface IDeck
    {
        Card[] Cards { get; }

        void ResetDeck();
        Card TakeCard();
        Card[] TakeCards(int numCards = 1);
    }
}