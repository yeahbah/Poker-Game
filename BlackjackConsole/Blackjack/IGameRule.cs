using Yeahbah.Poker;

namespace Blackjack
{
    public interface IGameRule    
    {
        // can the player split his cards?
        bool CanSplit(Card[] hand);

        // can the player double down?
        bool CanDoubleDown(Card[] hand);

        // can the player surrender?
        bool CanSurrender(Card[] hand);

        // can the player double down after a split?
        bool CanDoubleDownAfterSplit(Card[] hand);

        bool CanTakeInsurance();

        // how much does the player gets paid with a blackjack
        decimal BlackjackMultiplier();

    }
}