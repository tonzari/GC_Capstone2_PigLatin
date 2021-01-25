using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

// Grand Circus Capstone 2: Pig Latin
// Antonio Manzari

namespace GC_Capstone2_PigLatin
{
    //TODO
    //Use split stringoptions to remove empty entries instead of juggling Lists
    partial class Program
    {
        public static CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
        public static TextInfo textInfo = cultureInfo.TextInfo;
        
        public static string translatedSentence;
       
        public static List<string> wordListRaw = new List<string>();
        public static List<string> wordListSanitized = new List<string>();
        public static List<string> wordListTranslated = new List<string>();
        
        public static CaseType[] savedCaseData;

        static void Main(string[] args)
        {
            PrintWelcomeMessage();

            do
            {
                UpdateLoop();
            } while (CheckUserWantsToContinue());

            ExitApp();
        }

        private static void UpdateLoop()
        {
            GetAndTrimUserInput();
            StoreCaseOfEachWord();
            TranslateAndPrintResults();
            ClearAllLists();
        }

        public static void PrintWelcomeMessage()
        {
            Console.WriteLine("Welcome to the PIG LATIN TRANSLATOR!");
            Console.WriteLine(Environment.NewLine);
        }

        public static string PromptUserForSentence()
        {
            Console.Write("Enter a sentence to be translated: ");
            string userInput = Console.ReadLine();
            Console.WriteLine(Environment.NewLine);

            if (!String.IsNullOrWhiteSpace(userInput))
            {
                return userInput;
            }
            else
            {
                Console.WriteLine("Trying to break things again, are you? You have to enter *something* to translate! Let's try that again.");
                return PromptUserForSentence();
            }
        }

        private static void GetAndTrimUserInput()
        {
            wordListRaw = PromptUserForSentence().Split().ToList();                        // Get user input, split it, and store in a List
            wordListSanitized = new List<string>();                                        // Loop through raw list, and only add actualy words to sanitized list

            foreach (var word in wordListRaw)
            {
                if (!String.IsNullOrWhiteSpace(word))
                {
                    wordListSanitized.Add(word);
                }
            }
        }
        
        private static void TranslateAndPrintResults()
        {
            for (int i = 0; i < wordListSanitized.Count; i++)
            {
                //wordListSanitized[i].ToLower();                                                       // I'm not sure I need to do this if I want to maintain the case for the extended exercise

                if (CheckIfContainsNumberOrSymbol(wordListSanitized[i]))
                {
                    wordListTranslated.Insert(i, wordListSanitized[i]);
                }
                else
                {
                    wordListTranslated.Insert(i, TranslateWordToPigLatin(wordListSanitized[i]));
                }

                if (savedCaseData[i] == CaseType.Lower)
                {
                    wordListTranslated[i] = wordListTranslated[i].ToLower();
                }
                else if (savedCaseData[i] == CaseType.Upper && wordListTranslated[i] != "Iway")         // "I" is common enough that if it is returned as case type UPPER, it looks weird in a translate setnence as IWAY
                {
                    wordListTranslated[i] = wordListTranslated[i].ToUpper();
                }
                else if (savedCaseData[i] == CaseType.Title)
                {
                    wordListTranslated[i] = ConvertToTitleCase(wordListTranslated[i]);
                }
            }

            translatedSentence = String.Join(" ", wordListTranslated);
            Console.WriteLine(translatedSentence);
        }

        private static void StoreCaseOfEachWord()
        {
            savedCaseData = new CaseType[wordListSanitized.Count];                           // Store the case type for each word in an array for later
            for (int i = 0; i < wordListSanitized.Count; i++)
            {
                savedCaseData[i] = GetLetterCaseType(wordListSanitized[i]);
            }
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
            // Do not consider the single quote, cannot find info on ‘ ’ 
            foreach (char character in input)
            {
                if (Char.IsNumber(character) || Char.IsSymbol(character) || Char.IsPunctuation(character) && !character.Equals('\'')) 
                {
                    return true;
                }
            }

            return false;
        }

        public static bool CheckForTrailingPunctuation(string input)
        {
            //This proved to be too dificult for the amount of time I have.
            //I wanted to strip out trailin punctuation, but ran into 
            //time constraint issues. I would ideally split off the punctuation,
            //translate the word portion, then join them back together.
            return char.IsPunctuation(input.Last());
        }

        private static void ClearAllLists()
        {
            wordListTranslated.TrimExcess();
            wordListTranslated.Clear();

            wordListSanitized.TrimExcess();
            wordListSanitized.Clear();

            wordListRaw.TrimExcess();
            wordListRaw.Clear();
        }

        public static bool CheckUserWantsToContinue()
        {
            Console.WriteLine(Environment.NewLine);
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
