using System;
using TheSeer.Utilities.Helpers;

namespace TheSeer.Models
{
    internal class User
    {
        // ----------------------- PROPERTIES --------------------------//
        public Guid Id { get; init; }
        public string Username { get; private set; }
        public string PasswordHash { get; private set; }
        public DateTime CreatedDate { get; init; }
        public DateTime? LastLogin { get; private set; }
        public string? Nickname { get; private set; }
        public string Email { get; private set; }
        public bool TwoAuthOpt { get; private set; }

        // ---------------- CONSTRUCTORS -----------------------------//
        public User(string username, string passwordHash, string email)
        {
            SetUsername(username);
            SetPasswordHash(passwordHash);
            SetEmail(email);

            Id = Guid.NewGuid();
            CreatedDate = DateTime.Now;
            TwoAuthOpt = false;
        }

        public User() { }

        // --------------- METHODS -----------------------//

        public void SetPassword(string hashedPassword)
        {
            SetPasswordHash(hashedPassword);
        }

        public void UpdateLastLogin()
        {
            LastLogin = DateTime.Now;
        }

        public void UpdateNickname(string? nickname)
        {
            SetNickname(nickname);
        }

        public void UpdateEmail(string email)
        {
            SetEmail(email);
        }

        public void UpdateTwoAuthOpt(bool opt)
        {
            TwoAuthOpt = opt;
        }

        public void UpdateUsername(string username)
        {
            SetUsername(username);
        }

        private void SetUsername(string username)
        {
            if (!ValidationHelper.IsLengthInRange(username, 3, 20) || !ValidationHelper.IsAlphanumericWithUnderscore(username))
            {
                throw new ArgumentException("Username must be 3-20 characters and contain only letters, numbers, and underscores", nameof(username));
            }

            Username = username;
        }

        private void SetPasswordHash(string passwordHash)
        {
            if (!ValidationHelper.IsNotEmpty(passwordHash))
            {
                throw new ArgumentException("Password hash cannot be empty", nameof(passwordHash));
            }

            PasswordHash = passwordHash;
        }

        private void SetEmail(string email)
        {
            if (!ValidationHelper.IsNotEmpty(email) || !ValidationHelper.ContainsAll(email, '@', '.'))
            {
                throw new ArgumentException("Email must be a valid format", nameof(email));
            }

            Email = email;
        }

        private void SetNickname(string? nickname)
        {
            if (nickname != null && !ValidationHelper.IsLengthInRange(nickname, 1, 50))
                throw new ArgumentException("Nickname must be between 1 and 50 characters", nameof(nickname));

            Nickname = nickname;
        }
    }
}
