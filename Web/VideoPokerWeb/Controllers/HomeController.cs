using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Poker;
using VideoPokerWeb.Models;

namespace VideoPokerWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {            
            return View();
        }

        [Route("/Home/Game/{gameId:int}")]
        public IActionResult SelectGame(int gameId)
        {
            return RedirectToAction("Index", "JacksOrBetter");
        }

        [Route("Home/BetUnit/{size:int}")]
        public IActionResult BetUnit(int size)
        {
            ViewBag.BetUnit = size switch
            {
                2 => 0.25,
                3 => 1,
                _ => 0.05
            };
            return RedirectToAction("Index", "Home");
        }
    }
}