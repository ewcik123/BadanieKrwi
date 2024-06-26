﻿using BadanieKrwi.Models;
using BadanieKrwi.Models.Database;
using BadanieKrwi.Views;
using MahApps.Metro.Controls.Dialogs;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BadanieKrwi.ViewModels
{
    public class KlinikiViewModel : KlasaBazowa
    {
        #region Properties
        public IDialogCoordinator DialogCoordinator { get; set; }

        private ObservableCollection<Klinika> _kliniki;
        public ObservableCollection<Klinika> Kliniki
        {
            get => _kliniki;
            set
            {
                if (_kliniki != value)
                {
                    _kliniki = value;
                    OnPropertyChanged();
                }
            }
        }
        private Klinika _wybranaKlinika;
        public Klinika WybranaKlinika
        {
            get => _wybranaKlinika;
            set
            {
                if (_wybranaKlinika != value)
                {
                    _wybranaKlinika = value;
                    NowaKlinika = new Klinika(_wybranaKlinika);
                    OnPropertyChanged();
                }
            }
        }

        private Klinika _nowaKlinika;
        public Klinika NowaKlinika
        {
            get => _nowaKlinika;
            set
            {
                if (_nowaKlinika != value)
                {
                    _nowaKlinika = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool czyMoznaZapisac
        => NowaKlinika?.Id != Guid.NewGuid()
            && !string.IsNullOrWhiteSpace(NowaKlinika?.Adres)
            && !string.IsNullOrWhiteSpace(NowaKlinika?.Telefon)
            && Klinika.IsValidPhoneNumber(NowaKlinika?.Telefon);
        #endregion Properties

        #region Commands
        public ICommand WrocCommand { get; set; }
        public ICommand ZapiszCommand { get; set; }
        public ICommand NowyCommand { get; set; }

        #endregion Commands

        #region Constructors
        public KlinikiViewModel()
        {
            Inicjalizacja();
            WczytajKliniki();
        }
        #endregion Constructors

        #region Methods
        #region Main
        private void Inicjalizacja()
        {
            NowaKlinika = new();
            InicjalizacjaKomend();
        }

        private void InicjalizacjaKomend()
        {
            WrocCommand = new RelayCommand(ExecWroc);
            ZapiszCommand = new RelayCommand(ExecZapisz, x => czyMoznaZapisac);
            NowyCommand = new RelayCommand(ExecNowy);
        }

        private void WczytajKliniki()
        {
            using AppDbContext cont = new();
            Kliniki = new ObservableCollection<Klinika>([.. cont.Kliniki]);
            WybranaKlinika = Kliniki.FirstOrDefault();
        }

        private async Task<bool> Aktualizuj()
        {
            using AppDbContext cont = new();
            WiadomoscModel wiadomosc = null;
            bool czyZapisano = false;

            if (WybranaKlinika != null)
            {
                WybranaKlinika.AktualizujKlinike(NowaKlinika);
                cont.Update(WybranaKlinika);
                czyZapisano =  cont.SaveChanges() > 0;
                wiadomosc = new("Aktualizacja", czyZapisano ? "Zaktualizowano klinika." : "Nie udało się zaktualizować kliniki");
            }
            else if (WybranaKlinika == null)
            {
                cont.Add(NowaKlinika);
                czyZapisano =  cont.SaveChanges() > 0;
                wiadomosc = new("Nowa", czyZapisano ? "Dodano nową klinikę" : "Nie udało się stworzyć kliniki.");
            }

            await ShowMessageAsync(wiadomosc, this, DialogCoordinator);
            return czyZapisano;
        }

        #endregion Main
        private void ExecWroc(object obj)
        {
            if (obj is KlinikiOkno ko)
                ko.Close();
        }

        private async void ExecZapisz(object obj)
        {
            if (obj is KlinikiOkno ko && await Aktualizuj())
                ko.Close();
        }

        private void ExecNowy(object obj)
        {
            WybranaKlinika = null;
            NowaKlinika = new Klinika() { Id = Guid.NewGuid() };
        }
        #endregion Methods 
    }
}