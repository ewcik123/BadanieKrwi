using BadanieKrwi.Models;
using BadanieKrwi.Models.Database;
using BadanieKrwi.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BadanieKrwi.ViewModels
{
    public class TwojeBadanieViewModel : KlasaBazowa
    {
        #region Properties
        private ObservableCollection<BadanieModel> _badania;
        public ObservableCollection<BadanieModel> Badania
        {
            get => _badania;
            set
            {
                if (_badania != value)
                {
                    _badania = value;
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

        #region Commands
        public ICommand SzczegolyCommand { get; set; }
        public ICommand WrocCommand { get; set; }

        #endregion Commands

        #endregion Properties

        #region Constructors
        public TwojeBadanieViewModel()
        {
            Inicjalizacja();
            WczytajBadania();
        }
        #endregion Constructors

        #region Methods
        #region Main

        private void Inicjalizacja()
        {
            InicjalizacjaKomend();
            SprawdzTerminKolejnegoBadania();
        }

        private async void SprawdzTerminKolejnegoBadania()
        {
            return;

            if (Badania == null || Badania.Count == 0)
                return;

            var obecnaData = DateTime.Now;
            var badanieZJutra = Badania.FirstOrDefault(x => x.DataBadania.Day == obecnaData.Day + 1 && x.DataBadania.Month == obecnaData.Month && x.DataBadania.Year == obecnaData.Year);
            if (badanieZJutra != null)
            {
                try
                {
                    MenadzerPowiadomien.Instance.WyslijPowiadomienieOBadaniu(badanieZJutra.DataBadania);
                }
                catch (Exception ex)
                {
                    await ShowMessageAsync($"Błąd podczas wysyłania powiadomienia:\n{ex.Message}", "Sprawdzenie kolejnego wolnego terminu", this);
                }
            }
        }

        private void InicjalizacjaKomend()
        {
            SzczegolyCommand = new RelayCommand(ExecSzczegolyCommand, x => WybraneBadanie != null);
            WrocCommand = new RelayCommand(ExecWrocCommand);
        }
        private void WczytajBadania()
        {
            using AppDbContext cont = new();
            Badania = new ObservableCollection<BadanieModel>([.. cont.Badania]);
        }

        #endregion Main

        private void ExecWrocCommand(object obj)
        {
            if (obj is TwojeBadaniaOkno tbo)
                tbo.Close();
        }

        private void ExecSzczegolyCommand(object obj)
        {
            BadanieOkno bo = new();
            ((BadanieViewModel)bo.DataContext).NaglowekOkna = $"Szczegóły Badnia: {WybraneBadanie.NazwaBadania}";
            ((BadanieViewModel)bo.DataContext).OryginalneBadanie = WybraneBadanie;
            ((BadanieViewModel)bo.DataContext).WybraneBadanie = new BadanieModel(WybraneBadanie);
            if (bo.ShowDialog().Value)
            {
                Badania[Badania.IndexOf(WybraneBadanie)] = ((BadanieViewModel)bo.DataContext).WybraneBadanie;
            }
            WybraneBadanie = null;
        }
        #endregion Methods        
    }
}