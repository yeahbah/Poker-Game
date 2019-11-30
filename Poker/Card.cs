using System;

namespace Poker
{
    public struct Card
    {
        public Card(CardValue cardValue, Suit suit)
        {
            CardValue = cardValue;
            Suit = suit;
        }

        public CardValue CardValue { get; }
        public Suit Suit { get; set; }

        public int GetCardId()
        {
            return (int)CardValue + (int)Suit;
        }

        public string ShortCode 
        { 
            get 
            {
                var cardValue = (int)CardValue;
                string valueCode;
                if (cardValue >= 2 && cardValue <= 9)
                {
                    valueCode = cardValue.ToString();
                }
                else 
                {
                    valueCode = CardValue.ToString()[0].ToString();
                }

                return valueCode + Suit.ToString()[0].ToString().ToLower();
            }
        }        

        public override string ToString()
        {
            return $"{CardValue} of {Suit}";
        }

        public static bool operator ==(Card left, Card right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Card left, Card right)
        {
            return !(left == right);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Card))
            {
                return false;
            }

            return ((Card)obj).GetHashCode() == this.GetHashCode();
        }

        public override int GetHashCode()
        {
            return new { CardValue, Suit }.GetHashCode();
        }
    }

    public enum CardValue
    {
        Ace = 1,
        Deuce = 2,
        Trey = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 11,
        Queen = 12,
        King = 13        
    }

    public enum Suit
    {
        Spades = 1,
        Hearts = 2,
        Clubs = 3,
        Diamonds = 4
    }
}
