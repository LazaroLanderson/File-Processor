using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Processor.Extensions
{
    public static class StringExtensions
    {

        public static string ToCamelCase(this string text)
        {

            string result = "";
            bool uppercaseNextLetter = false;

            foreach (char letter in text)
            {
                if (letter == '-' || letter == '_')
                {
                    uppercaseNextLetter = true;
                }

                else if (uppercaseNextLetter)
                {
                    result += char.ToUpper(letter);
                    uppercaseNextLetter = false;

                }
                else
                {
                    result += letter;
                }

            }

            return result;


        }


        public static string BuildSecret(this string text, string missingLetters)

        {
            string result = "";
            int letterIndex = 0;


            foreach (char letter in text)
            {
                if (letter == '*')
                {
                    result += missingLetters[letterIndex];
                    letterIndex++;
                }

                else
                {
                    result += letter;
                }

            }

            return result;
        }



        public static int CountDuplicates(this string text)
        {
            return text
                .ToLower()
                .GroupBy(character => character)
                .Count(group => group.Count() > 1);
        }


        public static string EncryptSecret(this string text)
        {
            string result = "";

            foreach (char character in text.ToLower())
            {
                if (character >= 'a' && character <= 'z')
                {
                    int alphabetPosition = character - 'a' + 1;
                    result += alphabetPosition + " ";
                }
            }

            return result.Trim();
        }

        public static int FindLongestSubstring(this string text)
        {
            int longestLenght = 0;

            for (int startPosition = 0; startPosition < text.Length; startPosition++)
            {
                string currentSubstring = "";

                for (int currentPosition = startPosition;currentPosition < text.Length; currentPosition++)
                {
                    char currentCharacter = text[currentPosition];

                    if (currentSubstring.Contains(currentCharacter))
                    {
                        break;
                    }

                    currentSubstring += currentCharacter;

                    if (currentSubstring.Length > longestLenght)
                    {
                        longestLenght = currentSubstring.Length;
                    }
                }
            }

            return longestLenght;
        }


        public static bool IsAlmostPalindrome(this string text)
        {
            int differentPairs = 0;
            int leftPosition = 0;
            int rightPosition = text.Length - 1;

            while (leftPosition < rightPosition)
            {
                if (text[leftPosition] != text[rightPosition])
                {
                    differentPairs++;
                }
                
                if (differentPairs > 1)
                {
                    return false;
                }

                leftPosition++;
                rightPosition--;
            }

            return true;
        }

    }
}
