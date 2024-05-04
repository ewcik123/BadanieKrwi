using BadanieKrwi.Models;
using MahApps.Metro.Controls.Dialogs;

namespace BadanieKrwi.ViewModels
{
    public class RejestracjaViewModel : KlasaBazowa
    {
        #region Properties
        public IDialogCoordinator DialogCoordinator { get; set; }

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _haslo;
        public string Haslo
        {
            get => _haslo;
            set
            {
                if (_haslo != value)
                {
                    _haslo = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _powtorzHaslo;
        public string PowtorzHaslo
        {
            get => _powtorzHaslo;
            set
            {
                if (_powtorzHaslo != value)
                {
                    _powtorzHaslo = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _imie;

        public string Imie
        {
            get => _imie;
            set
            {
                if (_imie != value)
                {
                    _imie = value;
                    OnPropertyChanged();
                }
            }
        }


        private string _nazwisko;
        public string Nazwisko
        {
            get => _nazwisko;
            set
            {
                if (_nazwisko != value)
                {
                    _nazwisko = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _wiek;
        public int Wiek
        {
            get => _wiek;
            set
            {
                if (_wiek != value)
                {
                    _wiek = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _plec;
        public string Plec
        {
            get => _plec;
            set
            {
                if (_plec != value)
                {
                    _plec = value;
                    OnPropertyChanged();
                }
            }
        }

        private List<string> _plcie;
        public List<string> Plcie
        {
            get => _plcie;
            set
            {
                if (_plcie != value)
                {
                    _plcie = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool CzyMoznaSieZarejstrowac
            => !string.IsNullOrWhiteSpace(_email)
            && !string.IsNullOrWhiteSpace(_imie)
            && !string.IsNullOrWhiteSpace(_nazwisko)
            && _wiek >= 18 && _wiek <= 125
            && !string.IsNullOrWhiteSpace(_plec)
            && !string.IsNullOrWhiteSpace(_haslo)
            && !string.IsNullOrWhiteSpace(_powtorzHaslo)
            && _haslo == _powtorzHaslo;

        #endregion Properties

        #region Constructors
        public RejestracjaViewModel()
        {
            Inicializacja();
        }

        #endregion Constructors

        #region Methods
        #region Main
        private void Inicializacja()
        {
            _wiek = 18;

            InicjalizacjaPlci();
        }

        private void InicjalizacjaPlci()
        {
            _plcie =
            [
                "Kobieta",
                "Mężczyzna"
            ];

            Plec = _plcie[0];
        }

        #endregion Main

        #endregion Methods
    }
}