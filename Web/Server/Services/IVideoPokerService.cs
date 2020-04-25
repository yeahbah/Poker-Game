using Poker;
using Poker.Games.VideoPoker;
using System.Collections.Generic;
using Web.Models;

namespace Web.Services
{
    public interface IVideoPokerService
    {
        IVideoPoker NewGame(GameVarsModel gameVarsModel);

        VpDealCardsModel Deal(GameVarsModel gameVars);

        VpDrawResultModel Draw(GameVarsModel gameVars, Card[] cards, int[] heldCardIndeces);

        IEnumerable<PaySchedule> GetPaySchedule(GameVarsModel gameVars);
    }
}
