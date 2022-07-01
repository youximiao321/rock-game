using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace A_Smart_Game
{
    class Program
    {
        string instructions = "\nRock-Paper-Scissors introduce： Rock（num1) ,Scissors（num2) ,Paper（num3）,press B introduce, press R restart，press ESC to exit，win 1 round get 1 point，winner who first get max point.\n";
        static void Main(string[] args)
        {
            Program pro = new Program();
            Console.Write("\n^^^Rock-Paper-Scissors^^^\n" + pro.instructions + "\nchoose：  [S]START   [E]EXIT\n");
            ConsoleKey cki = Console.ReadKey(true).Key;
            int maxgrade = 0;
            Console.Write("\nSet Game Round：");
            while (!int.TryParse(Console.ReadLine(), out maxgrade))
            {
                Console.Write("Invalid Input!!!\nSet Game Round again：");
            }
            if (cki == ConsoleKey.S)
            {
                pro.Start(maxgrade);
            }
            else if (cki == ConsoleKey.E)
            {
                return;
            }
            else
            {
                Main(null);
            }
        }
        class ScoreSheet
        {
            Dictionary<int, string> GameResult = new Dictionary<int, string>()
            {
                { -2, "You Win" },
                { -1, "You Lose" },
                { 0, "Draw Game" },
                { 1, "You Win" },
                { 2, "You Lose" },
            };
            public int CPUScore { get; private set; }
            public int humanScore { get; private set; }
            public string gameText { get; private set; }
            public void Score(int cpu, int human)
            {
                if ((cpu - human) == 2 || (cpu - human) == -1) this.CPUScore += 1;
                if ((cpu - human) == 1 || (cpu - human) == -2) this.humanScore += 1;
                this.gameText = GameResult[cpu - human];
            }
            public ScoreSheet()
            {
                this.CPUScore = 0;
                this.humanScore = 0;
                this.gameText = null;
            }
        }

        void Start(int maxgrade)
        {
            Console.WriteLine("\ngame start!!!");
            Dictionary<int, string> GameKernel = new Dictionary<int, string>()
            {
                { 1, "Rock" },
                { 2, "Sissors" },
                { 3, "Paper" }
            };

            ScoreSheet scores = new ScoreSheet();
            while (scores.CPUScore < maxgrade && scores.humanScore < maxgrade)
            {
                for (int i = 0; i < 3; i++)
                {
                    Console.Write($"---{3 - i} ");
                    Thread.Sleep(300);
                }
                Console.Write("---GO: ");
                ConsoleKey cki = Console.ReadKey(true).Key;
                Console.WriteLine();

                int com = new Random().Next(1, 3);

                Dictionary<ConsoleKey, int> Menu = new Dictionary<ConsoleKey, int>()
                {
                    { ConsoleKey.NumPad1, 1 },
                    { ConsoleKey.NumPad2, 2 },
                    { ConsoleKey.NumPad3, 3 },
                };

                if (Menu.ContainsKey(cki))
                {
                    scores.Score(com,Menu[cki]);
                    Console.WriteLine($"You have {GameKernel[Menu[cki]]}, and CPU has {GameKernel[com]}. {scores.gameText}");
                    Console.WriteLine($"Player {scores.humanScore} point VS Computer {scores.CPUScore} point\n");
                }
                else if (cki == ConsoleKey.B)
                {
                    Console.Write(instructions);
                    Console.ReadKey(true);
                }
                else if (cki == ConsoleKey.Escape)
                {
                    break;
                }
                else if (cki == ConsoleKey.R)
                {
                    Start(maxgrade);
                }
                else
                {
                    Console.WriteLine("No such option.");
                }
            }
            if (scores.humanScore == maxgrade || scores.CPUScore == maxgrade)
            {
                if (scores.humanScore > scores.CPUScore)
                {
                    Console.WriteLine("\nCongratulations, you have defended the digity of HumanKind");
                    Console.ReadKey(true);
                }
                else
                {
                    Console.WriteLine("\nYou have failed HumanKind, As the creator of the game, I feel bad for you");
                    Console.ReadKey(true);
                }
            }
        }
    }
}