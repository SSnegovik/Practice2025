using System;
using System.Linq;

namespace task01
{
    public static class StringExtensions
    {
        public static bool IsPalindrome(this string input)
        {

            string palindrome = string.Empty;
            foreach (char c in input.ToLower())
            {
                if (!char.IsWhiteSpace(c) && !char.IsPunctuation(c))
                {
                    palindrome += c;
                }
            }

            if (string.IsNullOrEmpty(palindrome))
            {
                return false;
            }

            string reversed = new string(palindrome.Reverse().ToArray());
            return palindrome == reversed;
        }
    }
}
