using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Poker;
using Server.Models;
using Web.Models;
using Web.Services;
using Server.Extensions;
using System.Linq;
using Poker.HandEvaluator.PokerHands;

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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetPayschedule(VideoPokerType videoPokerType, decimal unitSize, int betSize)
        {
            var gameVars = new GameVarsModel
            {
                BetSize = betSize,
                UnitSize = unitSize,
                Money = 1,
                VideoPokerType = videoPokerType
            };
            var paySchedule = videoPokerService.GetPaySchedule(gameVars)
                ?.Where(pay => pay.BetSize == gameVars.BetSize);

            if (!paySchedule.Any()) 
            {
                return NotFound();
            }
            var result = new PayScheduleModel 
            {
                Pair = paySchedule.Single(p => p.HandType == HandType.Pair).PaySizeInUnits,
                TwoPair = paySchedule.Single(p => p.HandType == HandType.TwoPair).PaySizeInUnits,
                ThreeofAkind = paySchedule.Single(p => p.HandType == HandType.ThreeOfAKind).PaySizeInUnits,
                Straight = paySchedule.Single(p => p.HandType == HandType.Straight).PaySizeInUnits,
                Flush = paySchedule.Single(p => p.HandType == HandType.Flush).PaySizeInUnits,
                Fullhouse = paySchedule.Single(p => p.HandType == HandType.Fullhouse).PaySizeInUnits,
                FourOfAKind = paySchedule.Single(p => p.HandType == HandType.FourOfAKind).PaySizeInUnits,
                StraightFlush = paySchedule.Single(p => p.HandType == HandType.StraightFlush).PaySizeInUnits,
                RoyalFlush = paySchedule.Single(p => p.HandType == HandType.RoyalFlush).PaySizeInUnits
            };
                            
            return Ok(result);
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

        [HttpGet]
        [Route("gamevars")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetGameVars() 
        {
            var result = HttpContext.Session.Get<GameVarsModel>("game") as GameVarsModel;
            if (result == null)
            {
                return NotFound(StatusCodes.Status204NoContent);
            }

            return Ok(result);
        }
    }
}