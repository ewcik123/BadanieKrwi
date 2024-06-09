namespace BadanieKrwi.Models
{
    /// <summary>
    /// Klasa użyta przy wyświetlaniu komunikatów
    /// </summary>
    public class WiadomoscModel : KlasaBazowa
    {
        private string _tytul;
        public string Tytul
        {
            get { return _tytul; }
            set
            {
                if (_tytul != value)
                {
                    _tytul = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _tresc;
        public string Tresc
        {
            get { return _tresc; }
            set
            {
                if (_tresc != value)
                {
                    _tresc = value;
                    OnPropertyChanged();
                }
            }
        }

        public WiadomoscModel(string tytul, string tresc)
        {
            Tytul=tytul;
            Tresc=tresc;
        }
    }
}
