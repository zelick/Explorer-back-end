using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class PasswordHasher
    {
        public string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Konvertujte lozinku u bajt niz
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                // Izračunajte heš
                byte[] hashedBytes = sha256.ComputeHash(passwordBytes);

                // Konvertujte bajt niz u heširanu lozinku u hexadecimalni format
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        public bool VerifyPassword(string inputPassword, string storedHash)
        {
            // Heširaj unesenu lozinku
            string hashedInput = HashPassword(inputPassword);

            // Uporedi heš unesene lozinke sa hešem koji je sačuvan u bazi podataka
            return string.Equals(hashedInput, storedHash, StringComparison.OrdinalIgnoreCase);
        }
    }
}
