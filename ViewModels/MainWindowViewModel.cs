﻿using BadanieKrwi.Models;
using BadanieKrwi.Models.Database;
using BadanieKrwi.Views;
using MahApps.Metro.Controls.Dialogs;
using System.Windows;
using System.Windows.Input;

namespace BadanieKrwi.ViewModels
{
    public class MainWindowViewModel : KlasaBazowa
    {
        #region Properties
        public IDialogCoordinator DialogCoordinator { get; set; }

        private string _tekstEtykietyOkna = "Logowanie";
        public string TekstEtykietyOkna
        {
            get => _tekstEtykietyOkna;
            set
            {
                if (_tekstEtykietyOkna != value)
                {
                    _tekstEtykietyOkna = value;
                    OnPropertyChanged();
                }
            }
        }

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

        private bool _czyMoznaSieZalogowac => !string.IsNullOrWhiteSpace(_email) && _email.Contains("@") && !string.IsNullOrWhiteSpace(_haslo);

        private Visibility _pokazGridLogowania = Visibility.Visible;
        public Visibility PokazGridLogowania
        {
            get => _pokazGridLogowania;
            set
            {
                if (_pokazGridLogowania != value)
                {
                    _pokazGridLogowania = value;
                    OnPropertyChanged();
                }
            }
        }

        private Visibility _pokazGridRejestracji = Visibility.Collapsed;
        public Visibility PokazGridRejestracji
        {
            get => _pokazGridRejestracji;
            set
            {
                if (_pokazGridRejestracji != value)
                {
                    _pokazGridRejestracji = value;
                    OnPropertyChanged();
                }
            }
        }

        private RejestracjaViewModel _rejestracjaVM;
        public RejestracjaViewModel RejestracjaVM
        {
            get => _rejestracjaVM;
            set
            {
                if (_rejestracjaVM != value)
                {
                    _rejestracjaVM = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion Properties

        #region Commands
        public ICommand RejestracjaWidokCommand { get; set; }
        public ICommand RejestracjaCommand { get; set; }
        public ICommand PowrotDoLogowaniaCommand { get; set; }
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
            RejestracjaVM = new();
            InicjalizacjaKomend();
            SprawdzTerminKolejnegoBadania();
        }

        private void InicjalizacjaKomend()
        {
            RejestracjaWidokCommand = new RelayCommand(ExecRejestracjaWidok);
            RejestracjaCommand = new RelayCommand(ExecRejestracja, x => RejestracjaVM.CzyMoznaSieZarejstrowac);
            LogowanieWidokCommand = new RelayCommand(ExecLogowanieWidok, x => _czyMoznaSieZalogowac);
            PowrotDoLogowaniaCommand = new RelayCommand(ExecPowrotDoLogowania);
        }

        public async void SprawdzTerminKolejnegoBadania()
        {
            using AppDbContext cont = new();
            var obecnaData = DateTime.Now;

            var wpisyWKalendarzu = cont.Kalendarz.Where(x => x.CzyUstawionoPrzypomnienie
                && x.DataBadania.Date == obecnaData.AddDays(1).Date).ToList();
            if (wpisyWKalendarzu == null || wpisyWKalendarzu.Count == 0)
                return;

            foreach (var wpis in wpisyWKalendarzu)
            {
                try
                {
                    MenadzerPowiadomien.Instance.WyslijPowiadomienieOBadaniu(wpis);
                    wpis.CzyUstawionoPrzypomnienie = false;
                    cont.Update(wpis);
                    cont.SaveChanges();
                }
                catch (Exception ex)
                {
                    await ShowMessageAsync(new("Wysłanie powiadomienia", $"Błąd podczas wysyłania powiadomienia:\n{ex.Message}"), this);
                }
            }
        }
        #endregion Main

        private void ExecRejestracjaWidok(object obj)
        {
            PrzejdzDoRejestracji();
        }

        private async void ExecRejestracja(object obj)
        {
            if (obj is MainWindow mw)
            {
                Uzytkownik nowyUzytkownik = new()
                {
                    Imie = RejestracjaVM.Imie,
                    Nazwisko = RejestracjaVM.Nazwisko,
                    Email = RejestracjaVM.Email,
                    Wiek = RejestracjaVM.Wiek,
                    Plec = RejestracjaVM.Plec,
                    DataRejestracji = DateTime.Now
                };

                Email = RejestracjaVM.Email;
                Haslo = RejestracjaVM.Haslo;

                if (UwierzytelnianieSerwis.CzyUzytkownikIstnieje(RejestracjaVM.Email))
                {
                    await ShowMessageAsync(new("Rejestracja", "Użytkownik już zarejestrowany"), this, DialogCoordinator);
                    return;
                }

                if (await UwierzytelnianieSerwis.RejestracjaAsync(nowyUzytkownik, RejestracjaVM.Haslo))
                {
                    await ShowMessageAsync(new("Rejestracja", "Twoje dane zostały zapisane"), this, DialogCoordinator);
                    Logowanie(mw);
                }
            }
        }

        private async void Logowanie(MainWindow mw)
        {
            using AppDbContext cont = new();
            var user = cont.Uzytkownik.FirstOrDefault(u => u.Email == _email);  // Sprawdzamy czy istnieje użytkownik o podanym emailu

            if (user != null && UwierzytelnianieSerwis.Logowanie(user, _haslo)) // Sprawdzamy czy hasło jest poprawne
            {
                Globals.ZalogowanyUzytkownik = user;
                MenuOkno menuWindow = new();
                menuWindow.Show();
                mw.Close();
            }
            else
                await ShowMessageAsync(new("Logowanie", "Nieprawidłowy adres email lub hasło."), this, DialogCoordinator);
        }

        private void ExecLogowanieWidok(object obj)
        {
            if (obj is MainWindow mw)
                Logowanie(mw);
        }

        private void ExecPowrotDoLogowania(object obj)
        {
            PrzejdzDoLogowania();
        }

        private void PrzejdzDoLogowania()
        {
            PokazGridLogowania = Visibility.Visible;
            PokazGridRejestracji = Visibility.Collapsed;
            TekstEtykietyOkna = "Logowanie";
        }

        private void PrzejdzDoRejestracji()
        {
            PokazGridLogowania = Visibility.Collapsed;
            PokazGridRejestracji = Visibility.Visible;
            TekstEtykietyOkna = "Rejestracja";
        }
        #endregion Methods  
    }
}