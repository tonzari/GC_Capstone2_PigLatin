using System;
using System.Linq;
using System.Collections;
using System.Globalization;
using System.Threading;
using System.Collections.Generic;

// Grand Circus Capstone 2: Pig Latin
// Antonio Manzari

namespace GC_Capstone2_PigLatin
{
    partial class Program
    {
        public static string userName = String.Empty;
        public static CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
        public static TextInfo textInfo = cultureInfo.TextInfo;

        static void Main(string[] args)
        {
            PrintWelcomeMessage();

            do
            {
                TranslateToPigLatin(GetUserInput());
            } while (CheckUserWantsToContinue());

            ExitApp();
        }

        //TODO:
        // X  Welcome the user
        // X  Get user input
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

        public static string GetUserInput()
        {
            Console.Write("Enter a sentence to be translated: ");
            string userInput = Console.ReadLine();

            Console.WriteLine(Environment.NewLine);
            return userInput;
        }

        public static void TranslateToPigLatin(string sentence)
        {

            if (String.IsNullOrWhiteSpace(sentence))
            {
                Console.WriteLine($"Oh, {userName}. Trying to break things again, are you? You have to enter *something* to translate! Let's try that again.");
            }
            else
            {
                String[] words = sentence.Split();

                Dictionary<int, CaseType> savedCaseData = new Dictionary<int, CaseType>();

                for (int i = 0; i < words.Length; i++)
                {
                    //check the case, save that info

                    // if contains number, leave it alone
                    //remove ' from contractions

                }

            }

            // check if just one word, if so tell the user they can do a full sentence.

            //if vowel
            //add "way"

            //else
            //move all consonants before first vowel to end
            //add "ay"


        }

        public static bool CheckIsAllUpper(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (Char.IsLetter(input[i]) && !Char.IsUpper(input[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool CheckIsAllLower(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (Char.IsLetter(input[i]) && !Char.IsLower(input[i]))
                {
                    return false;
                }
            }
            return true;
        }

        

        public static string ConvertToTitleCase(string input)
        {
            return textInfo.ToTitleCase(input);
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
