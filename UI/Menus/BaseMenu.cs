using System;
using System.Collections.Generic;
using System.Linq;
using TheSeer.Services;
using TheSeer.Utilities.Helpers;

namespace TheSeer.UI.Menu
{
    /// <summary>
    /// Base class for all menu implementations providing common functionality
    /// </summary>
    internal abstract class BaseMenu(Narrator narrator)
    {
        protected readonly Narrator _narrator = narrator;
        
        // Customizable menu styling
        protected virtual int TypingSpeed => 1;
        protected virtual string MenuPrefix => "-> ";

        public abstract bool Show();
        protected abstract List<string> GetMenuOptions();

        /// <summary>
        /// Displays interactive menu with arrow key navigation and returns selected option (1-based index)
        /// </summary>
        protected int ShowInteractiveMenu(string title, string? prompt = null)
        {
            ShowHeader(title);

            if (!string.IsNullOrEmpty(prompt))
            {
                ConsoleHelper.WriteMystical(prompt);
                ConsoleHelper.WriteBlankLine();
            }

            List<string> options = GetMenuOptions();
            
            if (options == null || options.Count == 0)
            {
                ShowError("No menu options available.");
                return 0;
            }

            int hoveredOption = 0;
            int menuStartLine = Console.CursorTop;
            int maxWidth = options.Max(opt => opt.Length) + MenuPrefix.Length;
            
            try
            {
                Console.CursorVisible = false;
                RenderAllOptions(options, hoveredOption, maxWidth);
                return NavigateMenu(options, ref hoveredOption, menuStartLine, maxWidth);
            }
            finally
            {
                Console.CursorVisible = true;
            }
        }

        private int NavigateMenu(List<string> options, ref int hoveredOption, int menuStartLine, int maxWidth)
        {
            while (true)
            {
                int previousOption = hoveredOption;
                
                hoveredOption = Console.ReadKey(true).Key switch
                {
                    ConsoleKey.DownArrow => (hoveredOption + 1) % options.Count,
                    ConsoleKey.UpArrow => (hoveredOption - 1 + options.Count) % options.Count,
                    ConsoleKey.Enter => hoveredOption + 101, // Special marker for return
                    ConsoleKey.Escape => 100, // Special marker for escape
                    _ => hoveredOption // No change
                };

                // Handle returns
                if (hoveredOption > 100) return hoveredOption - 100;

                if (previousOption != hoveredOption)
                {
                    RenderOption(previousOption, hoveredOption, options[previousOption], maxWidth, menuStartLine);
                    RenderOption(hoveredOption, hoveredOption, options[hoveredOption], maxWidth, menuStartLine);
                }
            }
        }

        private void RenderAllOptions(List<string> options, int hoveredIndex, int maxWidth)
        {
            for (int i = 0; i < options.Count; i++)
                RenderOption(i, hoveredIndex, options[i], maxWidth);
        }

        private void RenderOption(int optionIndex, int hoveredIndex, string optionText, int maxWidth, int menuStartLine = -1)
        {
            if (menuStartLine >= 0)
                Console.SetCursorPosition(0, menuStartLine + optionIndex);

            bool isHovered = optionIndex == hoveredIndex;
            string prefix = isHovered ? MenuPrefix : new string(' ', MenuPrefix.Length);
            ConsoleColor color = isHovered ? ConsoleHelper.Colors.CardName : ConsoleHelper.Colors.Info;
            
            ConsoleHelper.TypeWrite((prefix + optionText).PadRight(maxWidth), color, TypingSpeed);
            Console.WriteLine();
        }

        protected void ShowHeader(string title)
        {
            ConsoleHelper.Clear();
            ConsoleHelper.WriteHeader(title);
        }

        protected void PressAnyKey() => ConsoleHelper.WaitForKeyPress();
        
        protected void ShowError(string message)
        {
            ConsoleHelper.WriteError(message);
            PressAnyKey();
        }
    }
}