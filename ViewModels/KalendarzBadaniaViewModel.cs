using BadanieKrwi.Models;
using BadanieKrwi.Models.Database;
using BadanieKrwi.Views;
using MahApps.Metro.Controls.Dialogs;
using System.Windows.Controls;
using System.Windows.Input;

namespace BadanieKrwi.ViewModels
{
    public class KalendarzBadaniaViewModel : KlasaBazowa
    {
        #region Properties
        public IDialogCoordinator DialogCoordinator { get; set; }
        public DateModel DataBadania { get; set; }
        public List<Klinika> Kliniki { get; set; }
        public Klinika WybranaKlinika { get; set; }
        public string Tresc { get; set; }

        public List<String> TypyBadan { get; set; }
        public string WybranyTypBadania { get; set; }

        public bool czyMoznaKliknac
        {
            get => Kliniki?.Count > 0
                && WybranaKlinika != null
                && !string.IsNullOrWhiteSpace(WybranyTypBadania);
        }
        #endregion Properties

        #region Commands
        public ICommand LoadedCommand { get; set; }
        public ICommand WrocCommand { get; set; }
        public ICommand ZaplanujBadanieCommand { get; set; }


        #endregion Commands

        #region Constructors
        public KalendarzBadaniaViewModel()
        {
            Inicjalizacja();
        }
        #endregion Constructors
        
        #region Methods
        #region Main
        private void Inicjalizacja()
        {
            DataBadania = new DateModel();

            InicjalizacjaKlinik();
            InicjalizacjaTypyBadan();
            InicjalizacjaKomend();
        }

        private void InicjalizacjaKlinik()
        {
            using AppDbContext cont = new();
            Kliniki = new List<Klinika>([.. cont.Kliniki]);
            WybranaKlinika = Kliniki[0];
        }

        private void InicjalizacjaTypyBadan()
        {
            TypyBadan = Globals.TypyBadan;
            WybranyTypBadania = TypyBadan.FirstOrDefault();
        }

        private void InicjalizacjaKomend()
        {
            LoadedCommand = new RelayCommand(ExecLoaded);
            WrocCommand = new RelayCommand(ExecWroc);
            ZaplanujBadanieCommand = new RelayCommand(ExecZaplanujBadanie, x => czyMoznaKliknac);
        }

        private void DodajBadanieDoKalendarza(KalendarzBadanModel wpisDoKalendarza)
        {
            if (wpisDoKalendarza == null)
                return;

            using AppDbContext cont = new();
            cont.Kalendarz.Add(wpisDoKalendarza);
            cont.SaveChanges();
        }

        #endregion Main
        private void ExecLoaded(object obj)
        {
            if (obj is DatePicker dp)
            {
                // blokada dat z przeszłości 
                dp.BlackoutDates.AddDatesInPast();
            }
        }

        private void ExecWroc(object obj)
        {
            if (obj is KalendarzBadanOkno kb)
                kb.Close();
        }

        private async void ExecZaplanujBadanie(object obj)
        {
            if (obj is KalendarzBadanOkno kb)
            {
                KalendarzBadanModel wpis = new(Globals.ZalogowanyUzytkownik.Id, WybranaKlinika.Id, WybranyTypBadania, DataBadania.GotowaData, Tresc);
                var result = await DialogCoordinator.ShowMessageAsync(this, "Planowanie badania", "Badanie zaplanowane. Czy ustawić przypomnienie na dzień przed badaniem?",
                    MessageDialogStyle.AffirmativeAndNegativeAndSingleAuxiliary, new MetroDialogSettings()
                    {
                        AffirmativeButtonText = "Tak",
                        NegativeButtonText = "Nie",
                        FirstAuxiliaryButtonText = "Przerwij",
                        DefaultButtonFocus = MessageDialogResult.Affirmative
                    });

                switch (result)
                {
                    case MessageDialogResult.Affirmative:
                        wpis.CzyUstawionoPrzypomnienie = true;
                        break;
                    case MessageDialogResult.FirstAuxiliary:
                        return;
                }

                DodajBadanieDoKalendarza(wpis);
                kb.Close();
            }
        }
        #endregion Methods
    }
}