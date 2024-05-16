using BadanieKrwi.Models;
using BadanieKrwi.Models.Database;
using BadanieKrwi.Views;
using MahApps.Metro.Controls.Dialogs;
using System.Windows.Controls;
using System.Windows.Input;

namespace BadanieKrwi.ViewModels
{
    public class NoweBadaniaViewModel : KlasaBazowa
    {
        #region Properties
        public IDialogCoordinator DialogCoordinator { get; set; }

        public List<string> TypyBadania { get; set; }

        private BadanieModel _noweBadanie;
        public BadanieModel NoweBadanie
        {
            get => _noweBadanie;
            set
            {
                if (_noweBadanie != value)
                {
                    _noweBadanie = value;
                    OnPropertyChanged();
                }
            }
        }

        public List<string> Kliniki { get; set; }

        private bool _czyZapisac
            => NoweBadanie.DataBadania <= new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)
            && NoweBadanie.DataBadania > new DateTime(1900)
            && !string.IsNullOrWhiteSpace(NoweBadanie.TypBadania)
            && _czyStezenieErytrocytowJestOk
            && _czyHemoglobina
            && _czyHematokryt
            && _czySredniaObjErytrocytow
            && _czySredniaMasaHemoglobiny
            && _czySrednieStezenieHemoglobiny
            && _czyRozpietoscRozkladu_Obj_Erytrocytow
            && _czyRetikulocyty
            && _czyStezenieLeukocytow
            && _czyNeutrofile
            && _czyBazofile
            && _czyEozynofile
            && _czyLimfocyty
            && _czyMonocyty
            && _czyPlytkiKrwi
            && _czySredniaObj
            && _czyZelazo
            && _czyMagnez
            && _czyWybranoKlinike
            ;

        private bool _czyStezenieErytrocytowJestOk => NoweBadanie.StezenieErytrocytowRbc >= 0;
        private bool _czyHemoglobina => NoweBadanie.HemoglobinaHb >= 0;
        private bool _czyHematokryt => NoweBadanie.HematokrytHtc >= 0;
        private bool _czySredniaObjErytrocytow => NoweBadanie.SredniaObjetoscErytrocytuMcv >= 0;
        private bool _czySredniaMasaHemoglobiny => NoweBadanie.SredniaMasaHemoglobinyWErytrocycieMch >= 0;
        private bool _czySrednieStezenieHemoglobiny => NoweBadanie.SrednieStezenieHemoglobinyWErytrocytachMchc >= 0;

        private bool _czyRozpietoscRozkladu_Obj_Erytrocytow => NoweBadanie.RozpietoscRozkladuObjetosciErytrocytowRdwCw >= 0;

        private bool _czyRetikulocyty => NoweBadanie.RetikulocytyRc >= 0;
        private bool _czyStezenieLeukocytow => NoweBadanie.StezenieLeukocytowWbc >= 0;

        private bool _czyNeutrofile => NoweBadanie.Neutrofile >= 0;

        private bool _czyBazofile => NoweBadanie.Bazofile >= 0;

        private bool _czyEozynofile => NoweBadanie.Eozynofile >= 0;

        private bool _czyLimfocyty => NoweBadanie.Limfocyty >= 0;
        private bool _czyMonocyty => NoweBadanie.Monocyty >= 0;

        private bool _czyPlytkiKrwi => NoweBadanie.PlytkiKrwiPlt >= 0;

        private bool _czySredniaObj => NoweBadanie.SredniaObjetoscKrwiMpv >= 0;
        private bool _czyZelazo => NoweBadanie.Zelazo >= 0;
        private bool _czyMagnez => NoweBadanie.Magnez >= 0;
        private bool _czyWybranoKlinike => !string.IsNullOrWhiteSpace(NoweBadanie.NazwaKliniki);
        #endregion Properties

        #region Commands
        public ICommand LoadedCommand { get; set; }
        public ICommand WrocCommand { get; set; }
        public ICommand ZapiszCommand { get; set; }

        #endregion Commands

        #region Constructors
        public NoweBadaniaViewModel()
        {
            Inicializacja();
        }

        #endregion Constructors

        #region Methods
        #region Main
        private void Inicializacja()
        {
            NoweBadanie = new BadanieModel();
            TypyBadania = Globals.TypyBadan;
            NoweBadanie.TypBadania  = TypyBadania.FirstOrDefault();

            using AppDbContext cont = new();
            Kliniki = [.. cont.Kliniki.Select(x => x.Nazwa)];
            NoweBadanie.NazwaKliniki = Kliniki.FirstOrDefault();
            InicjalizacjaKomend();
        }

        private void InicjalizacjaKomend()
        {
            LoadedCommand = new RelayCommand(ExecLoaded);
            WrocCommand = new RelayCommand(ExecWrocAsync);
            ZapiszCommand = new RelayCommand(ExecZapisz, x => _czyZapisac);
        }

        #endregion Main

        private void ExecLoaded(object obj)
        {
            if (obj is DatePicker dp)
            {
                // blokada dat od jutra w przyszłość
                dp.BlackoutDates.Add(new CalendarDateRange(DateTime.Today.AddDays(1), DateTime.MaxValue));
            }
        }

        private async void ExecWrocAsync(object obj)
        {
            if (obj is NoweBadanieOkno nbo)
            {
                if (NoweBadanie.CzyZmodyfikowano)
                {
                    var result = await ShowMessageAsync("Czy na pewno chcesz cofnąć? Twoje dane nie zostaną zapisane", "Powrót", MessageDialogStyle.AffirmativeAndNegative, this, DialogCoordinator);
                    if (result == MessageDialogResult.Negative)
                        return;
                }

                nbo.DialogResult = false;
            }
        }

        private async void ExecZapisz(object obj)
        {
            if (obj is NoweBadanieOkno nbo)
            {
                if (NoweBadanie.CzyZmodyfikowano)
                {
                    using AppDbContext cont = new();
                    cont.Badania.Add(NoweBadanie); // Dodajemy nowego użytkownika do kontekstu
                    cont.SaveChanges(); // Zapisujemy zmiany w bazie danych

                    // Sprawdź, czy klinika o podanej nazwie istnieje w bazie
                    if (!cont.Kliniki.Any(k => k.Nazwa == NoweBadanie.NazwaKliniki))
                    {
                        // Jeśli klinika nie istnieje, utwórz nową
                        Klinika nowaKlinika = new() { Nazwa = NoweBadanie.NazwaKliniki };

                        // Dodaj nową klinikę do bazy danych
                        cont.Kliniki.Add(nowaKlinika);

                        // Zapisz zmiany w bazie danych
                        cont.SaveChanges();
                    }

                    var result = await ShowMessageAsync("Twoje dane zostały zapisane", "Zapis Nowego Badania", this, DialogCoordinator);
                    if (result == MessageDialogResult.Affirmative)
                        nbo.DialogResult = true;
                }
                else
                    nbo.DialogResult = true;
            }
        }
        #endregion Methods
    }
}