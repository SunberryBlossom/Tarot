using System;
using System.Collections.Generic;
using TheSeer.Managers;
using TheSeer.Services;
using TheSeer.Utilities.Helpers;

namespace TheSeer.UI.Menus
{
    /// <summary>
    /// Authentication menu for user login, registration, and application exit
    /// </summary>
    internal class AuthenticationMenu : BaseMenu
    {
        private readonly UserManager _userManager;

        public AuthenticationMenu(UserManager userManager, Narrator narrator) : base(narrator)
        {
            _userManager = userManager;
        }

        protected override List<string> GetMenuOptions()
        {
            return
            [
                "Login",
                "Create New Account",
                "Exit Application"
            ];
        }

        /// <summary>
        /// Shows authentication menu and returns true if user successfully logged in
        /// </summary>
        public override bool Show()
        {
            while (true)
            {
                int choice = ShowInteractiveMenu(
                    "The Seer's Threshold",
                    "Will you enter the chamber, or return to the mundane world?"
                );

                switch (choice)
                {
                    case 1: // Login
                        if (HandleLogin())
                            return true; // Successfully logged in
                        break;

                    case 2: // Create New Account
                        if (HandleRegistration())
                            return true; // Successfully registered and auto-logged in
                        break;

                    case 3: // Exit Application
                    case 0: // Escape pressed
                        if (ConfirmExit())
                            return false; // User wants to exit app
                        break;

                    default:
                        ShowError("An unexpected choice... try again.");
                        break;
                }
            }
        }

        private bool HandleLogin()
        {
            ShowHeader("Return to the Seer");
            _narrator.WelcomeReturningTraveler();

            // Get username
            string username = ConsoleHelper.ReadLine("  Username: ", ConsoleHelper.Colors.Info);

            if (string.IsNullOrWhiteSpace(username))
            {
                ShowError("You must speak your name, traveler.");
                return false;
            }

            // Get password
            string password = ConsoleHelper.ReadPassword("  Password: ", ConsoleHelper.Colors.Info);

            if (string.IsNullOrWhiteSpace(password))
            {
                ShowError("A password is required to enter.");
                return false;
            }

            // Attempt login
            var user = _userManager.Login(username, password);

            if (user != null)
            {
                _narrator.GreetUser(user.Username);
                PressAnyKey();
                return true; // Login successful
            }
            else
            {
                _narrator.WarnTraveler("I do not recognize you... Perhaps you have forgotten your name, or your words?");
                PressAnyKey();
                return false; // Login failed
            }
        }

        private bool HandleRegistration()
        {
            ShowHeader("A New Traveler Arrives");
            _narrator.WelcomeNewTraveler();

            // Get username
            string username = ConsoleHelper.ReadLine("  Username: ", ConsoleHelper.Colors.Info);

            if (string.IsNullOrWhiteSpace(username))
            {
                ShowError("You must choose a name, traveler.");
                return false;
            }

            // Get email
            string email = ConsoleHelper.ReadLine("  Email: ", ConsoleHelper.Colors.Info);

            if (string.IsNullOrWhiteSpace(email))
            {
                ShowError("An email is required for your journey.");
                return false;
            }

            // Get password
            string password = ConsoleHelper.ReadPassword("  Password: ", ConsoleHelper.Colors.Info);

            if (string.IsNullOrWhiteSpace(password))
            {
                ShowError("A password is required to secure your path.");
                return false;
            }

            // Confirm password
            string confirmPassword = ConsoleHelper.ReadPassword("  Confirm Password: ", ConsoleHelper.Colors.Info);

            if (password != confirmPassword)
            {
                ShowError("Your words do not match. The passwords must be identical.");
                return false;
            }

            // Attempt registration
            bool success = _userManager.NewUser(username, password, email, out string? errorMessage);

            if (success)
            {
                _narrator.SpeakWisdom($"Welcome, {username}. Your presence has been recorded in the ancient tome.");
                
                // Auto-login after successful registration
                var user = _userManager.Login(username, password);
                
                if (user != null)
                {
                    _narrator.SpeakWisdom("Step forward into the chamber. The cards await...");
                    PressAnyKey();
                    return true; // Registration successful and auto-logged in
                }
                else
                {
                    // Shouldn't happen, but handle gracefully
                    _narrator.SpeakWisdom("You may now return and enter with your new credentials.");
                    PressAnyKey();
                    return false;
                }
            }
            else
            {
                _narrator.WarnTraveler($"I cannot accept your offering: {errorMessage}");
                PressAnyKey();
                return false;
            }
        }

        private bool ConfirmExit()
        {
            var confirmation = new ConfirmationMenu(
                _narrator,
                "Depart from the Threshold",
                "Are you certain you wish to leave? The Seer will await your return.",
                "Yes, I must go",
                "No, I shall stay"
            );

            return confirmation.Show();
        }
    }
}
