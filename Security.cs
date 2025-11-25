using System;
using System.Security.Cryptography;
using System.Text;

namespace GestionDesMedicaments.Classes
{
    internal class Security
    {
        // Hashage simple SHA256
        public static string HashPassword(string password)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha.ComputeHash(bytes);
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hash)
                    sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }

        // Vérifie si le mot de passe correspond au hash
        public static bool VerifyPassword(string password, string hash)
        {
            string hashedInput = HashPassword(password);
            return hashedInput == hash;
        }
    }
}
