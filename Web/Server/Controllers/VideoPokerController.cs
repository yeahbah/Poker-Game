using Microsoft.AspNetCore.Mvc;
using Poker;
using Poker.Games.VideoPoker;
using System.Collections.Generic;
using Web.Models;
using Web.Services;

namespace Web.Controllers
{
    [Route("api/videopoker")]
    public class VideoPokerController : Controller
    {
        private readonly IVideoPokerService videoPokerService;

        public VideoPokerController(IVideoPokerService videoPokerService)
        {
            this.videoPokerService = videoPokerService;
        }

        [HttpGet]        
        [Route("api/videopoker/deal")]
        public VpDealCardsModel Deal(VideoPokerType videoPokerType, decimal unitSize, int betSize, decimal money)
        {
            var gameVars = new GameVarsModel
            {
                BetSize = betSize,
                UnitSize = unitSize,
                Money = money,
                VideoPokerType = videoPokerType
            };
            return videoPokerService.Deal(gameVars);
        }

        [HttpPut]
        [Route("api/videopoker/draw")]
        public VpDrawResultModel Draw([FromBody]GameVarsModel gameVarsModel, Card[] cards, int[] heldCardIndeces)
        {
            return videoPokerService.Draw(gameVarsModel, cards, heldCardIndeces);
        }

        [HttpGet]
        [Route("api/videopoker/payschedule")]
        public IEnumerable<PaySchedule> GetPayschedule(VideoPokerType videoPokerType, decimal unitSize, int betSize)
        {
            var gameVars = new GameVarsModel
            {
                BetSize = betSize,
                UnitSize = unitSize,
                Money = 1,
                VideoPokerType = videoPokerType
            };
            return videoPokerService.GetPaySchedule(gameVars);
        }
    }
}