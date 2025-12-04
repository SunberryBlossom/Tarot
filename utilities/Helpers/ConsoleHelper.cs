using System;
using System.Threading;

namespace TheSeer.Utilities.Helpers
{
    internal static class ConsoleHelper
    {
        // Default typing speed (milliseconds per character)
        private const int DefaultTypingSpeed = 30;

        // Color scheme for The Seer
        public static class Colors
        {
            public static readonly ConsoleColor Header = ConsoleColor.Magenta;
            public static readonly ConsoleColor Mystical = ConsoleColor.DarkCyan;
            public static readonly ConsoleColor CardName = ConsoleColor.Yellow;
            public static readonly ConsoleColor Reversed = ConsoleColor.Red;
            public static readonly ConsoleColor Success = ConsoleColor.Green;
            public static readonly ConsoleColor Error = ConsoleColor.Red;
            public static readonly ConsoleColor Warning = ConsoleColor.DarkYellow;
            public static readonly ConsoleColor Normal = ConsoleColor.White;
            public static readonly ConsoleColor Info = ConsoleColor.Gray;
        }

        // ==================== TYPING ANIMATION ====================

        /// <summary>
        /// Writes text with typing animation effect
        /// </summary>
        public static void TypeWrite(string text, ConsoleColor color = ConsoleColor.White, int speed = DefaultTypingSpeed)
        {
            Console.ForegroundColor = color;
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(speed);
            }
            Console.ResetColor();
        }

        /// <summary>
        /// Writes text with typing animation effect and new line
        /// </summary>
        public static void TypeWriteLine(string text, ConsoleColor color = ConsoleColor.White, int speed = DefaultTypingSpeed)
        {
            TypeWrite(text, color, speed);
            Console.WriteLine();
        }

        // ==================== THEMED TYPING OUTPUT ====================

        public static void TypeHeader(string text, int speed = DefaultTypingSpeed)
        {
            Console.WriteLine();
            TypeWriteLine(new string('═', text.Length + 4), Colors.Header, speed / 2);
            TypeWrite("  ", Colors.Header, speed);
            TypeWriteLine(text, Colors.Header, speed);
            TypeWriteLine(new string('═', text.Length + 4), Colors.Header, speed / 2);
            Console.WriteLine();
        }

        public static void TypeMystical(string text, int speed = DefaultTypingSpeed)
        {
            TypeWriteLine(text, Colors.Mystical, speed);
        }

        public static void TypeSuccess(string text, int speed = DefaultTypingSpeed)
        {
            TypeWrite("✓ ", Colors.Success, speed);
            TypeWriteLine(text, Colors.Success, speed);
        }

        public static void TypeError(string text, int speed = DefaultTypingSpeed)
        {
            TypeWrite("✗ ", Colors.Error, speed);
            TypeWriteLine(text, Colors.Error, speed);
        }

        public static void TypeWarning(string text, int speed = DefaultTypingSpeed)
        {
            TypeWrite("⚠ ", Colors.Warning, speed);
            TypeWriteLine(text, Colors.Warning, speed);
        }

        // ==================== REGULAR OUTPUT (NO ANIMATION) ====================

        public static void Write(string text, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }

        public static void WriteLine(string text, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public static void WriteHeader(string text)
        {
            Console.WriteLine();
            WriteLine(new string('═', text.Length + 4), Colors.Header);
            WriteLine($"  {text}", Colors.Header);
            WriteLine(new string('═', text.Length + 4), Colors.Header);
            Console.WriteLine();
        }

        public static void WriteSuccess(string text)
        {
            Write("✓ ", Colors.Success);
            WriteLine(text, Colors.Success);
        }

        public static void WriteError(string text)
        {
            Write("✗ ", Colors.Error);
            WriteLine(text, Colors.Error);
        }

        public static void WriteWarning(string text)
        {
            Write("⚠ ", Colors.Warning);
            WriteLine(text, Colors.Warning);
        }

        public static void WriteMystical(string text)
        {
            WriteLine(text, Colors.Mystical);
        }

        // ==================== FORMATTING ====================

        public static void WriteDivider(char character = '═', int length = 50)
        {
            WriteLine(new string(character, length), Colors.Header);
        }

        public static void TypeDivider(char character = '═', int length = 50, int speed = 10)
        {
            TypeWriteLine(new string(character, length), Colors.Header, speed);
        }

        public static void WriteBlankLine()
        {
            Console.WriteLine();
        }

        public static void Clear()
        {
            Console.Clear();
        }

        // ==================== INPUT ====================

        public static string ReadLine(string prompt, ConsoleColor promptColor = ConsoleColor.White)
        {
            Write(prompt, promptColor);
            Console.ForegroundColor = ConsoleColor.Cyan;
            string? input = Console.ReadLine();
            Console.ResetColor();
            return input ?? string.Empty;
        }

        public static string TypeReadLine(string prompt, ConsoleColor promptColor = ConsoleColor.White, int speed = DefaultTypingSpeed)
        {
            TypeWrite(prompt, promptColor, speed);
            Console.ForegroundColor = ConsoleColor.Cyan;
            string? input = Console.ReadLine();
            Console.ResetColor();
            return input ?? string.Empty;
        }

        public static int ReadInt(string prompt, int min, int max)
        {
            while (true)
            {
                string input = ReadLine(prompt);

                if (int.TryParse(input, out int result))
                {
                    if (result >= min && result <= max)
                        return result;

                    WriteError($"Please enter a number between {min} and {max}.");
                }
                else
                {
                    WriteError("Invalid input. Please enter a number.");
                }
            }
        }

        public static int TypeReadInt(string prompt, int min, int max, int speed = DefaultTypingSpeed)
        {
            while (true)
            {
                string input = TypeReadLine(prompt, ConsoleColor.White, speed);

                if (int.TryParse(input, out int result))
                {
                    if (result >= min && result <= max)
                        return result;

                    TypeError($"Please enter a number between {min} and {max}.");
                }
                else
                {
                    TypeError("Invalid input. Please enter a number.");
                }
            }
        }

        // ==================== EFFECTS ====================

        public static void PauseForEffect(int milliseconds = 1000)
        {
            Thread.Sleep(milliseconds);
        }

        public static void WaitForKeyPress(string message = "Press any key to continue...")
        {
            WriteLine(message, Colors.Info);
            Console.ReadKey(true);
        }

        public static void TypeWaitForKeyPress(string message = "Press any key to continue...", int speed = DefaultTypingSpeed)
        {
            TypeWriteLine(message, Colors.Info, speed);
            Console.ReadKey(true);
        }

        // ==================== SPECIAL EFFECTS ====================

        /// <summary>
        /// Types text slowly with dramatic pauses for emphasis
        /// </summary>
        public static void TypeDramatic(string text, ConsoleColor color = ConsoleColor.White)
        {
            TypeWrite(text, color, 50);
            PauseForEffect(500);
        }

        /// <summary>
        /// Types text very fast (good for less important text)
        /// </summary>
        public static void TypeFast(string text, ConsoleColor color = ConsoleColor.White)
        {
            TypeWrite(text, color, 10);
        }

        /// <summary>
        /// Reveals text with a fade-in effect (simulated with typing)
        /// </summary>
        public static void RevealText(string text, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;

            // Start slow, speed up, then slow down again
            int length = text.Length;
            for (int i = 0; i < length; i++)
            {
                Console.Write(text[i]);

                // Calculate speed based on position
                int speed;
                if (i < length / 4)
                    speed = 50 - (i * 2); // Speed up
                else if (i > length * 3 / 4)
                    speed = 10 + ((i - length * 3 / 4) * 2); // Slow down
                else
                    speed = 10; // Full speed

                Thread.Sleep(Math.Max(10, speed));
            }

            Console.WriteLine();
            Console.ResetColor();
        }
    }
}