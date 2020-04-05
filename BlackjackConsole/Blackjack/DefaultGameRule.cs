using System.Linq;
using Poker;

namespace Blackjack 
{
    public class DefaultGameRule : IGameRule
    {
        public decimal BlackjackMultiplier()
        {
            // bj pays 3 to 2 of the bet
            return 1.5m;
        }

        public bool CanDoubleDown(Card[] hand)
        {
            // double down any hand
            return true;
        }

        public bool CanDoubleDownAfterSplit(Card[] hand)
        {
            throw new System.NotImplementedException();
        }

        public bool CanSplit(Card[] hand)
        {
            throw new System.NotImplementedException();
        }

        public bool CanSurrender(Card[] hand)
        {
            throw new System.NotImplementedException();
        }

        public bool CanTakeInsurance()
        {
            throw new System.NotImplementedException();
        }

        private int GetScore(Card[] hand) 
        {
            var score = 0;

            var faceCards = hand
                .Where(c => (new[] {CardValue.Ten, CardValue.Jack, CardValue.Queen, CardValue.King})
                .Contains(c.CardValue))
                .ToArray();
            score = faceCards.Length * 10;

            var aces = hand.Where(c => c.CardValue == CardValue.Ace)
                .ToArray();

            if (hand.Length == 2 && aces.Length == 1 && faceCards.Length == 1)
            {
                // blackjack
                return 21;
            }
            else
            {
                score += 1 * aces.Length;
            }

            score += hand.Where(c => !(new[] { CardValue.Ten, CardValue.Jack, CardValue.Queen, CardValue.King, CardValue.Ace })
                .Contains(c.CardValue))
                .Sum(c => (int)c.CardValue);

            return score;
        }
    }
}