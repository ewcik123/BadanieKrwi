namespace BadanieKrwi.Models.Database
{
    public class Uzytkownik
    {
        public int Id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Email { get; set; }
        public string HasloHash { get; set; }
        public string Salt { get; set; }
        public int Wiek { get; set; }
        public string Plec { get; set; }
        public DateTime DataRejestracji { get; set; }
    }
}
