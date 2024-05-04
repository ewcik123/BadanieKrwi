using BadanieKrwi.Data_Base;
using BadanieKrwi.Models;
using BadanieKrwi.Views;
using System.Windows;
using System.Windows.Input;

namespace BadanieKrwi.ViewModels
{
    public class RejestracjaViewModel : KlasaBazowa
    {
        #region Properties

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

        private bool _czyMoznaSieZarejstrowac
            => !string.IsNullOrWhiteSpace(_email)
            && !string.IsNullOrWhiteSpace(_imie)
            && !string.IsNullOrWhiteSpace(_nazwisko)
            && _wiek >= 18 && _wiek <= 125
            && !string.IsNullOrWhiteSpace(_plec)
            && !string.IsNullOrWhiteSpace(_haslo)
            && !string.IsNullOrWhiteSpace(_powtorzHaslo)
            && _haslo == _powtorzHaslo;

        #endregion Properties

        #region Commands
        public ICommand RejstracjstracjaWidokCommand { get; set; }
        #endregion Commands

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

            InicjalizacjaKomend();
            InicjalizacjaPlci();
        }

        private void InicjalizacjaPlci()
        {
            _plcie = new List<string>()
            {
                "Kobieta",
                "Mężczyzna"
            };

            Plec = _plcie[0];
        }

        private void InicjalizacjaKomend()
        {
            RejstracjstracjaWidokCommand = new RelayCommand(ExecRejstracjaWidok, x => _czyMoznaSieZarejstrowac);
        }

        #endregion Main

        private void ExecRejstracjaWidok(object obj)
        {
            if (obj is RejestracjaOkno r)
            {
                Uzytkownik nowyUzytkownik = new Uzytkownik
                {
                    Imie = Imie,
                    Nazwisko = Nazwisko,
                    Email = Email,
                    HasloHash = Haslo.GetHashCode().ToString(), // Hashujemy hasło
                    Wiek = Wiek,
                    Plec = Plec,
                    DataRejestracji = DateTime.Now
                };


                AppDbContext.Instance.Uzytkownik.Add(nowyUzytkownik); // Dodajemy nowego użytkownika do kontekstu
                AppDbContext.Instance.SaveChanges(); // Zapisujemy zmiany w bazie danych


                MessageBox.Show("Twoje dane zostały zapisane", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                r.Close();


            }
        }
        #endregion Methods
    }
}