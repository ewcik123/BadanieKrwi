using BadanieKrwi.Models;
using BadanieKrwi.Views;
using System.Windows.Input;

namespace BadanieKrwi.ViewModels
{
    public class KalendarzBadaniaViewModel : KlasaBazowa
    {
        #region Properties
        public DateModel DataBadania { get; set; }
        public List<string> Kliniki { get; set; }
        public string WybranaKlinika { get; set; }
        public string Tresc { get; set; }

        public bool czyMoznaKliknac
        {
            get => Kliniki?.Count > 0
                && WybranaKlinika != null
                && !string.IsNullOrEmpty(Tresc);
        }
        #endregion Properties

        #region Commands
        public ICommand WrocCommand { get; set; }
        public ICommand ZapiszCommand { get; set; }
        public ICommand WyslijCommand { get; set; }


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
            Kliniki = ["Klinika 1", "Klinika 2"];
            WybranaKlinika = Kliniki[0];

            InicjalizacjaKomend();
        }

        private void InicjalizacjaKomend()
        {
            WrocCommand = new RelayCommand(ExecWroc);
            ZapiszCommand = new RelayCommand(ExecZapisz, x => czyMoznaKliknac);
            WyslijCommand = new RelayCommand(ExecWyslij, x => czyMoznaKliknac);

        }

        #endregion Main
        private void ExecWroc(object obj)
        {
            if (obj is KalendarzBadanOkno kb)
                kb.Close();
        }

        private void ExecZapisz(object obj)
        {
            if (obj is KalendarzBadanOkno kb)
                kb.Close();
        }

        private void ExecWyslij(object obj)
        {
            if (obj is KalendarzBadanOkno kb)
                kb.Close();
        }
        #endregion Methods
    }
}