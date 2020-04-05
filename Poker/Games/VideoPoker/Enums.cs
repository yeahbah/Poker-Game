namespace Poker.Games.VideoPoker 
{
    public enum GameTrigger
    {
        Bet,
        Draw,
        Hold,
        Deposit,
        Evaluate,
        Deal,
        SelectUnitSize,
        SelectBetSize,    
        NewGame    
    }

    public enum GameState 
    {
        DealtCards,
        Idle,
        HandEvaluated,
        CardsDrawn,
        MoneyDeposited,
        UnitSizeSelected,
        NewGame,
        BetSizeSelected
    }
}