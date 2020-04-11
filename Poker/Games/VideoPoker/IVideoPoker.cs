using System.Collections.Generic;
using Poker.HandEvaluator;

namespace Poker.Games.VideoPoker
{
    public interface IVideoPoker
    {
        Card[] Hand { get; }        

        IList<PayShedule> PaySchedule { get; set; }

        IDeck Deck { get; set; }

        GameVars GameVars {get; set;}

        void NewGame();
        void SelectUnitSize(decimal unitSize);
        void SelectBetSize(short betSize);
        void DepositMoney(decimal depositAmount);
        HandEvaluationResult Deal();

        VideoPokerResult Draw(int[] heldCardsIndeces);        
    }
}
