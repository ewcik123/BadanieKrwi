using BadanieKrwi.Models;
using BadanieKrwi.Models.Database;
using BadanieKrwi.Views;
using MahApps.Metro.Controls.Dialogs;
using System.Windows.Input;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
                new ButtonModel("Stężenie Erytrocytów RBC", PrzyciskiStezeniaSubstrancjiCommand, "Stężenie Erytrocytów RBC"),
                new ButtonModel("Hemoglobina Hb", PrzyciskiStezeniaSubstrancjiCommand, "Hemoglobina Hb"),
                new ButtonModel("Hematokryt Htc", PrzyciskiStezeniaSubstrancjiCommand, "Hematokryt Htc"),
                new ButtonModel("Średnia objętość erytrocytu MCV", PrzyciskiStezeniaSubstrancjiCommand, "Średnia objętość erytrocytu MCV"),
                new ButtonModel("Średnia masa hemoglobiny w erytrocycie MCH", PrzyciskiStezeniaSubstrancjiCommand, "Średnia masa hemoglobiny w erytrocycie MCH"),
                new ButtonModel("Średnie stężenie hemoglobiny w erytrocytach MCHC", PrzyciskiStezeniaSubstrancjiCommand, "Średnie stężenie hemoglobiny w erytrocytach MCHC"),
                new ButtonModel("Rozpiętość rozkładu objętości erytrocytów RDW-CV", PrzyciskiStezeniaSubstrancjiCommand, "Rozpiętość rozkładu objętości erytrocytów RDW-CV"),
                new ButtonModel("Retikulocyty RC", PrzyciskiStezeniaSubstrancjiCommand, "Retikulocyty RC"),
                new ButtonModel("Stężenie leukocytów WBC", PrzyciskiStezeniaSubstrancjiCommand, "Stężenie leukocytów WBC"),
                new ButtonModel("Neutrofile", PrzyciskiStezeniaSubstrancjiCommand, "Neutrofile"),
                new ButtonModel("Bazofile", PrzyciskiStezeniaSubstrancjiCommand, "Bazofile"),
                new ButtonModel("Eozynofile", PrzyciskiStezeniaSubstrancjiCommand, "Eozynofile"),
                new ButtonModel("Limfocyty", PrzyciskiStezeniaSubstrancjiCommand, "Limfocyty"),
                new ButtonModel("Monocyty", PrzyciskiStezeniaSubstrancjiCommand, "Monocyty"),
                new ButtonModel("Płytki krwi PLT", PrzyciskiStezeniaSubstrancjiCommand, "Płytki krwi PLT"),
                new ButtonModel("Średnia objętość krwi MPV", PrzyciskiStezeniaSubstrancjiCommand, "Średnia objętość krwi MPV"),
                new ButtonModel("Żelazo", PrzyciskiStezeniaSubstrancjiCommand, "Żelazo"),
                new ButtonModel("Magnez", PrzyciskiStezeniaSubstrancjiCommand, "Magnez"),
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
            if (obj is string nazwaStezeniaSubstancji)
            {
                try
                {
                    SzczegolyStezeniaSubstancjiOkno sssOkno = new();
                    (sssOkno.DataContext as SzczegolyStezeniaSubstancjiViewModel)!.InicjalizacjaWykres(nazwaStezeniaSubstancji);
                    sssOkno.ShowDialog();
                }
                catch (Exception ex)
                {
                    await ShowMessageAsync(ex.Message, "Stężenie substancji", this, DialogCoordinator);
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