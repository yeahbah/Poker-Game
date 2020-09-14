using System;
using System.Collections.Generic;
using System.Linq;
using Yeahbah.Poker;
using PokerGames.VideoPoker;
using Yeahbah.Poker.HandEvaluator.PokerHands;

namespace VideoPoker
{
    internal class Program
    {
        private static IVideoPoker _game;
        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            ResetConsoleColor();
            Console.WriteLine("Jacks or Better.\n");

            _game = InitializeGame(true);

            var play = true;
            var handNumber = 0;
            while (play) 
            {
                handNumber += 1;
                Console.WriteLine($"\n------------- Hand #{handNumber} -------------");

                var hand = _game.Deal();                
                Console.WriteLine($"{hand.Description}");

                DisplayHand(hand.Hand.ToArray());
                Console.WriteLine("1  2  3  4  5");

                Console.Write("Enter the numbers of the card(s) you want to hold: ");
                var holdCards = Console.ReadLine();
                var holdIndeces = new List<int>();
                if (holdCards != null)
                {
                    holdIndeces.AddRange(holdCards
                        .ToCharArray()
                        .Select(c => int.Parse(c.ToString()) - 1));
                }

                var result = _game.Draw(holdIndeces.ToArray());
                DisplayHand(result.HandEvaluationResult.Hand.ToArray());
                                
                play = DisplayResult(result);

            }
        }

        private static bool DisplayResult(VideoPokerResult result)
        {
            Console.WriteLine(result.HandEvaluationResult.Description);                                                        
            Console.WriteLine($"You won {result.PayoutMoney:C}!");
            if (_game.GameVars.Money == 0)                
            {
                Console.WriteLine("You are broke. Go home and be a family man.");
                return false;
            }
            else
            {
                Console.WriteLine($"You have {_game.GameVars.Money:C}.");
                Console.Write("Press any key to deal cards. Escape (ESC) to quit. F1 to change bet size.");
                var key = Console.ReadKey().Key;
                if (key == ConsoleKey.F1)
                {
                    _game = InitializeGame();
                    return true;
                }
                else
                {
                    _game.NewGame();
                    Console.WriteLine();                    
                    return key != ConsoleKey.Escape;
                }
            }                
        }

        private static void DisplayHand(Card[] videoPokerHand)
        {
            Console.BackgroundColor = ConsoleColor.White;            
            foreach (var c in videoPokerHand)
            {
                Console.ForegroundColor = c.Suit switch
                {
                    Suit.Diamonds => ConsoleColor.DarkBlue,
                    Suit.Clubs => ConsoleColor.DarkGreen,
                    Suit.Hearts => ConsoleColor.DarkRed,
                    Suit.Spades => ConsoleColor.Black,
                    _ => throw new ArgumentOutOfRangeException()
                };
                Console.Write(c.ShortCode + " ");
            }

            ResetConsoleColor();
            Console.WriteLine();
        }

        private static IVideoPoker InitializeGame(bool forceDeposit = false)
        {
            var game = new JacksOrBetter(new Deck());                        
            if (forceDeposit) 
            {
                Console.Write("How much do you want to play with? ");
                var depositAmount = decimal.Parse(Console.ReadLine() ?? throw new InvalidOperationException());                            
                game.DepositMoney(depositAmount);
            }     
            else
            {
                game.GameVars.Money = _game.GameVars.Money;
            }       

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

            Console.Write("How many units (1 to 25): ");
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

        private static void ResetConsoleColor()
        {
            Console.ResetColor();
            // Console.BackgroundColor = ConsoleColor.Black;
            // Console.ForegroundColor = ConsoleColor.White;
        }        

    }

}