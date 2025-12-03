using System;
using System.Security.Cryptography;
using TheSeer.Interfaces;

namespace TheSeer.Services
{
    internal class CryptographyService : ICryptographyService
    {
        private const int SaltSize = 32;
        private const int HashSize = 32;
        private const int Iterations = 200000;

        public string HashPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);

            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                Iterations,
                HashAlgorithmName.SHA256,
                HashSize);

            string base64Salt = Convert.ToBase64String(salt);
            string base64Hash = Convert.ToBase64String(hash);

            return $"{Iterations}.{base64Salt}.{base64Hash}";
        }

        public bool VerifyPassword(string password, string storedHash)
        {
            string[] parts = storedHash.Split(".");

            if (parts.Length != 3)
            {
                return false;
            }

            if (!int.TryParse(parts[0], out int iterations))
            {
                return false;
            }

            byte[] salt = Convert.FromBase64String(parts[1]);
            byte[] expectedHash = Convert.FromBase64String(parts[2]);

            byte[] actualHash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                iterations,
                HashAlgorithmName.SHA256,
                expectedHash.Length);

            return CryptographicOperations.FixedTimeEquals(actualHash, expectedHash);
        }
    }
}
