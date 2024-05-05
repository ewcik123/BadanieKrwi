using BadanieKrwi.Models.Database;
using System.Security.Cryptography;

namespace BadanieKrwi.Models
{
    public class UwierzytelnianieSerwis
    {
        private const int SaltSize = 32; // Rozmiar soli (w bajtach)
        private const int HashSize = 32; // Rozmiar wygenerowanego hasha (w bajtach)
        private const int Iterations = 10000; // Liczba iteracji algorytmu haszującego

        // Metoda do generowania soli
        private static string GenerateSalt()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[SaltSize];
                rng.GetBytes(salt);
                return Convert.ToBase64String(salt);
            }
        }

        // Metoda do hashowania hasła
        private static string ComputeHash(string password, string salt)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, Iterations))
            {
                byte[] hash = pbkdf2.GetBytes(HashSize);
                return Convert.ToBase64String(hash);
            }
        }

        // Metoda do rejestracji użytkownika
        public static async Task<bool> RejestracjaAsync(Uzytkownik user, string password)
        {
            // Generowanie soli
            string salt = GenerateSalt();

            // Haszowanie hasła
            string hashedPassword = ComputeHash(password, salt);

            // Ustawienie hasła hashowanego i soli dla użytkownika
            user.HasloHash = hashedPassword;
            user.Salt = salt;
            user.DataRejestracji = DateTime.Now;

            using AppDbContext cont = new();
            cont.Uzytkownik.Add(user);
            return await cont.SaveChangesAsync() > 0;
        }

        public static bool CzyUzytkownikIstnieje(string email)
        {
            using AppDbContext cont = new();
            return cont.Uzytkownik.Any(x => x.Email.Equals(email));
        }

        // Metoda do logowania użytkownika
        public static bool Logowanie(Uzytkownik user, string password)
        {
            // Haszowanie wprowadzonego hasła przy użyciu przechowywanej soli
            string hashedPassword = ComputeHash(password, user.Salt);

            // Porównanie wygenerowanego hasła z przechowywanym w bazie
            return hashedPassword == user.HasloHash;
        }
    }
}