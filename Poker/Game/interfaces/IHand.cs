using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Game.interfaces
{
    public interface IHand
    {
        Card[] Cards { get; set; }
    }
}
