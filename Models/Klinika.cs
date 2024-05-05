namespace BadanieKrwi.Models
{
    public class Klinika : KlasaBazowa
    {
        public Guid Id { get; set; }
        private string _adres;
        private string _nrTel;
        private string? _informacja;
        private string _nazwa;

        public string Adres
        {
            get { return _adres; }
            set
            {
                if (_adres != value)
                {
                    _adres = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Telefon
        {
            get { return _nrTel; }
            set
            {
                if (_nrTel != value)
                {
                    _nrTel = value;
                    NazwaITelefon = $"{_nazwa} ({Telefon})";
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(NazwaITelefon));
                }
            }
        }

        public string? Informacja
        {
            get { return _informacja; }
            set
            {
                if (_informacja != value)
                {
                    _informacja = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Nazwa
        {
            get { return _nazwa; }
            set
            {
                if (_nazwa != value)
                {
                    _nazwa = value;
                    NazwaITelefon = $"{_nazwa} ({Telefon})";
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(NazwaITelefon));
                }
            }
        }

        public string NazwaITelefon { get; set; }

        public Klinika() { }

        public Klinika(Klinika klinika) : this()
        {
            AktualizujKlinike(klinika);
        }

        public void AktualizujKlinike(Klinika klinika)
        {
            if (klinika == null)
                return;

            Nazwa = klinika.Nazwa;
            Id = klinika.Id;
            Adres = klinika.Adres;
            Telefon = klinika.Telefon;
            Informacja = klinika.Informacja;
        }
    }
}