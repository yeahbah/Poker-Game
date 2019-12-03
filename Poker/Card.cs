using System;
using System.Linq;
using System.Reflection;

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

        public int DefaultCardWeight
        {
            get
            {
                var enumType = this.CardValue
                    .GetType();
                var name = Enum.GetName(enumType, CardValue);
                var attr = enumType.GetField(name)
                    .GetCustomAttribute<CardWeightAttribute>();

                return attr.Weight + (int)Suit;               

            }
        }
        
        public string ShortCode 
        { 
            get 
            {                
                var cardValue = (int)CardValue;
                var valueCode = string.Empty;
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

        public static bool operator >(Card left, Card right)
        {
            return left.DefaultCardWeight > right.DefaultCardWeight;
        }

        public static bool operator <(Card left, Card right)
        {
            return left.DefaultCardWeight < right.DefaultCardWeight;
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

        // descending order sort
        public int CompareTo(object obj)
        {
            if (obj == null) return -1;

            var otherCard = ((Card)obj);            
            if (this < otherCard)
            {
                return 1;
            }

            if (this == otherCard)
            {
                return 0;
            }
           
            return -1;
        }
    }
}
