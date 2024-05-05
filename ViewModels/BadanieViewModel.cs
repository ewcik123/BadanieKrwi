using BadanieKrwi.Models;
using BadanieKrwi.Views;
using MahApps.Metro.Controls.Dialogs;
using System.Windows.Input;

namespace BadanieKrwi.ViewModels
{
    public class BadanieViewModel : KlasaBazowa
    {
        #region Properties
        public IDialogCoordinator DialogCoordinator { get; set; }

        public BadanieModel OryginalneBadanie { get; set; }

        private bool _czyEdytowac;
        public bool CzyEdytowac
        {
            get => _czyEdytowac;
            set
            {
                if (_czyEdytowac != value)
                {
                    _czyEdytowac = value;
                    CzyKontrolkiTylkoDoOdczytu = !value;
                    OnPropertyChanged();

                    AktualizacjaNaglowkaIPrzyciskuZapisuEdycji();
                }
            }
        }

        private bool _czyKontrolkiTylkoDoOdczytu;
        public bool CzyKontrolkiTylkoDoOdczytu
        {
            get => _czyKontrolkiTylkoDoOdczytu;
            set
            {
                if (_czyKontrolkiTylkoDoOdczytu != value)
                {
                    _czyKontrolkiTylkoDoOdczytu = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _naglowekOkna;
        public string NaglowekOkna
        {
            get => _naglowekOkna;
            set
            {
                if (_naglowekOkna != value)
                {
                    _naglowekOkna = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _naglowek;
        public string Naglowek
        {
            get => _naglowek;
            set
            {
                if (_naglowek != value)
                {
                    _naglowek = value;
                    OnPropertyChanged();
                }
            }
        }

        private BadanieModel _wybraneBadanie;
        public BadanieModel WybraneBadanie
        {
            get => _wybraneBadanie;
            set
            {
                if (_wybraneBadanie != value)
                {
                    _wybraneBadanie = value;
                    OnPropertyChanged();
                }
            }
        }

        public ButtonModel EdytujBadaniePrzycisk { get; set; }
        public ButtonModel ZapiszZmianyPrzycisk { get; set; }
        public ButtonModel PrzerwijEdycjePrzycisk { get; set; }
        public ButtonModel WrocPrzycisk { get; set; }

        #region Commands
        public ICommand EdytujBadanieCommand { get; set; }
        public ICommand ZapiszZmianyCommand { get; set; }
        public ICommand PrzerwijEdycjeCommand { get; set; }
        public ICommand WrocCommand { get; set; }

        #endregion Commands

        #endregion Properties

        #region Constructors
        public BadanieViewModel()
        {
            Inicjalizacja();
        }
        #endregion Constructors

        #region Methods
        #region Main

        private void Inicjalizacja()
        {
            InicjalizacjaKomend();

            CzyKontrolkiTylkoDoOdczytu = true;
            EdytujBadaniePrzycisk = new ButtonModel("Edytuj", EdytujBadanieCommand, "Edytuj");
            ZapiszZmianyPrzycisk = new ButtonModel("Zapisz", ZapiszZmianyCommand, "Zapisz");
            PrzerwijEdycjePrzycisk = new ButtonModel("Przerwij", PrzerwijEdycjeCommand, null);
            WrocPrzycisk = new ButtonModel("Wróć", WrocCommand, null);
            AktualizacjaNaglowkaIPrzyciskuZapisuEdycji();
        }

        private void InicjalizacjaKomend()
        {
            EdytujBadanieCommand = new RelayCommand(ExecEdytujBadani, x => !CzyEdytowac);
            ZapiszZmianyCommand = new RelayCommand(ExecZapiszZmiany);
            PrzerwijEdycjeCommand = new RelayCommand(ExecPrzerwijEdycje);
            WrocCommand = new RelayCommand(ExecWrocCommand);
        }

        private void AktualizacjaNaglowkaIPrzyciskuZapisuEdycji()
        {
            Naglowek = _czyEdytowac
                ? "Badanie Krwi - Edycja Badnia Krwi"
                : "Badanie Krwi - Badanie Krwi";

            //if (_czyEdytowac)
            //    EdytujZapiszPrzycisk = new ButtonModel("Zapisz", EdytujBadanieCommand, "Zapisz");
            //else
            //    EdytujZapiszPrzycisk = new ButtonModel("Edytuj", EdytujBadanieCommand, "Edytuj");
        }

        #endregion Main

        private void ExecEdytujBadani(object obj)
        {
            CzyEdytowac = true;
        }

        private void ExecZapiszZmiany(object obj)
        {
            if (obj is BadanieOkno bo)
                bo.DialogResult = true;
        }

        private void ExecPrzerwijEdycje(object obj)
        {
            CzyEdytowac = false;
            WybraneBadanie = new BadanieModel(OryginalneBadanie);
        }

        private void ExecWrocCommand(object obj)
        {
            if (obj is BadanieOkno bo)
                bo.DialogResult = false;
        }
        #endregion Methods
    }
}