using Poker;

namespace PokerGame.Game.interfaces
{
    public interface IHoldemDealer : IDealer
    {
        Card[] DealFlop();
        Card DealTurn();
        Card DealRiver();
        Card ShowRabbitCard();
    }
}