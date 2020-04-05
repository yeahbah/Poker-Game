using System;
using System.Linq;
using Poker;
using Poker.Games.VideoPoker;
using Poker.HandEvaluator.PokerHands;

namespace VideoPoker
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            //ResetConsoleColor();
            Console.WriteLine("Jacks or Better.\n");

            var game = InitializeGame();

        }

        private static IVideoPoker InitializeGame()
        {
            var game = new JacksOrBetter(new Deck());
            
            Console.WriteLine("How much do you want to play with?");
            var depositAmount = decimal.Parse(Console.ReadLine() ?? throw new InvalidOperationException());            
            
            game.DepositMoney(depositAmount);

            Console.Write("\nSet your unit size (1 - 5cents, 2 - 25cents, 3 - $1): ");
            var unitValueInput = Console.ReadLine() ?? "1";
            var unitSize = unitValueInput switch
            {
                "1" => 0.05m,
                "2" => 0.25m,
                "3" => 1m,
                _ => 0.05m
            };
            game.SelectUnitSize(unitSize);

            Console.Write("How many units (1 to 25):");
            if (!short.TryParse(Console.ReadLine(), out var numUnits))
            {
                numUnits = 1;
            }
                    
            game.SelectBetSize(numUnits);            

            var betSize = unitSize * numUnits;                        
            Console.WriteLine($"Your bet size: {betSize:C}");            
            Console.WriteLine($"You have {game.GameVars.Money:C}");
            DisplayGameInfo(game);

            return game;
        }

        private static void DisplayGameInfo(IVideoPoker game)
        {
            Console.WriteLine("----------------------------------+");
            Console.WriteLine("Hand              | Pay           |");
            Console.WriteLine("----------------------------------+");

            var paySchedule = game.PaySchedule
                .Where(p => p.BetSize == game.GameVars.BetSize)
                .OrderByDescending(p => p.HandType)
                .ToList();
            foreach (var pay in paySchedule)
            {
                var handDescription = GetDescription(pay.HandType);
                Console.WriteLine($"{handDescription} {pay.PaySizeInUnits}  ");
            }

            Console.WriteLine("----------------------------------+");
        }

        private static string GetDescription(HandType handType)
        {
            var result = "";
            switch (handType)
            {
                case HandType.Pair:
                    result = "Jacks or Better   |";
                    break;
                case HandType.TwoPair:
                    result = "Two Pair          |";
                    break;
                case HandType.ThreeOfAKind:
                    result = "3 of a Kind       |";
                    break;
                case HandType.Straight:
                    result = "Straight          |";
                    break;
                case HandType.Flush:
                    result = "Flush             |";
                    break;
                case HandType.Fullhouse:
                    result = "Full House        |";
                    break;
                case HandType.FourOfAKind:
                    result = "4 of a Kind       |";
                    break;
                case HandType.StraightFlush:
                    result = "Straight Flush    |";
                    break;
                case HandType.RoyalFlush:
                    result = "Royal Flush       |";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(handType), handType, null);
            };
            return result;
        }

    }

}