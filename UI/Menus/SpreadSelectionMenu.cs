using System;
using System.Collections.Generic;
using System.Linq;
using TheSeer.Models;
using TheSeer.Models.Enums;
using TheSeer.Utilities.Helpers;

namespace TheSeer.UI.Menus
{
    internal class SpreadSelectionMenu : BaseMenu
    {
        private readonly Controllers.TheSeer _app;
        private List<Spread> _spreads = new();
        private List<DeckType> _deckValues = new();
        private DeckType? _selectedDeck;

        public SpreadSelectionMenu(Controllers.TheSeer app) : base(app.Narrator)
        {
            _app = app ?? throw new ArgumentNullException(nameof(app));
        }

        // Overload to start menu already scoped to a deck
        public SpreadSelectionMenu(Controllers.TheSeer app, DeckType selectedDeck) : base(app.Narrator)
        {
            _app = app ?? throw new ArgumentNullException(nameof(app));
            _selectedDeck = selectedDeck;
        }

        protected override List<string> GetMenuOptions()
        {
            // Build option list dynamically from available spreads and include a Back option
            var names = _spreads.Select(s => s.Name).ToList();
            names.Add("Back");
            return names;
        }

        /// <summary>
        /// Shows spread selection, (if needed) deck selection, performs the reading and narrates it.
        /// </summary>
        public override bool Show()
        {
            var currentUser = _app.UserManager.GetCurrentUser();
            if (currentUser == null)
            {
                ShowError("No user is currently logged in.");
                return true; // force return to parent (MainMenu will handle)
            }

            // Load available spreads and deck values once
            var allSpreads = _app.ReadingManager.GetAllAvailableSpreads().ToList();
            _deckValues = Enum.GetValues(typeof(DeckType)).Cast<DeckType>().ToList();

            if (_selectedDeck.HasValue)
            {
                _spreads = allSpreads.Where(s => s.Deck == _selectedDeck.Value).ToList();
            }
            else
            {
                // show only spreads supported by any deck (or all spreads)
                _spreads = allSpreads.ToList();
            }

            if (_spreads.Count == 0)
            {
                ShowError(_selectedDeck.HasValue
                    ? $"No spreads are available for {_selectedDeck.Value}."
                    : "No spreads are currently available.");
                return false;
            }

            while (true)
            {
                // If deck already selected via constructor, skip deck prompt and go straight to spreads
                DeckType chosenDeck;
                if (_selectedDeck.HasValue)
                {
                    chosenDeck = _selectedDeck.Value;
                }
                else
                {
                    var deck = PromptForDeck(_deckValues);
                    if (deck == null)
                        return false; // user chose Back at deck prompt -> return to caller

                    chosenDeck = deck.Value;

                    // now filter spreads by chosen deck
                    _spreads = allSpreads.Where(s => s.Deck == chosenDeck).ToList();

                    if (_spreads.Count == 0)
                    {
                        ShowError($"No spreads available for {chosenDeck}. Choose a different deck.");
                        continue;
                    }
                }

                int spreadChoice = ShowInteractiveMenu($"Choose a Spread ({chosenDeck})", "Select the spread you wish to consult:");

                // Back or Escape (out of range treated as back)
                if (spreadChoice <= 0 || spreadChoice > _spreads.Count + 1)
                    return false;

                if (spreadChoice == _spreads.Count + 1) // Back option
                    return false;

                var selectedSpread = _spreads[spreadChoice - 1];
                _narrator.IntroduceSpread(selectedSpread);

                // Prompt for optional question to save with the reading
                ConsoleHelper.WriteBlankLine();
                string questionInput = ConsoleHelper.ReadLine("Optional - enter a question to save with this reading (leave empty to skip): ", ConsoleHelper.Colors.Info);
                string? question = string.IsNullOrWhiteSpace(questionInput) ? null : questionInput.Trim();

                try
                {
                    var reading = _app.ReadingManager.CreateReading(currentUser.Id, selectedSpread.Type, chosenDeck, question);
                    _narrator.NarrateReading(reading, selectedSpread);
                    PressAnyKey();
                    return false; // return to main menu after successful reading
                }
                catch (Exception ex)
                {
                    // Friendly message for business-rule failures (e.g., daily limit)
                    _narrator.WarnTraveler(ex.Message);
                    PressAnyKey();
                    // allow user to reselect spread or deck
                    if (_selectedDeck.HasValue)
                        return false; // if deck fixed, return to caller after the error
                }
            }
        }

        // Prompt user to choose a deck. Returns null if user selects Back.
        private DeckType? PromptForDeck(IReadOnlyList<DeckType> deckValues)
        {
            while (true)
            {
                ShowHeader("Choose Deck");
                ConsoleHelper.TypeWriteLine("Select the deck to use for this reading:", ConsoleHelper.Colors.Mystical, 1);
                ConsoleHelper.WriteBlankLine();

                for (int i = 0; i < deckValues.Count; i++)
                {
                    ConsoleHelper.TypeWriteLine($"  {i + 1}. {deckValues[i]}", ConsoleHelper.Colors.Info, 1);
                }

                ConsoleHelper.TypeWriteLine($"  {deckValues.Count + 1}. Back", ConsoleHelper.Colors.Info, 1);
                ConsoleHelper.WriteBlankLine();

                string input = ConsoleHelper.ReadLine("Choose deck number: ", ConsoleHelper.Colors.Info);

                if (!int.TryParse(input, out int deckChoice))
                {
                    ShowError("Invalid input. Enter the number of the deck.");
                    continue;
                }

                if (deckChoice == deckValues.Count + 1 || deckChoice == 0)
                    return null; // Back

                if (deckChoice < 1 || deckChoice > deckValues.Count)
                {
                    ShowError("Choice out of range. Try again.");
                    continue;
                }

                return deckValues[deckChoice - 1];
            }
        }
    }
}
