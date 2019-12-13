using Poker;
using Poker.HandEvaluator.PokerHands;
using System.Collections.Generic;

namespace Poker.Games.VideoPoker
{
    public interface IVideoPoker
    {
        Card[] Hand { get; }

        void Deal();

        /// <summary>
        /// Draw cards from Hand then return the amount won.
        /// </summary>
        /// <param name="indexOfCards"></param>
        /// <param name="bet"></param>
        /// <returns></returns>
        VideoPokerResult Play(int[] indexOfCards, decimal bet);

        IDictionary<HandType, decimal> PaySchedule { get; set; }
    }
}
