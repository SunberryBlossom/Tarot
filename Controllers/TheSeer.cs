using System;
using System.Collections.Generic;
using TheSeer.Interfaces;
using TheSeer.Managers;
using TheSeer.Models;
using TheSeer.Models.Enums;
using TheSeer.Services;
using TheSeer.UI.Menus;

namespace TheSeer.Controllers
{
    /// <summary>
    /// Application controller - orchestrates the entire application flow
    /// </summary>
    internal class TheSeer
    {
        // ----------------------- DEPENDENCIES --------------------------//
        private readonly UserManager _userManager;
        private readonly ReadingManager _readingManager;
        private readonly Narrator _narrator;

        private bool _isRunning;

        // ---------------- CONSTRUCTORS -----------------------------//
        
        public TheSeer(
            UserManager userManager,
            ReadingManager readingManager,
            Narrator narrator)
        {
            _userManager = userManager;
            _readingManager = readingManager;
            _narrator = narrator;
            _isRunning = false;
        }

        // --------------- PUBLIC METHODS -----------------------//

        /// <summary>
        /// Main application entry point - orchestrates the entire flow
        /// </summary>
        public void Run()
        {
            _isRunning = true;
            _narrator.Welcome();

            while (_isRunning)
            {
                // Authentication loop - allows multiple users to log in
                if (!AuthenticateUser())
                {
                    // User chose to exit from login screen
                    break;
                }

                // Main application loop for authenticated user
                MainLoop();

                // User logged out - loop back to authentication
                _userManager.Logout();
            }

            _narrator.Farewell();
        }

        /// <summary>
        /// Stops the application entirely
        /// </summary>
        public void Stop()
        {
            _isRunning = false;
        }

        // --------------- PRIVATE ORCHESTRATION METHODS -----------------------//

        /// <summary>
        /// Handles user authentication - returns false if user wants to exit app
        /// </summary>
        private bool AuthenticateUser()
        {
            var authMenu = new AuthenticationMenu(_userManager, _narrator);
            return authMenu.Show(); // Returns true if logged in, false if user wants to exit
        }

        /// <summary>
        /// Main application loop for authenticated user - delegates to MainMenu
        /// </summary>
        private void MainLoop()
        {
            bool userWantsToLogout = false;

            while (!userWantsToLogout && _isRunning)
            {
                var mainMenu = new MainMenu(this);
                userWantsToLogout = mainMenu.Show();
            }
        }

        // --------------- SERVICE ACCESS FOR MENUS -----------------------//

        public UserManager UserManager => _userManager;
        public ReadingManager ReadingManager => _readingManager;
        public Narrator Narrator => _narrator;
    }
}
