using BadanieKrwi.Data_Base;
using BadanieKrwi.Models;
using BadanieKrwi.Views;
using System.Windows;
using System.Windows.Input;

namespace BadanieKrwi.ViewModels
{
    public class MainWindowViewModel : KlasaBazowa
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

        private bool _czyMoznaSieZalogowac => !string.IsNullOrWhiteSpace(_email) && !string.IsNullOrWhiteSpace(_haslo);


        #endregion Properties

        #region Commands
        public ICommand RejestracjaWidokCommand { get; set; }
        public ICommand LogowanieWidokCommand { get; set; }
        #endregion Commands

        #region Constructors
        public MainWindowViewModel()
        {
            Inicializacja();
        }

        #endregion Constructors

        #region Methods
        #region Main
        private void Inicializacja()
        {
            InicjalizacjaKomend();
        }

        private void InicjalizacjaKomend()
        {
            RejestracjaWidokCommand = new RelayCommand(ExecRejestracjaWidok);
            LogowanieWidokCommand = new RelayCommand(ExecLogowanieWidok, x => _czyMoznaSieZalogowac);
        }
        #endregion Main

        private void ExecRejestracjaWidok(object obj)
        {
            if (obj is MainWindow mw)
            {
                RejestracjaOkno rejWindow = new();
                rejWindow.Closing += ((sender, args) =>
                {
                    mw.ShowInTaskbar = true;
                    mw.WindowState = WindowState.Normal;
                });

                rejWindow.Show();
                mw.ShowInTaskbar = false;
                mw.WindowState = WindowState.Minimized;
            }
        }

        private void Logowanie(MainWindow mw)
        {
            //if (_email == "a@gmail.com" && _haslo == "haslo")
            //{
            var user = AppDbContext.Instance.Uzytkownik.FirstOrDefault(u => u.Email == _email); // Sprawdzamy czy istnieje użytkownik o podanym emailu

            if (user != null && user.HasloHash == _haslo.GetHashCode().ToString()) // Sprawdzamy czy hasło jest poprawne
            {
                Globals.ZalogowanyUzytkownik = user;
                MenuOkno menuWindow = new();
                menuWindow.Show();
                mw.Close();
            }
            else
            {
                MessageBox.Show("Nieprawidłowy adres email lub hasło.", "Błąd logowania", MessageBoxButton.OK, MessageBoxImage.Error);
            }


            //}
            //else if (_email == "a@gmail.com")
            //{
            //    MessageBox.Show("Nieprawidłowe hasło.", "Błąd logowania", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
            //else
            //{
            //    MessageBox.Show("Nieprawidłowy adres email.", "Błąd logowania", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
        }

        private void ExecLogowanieWidok(object obj)
        {
            if (obj is MainWindow mw)
                Logowanie(mw);
        }
        #endregion Methods  
    }
}