namespace Poker.Games.VideoPoker 
{
    public interface IVideoPokerGameFlow
    {
        void NewGame();
        void SelectUnitSize(decimal unitSize);
        void SelectBetSize(short betSize);
        void DepositMoney(decimal depositAmount);
        void Deal();

    }
}