using BadanieKrwi.Models;
using BadanieKrwi.Models.Database;
using BadanieKrwi.Views;
using MahApps.Metro.Controls.Dialogs;
using System.Windows.Input;

namespace BadanieKrwi.ViewModels
{
    public class StatystykiViewModel : KlasaBazowa
    {
        #region Properties
        public IDialogCoordinator DialogCoordinator { get; set; }
        public string TwojaIloscBadan { get; set; }
        public string BadanieWTymRoku { get; set; }

        public string SredniCzas { get; set; }

        public List<ButtonModel> PrzyciskiStezeniaSubstrancji { get; set; }

        #endregion Properties

        #region Commands
        public ICommand WrocCommand { get; set; }
        public ICommand PrzyciskiStezeniaSubstrancjiCommand { get; set; }

        #endregion Commands

        #region Constructors
        public StatystykiViewModel()
        {
            Inicjalizacja();
            PoliczBadania();
        }
        #endregion Constructors

        #region Methods
        #region Main
        private void Inicjalizacja()
        {
            InicjalizacjaKomend();
            InicjalizacjaPrzyciskowStezeniaSubstancji();
            PoliczBadania();
            
        }

        private void InicjalizacjaPrzyciskowStezeniaSubstancji()
        {
            PrzyciskiStezeniaSubstrancji =
            [
                new ButtonModel("Stężenie Erytrocytów RBC", PrzyciskiStezeniaSubstrancjiCommand, "Stężenie Erytrocytów RBC||μl"),
                new ButtonModel("Hemoglobina Hb", PrzyciskiStezeniaSubstrancjiCommand, "Hemoglobina Hb||g/dl"),
                new ButtonModel("Hematokryt Htc", PrzyciskiStezeniaSubstrancjiCommand, "Hematokryt Htc||%"),
                new ButtonModel("Średnia objętość erytrocytu MCV", PrzyciskiStezeniaSubstrancjiCommand, "Średnia objętość erytrocytu MCV||fl"),
                new ButtonModel("Średnia masa hemoglobiny w erytrocycie MCH", PrzyciskiStezeniaSubstrancjiCommand, "Średnia masa hemoglobiny w erytrocycie MCH||pg"),
                new ButtonModel("Średnie stężenie hemoglobiny w erytrocytach MCHC", PrzyciskiStezeniaSubstrancjiCommand, "Średnie stężenie hemoglobiny w erytrocytach MCHC||g/dl"),
                new ButtonModel("Rozpiętość rozkładu objętości erytrocytów RDW-CV", PrzyciskiStezeniaSubstrancjiCommand, "Rozpiętość rozkładu objętości erytrocytów RDW-CV||%"),
                new ButtonModel("Retikulocyty RC", PrzyciskiStezeniaSubstrancjiCommand, "Retikulocyty RC||%"),
                new ButtonModel("Stężenie leukocytów WBC", PrzyciskiStezeniaSubstrancjiCommand, "Stężenie leukocytów WBC||liczba/μl"),
                new ButtonModel("Neutrofile", PrzyciskiStezeniaSubstrancjiCommand, "Neutrofile||liczba/μl"),
                new ButtonModel("Bazofile", PrzyciskiStezeniaSubstrancjiCommand, "Bazofile||liczba/μl"),
                new ButtonModel("Eozynofile", PrzyciskiStezeniaSubstrancjiCommand, "Eozynofile||liczba/μl"),
                new ButtonModel("Limfocyty", PrzyciskiStezeniaSubstrancjiCommand, "Limfocyty||liczba/μl"),
                new ButtonModel("Monocyty", PrzyciskiStezeniaSubstrancjiCommand, "Monocyty||liczba/μl"),
                new ButtonModel("Płytki krwi PLT", PrzyciskiStezeniaSubstrancjiCommand, "Płytki krwi PLT||liczba/μl"),
                new ButtonModel("Średnia objętość krwi MPV", PrzyciskiStezeniaSubstrancjiCommand, "Średnia objętość krwi MPV||fl"),
                new ButtonModel("Żelazo", PrzyciskiStezeniaSubstrancjiCommand, "Żelazo||μg/dl"),
                new ButtonModel("Magnez", PrzyciskiStezeniaSubstrancjiCommand, "Magnez||mmol/l"),
                //new ButtonModel("", PrzyciskiStezeniaSubstrancjiCommand, ""),
            ];
        }

        private void InicjalizacjaKomend()
        {
            PrzyciskiStezeniaSubstrancjiCommand = new RelayCommand(ExecPrzyciskiStezeniaSubstrancji);
            WrocCommand = new RelayCommand(ExecWroc);
        }

        #endregion Main
        private async void ExecPrzyciskiStezeniaSubstrancji(object obj)
        {
            if (obj is string nazwaStezeniaSubstancjiIJednostka)
            {
                try
                {
                    string[] nazwaZJednostka = nazwaStezeniaSubstancjiIJednostka.Split("||");
                    SzczegolyStezeniaSubstancjiOkno sssOkno = new();
                    (sssOkno.DataContext as SzczegolyStezeniaSubstancjiViewModel)!.InicjalizacjaWykres(nazwaZJednostka[0], nazwaZJednostka[1]);
                    sssOkno.ShowDialog();
                }
                catch (Exception ex)
                {
                    await ShowMessageAsync(new("Stężenie substancji", ex.Message), this, DialogCoordinator);
                }
            }
        }
        private void PoliczBadania()
        {
            using AppDbContext cont = new ();
            int iloscBadan =  cont.Badania.Where(x => x.IdUzytkownika.Equals(Globals.ZalogowanyUzytkownik.Id)).Count();
            TwojaIloscBadan = iloscBadan.ToString();

            var currentYear = DateTime.Now.Year;
            int iloscBadanRok = cont.Badania.Where(b => b.IdUzytkownika == Globals.ZalogowanyUzytkownik.Id && b.DataBadania.Year == currentYear).Count();
            BadanieWTymRoku = iloscBadanRok.ToString();

            var badania = cont.Badania
                      .Where(b => b.IdUzytkownika == Globals.ZalogowanyUzytkownik.Id)
                      .OrderBy(b => b.DataBadania)
                      .ToList();

            if (badania.Count < 2)
            {
                SredniCzas = "Brak wystarczającej liczby badań";
            }
            else
            {
                var totalDays = badania.Zip(badania.Skip(1), (first, second) => (second.DataBadania - first.DataBadania).TotalDays).Sum();
                var averageDays = totalDays / (badania.Count - 1);
                SredniCzas = TimeSpan.FromDays(averageDays).ToString(@"dd");
            }
        }
       
        private void ExecWroc(object obj)
        {
            if (obj is StatystykiOkno so)
                so.Close();
        }

        #endregion Methods
    }
}