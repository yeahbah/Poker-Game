using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Poker;
using Poker.Games.VideoPoker;
using Server.Models;
using System.Collections.Generic;
using Web.Models;
using Web.Services;
using Server.Extensions;

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
        [Route("deal")]
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
        [Route("draw")]
        public VpDrawResultModel Draw([FromBody]GameVarsModel gameVarsModel, Card[] cards, int[] heldCardIndeces)
        {
            return videoPokerService.Draw(gameVarsModel, cards, heldCardIndeces);
        }

        [HttpGet]
        [Route("payschedule")]
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

        [HttpPost]
        [Route("newgame")]
        public IActionResult NewGame([FromBody]NewGameModel newGame) 
        {
            HttpContext.Session.Set<GameVarsModel>("game", new GameVarsModel {
                BetSize = 1,
                Money = newGame.PlayMoney,
                UnitSize = newGame.UnitSize,
                VideoPokerType = newGame.VideoPokerType                    
            });
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}