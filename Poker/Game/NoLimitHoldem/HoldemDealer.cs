using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Game.NoLimitHoldem
{
    public class HoldemDealer : IDealer
    {
        private Deck _deck;
        public HoldemDealer()
        {
            _deck = new Deck();
        }
        
        public Card[] Deal()
        {
            throw new NotImplementedException();
        }

        public void Shuffle()
        {
            throw new NotImplementedException();
        }
    }
}
