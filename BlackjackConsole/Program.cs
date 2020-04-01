using System;
using System.Linq;
using Poker;

namespace BlackjackConsole
{
    class Program
    {
        internal class Player
        {
            public decimal Money { get; set; }
            public decimal Bet { get; set; }
            public Card[] CurrentHand { get; set; }
        }

        private static readonly Deck _deck = new Deck();

        static void Main(string[] args)
        {
            Console.WriteLine("Blackjack\n\n");
            var player = new Player
            {
                Money = 1000
            };

            var play = true;
            var handNumber = 0;
            while (play)
            {
                handNumber += 1;
                Console.WriteLine($"\n------------- Hand #{handNumber} -------------");

                _deck.ResetDeck();
                
                Console.WriteLine($"You have ${player.Money}");
                Console.Write("Place your bet: ");
                player.Bet = decimal.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
                player.Money -= player.Bet;

                player.CurrentHand = _deck.TakeCards(2);
                Card[] dealerHand = _deck.TakeCards(2);

                var playerScore = GetScore(player.CurrentHand);
                Console.Write($"Your hand: ");
                // Console.BackgroundColor = ConsoleColor.Blue;
                Console.WriteLine(
                    $"{player.CurrentHand[0].ShortCode}{player.CurrentHand[1].ShortCode} = {playerScore}");
                ResetConsoleColor();

                var dealerScore = GetScore(dealerHand);
                Console.Write($"Dealer shows: ");
                // Console.BackgroundColor = ConsoleColor.Blue;
                Console.WriteLine($"{dealerHand[0].ShortCode}");
                ResetConsoleColor();

                if (dealerScore == 21 && playerScore == 21)
                {
                    Console.Write($"Dealer hand: ");
                    // Console.BackgroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"{dealerHand[0].ShortCode}");
                    ResetConsoleColor();

                    Console.WriteLine("Push.");
                    player.Money += player.Bet;
                    Console.WriteLine($"You have {player.Money}");
                }
                else if (dealerScore == 21)
                {
                    Console.Write($"Dealer hand: ");
                    // Console.BackgroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"{dealerHand[0].ShortCode}{dealerHand[1].ShortCode}");
                    ResetConsoleColor();

                    Console.WriteLine("You lose!");
                    Console.WriteLine($"You have {player.Money}");
                }
                else if (playerScore == 21)
                {
                    var win = (player.Bet * 1.5m) + player.Bet;
                    player.Money += win;
                    Console.WriteLine($"Blackjack! You win {win}");
                    Console.Write($"Dealer hand: ");
                    // Console.BackgroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"{dealerHand[0].ShortCode}{dealerHand[1].ShortCode}");
                    ResetConsoleColor();

                    Console.WriteLine($"You have {player.Money}");
                }
                else
                {

                    var playerMove = 0;
                    playerScore = 0;
                    while (playerMove != 1 && playerScore < 21)
                    {
                        Console.Write("1 - Stay, 2 - Hit, 3 - Double Down, 4 - Split : ");
                        playerMove = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
                        switch (playerMove)
                        {
                            case 1:
                                Evaluate(ref dealerHand, player);
                                break;
                            case 2:
                                var playerDraw = _deck.TakeCard();
                                player.CurrentHand = player.CurrentHand.Append(playerDraw).ToArray();
                                playerScore = GetScore(player.CurrentHand);
                                Console.WriteLine($"Draw: {playerDraw.ShortCode} = {playerScore}");
                                if (playerScore > 21)
                                {
                                    Console.WriteLine("Bust. You lose!");
                                    Console.Write($"Dealer hand: ");
                                    // Console.BackgroundColor = ConsoleColor.Blue;
                                    Console.WriteLine($"{dealerHand[0].ShortCode}{dealerHand[1].ShortCode}");
                                    ResetConsoleColor();

                                    Console.WriteLine($"You have {player.Money}");
                                }

                                break;
                            case 3:
                                break;
                            case 4:
                                break;
                        }
                    }
                }

                Console.Write("Press any key to play again. Escape (ESC) to quit. ");
                var key = Console.ReadKey().Key;
                play = key != ConsoleKey.Escape;

                Console.WriteLine("\n");
            }
        }

        private static void Evaluate(ref Card[] dealerHand, Player player)
        {
            Console.Write($"Dealer hand: ");
            // Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine($"{dealerHand[0].ShortCode}{dealerHand[1].ShortCode}");
            ResetConsoleColor();

            var dealerScore = GetScore(dealerHand);
            while (dealerScore < 17)
            {
                var draw = _deck.TakeCard();
                dealerHand = dealerHand.Append(draw).ToArray();
                dealerScore = GetScore(dealerHand);
                Console.WriteLine($"Dealer Draw: {draw.ShortCode} = {dealerScore}");
            }

            var playerScore = GetScore(player.CurrentHand);
            if (dealerScore > 21)
            {
                var win = player.Bet * 2;
                Console.WriteLine($"Dealer bust. You win {win}!");
                player.Money += win;
            }
            else if (playerScore < dealerScore)
            {
                Console.WriteLine("You lose!");
            }
            else if (playerScore > dealerScore)
            {
                var win = player.Bet * 2;
                Console.WriteLine($"You win {win}!");
                player.Money += win;
            }
            else if (playerScore == dealerScore)
            {
                Console.WriteLine("Push");
                player.Money += player.Bet;
            }

            Console.WriteLine($"You have {player.Money}");
        }

        private static int GetScore(Card[] hand)
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

        private static void ResetConsoleColor()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            //Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
