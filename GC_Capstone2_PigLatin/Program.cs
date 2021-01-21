using System;
using System.Linq;

// Grand Circus Capstone 2: Pig Latin
// Antonio Manzari

namespace GC_Capstone2_PigLatin
{
    class Program
    {
        public static string userName = String.Empty;

        static void Main(string[] args)
        {
            PrintWelcomeMessage();
            ExitApp();
        }

        //TODO:
        // X  Welcome the user
        // O  Get user input
        // O  Validate the user input
        // O  Transform the input 
        // X  Ask user to repeat

        public static void PrintWelcomeMessage()
        {
            Console.WriteLine("Welcome to the PIG LATIN TRANSLATOR!");
            Console.Write("Before we ge started, can you please enter your name? ");
            userName = Console.ReadLine();
            Console.WriteLine(Environment.NewLine);
            if (!String.IsNullOrWhiteSpace(userName))
            {
                Console.WriteLine($"Thanks, {userName}!");
            }
            else
            {
                Console.WriteLine("I'm sorry, but you have to enter *something* for your name... let's start over.");
                Console.WriteLine(Environment.NewLine);
                PrintWelcomeMessage();
            }
        }

        public static bool CheckUserWantsToContinue()
        {
            Console.WriteLine($"{userName}, would you like to continue? (y/n) ");
            char userInput = Char.ToLower(Console.ReadKey().KeyChar);

            if (userInput.Equals('y'))
            {
                Console.WriteLine(Environment.NewLine);
                return true;
            }
            else if (userInput.Equals('n'))
            {

                Console.WriteLine(Environment.NewLine);
                Console.WriteLine($"Goodbye, {userName}!");
                return false;
            }
            else
            {
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine($"So, here's the deal, {userName}. You can press the 'y' key, or you can press the 'n' key. What's it gonna be?");
                return CheckUserWantsToContinue();
            }
        }
        public static void ExitApp()
        {
            Console.WriteLine("I guess this is goodbye...");
            Environment.Exit(0);
        }
    }
}
