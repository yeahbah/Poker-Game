using System;
using System.Reflection;

namespace Poker
{
    public class Card : IComparable
    {
        public Card(CardValue cardValue, Suit suit)
        {
            CardValue = cardValue;
            Suit = suit;
        }

        public static Card InstanceFromShortCode(string shortCode)
        {
            if (string.IsNullOrEmpty(shortCode))
            {
                throw new InvalidOperationException();
            }

            var suit = shortCode[1].ToString().ToLower() switch 
            {
                "s" => Suit.Spades,
                "c" => Suit.Clubs,
                "h" => Suit.Hearts,
                "d" => Suit.Diamonds,
                _ => throw new Exception("Invalid suit.")
            };

            var c = shortCode[0].ToString().ToUpper();
            if (int.TryParse(c, out var shortCodeInt))
            {
                return new Card((CardValue)shortCodeInt, suit);
            }
            else
            {
                var cardValue = c switch
                {
                    "T" => CardValue.Ten,
                    "J" => CardValue.Jack,
                    "Q" => CardValue.Queen,
                    "K" => CardValue.King,
                    "A" => CardValue.Ace,
                    _ => throw new Exception($"{c} is not a valid card value code.")
                };

                return new Card(cardValue, suit);
            }            
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

                return attr.Weight;

            }
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
            if (object.ReferenceEquals(left, null))
            {
                return true;
            }
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
