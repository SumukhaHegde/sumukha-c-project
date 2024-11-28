using System.Security.Cryptography;

namespace Infrastructure.Persistance.Repositories.UserRepository
{
    public class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            // Generate a cryptographic random number for the salt
            byte[] salt = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }

            // Derive the hash using PBKDF2 algorithm (Rfc2898DeriveBytes)
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000); // 10000 iterations
            byte[] hash = pbkdf2.GetBytes(20); // 20 bytes for the hash

            // Combine the salt and password hash into a single array
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            // Convert the combined salt and hash to a Base64 string for storage
            return Convert.ToBase64String(hashBytes);
        }

        public static bool VerifyPassword(string enteredPassword, string storedHash)
        {
            // Convert the stored hash (Base64) back to byte array
            byte[] hashBytes = Convert.FromBase64String(storedHash);

            // Extract the salt from the stored hash
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            // Hash the entered password with the same salt
            var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            // Compare the new hash with the hash from the stored password
            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
