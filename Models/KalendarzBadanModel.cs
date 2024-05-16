namespace BadanieKrwi.Models
{
    public class KalendarzBadanModel : KlasaBazowa
    {
        private Guid _id;
        public Guid Id
        {
            get => _id;
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged();
                }
            }
        }

        private Guid _idUzytkownika;
        public Guid IdUzytkownika
        {
            get => _idUzytkownika;
            set
            {
                if (_idUzytkownika != value)
                {
                    _idUzytkownika = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime _dataBadania;
        public DateTime DataBadania
        {
            get => _dataBadania;
            set
            {
                if (_dataBadania != value)
                {
                    _dataBadania = value;
                    OnPropertyChanged();
                }
            }
        }

        private Guid _idKliniki;
        public Guid IdKliniki
        {
            get => _idKliniki;
            set
            {
                if (_idKliniki != value)
                {
                    _idKliniki = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _typBadania;
        public string TypBadania
        {
            get => _typBadania;
            set
            {
                if (_typBadania != value)
                {
                    _typBadania = value;
                    OnPropertyChanged();
                }
            }
        }

        private string? _tresc;
        public string? Tresc
        {
            get => _tresc;
            set
            {
                if (_tresc != value)
                {
                    _tresc = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _czyUstawionoPrzypomnienie;
        public bool CzyUstawionoPrzypomnienie
        {
            get => _czyUstawionoPrzypomnienie;
            set
            {
                if (_czyUstawionoPrzypomnienie != value)
                {
                    _czyUstawionoPrzypomnienie = value;
                    OnPropertyChanged();
                }
            }
        }

        public KalendarzBadanModel() { }
        public KalendarzBadanModel(Guid idUzytkownika, Guid idKliniki, string nazwa, DateTime dataBadania, string tresc) : this()
        {
            IdUzytkownika = idUzytkownika;
            IdKliniki = idKliniki;
            TypBadania = nazwa;
            DataBadania = dataBadania;
            Tresc = tresc;
        }
    }
}