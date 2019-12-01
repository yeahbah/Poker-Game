namespace Poker
{
    public interface IDealer
    {
        Card[] DealHand();

        void Shuffle();
    }
}
