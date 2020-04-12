using Microsoft.AspNetCore.Mvc;
using Poker;
using System.Linq;
using VideoPokerWeb.Models;

namespace VideoPokerWeb.Controllers
{
    public class JacksOrBetterController : Controller
    {
        public IActionResult Index()
        {
            var deck = new Deck();
            var hand = deck.TakeCards(5);
            var videoPoker = new VideoPokerModel
            {
                Hand = hand.ToArray(),
                HoldCardIndex = new int[] { }
            };
            return View(videoPoker);
        }
    }
}