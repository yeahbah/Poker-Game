using Poker;
using Poker.HandEvaluator.PokerHands;
using System.Collections.Generic;

namespace VideoPoker
{
    public interface IVideoPoker
    {
        Card[] Hand { get; }

        void Deal();

        /// <summary>
        /// Draw cards from Hand then return the amount won.
        /// </summary>
        /// <param name="indexOfCards"></param>
        /// <returns></returns>
        decimal Play(int[] indexOfCards, decimal bet);        

        IDictionary<HandType, decimal> PaySchedule { get; set; }
    }
}
