using BadanieKrwi.Models;
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
            && !string.IsNullOrWhiteSpace(NowaKlinika?.Informacja)
            && !string.IsNullOrWhiteSpace(NowaKlinika?.Adres)
            && !string.IsNullOrWhiteSpace(NowaKlinika?.Telefon);
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
        }
        #endregion Constructors

        #region Methods
        #region Main
        private void Inicjalizacja()
        {
            Kliniki =
            [
                 new(){ Id = Guid.NewGuid(), Nazwa = "Klinika 1", Adres = "Adres 1", Informacja = "Info 1", Telefon = "123 456 788" },
                 new(){ Id = Guid.NewGuid(), Nazwa = "Klinika 2", Adres = "Adres 2", Informacja = "Info 2", Telefon = "123 456 789" },
            ];

            WybranaKlinika = Kliniki[0];
            InicjalizacjaKomend();
        }

        private void InicjalizacjaKomend()
        {
            WrocCommand = new RelayCommand(ExecWroc);
            ZapiszCommand = new RelayCommand(ExecZapisz, x => czyMoznaZapisac);
            NowyCommand = new RelayCommand(ExecNowy);
        }

        #endregion Main
        private void ExecWroc(object obj)
        {
            if (obj is KlinikiOkno ko)
                ko.Close();
        }

        private void ExecZapisz(object obj)
        {
            if (obj is KlinikiOkno ko)
                ko.Close();
        }

        private void ExecNowy(object obj)
        {
            NowaKlinika = new Klinika() { Id = Guid.NewGuid() };
        }
        #endregion Methods 
    }
}