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
            var dc = ((BadanieViewModel)bo.DataContext);
            dc.NaglowekOkna = $"Szczegóły Badnia: {WybraneBadanie.TypBadania}";
            dc.OryginalneBadanie = WybraneBadanie;
            dc.WybraneBadanie = new BadanieModel(WybraneBadanie);
            if (bo.ShowDialog().Value)
            {
                Badania[Badania.IndexOf(WybraneBadanie)] = dc.WybraneBadanie;
            }
            WybraneBadanie = null;
        }
        #endregion Methods        
    }
}