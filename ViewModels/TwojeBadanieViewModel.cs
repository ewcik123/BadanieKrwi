using BadanieKrwi.Models;
using BadanieKrwi.Models.Database;
using BadanieKrwi.Views;
using MahApps.Metro.Controls.Dialogs;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BadanieKrwi.ViewModels
{
    public class TwojeBadanieViewModel : KlasaBazowa
    {
        #region Properties
        public IDialogCoordinator DialogCoordinator { get; set; }

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

        public ButtonModel GenerujPdfPrzycisk { get; set; }
        public ButtonModel SzczegolyPrzycisk { get; set; }
        public ButtonModel WrocPrzycisk { get; set; }

        #region Commands
        public ICommand GenerujPdfCommand { get; set; }
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

            GenerujPdfPrzycisk = new ButtonModel("Generuj Pdf", GenerujPdfCommand, null);
            SzczegolyPrzycisk = new ButtonModel("Szczegóły", SzczegolyCommand, null);
            WrocPrzycisk = new ButtonModel("Wróć", WrocCommand, null);
        }

        private void InicjalizacjaKomend()
        {
            GenerujPdfCommand = new RelayCommand(ExecGenerujPdf, x => WybraneBadanie != null);
            SzczegolyCommand = new RelayCommand(ExecSzczegolyCommand, x => WybraneBadanie != null);
            WrocCommand = new RelayCommand(ExecWrocCommand);
        }
        private void WczytajBadania()
        {
            using AppDbContext cont = new();
            Badania = new ObservableCollection<BadanieModel>([.. cont.Badania.Where(x => x.IdUzytkownika == Globals.ZalogowanyUzytkownik.Id)]);
        }

        #endregion Main

        private async void ExecGenerujPdf(object obj)
        {
            Microsoft.Win32.SaveFileDialog dialog = new()
            { Filter = "Badanie (*.pdf)|*.pdf" };

            try
            {
                dialog.FileName = $"Badanie krwi - {WybraneBadanie.TypBadania}";
                if (dialog.ShowDialog().Value)
                {
                    if (MenadzerPdf.Instance.Generuj(dialog.FileName, WybraneBadanie))
                        await ShowMessageAsync("Raport wygenerowano pomyślnie", "Generowanie Pdf", this, DialogCoordinator);
                }
            }
            catch (Exception ex)
            {
                await ShowMessageAsync(ex.Message, "Generowanie Pdf", this, DialogCoordinator);
            }
        }

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