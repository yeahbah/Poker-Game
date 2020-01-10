using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Poker;

namespace VideoPokerWeb.Models
{
    public class VideoPokerModel
    {
        public Card[] Hand { get; set; }
        public int[] HoldCardIndex { get; set; }
    }
}
