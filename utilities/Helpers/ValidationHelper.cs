using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheSeer.Utilities.Helpers
{

    internal static class ValidationHelper
    {
        public static bool IsNotEmpty(string? value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        public static bool IsLengthInRange(string? value, int minLength, int maxLength)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            return value.Length >= minLength && value.Length <= maxLength;
        }

        public static bool IsAlphanumericWithUnderscore(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            return value.All(c => char.IsLetterOrDigit(c) || c == '_');
        }

        public static bool Contains(string? value, char character)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            return value.Contains(character);
        }

        public static bool ContainsAll(string? value, params char[] characters)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            return characters.All(c => value.Contains(c));
        }
    }
}
