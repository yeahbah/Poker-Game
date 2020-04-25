using Poker;
using Poker.Games.VideoPoker;
using System.Collections.Generic;
using Web.Models;

namespace Web.Services
{
    public class VideoPokerService : IVideoPokerService
    {
        public VpDealCardsModel Deal(GameVarsModel gameVars)
        {
            var game = NewGame(gameVars);
            var result = game.Deal();
            return new VpDealCardsModel
            {
                Description = result.Description,
                Hand = result.Hand,
                HandType = result.HandType
            };
        }

        public VpDrawResultModel Draw(GameVarsModel gameVars, Card[] cards, int[] heldCardIndeces)
        {
            var game = NewGame(gameVars);
            var result = game.Draw(cards, heldCardIndeces);

            return new VpDrawResultModel
            {
                Description = result.HandEvaluationResult.Description,
                HandType = result.HandEvaluationResult.HandType,
                PayoutInUnits = result.PayoutInUnits,
                PayoutMoney = result.PayoutMoney,
                Hand = result.HandEvaluationResult.Hand
            };
        }

        public IEnumerable<PaySchedule> GetPaySchedule(GameVarsModel gameVars)
        {
            var game = NewGame(gameVars);
            return game.PaySchedule;
        }

        public IVideoPoker NewGame(GameVarsModel gameVarsModel)
        {
            var deck = new Deck();
            var game = gameVarsModel.VideoPokerType switch
            {
                VideoPokerType.JacksOrBetter => new JacksOrBetter(deck),
                _ => new JacksOrBetter(deck),
            };
            game.DepositMoney(gameVarsModel.Money);
            game.SelectBetSize(gameVarsModel.BetSize);
            game.SelectUnitSize(gameVarsModel.UnitSize);

            return game;
        }
    }
}
