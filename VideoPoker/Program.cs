﻿using Poker;
using Poker.Games.VideoPoker;
using Poker.HandEvaluator;
using Poker.HandEvaluator.PokerHands;
using System;
using System.Collections.Generic;
using System.Linq;

namespace VideoPoker
{
    class Program
    {
        internal class GameVars
        {
            public GameVars(decimal unitValue, int numUnits, decimal money, IVideoPoker videoPoker)
            {
                UnitValue = unitValue;
                Money = money;
                VideoPoker = videoPoker;
                NumUnits = numUnits;
            }

            public decimal UnitValue { get; set; }
            public int NumUnits { get; set; }
            public decimal Money { get; set; }

            public IVideoPoker VideoPoker { get; set; }
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            ResetConsoleColor();
            Console.WriteLine("Jacks or Better.\n");

            var game = InitializeGame();

            var play = true;
            var handNumber = 0;
            while (play)
            {
                handNumber += 1;
                Console.WriteLine($"\n------------- Hand #{handNumber} -------------");

                var result = Play(game);

                var win = result.Payout * game.UnitValue;
                game.Money += win;
                Console.WriteLine($"You won {win:C}!");
                if (game.Money == 0)
                {
                    Console.WriteLine("You are broke. Go home and be a family man.");
                    play = false;
                }
                else
                {
                    Console.WriteLine($"You have {game.Money:C}.");
                    Console.Write("Press any key to deal cards. Escape (ESC) to quit. F1 to change bet size.");
                    var key = Console.ReadKey().Key;
                    if (key == ConsoleKey.F1)
                    {
                        InitializeGame();
                    }
                    else
                    {
                        play = key != ConsoleKey.Escape;
                    }

                    Console.WriteLine();
                }
            }

        }

        private static VideoPokerResult Play(GameVars game)
        {
            game.Money -= (game.NumUnits * game.UnitValue);
            game.VideoPoker.Deal();
            var hand = new DefaultHandEvaluator().Evaluate(game.VideoPoker.Hand);
            Console.WriteLine($"{hand.Description}");

            DisplayHand(game.VideoPoker.Hand);
            Console.WriteLine("1  2  3  4  5");

            Console.Write("Enter the numbers of the card(s) you want to hold: ");
            var holdCards = Console.ReadLine();
            var holdIndeces = new List<int>();
            if (holdCards != null)
            {
                holdIndeces.AddRange(holdCards.ToCharArray().Select(c => int.Parse(c.ToString()) - 1));
            }

            //var bet = game.Bet();
            var result = game.VideoPoker.Play(holdIndeces.ToArray(), game.NumUnits);
            DisplayHand(result.HandEvaluationResult.Cards);

            Console.WriteLine(result.HandEvaluationResult.Description);
            return result;
        }

        private static GameVars InitializeGame()
        {
            Console.Write("\nSet your unit bet (1 - 5cents, 2 - 25cents, 3 - $1): ");
            var unitValueInput = Console.ReadLine() ?? "1";
            var unitValue = unitValueInput switch
            {
                "1" => 0.05m,
                "2" => 0.25m,
                "3" => 1m,
                _ => 0.05m
            };
            Console.Write("How many units (1 to 25):");
            if (!int.TryParse(Console.ReadLine(), out var numUnits))
            {
                numUnits = 1;
            }

            var betSize = unitValue * numUnits;
            Console.WriteLine($"Your bet size: {betSize:C}");
            var game = new GameVars(unitValue, numUnits, 100, new JacksOrBetter(new Deck()));
            Console.WriteLine($"You have {game.Money:C}");
            DisplayGameInfo(game);

            return game;
        }

        private static void DisplayGameInfo(GameVars game)
        {
            Console.WriteLine("----------------------------------+");
            Console.WriteLine("Hand              | Pay           |");
            Console.WriteLine("----------------------------------+");

            var paySchedule = game.VideoPoker.PaySchedule
                .Where(p => p.NumUnits == game.NumUnits)
                .OrderByDescending(p => p.HandType)
                .ToList();
            foreach (var pay in paySchedule)
            {
                var handDescription = GetDescription(pay.HandType);
                Console.WriteLine($"{handDescription} {pay.NumUnitPay}  ");
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
