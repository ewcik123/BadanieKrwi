using BadanieKrwi.Models;
using BadanieKrwi.Models.Database;
using BadanieKrwi.Views;
using LiveCharts;
using LiveCharts.Wpf;
using MahApps.Metro.Controls.Dialogs;
using System.Windows.Input;

namespace BadanieKrwi.ViewModels
{
    public class SzczegolyStezeniaSubstancjiViewModel : KlasaBazowa
    {
        #region Properties
        public IDialogCoordinator DialogCoordinator { get; set; }

        private readonly List<int> _wartosciInt = [];
        private readonly List<double> _wartosciDouble = [];

        private SeriesCollection _serieNaWykresie;
        public SeriesCollection SerieNaWykresie
        {
            get => _serieNaWykresie;
            set
            {
                if (_serieNaWykresie != value)
                {
                    _serieNaWykresie = value;
                    OnPropertyChanged();
                }
            }
        }

        private List<string> _etykietyX;
        public List<string> EtykietyX
        {
            get => _etykietyX;
            set
            {
                if (_etykietyX != value)
                {
                    _etykietyX = value;
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
        
        public Func<double, string> YFormatter { get; set; }
        #endregion Properties

        #region Commands
        public ICommand WrocCommand { get; set; }
        #endregion Commands

        #region Constructors
        public SzczegolyStezeniaSubstancjiViewModel()
        {
            Inicjalizacja();
        }
        #endregion Constructors

        #region Methods
        #region Main
        private void Inicjalizacja()
        {
            InicjalizacjaWykres(null, null);
            InicjalizacjaKomend();
            _badania = [];
        }
        public void InicjalizacjaWykres(string nazwaStezeniaSubstancji, string jednostka)
        {
            if (string.IsNullOrWhiteSpace(nazwaStezeniaSubstancji) || string.IsNullOrWhiteSpace(jednostka))
            {
                Naglowek = "Jakaś tam nazwa stężenia substancji";
                SerieNaWykresie =
                [
                    new LineSeries
                    {
                        Title = "Seria 1",
                        Values = new ChartValues<int>{2,4,6,7,10,18,20,21,30 }
                    }
                ];

                EtykietyX = ["2", "4", "6", "7", "10", "18", "20", "21", "30"];
                //YFormatter = value => value.ToString();
                return;
            }

            var badania = WeZBadania(nazwaStezeniaSubstancji);
            if (badania?.Count == 0)
                return;

            EtykietyX.Clear();
            YFormatter = value => $"{value:0.##} {jednostka}";

            Naglowek = nazwaStezeniaSubstancji;

            int count = 1;
            if (_wartosciInt.Count != 0)
            {
                SerieNaWykresie =
                [
                    new LineSeries
                    {
                        Title = "Wartość",
                        Values = new ChartValues<int>(_wartosciInt)
                    }
                ];
                _wartosciInt.ForEach(x =>
                {
                    EtykietyX.Add(count++.ToString());
                });
            }
            else if (_wartosciDouble.Count != 0)
            {
                SerieNaWykresie =
                [
                    new LineSeries
                    {
                        Title = "Wartość",
                        Values = new ChartValues<double>(_wartosciDouble)
                    }
                ];
                _wartosciDouble.ForEach(x =>
                {
                    EtykietyX.Add(count++.ToString());
                });
            }
        }

        private void InicjalizacjaKomend()
        {
            WrocCommand = new RelayCommand(ExecWroc);
        }

        private List<BadanieModel> _badania;
        private List<BadanieModel> WeZBadania(string nazwaStrezeniaSubstancji)
        {
            if (string.IsNullOrWhiteSpace(nazwaStrezeniaSubstancji))
                return _badania;

            try
            {
                using AppDbContext cont = new();
                _badania = [.. cont.Badania.Where(x => x.IdUzytkownika.Equals(Globals.ZalogowanyUzytkownik.Id))];

                _wartosciInt.Clear();
                _wartosciDouble.Clear();

                switch (nazwaStrezeniaSubstancji)
                {
                    case "Stężenie Erytrocytów RBC": { _wartosciInt.AddRange(_badania.Select(x => x.StezenieErytrocytowRbc)); break; }
                    case "Hemoglobina Hb": { _wartosciInt.AddRange(_badania.Select(x => x.HemoglobinaHb)); break; }
                    case "Hematokryt Htc": { _wartosciInt.AddRange(_badania.Select(x => x.HematokrytHtc)); break; }
                    case "Średnia objętość erytrocytu MCV": { _wartosciDouble.AddRange(_badania.Select(x => x.SredniaObjetoscErytrocytuMcv)); break; }
                    case "Średnia masa hemoglobiny w erytrocycie MCH": { _wartosciDouble.AddRange(_badania.Select(x => x.SredniaMasaHemoglobinyWErytrocycieMch)); break; }
                    case "Średnie stężenie hemoglobiny w erytrocytach MCHC": { _wartosciDouble.AddRange(_badania.Select(x => x.SrednieStezenieHemoglobinyWErytrocytachMchc)); break; }
                    case "Rozpiętość rozkładu objętości erytrocytów RDW-CV": { _wartosciDouble.AddRange(_badania.Select(x => x.RozpietoscRozkladuObjetosciErytrocytowRdwCw)); break; }
                    case "Retikulocyty RC": { _wartosciDouble.AddRange(_badania.Select(x => x.RetikulocytyRc)); break; }
                    case "Stężenie leukocytów WBC": { _wartosciInt.AddRange(_badania.Select(x => x.StezenieLeukocytowWbc)); break; }
                    case "Neutrofile": { _wartosciInt.AddRange(_badania.Select(x => x.Neutrofile)); break; }
                    case "Bazofile": { _wartosciInt.AddRange(_badania.Select(x => x.Bazofile)); break; }
                    case "Eozynofile": { _wartosciInt.AddRange(_badania.Select(x => x.Eozynofile)); break; }
                    case "Limfocyty": { _wartosciInt.AddRange(_badania.Select(x => x.Limfocyty)); break; }
                    case "Monocyty": { _wartosciInt.AddRange(_badania.Select(x => x.Monocyty)); break; }
                    case "Płytki krwi PLT": { _wartosciInt.AddRange(_badania.Select(x => x.PlytkiKrwiPlt)); break; }
                    case "Średnia objętość krwi MPV": { _wartosciDouble.AddRange(_badania.Select(x => x.SredniaObjetoscKrwiMpv)); break; }
                    case "Żelazo": { _wartosciDouble.AddRange(_badania.Select(x => x.Zelazo)); break; }
                    case "Magnez": { _wartosciDouble.AddRange(_badania.Select(x => x.Magnez)); break; }
                }

                return _badania;
            }
            catch (Exception ex)
            { }

            return _badania;
        }

        #endregion Main

        private void ExecWroc(object obj)
        {
            if (obj is SzczegolyStezeniaSubstancjiOkno sssOkno)
                sssOkno.Close();
        }

        #endregion Methods
    }
}