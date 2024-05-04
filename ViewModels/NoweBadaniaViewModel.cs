using BadanieKrwi.Models;
using BadanieKrwi.Views;
using System.Windows;
using System.Windows.Input;

namespace BadanieKrwi.ViewModels
{
    public class NoweBadaniaViewModel : KlasaBazowa
    {
        #region Properties

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

        private bool _czyZapisac
            => NoweBadanie.DataBadania <= new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)
            && NoweBadanie.DataBadania > new DateTime(1900)
            && !string.IsNullOrWhiteSpace(NoweBadanie.NazwaKliniki)
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
            ;

        private bool _czyStezenieErytrocytowJestOk => NoweBadanie.StezenieErytrocytowRbc > 0;
        private bool _czyHemoglobina => NoweBadanie.HemoglobinaHb > 0;
        private bool _czyHematokryt => NoweBadanie.HematokrytHtc > 0;
        private bool _czySredniaObjErytrocytow => NoweBadanie.SredniaObjetoscErytrocytuMcv > 0;
        private bool _czySredniaMasaHemoglobiny => NoweBadanie.SredniaMasaHemoglobinyWErytrocycieMch > 0;
        private bool _czySrednieStezenieHemoglobiny => NoweBadanie.SrednieStezenieHemoglobinyWErytrocytachMchc > 0;

        private bool _czyRozpietoscRozkladu_Obj_Erytrocytow => NoweBadanie.RozpietoscRozkladuObjetosciErytrocytowRdwCw > 0;

        private bool _czyRetikulocyty => NoweBadanie.RetikulocytyRc > 0;
        private bool _czyStezenieLeukocytow => NoweBadanie.StezenieLeukocytowWbc > 0;

        private bool _czyNeutrofile => NoweBadanie.Neutrofile > 0;

        private bool _czyBazofile => NoweBadanie.Bazofile > 0;

        private bool _czyEozynofile => NoweBadanie.Eozynofile > 0;

        private bool _czyLimfocyty => NoweBadanie.Limfocyty > 0;
        private bool _czyMonocyty => NoweBadanie.Monocyty > 0;

        private bool _czyPlytkiKrwi => NoweBadanie.PlytkiKrwiPlt > 0;

        private bool _czySredniaObj => NoweBadanie.SredniaObjetoscKrwiMpv > 0;
        private bool _czyZelazo => NoweBadanie.Zelazo > 0;
        private bool _czyMagnez => NoweBadanie.Magnez > 0;
        #endregion Properties

        #region Commands
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
            InicjalizacjaKomend();
        }

        private void InicjalizacjaKomend()
        {
            WrocCommand = new RelayCommand(ExecWroc);
            ZapiszCommand = new RelayCommand(ExecZapisz, x => _czyZapisac);
        }

        #endregion Main

        private void ExecWroc(object obj)
        {
            if (obj is NoweBadanieOkno nbo)
            {
                if (NoweBadanie.CzyZmodyfikowano)
                {
                    MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz cofnąć? Twoje dane nie zostaną zapisane", "Powrót", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.No)
                        return;
                }

                nbo.DialogResult = false;
            }
        }

        private void ExecZapisz(object obj)
        {
            if (obj is NoweBadanieOkno nbo)
            {
                if (NoweBadanie.CzyZmodyfikowano)
                {
                    MessageBoxResult result = MessageBox.Show("Twoje dane zostały zapisane", "Powrót", MessageBoxButton.OK, MessageBoxImage.Information);
                    if (result == MessageBoxResult.OK)
                        nbo.DialogResult = true;
                }
                else
                    nbo.DialogResult = true;
            }
        }
        #endregion Methods
    }
}