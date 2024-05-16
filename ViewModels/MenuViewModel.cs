﻿using BadanieKrwi.Models;
using BadanieKrwi.Models.Database;
using BadanieKrwi.Views;
using MahApps.Metro.Controls.Dialogs;
using System.Windows.Input;

namespace BadanieKrwi.ViewModels
{
    public class MenuViewModel : KlasaBazowa
    {
        #region Properties
        public IDialogCoordinator DialogCoordinator { get; set; }
        public ICommand LoadedCommand { get; set; }
        public ICommand NoweBadanieCommand { get; set; }
        public ICommand TwojeBadaniaCommand { get; set; }
        public ICommand KalendarzBadanCommand { get; set; }
        public ICommand KlinikiCommand { get; set; }
        public ICommand StatystykiCommand { get; set; }
        public ICommand InformacjeCommand { get; set; }

        #endregion Properties

        #region Constructors
        public MenuViewModel()
        {
            Inicjalizacja();
        }

        #endregion Constructors

        #region Methods
        #region Main
        private void Inicjalizacja()
        {
            InicjalizacjaKomend();
        }

        private void InicjalizacjaKomend()
        {
            LoadedCommand = new RelayCommand(ExecLoaded);
            NoweBadanieCommand = new RelayCommand(ExecNoweBadanieWidok);
            TwojeBadaniaCommand = new RelayCommand(ExecTwojeBadanieWidok);
            KalendarzBadanCommand = new RelayCommand(ExecKalendarzWidok);
            KlinikiCommand = new RelayCommand(ExecKlinikiWidok);
            StatystykiCommand = new RelayCommand(ExecStatystykiWidok);
            InformacjeCommand = new RelayCommand(ExecInformacjeWidok);
        }

        public async void SprawdzTerminKolejnegoBadania()
        {
            using AppDbContext cont = new();
            var obecnaData = DateTime.Now;
            var wpisyWKalendarzu = cont.Kalendarz.Where(x => x.IdUzytkownika == Globals.ZalogowanyUzytkownik.Id
                && x.CzyUstawionoPrzypomnienie
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
                    await ShowMessageAsync($"Błąd podczas wysyłania powiadomienia:\n{ex.Message}", "Wysłanie powiadomienia", this);
                }
            }
        }

        #endregion Main
        private void ExecLoaded(object obj)
        {
            SprawdzTerminKolejnegoBadania();
        }

        private async void ExecNoweBadanieWidok(object obj)
        {
            using AppDbContext cont = new();
            var brakKlinik = !cont.Kliniki.Any();
            if (brakKlinik)
            {
                await ShowMessageAsync("Brak klinik. Zanim dodasz badnie musisz utworzyć klinikę", "Nowe Badanie", MessageDialogStyle.Affirmative, this, DialogCoordinator);
                return;
            }
            NoweBadanieOkno badanieWindow = new();
            (badanieWindow.DataContext as NoweBadaniaViewModel).NoweBadanie.CzyZmodyfikowano = false;
            if (badanieWindow.ShowDialog().Value)
            {

            }
        }

        private void ExecTwojeBadanieWidok(object obj)
        {
            TwojeBadaniaOkno badanieWindow = new();
            badanieWindow.ShowDialog();
        }

        private void ExecKalendarzWidok(object obj)
        {
            KalendarzBadanOkno kalendarz = new();
            kalendarz.ShowDialog();
        }

        private void ExecKlinikiWidok(object obj)
        {
            KlinikiOkno klinikiWindow = new();
            klinikiWindow.ShowDialog();
        }

        private void ExecStatystykiWidok(object obj)
        {
            StatystykiOkno statystyki = new();
            statystyki.ShowDialog();
        }

        private void ExecInformacjeWidok(object obj)
        {
            if (obj is MenuOkno m)
            {
                //m.Close();
            }
        }
        #endregion Methods
    }
}