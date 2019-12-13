using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic;
using Poker;
using Poker.Games.VideoPoker;
using Poker.HandEvaluator;
using Poker.HandEvaluator.PokerHands;

namespace VideoPoker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            ResetConsoleColor();

            Console.WriteLine("Jacks or Better.\n");
            var videoPoker = new JacksOrBetter(new Deck());

            Console.WriteLine("-----------------------------------");
            Console.WriteLine("Hand              | Pay           |");
            Console.WriteLine("-----------------------------------");
            foreach (var p in videoPoker.PaySchedule.OrderByDescending(o => o.Key))
            {
                var handDescription = GetDescription(p.Key);
                Console.WriteLine($"{handDescription} {p.Value}  ");
            }
            Console.WriteLine("-----------------------------------");

            var play = true;
            var money = 100m;
            var handNumber = 0;
            while (play)
            {
                handNumber += 1;
                Console.WriteLine($"------------- Hand #{handNumber} -------------\n");
                Console.WriteLine($"You have {money:C}.");
                var bet = 0m;
                while (true)
                {
                    Console.Write("How much would you like to bet? ");
                    try
                    {
                        bet = decimal.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
                    }
                    catch (Exception)
                    {
                        bet = 1;
                    }

                    if (bet > money)
                    {
                        Console.WriteLine("You can't bet more than your money.");
                        continue;
                    }

                    break;
                }

                money -= bet;

                Console.WriteLine();

                videoPoker.Deal();
                var hand = new DefaultHandEvaluator().Evaluate(videoPoker.Hand);
                Console.WriteLine($"{hand.Description}");

                DisplayHand(videoPoker.Hand);
                Console.WriteLine("1  2  3  4  5");
                
                Console.Write("Enter the numbers of the card(s) you want to hold: ");
                var holdCards = Console.ReadLine();
                var holdIndeces = new List<int>();
                if (holdCards != null)
                {
                    holdIndeces.AddRange(holdCards.ToCharArray().Select(c => int.Parse(c.ToString()) - 1));
                }

                var result = videoPoker.Play(holdIndeces.ToArray(), bet);
                DisplayHand(result.HandEvaluationResult.Cards);


                Console.WriteLine(result.HandEvaluationResult.Description);

                money += result.Payout;
                Console.WriteLine($"You won {result.Payout:C}!");

                if (money == 0)
                {
                    Console.WriteLine("You are broke. Goodbye.");
                    play = false;
                }
                else
                {
                    Console.Write("Press any key to deal cards. Escape (ESC) to quit.");
                    play = Console.ReadKey().Key != ConsoleKey.Escape;

                    Console.WriteLine();
                }
            }

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
                    result = "Three of a Kind   |";
                    break;
                case HandType.Straight:
                    result = "Straight          |";
                    break;
                case HandType.Flush:
                    result = "Flush             |";
                    break;
                case HandType.Fullhouse:
                    result = "Fullhouse         |";
                    break;
                case HandType.FourOfAKind:
                    result = "Quads             |";
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

        private static void DisplayHand(Card[] videoPokerHand)
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            foreach (var c in videoPokerHand)
            {
                //var suit = c.Suit switch
                //{
                //    Suit.Clubs => Strings.ChrW(2663),
                //    Suit.Diamonds => Strings.ChrW(2666),
                //    Suit.Hearts => Strings.ChrW(2665),
                //    Suit.Spades => Strings.ChrW(2660)
                //};
                Console.Write(c.ShortCode + " ");
            }

            ResetConsoleColor();
            Console.WriteLine();
        }

        private static void ResetConsoleColor()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
