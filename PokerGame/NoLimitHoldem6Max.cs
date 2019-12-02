using Poker;
using PokerGame.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerGame
{
    public class NoLimitHoldem6Max25NL : HoldemGame
    {
        public NoLimitHoldem6Max25NL()
        {
            _dealer = new HoldemDealer();
        }

        private readonly IDealer _dealer;
        public override IDealer GetDealer()
        {
            return _dealer;
        }

        public override void Start()
        {
            throw new NotImplementedException();
        }

        protected override decimal GetMaxBuyInAmount()
        {
            return 25;
        }

        protected override short GetMaxPlayerCount()
        {
            return 6;
        }
    }
}
