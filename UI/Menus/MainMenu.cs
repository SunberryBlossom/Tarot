using System;
using System.Collections.Generic;
using TheSeer.Controllers;
using TheSeer.Models.Enums;
using TheSeer.Utilities.Helpers;

namespace TheSeer.UI.Menus
{
    /// <summary>
    /// Main menu for authenticated users - provides access to core application features
    /// </summary>
    internal class MainMenu : BaseMenu
    {
        private readonly Controllers.TheSeer _app;

        public MainMenu(Controllers.TheSeer app) : base(app.Narrator)
        {
            _app = app;
        }

        protected override List<string> GetMenuOptions()
        {
            return
            [
                "New Reading",
                "View Reading History",
                "Profile Settings",
                "About The Seer",
                "Logout"
            ];
        }

        /// <summary>
        /// Displays main menu and returns true if user chooses to logout
        /// </summary>
        public override bool Show()
        {
            var currentUser = _app.UserManager.GetCurrentUser();
            
            if (currentUser == null)
            {
                ShowError("No user is currently logged in.");
                return true; // Force logout if no user
            }

            while (true)
            {
                int choice = ShowInteractiveMenu(
                    $"The Seer's Chamber - {currentUser.Username}",
                    "What mysteries do you seek, traveler?"
                );

                switch (choice)
                {
                    case 1: // New Reading
                        HandleNewReading();
                        break;

                    case 2: // View Reading History
                        HandleReadingHistory();
                        break;

                    case 3: // Profile Settings
                        HandleProfileSettings();
                        break;

                    case 4: // About
                        HandleAbout();
                        break;

                    case 5: // Logout
                    case 0: // Escape pressed
                        if (ConfirmLogout())
                            return true;
                        break;

                    default:
                        ShowError("An unexpected choice... try again.");
                        break;
                }
            }
        }

        private void HandleNewReading()
        {
            _narrator.TransitionToReading();

            // Prompt for deck first
            var deckValues = Enum.GetValues(typeof(DeckType)).Cast<DeckType>().ToList();

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
                {
                    // Back to main menu
                    return;
                }

                if (deckChoice < 1 || deckChoice > deckValues.Count)
                {
                    ShowError("Choice out of range. Try again.");
                    continue;
                }

                var chosenDeck = deckValues[deckChoice - 1];

                // Launch spread selection scoped to chosen deck
                var spreadMenu = new SpreadSelectionMenu(_app, chosenDeck);
                spreadMenu.Show();
                return;
            }
        }

        private void HandleReadingHistory()
        {
            _narrator.TransitionToMenu("Reading History");
            // TODO: Create and call ReadingHistoryMenu
            // var historyMenu = new ReadingHistoryMenu(_app);
            // historyMenu.Show();
            
            _narrator.SpeakWisdom("Your past readings remain shrouded in mist... for now.");
            PressAnyKey();
        }

        private void HandleProfileSettings()
        {
            _narrator.TransitionToMenu("Profile Settings");
            // TODO: Create and call ProfileSettingsMenu
            // var profileMenu = new ProfileSettingsMenu(_app);
            // profileMenu.Show();
            
            _narrator.SpeakWisdom("The path to self-knowledge is not yet illuminated...");
            PressAnyKey();
        }

        private void HandleAbout()
        {
            ShowHeader("About The Seer");
            
            _narrator.SpeakWisdom("I am The Seer, guardian of the ancient cards.");
            _narrator.SpeakWisdom("Through the tarot, I glimpse the threads of fate that bind all things.");
            _narrator.SpeakWisdom("Seek my counsel, and the cards shall reveal what lies hidden.");
            
            PressAnyKey();
        }

        private bool ConfirmLogout()
        {
            var confirmation = new ConfirmationMenu(
                _narrator,
                "Farewell",
                "Do you truly wish to depart from my chamber?",
                "Yes, I must go",
                "No, I shall stay"
            );

            bool shouldLogout = confirmation.Show();

            if (shouldLogout)
            {
                _narrator.SpeakWisdom("Until we meet again, traveler...");
            }

            return shouldLogout;
        }
    }
}
