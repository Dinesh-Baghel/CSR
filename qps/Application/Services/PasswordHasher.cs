using Application.Interfaces.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(16);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(32);

            byte[] hashBytes = new byte[48];
            Buffer.BlockCopy(salt, 0, hashBytes, 0, 16);
            Buffer.BlockCopy(hash, 0, hashBytes, 16, 32);

            return Convert.ToBase64String(hashBytes);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            var hashBytes = Convert.FromBase64String(hashedPassword);

            byte[] salt = new byte[16];
            Buffer.BlockCopy(hashBytes, 0, salt, 0, 16);

            byte[] stored = new byte[32];
            Buffer.BlockCopy(hashBytes, 16, stored, 0, 32);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256);
            var computed = pbkdf2.GetBytes(32);

            return CryptographicOperations.FixedTimeEquals(stored, computed);
        }
        public string GenerateRandomPassword(int length = 8)
        {
            if (length < 8)
                throw new ArgumentException("Password length must be at least 8 characters.");

            // Character groups
            const string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lower = "abcdefghijklmnopqrstuvwxyz";
            const string digits = "0123456789";
            //const string special = "!@#$%^&*()-_=+<>?";
            const string special = "@#$";

            const string all = upper + lower + digits + special;

            var password = new List<char>();

            // Ensure at least one from each category
            password.Add(upper[RandomNumberGenerator.GetInt32(upper.Length)]);
            password.Add(lower[RandomNumberGenerator.GetInt32(lower.Length)]);
            password.Add(digits[RandomNumberGenerator.GetInt32(digits.Length)]);
            password.Add(special[RandomNumberGenerator.GetInt32(special.Length)]);

            // Fill remaining characters
            for (int i = password.Count; i < length; i++)
            {
                password.Add(all[RandomNumberGenerator.GetInt32(all.Length)]);
            }

            // Shuffle with secure RNG
            for (int i = 0; i < password.Count; i++)
            {
                int swapIndex = RandomNumberGenerator.GetInt32(password.Count);
                (password[i], password[swapIndex]) = (password[swapIndex], password[i]);
            }

            return new string(password.ToArray());
        }

    }

}
