using System;

namespace Poker
{
    public struct Card : IComparable
    {
        public Card(CardValue cardValue, Suit suit)
        {
            CardValue = cardValue;
            Suit = suit;
        }

        public CardValue CardValue { get; }
        public Suit Suit { get; set; }

        public int DefaultCardWeight => (int)CardValue + (int)Suit;
        
        public string ShortCode 
        { 
            get 
            {
                var valueCode = CardValue switch
                {
                    CardValue.Deuce => "2",
                    CardValue.Trey => "3",
                    CardValue.Four => "4",
                    CardValue.Five => "5",
                    CardValue.Six => "6",
                    CardValue.Seven => "7",
                    CardValue.Eight => "8",
                    CardValue.Nine => "9",
                    _ => CardValue.ToString()[0].ToString()
                };

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

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            var otherCardWeight = ((Card)obj).DefaultCardWeight;
            var cardWeight = DefaultCardWeight;
            if (cardWeight < otherCardWeight)
            {
                return -1;
            }

            if (cardWeight == otherCardWeight)
            {
                return 0;
            }
           
            return 1;
        }
    }
}
