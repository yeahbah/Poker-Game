using System;

namespace Poker
{
    public enum CardValue
    {
        //Joker = 0,
        [CardWeight(11)]
        Deuce = 2,

        [CardWeight(16)]
        Trey = 3,

        [CardWeight(21)]
        Four = 4,

        [CardWeight(26)]
        Five = 5,

        [CardWeight(31)]
        Six = 6,

        [CardWeight(36)]
        Seven = 7,

        [CardWeight(41)]
        Eight = 8,

        [CardWeight(46)]
        Nine = 9,

        [CardWeight(51)]
        Ten = 10,

        [CardWeight(56)]
        Jack = 11,

        [CardWeight(61)]
        Queen = 12,

        [CardWeight(66)]
        King = 13,

        [CardWeight(71)]
        Ace = 14
    }

    public enum Suit
    {
        //Any = 0,
        Diamonds = 1,
        Clubs = 2,
        Hearts = 3,
        Spades = 4
    }

    public sealed class CardWeightAttribute : Attribute
    {
        public CardWeightAttribute(int weight)
        {
            Weight = weight;
        }

        public int Weight { get; set; }
    }
}
