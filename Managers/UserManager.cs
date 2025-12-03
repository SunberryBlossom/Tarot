using System;
using System.Collections.Generic;
using System.Text;
using TheSeer.Interfaces;
using TheSeer.Models;


namespace TheSeer.Managers
{
    internal class UserManager
    {
        private readonly IDataService _dataService;
        private readonly ICryptographyService _cryptoService;
        private User? _currentUser;

        public UserManager(IDataService dataService, ICryptographyService cryptoService)
        {
            _dataService = dataService;
            _cryptoService = cryptoService;
            _currentUser = null;
        }

        public User? Login(string username, string password)
        {
            var user = _dataService.GetUser(username);

            if (user == null)
            {
                return null;
            }

            bool passwordCorrect = _cryptoService.VerifyPassword(password, user.PasswordHash);

            if (passwordCorrect)
            {
                user.UpdateLastLogin();

                _dataService.SaveUser(user);

                _currentUser = user;

                return user;
            }

            return null;
        }

        public bool NewUser(string username, string password, string email)
        {
            if (_dataService.GetUser(username) != null)
            {
                return false;
            }

            string hashedPassword = _cryptoService.HashPassword(password);

            var newUser = new User(username, hashedPassword, email);

            _dataService.SaveUser(newUser);

            return true;
        }

        public void Logout()
        {
            _currentUser = null;
        }

        public User? GetCurrentUser()
        {
            return _currentUser;
        }
    }
}
