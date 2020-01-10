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
            var deck = new Deck();
            var hand = deck.TakeCards(5);
            var videoPoker = new VideoPokerModel
            {
                Hand = hand.ToArray(),
                HoldCardIndex = new int[] {}
            };

            return View(videoPoker);
        }
    }
}