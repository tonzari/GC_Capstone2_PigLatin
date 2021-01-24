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
        public static CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
        public static TextInfo textInfo = cultureInfo.TextInfo;
        
        static void Main(string[] args)
        {
            PrintWelcomeMessage();

            do
            {
                string userInput = GetUserInput();
                List<string> wordListRaw = userInput.Split().ToList();                     // Get user input, split it, and store in a List
                List<string> wordListSanitized = new List<string>();                                // Loop through raw list, and only add actualy words to sanitized list

                foreach (var word in wordListRaw)
                {
                    if (!String.IsNullOrWhiteSpace(word))
                    {
                        wordListSanitized.Add(word);
                    }
                }

                CaseType[] savedCaseData = new CaseType[wordListSanitized.Count];                   // Store the case type for each word in an array for later
                for (int i = 0; i < wordListSanitized.Count; i++)
                {
                    savedCaseData[i] = GetLetterCaseType(wordListSanitized[i]);
                }

                List<string> wordListTranslated = new List<string>();                               // New List to store translated words
                for (int i = 0; i < wordListSanitized.Count; i++)
                {
                    wordListSanitized[i].ToLower();
                    
                    wordListTranslated.Insert(i, TranslateWordToPigLatin(wordListSanitized[i]));
/*
                    if (savedCaseData[i] == CaseType.Lower || savedCaseData[i] == CaseType.Mixed)   // Currently not handling mixed case words like "O'Bryan or McDougal" Needs to have a different approach
                    {                     
                        wordListTranslated[i].ToLower();
                    }
                    else if (savedCaseData[i] == CaseType.Upper)
                    {
                        wordListTranslated[i].ToUpper();
                    }
                    else if (savedCaseData[i] == CaseType.Title)
                    {
                        wordListTranslated[i] = ConvertToTitleCase(wordListTranslated[i]);
                    }*/

                    Console.WriteLine(i + wordListTranslated[i]);
                }




            } while (CheckUserWantsToContinue());

            ExitApp();
        }

        //TODO:
        // X  Welcome the user
        // X  Get user input
        // O  Validate the user input
        // O  Translate the input 
        // X  Ask user to repeat

        public static void PrintWelcomeMessage()
        {
            Console.WriteLine("Welcome to the PIG LATIN TRANSLATOR!");
            Console.WriteLine(Environment.NewLine);
        }

        public static string GetUserInput()
        {
            Console.Write("Enter a sentence to be translated: ");
            string userInput = Console.ReadLine();

            if (!String.IsNullOrWhiteSpace(userInput))
            {
                return userInput;
            }
            else
            {
                Console.WriteLine("Trying to break things again, are you? You have to enter *something* to translate! Let's try that again.");
                return GetUserInput();
            }

            Console.WriteLine(Environment.NewLine);
        }

        public static string TranslateWordToPigLatin(string input)
        {
            string translation;
            int firstVowelIndex = FindindexOfFirstVowel(input);
            
            if (firstVowelIndex == 0)
            {
                // TRANSLATE - add "way" to words that start with vowel
                translation = input + "way";
            }
            else
            {
                // split the string at location of first vowel, move first substring to end, add "ay"
                string firstHalf = input.Substring(0, firstVowelIndex);
                string secondHalf = input.Substring(firstVowelIndex);
                translation = secondHalf + firstHalf + "ay";
            }

            return translation;
        }

        public static void TranslateSentenceToPigLatin(string sentence)
        {
            // I think I should use a method to translate ONE WORD at a time, this is too complicated.
            // handling the sentence should happen elsewhere in the program.



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

        public static bool CheckIsTitleCase(string input)
        {
            if (!char.IsUpper(input.First()))
            {
                return false;
            }

            input = input.Substring(1);
            return CheckIsAllLower(input);
        }

        public static CaseType GetLetterCaseType(string input)
        {
            if (CheckIsAllLower(input))
                return CaseType.Lower;

            else if (CheckIsAllUpper(input))
                return CaseType.Upper;

            else if (CheckIsTitleCase(input))
                return CaseType.Title;

            else
                return CaseType.Mixed;
        }

        public static string ConvertToTitleCase(string input)
        {
            return textInfo.ToTitleCase(input);
        }

        public static bool CheckIfContainsVowel(string input, string vowelList)
        {
            foreach (char vowel in vowelList)
            {
                if (input.Contains(vowel))
                {
                    return true;
                }
                else if (input.Contains('y') || input.Contains('Y'))
                {
                    return true;
                }
            }
           
            return false;
        }

        public static int FindindexOfFirstVowel(string input)
        {
            string vowels = "aAeEiIoOuU";
            string yAsVowel = "yY";
            char[] chars = vowels.ToCharArray();
            char[] yChars = yAsVowel.ToCharArray();

            if (input.IndexOfAny(chars) != -1) // First look for vowels
            {
                return input.IndexOfAny(chars);
            }
            else if (input.IndexOfAny(yChars) != -1) // Otherwise look for Y
            {
                return input.IndexOfAny(yChars);
            }
            else // Last resort, word is likely gibberish, return 0 to avoid return -1 and error
            {
                return 0;
            }
            
        }

        public static bool CheckIfContainsNumberOrSymbol(string input)
        {
            foreach (char character in input)
            {
                if (Char.IsNumber(character) || Char.IsSymbol(character) || Char.IsPunctuation(character))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool CheckUserWantsToContinue()
        {
            Console.WriteLine("Would you like to continue? (y/n) ");
            char userInput = Char.ToLower(Console.ReadKey().KeyChar);

            if (userInput.Equals('y'))
            {
                Console.WriteLine(Environment.NewLine);
                return true;
            }
            else if (userInput.Equals('n'))
            {

                Console.WriteLine(Environment.NewLine);
                Console.WriteLine($"I guess this is goodbye...");
                return false;
            }
            else
            {
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine($"So, here's the deal: You can press the 'y' key, or you can press the 'n' key. What's it gonna be?");
                return CheckUserWantsToContinue();
            }
        }
        public static void ExitApp()
        {
            Console.WriteLine("Exiting application...");
            Environment.Exit(0);
        }
    }
}
