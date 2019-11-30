﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    public interface IDealer
    {
        Card[] Deal();
        void Shuffle();
    }
}
